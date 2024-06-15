using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IniParser.Model;
using RestSharp.Extensions.MonoHttp;

namespace EasyGunLauncherLite
{
    public class FlashProcess
    {
        protected Process ps = (Process)null;
        
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out RECT rect);

        [DllImport("user32.dll")]
        private static extern bool SetWindowText(IntPtr hWnd, string windowName);
        
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        // privileges
        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        // used for memory allocation
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        public string username;
        
        public void startGame(string url)
        {
            //if (!File.Exists(Startup.USER_APPLICATION_DATA_FOLDER + Startup.APPLICATION_DATA_FOLDER + "svchosts.exe"))
            //{
            //    MessageBox.Show("Lỗi khởi tạo tài nguyên, vui lòng tắt chương trình diệt virus!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //NameValueCollection _params = getFlashConfigFromUrl(url);
            //string _prstr = "?user=" + _params["user"] + "&key=" + _params["key"] + "&v=" + _params["v"] + "&rand=" + _params["rand"] + "&config=" + _params["config"];
            ps = Process.Start(Startup.windowsFilePath + "\\Client.exe", url + "&a=");
            ps.EnableRaisingEvents = true;
            ps.Exited += new EventHandler(Ps_Exited);
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(workerDoWork);
            backgroundWorker.RunWorkerAsync();
            ps.WaitForInputIdle();
            IniData configs = Startup.getConfigs();
            if (configs["UI"]["DarkMode"] == "true")
            {
                Helper.UseImmersiveDarkMode(ps.MainWindowHandle, true);
            }
            //SetParent(ps.MainWindowHandle, panel1.Handle);
            //InjectHook();
            //int protectorId = startProtector(Process.GetCurrentProcess().Id + " " + ps.Id + " " + username);
            ProcessMgr.AddProcess(ps.Id);
        }

        private NameValueCollection getFlashConfigFromUrl(string url)
        {
            Uri myUri = new Uri(url);
            NameValueCollection param1s = HttpUtility.ParseQueryString(myUri.Query);
            return param1s;
        }

        private void workerDoWork(object sender, DoWorkEventArgs e)
        {
            bool flag = true;
            while (flag)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.MainWindowTitle == "Adobe Flash Player 11")
                    {
                        SetWindowText(process.MainWindowHandle, this.username + " - GunBacTrungNam Launcher");
                        flag = false;
                    }
                }
                Thread.Sleep(50);
            }
        }

        private void Ps_Exited(object sender, EventArgs e)
        {
            ProcessMgr.RemoveProcess(ps.Id);
            ps = null;
            AccountMgr.removeAccount(username);
        }

        private int startProtector(string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo();            
            psi.FileName = Startup.USER_APPLICATION_DATA_FOLDER + Startup.APPLICATION_DATA_FOLDER + "svchosts.exe";            
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.Arguments = arguments;
            psi.CreateNoWindow = true;

            Process proc = Process.Start(psi);
            //proc.WaitForInputIdle();
            return proc.Id;
        }

        private void InjectHook()
        {
            IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, ps.Id);

            // searching for the address of LoadLibraryA and storing it in a pointer
            IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            // name of the dll we want to inject
            string dllName = Startup.windowsFilePath + "\\GunBacTrungNamHook.dll";

            // alocating some memory on the target process - enough to store the name of the dll
            // and storing its address in a pointer
            IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);

            // writing the name of the dll there
            UIntPtr bytesWritten;
            WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(dllName), (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);

            // creating a thread that will call LoadLibraryA with allocMemAddress as argument
            CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);
        }
    }
}