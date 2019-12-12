﻿using SmartTask.Properties;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win32ApI;

namespace SmartTask
{
    public partial class MainForm : Form
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;

        private readonly StringBuilder currentWindowClassName = new StringBuilder(256);
        private readonly string _ignoreWindowName = string.Empty;

        private bool currentWindowState = false;
        private bool preWindowState = false;

        private int cacheProcessId;
        private IntPtr cacheIntPtr;

        private static HookProc MouseHookProc;
        private static HookProc KeyBoardHookProc;
        //定义钩子句柄
        public static int hHook = 0;
        public static bool operating = false;


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr intPtr, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public MainForm()
        {
            InitializeComponent();
            RunStart.Checked = Program.IsRunStart();

            MouseHookProc = new HookProc(MouseHookHandler);
            KeyBoardHookProc = new HookProc(KeyBoardHookHandler);

            hHook = SetWindowsHookEx(
                   WH_MOUSE_LL,
                   MouseHookProc,
                   Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                   0);

            hHook = SetWindowsHookEx(
                   WH_KEYBOARD_LL,
                   KeyBoardHookProc,
                   Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                   0);
            _ignoreWindowName =
                "Windows.UI.Core.CoreWindow, " +
                "MultitaskingViewFrame, " +
                "ForegroundStaging, " +
                "Shell_TrayWnd, " +
                "Shell_SencondaryTrayWnd";
        }

        private int MouseHookHandler(int nCode, int wParam, IntPtr lParam)
        {
            operating = true;
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        private int KeyBoardHookHandler(int nCode, int wParam, IntPtr lParam)
        {
            operating = true;
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Hide();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(() =>
            {
                for (; ; )
                {
                    IntPtr hWnd = User32.GetForegroundWindowProcessId(out int calcID);
                    currentWindowState = IsZoomed(hWnd);
                    Thread.Sleep(1000);
                    if (operating) operating = false;
                }
            });


            Task.Factory.StartNew(() =>
            {
                for (; ; )
                {
                    if (preWindowState != currentWindowState) Thread.Sleep(3 * 1000);

                    IntPtr hWnd = User32.GetForegroundWindowProcessId(out int calcID);

                    if (hWnd != IntPtr.Zero)
                    {
                        GetClassName(hWnd, currentWindowClassName, currentWindowClassName.Capacity);
                        if (!_ignoreWindowName.Contains(currentWindowClassName.ToString()))
                        {
                            //bool isZoomed = IsZoomed(hWnd);
                            if (preWindowState != currentWindowState && !operating)
                            {
                                // 相同进程的子窗体不会切换任务栏显示
                                if (cacheProcessId != calcID || cacheIntPtr == hWnd)
                                {
                                    preWindowState = currentWindowState;
                                    SwichTaskBar(currentWindowState);
                                    cacheProcessId = calcID;
                                    cacheIntPtr = hWnd;
                                }
                            }
                        }
                    }
                    Thread.Sleep(500);
                }
            });

        }

        private void SwichTaskBar(bool isHide)
        {
            TaskbarController.SetTaskbarState(isHide ?
                TaskbarController.AppBarStates.AutoHide :
                TaskbarController.AppBarStates.AlwaysOnTop);
        }

        private void RunStart_CheckedChanged(object sender, EventArgs e)
        {
            if (!(RunStart.Checked ^ Program.IsRunStart())) return;
            if (Program.RunWhenStart(RunStart.Checked))
            {
                Settings.Default.RunStart = RunStart.Checked;
                Settings.Default.Save();
            }
            else
            {
                RunStart.Checked = !RunStart.Checked;
            }
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Exit();
        }

        private delegate int HookProc(int nCode, int wParam, IntPtr IParam);
    }

}
