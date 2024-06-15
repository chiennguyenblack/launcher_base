using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EasyGunLauncherLite
{
    class OpenWebpage
    {
        protected string URL;

        public OpenWebpage(string url)
        {
            this.URL = url;
        }

        bool OpenBrowser(string browserExecutable)
        {
            try
            {
                string process = browserExecutable;
                string args = this.URL;
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = process;
                psi.Arguments = args;
                var ps = Process.Start(psi);
                if (ps == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool OpenInChrome()
        {
            return this.OpenBrowser("chrome.exe");
        }

        public bool OpenInFirefox()
        {
            return this.OpenBrowser("firefox.exe");
        }

        public bool OpenInEdge()
        {
            try
            {
                string process = "microsoft-edge:" + this.URL;
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = process;
                var ps = Process.Start(psi);
                if (ps == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
