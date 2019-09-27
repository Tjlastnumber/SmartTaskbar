using SmartTask.Properties;
using System;
using System.Collections.Generic;
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
        private bool currentWindowState = false;
        private bool preWindowState = false;    

        private StringBuilder currentWindowClassName = new StringBuilder(256);

        private int cacheProcessId;
        private IntPtr cacheIntPtr;

        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        private readonly List<string> _ignoreWindowName = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            _ignoreWindowName.Add("Windows.UI.Core.CoreWindow");
            _ignoreWindowName.Add("MultitaskingViewFrame");
            _ignoreWindowName.Add("ForegroundStaging");
            _ignoreWindowName.Add("Shell_TrayWnd");
            _ignoreWindowName.Add("Shell_SencondaryTrayWnd");
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
                    Thread.Sleep(300);
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
                            if (preWindowState != currentWindowState)
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
    }
}
