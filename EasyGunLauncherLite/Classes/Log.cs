using System;
using System.IO;

namespace EasyGunLauncherLite
{
    public class Log
    {
        public static string DebugFilePath
        {
            get
            {
                return Startup.windowsFilePath + "\\debug.txt";
            }
        }
        public static void AddLog(string content)
        {
            if (!File.Exists(DebugFilePath))
            {
                File.Create(DebugFilePath);
            }
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(DebugFilePath);
                //Write a line of text
                sw.WriteLine("[" + DateTime.Now.ToString() + "] " + content);
                //Close the file
                sw.Close();
            }
            catch(Exception e)
            {
                //Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");
            }
        }
    }
}