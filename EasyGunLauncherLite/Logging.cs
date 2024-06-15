using System;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace EasyGunLauncherLite
{
    public class Logging
    {
        public static void AddDebugLogs(string title, string content, bool writeLog = false)
        {
            string data = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {title}, Content:{content}";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.StartupPath + @"\Debug.txt", false))
            {
                file.WriteLine(data);
            };
        }
    }
}