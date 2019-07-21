using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win32ApI;

namespace SmartTask
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*
             * 设置管理员启动 
             */
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
#if DEBUG
            Application.Run(new MainForm());
#else
            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                Application.Run(new MainForm());
            }
            else
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Application.ExecutablePath,
                    Verb = "runas" 
                };
                try
                {
                    Process.Start(startInfo);
                }
                catch 
                {
                    return;
                }
            }
#endif
        }

        const string REGISTRY_KEY_RUN_PATH = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        public static bool RunWhenStart(bool Started)
        {
            bool result = true;
            string productName = Application.ProductName;
            RegistryKey HKLM = Registry.LocalMachine;
            RegistryKey Run = HKLM.CreateSubKey(REGISTRY_KEY_RUN_PATH);
            if (Started)
            {
                try
                {
                    Run.SetValue(productName, Path.GetFullPath(Application.ExecutablePath));
                }
                catch (Exception err)
                {
                    result = false;
                    MessageBox.Show(err.Message.ToString(), productName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HKLM.Close();
                    Run.Close();
                }
            }
            else
            {
                try
                {
                    Run.DeleteValue(productName, false);
                }
                catch (Exception err)
                {
                    result = false;
                    MessageBox.Show(err.Message.ToString(), productName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
                finally
                {
                    HKLM.Close();
                    Run.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 是否为开机启动
        /// </summary>
        /// <returns></returns>
        public static bool IsRunStart()
        {
            string productName = Application.ProductName;
            RegistryKey HKLM = Registry.LocalMachine;
            RegistryKey Run = HKLM.CreateSubKey(REGISTRY_KEY_RUN_PATH);
            try
            {
                return Run.GetValueNames().Any(k => k == productName);
            }
            catch 
            {
                return false;
            }
        }
    }
}
