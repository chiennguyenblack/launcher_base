using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DeviceId;

namespace EasyGunLauncherLite
{
    internal class Helper
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        
        public static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (IsWindows8OrGreater(9200))
            {
                var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                //if (IsWindows8OrGreater(18985))
                //{
                    attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
                //}

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return DwmSetWindowAttribute(handle, (int)attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

        public static bool IsWindows8OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Build >= build;
        }

        public static string DeviceID()
        {
            string deviceId = new DeviceIdBuilder()
                .OnWindows(windows => windows.AddWindowsDeviceId())
                .ToString();
            return deviceId;
        }
    }
}
