using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGunLauncherLite
{
    internal class Servers
    {
        public static Dictionary<int, string> ServerList = new Dictionary<int, string>();

        public static List<ServerObject> getServerListAsync()
        {
            Task<string> t = AsyncRequest.createRequestAsync("api/serverlist", null);
            string responseString = t.Result;
            ServerListResponseObject deserialized = JsonConvert.DeserializeObject<ServerListResponseObject>(responseString);
            ServerList.Clear();
            if (deserialized != null && deserialized.success == true && deserialized.data != null)
            {
                foreach (ServerObject item in deserialized.data)
                {
                    ServerList.Add(item.ServerID, item.ServerName);
                }
                return deserialized.data;
            } else
            {
                Log.AddLog("api/serverlist: " + responseString);
            }
            return null;
        }
    }
}
