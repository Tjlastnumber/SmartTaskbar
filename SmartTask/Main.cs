using SmartTask.Properties;
using System;
using System.ComponentModel;
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

        private readonly StringBuilder _currentWindowClassName = new StringBuilder(256);
        private readonly string _ignoreWindowName = string.Empty;

        //定义钩子句柄
        public static int _hHook = 0;
        private static bool _isShow = false;
        private static System.Timers.Timer _timer;

        private static HookProc MouseHookProc;
        private static HookProc KeyBoardHookProc;

        private bool timeRunning;
        private bool? preWindowState;
        private bool currentWindowState;
        private int cacheProcessId;
        private IntPtr cacheIntPtr;

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
            SmartTaskEnable.Checked = true;

            MouseHookProc = new HookProc(MouseHookHandler);
            KeyBoardHookProc = new HookProc(KeyBoardHookHandler);

            _hHook = SetWindowsHookEx(
                   WH_MOUSE_LL,
                   MouseHookProc,
                   Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                   0);

            _hHook = SetWindowsHookEx(
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

            this.Nud_Interval.Value = Settings.Default.Interval;

            InitTimer();
        }

        private void InitTimer()
        {
            var inter = Convert.ToDouble(this.Nud_Interval.Value) * 1000D;
            _timer = new System.Timers.Timer(inter);
            _timer.Elapsed += Time_Elapsed;
            _timer.AutoReset = true;
            _timer.Start();
            timeRunning = true;
        }

        private void ResetTimer()
        {
            if (!SmartTaskEnable.Checked) return;
            _timer.Close();
            InitTimer();
        }

        private int MouseHookHandler(int nCode, int wParam, IntPtr lParam)
        {
            ResetTimer();
            return CallNextHookEx(_hHook, nCode, wParam, lParam);
        }

        private int KeyBoardHookHandler(int nCode, int wParam, IntPtr lParam)
        {
            ResetTimer();
            return CallNextHookEx(_hHook, nCode, wParam, lParam);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Hide();
            _isShow = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void Time_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IntPtr hWnd = User32.GetForegroundWindowProcessId(out int calcID);

            if (hWnd != IntPtr.Zero)
            {
                GetClassName(hWnd, _currentWindowClassName, _currentWindowClassName.Capacity);
                if (!_ignoreWindowName.Contains(_currentWindowClassName.ToString()))
                {
                    currentWindowState = IsZoomed(hWnd);
                    if (preWindowState == null || preWindowState != currentWindowState)
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

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.Interval = this.Nud_Interval.Value;
            Settings.Default.Save();
            var interval = Convert.ToDouble(this.Nud_Interval.Value) * 1000D;
            _timer.Stop();
            _timer.Interval = interval;
            _timer.Start();

            Hide();
        }

        private void taskbarIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Nud_Interval.Value = Settings.Default.Interval;
            Hide();
        }

        private delegate int HookProc(int nCode, int wParam, IntPtr IParam);

        private void SmartTaskEanbled()
        {
            SmartTaskEnable.Checked = !SmartTaskEnable.Checked;
            if (SmartTaskEnable.Checked)
            {
                var interval = Convert.ToDouble(this.Nud_Interval.Value) * 1000D;
                _timer.Interval = interval;
                _timer.Start();
                this.taskbarIcon.Icon = Resources.SmartTaskbarx32;
            }
            else
            {
                _timer.Stop();
                this.taskbarIcon.Icon = Resources.SmartTaskbarEnablex32;
            }
            SwichTaskBar(false);
        }

        private void taskbarIcon_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            SmartTaskEanbled();
        }

        private void SmartTaskEnable_Click(object sender, EventArgs e)
        {
            SmartTaskEanbled();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            this.SetTopLevel(true);
            this.Show();
        }
    }

}
