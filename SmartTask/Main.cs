﻿using SmartTask.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
        private bool currentWindowMax = false;

        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Hide();
        }

        private StringBuilder currentActiveClassName = new StringBuilder(256);

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Task.Factory.StartNew(() =>
            {
                for (; ; )
                {
                    IntPtr hWnd = User32.GetForegroundWindowProcessId(out int calcID);
                    if (hWnd != IntPtr.Zero)
                    {
                        GetClassName(hWnd, currentActiveClassName, currentActiveClassName.Capacity);
                        // 排除因任务栏切换造成不能查看任务视图
                        if (currentActiveClassName.ToString() != "Windows.UI.Core.CoreWindow")
                        {
                            bool isZoomed = IsZoomed(hWnd);
                            if (currentWindowMax != isZoomed)
                            {
                                SwichTaskBar(IsZoomed(hWnd));
                                currentWindowMax = isZoomed;
                            }
                        }
                    }
                    Thread.Sleep(500);
                }
            });
        }

        private void SwichTaskBar(bool isHide)
        {
            TaskbarController.SetTaskbarState(
                isHide ?
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
