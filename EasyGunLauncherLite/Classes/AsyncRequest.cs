using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGunLauncherLite
{
    internal class AsyncRequest
    {
        public static Task<string> createRequestAsync(string uri, string token, string data = "")
        {
            var client = new RestClient(Host.current);
            client.Timeout = -1;
            var request = new RestRequest(uri, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            if (token != null)
            {
                request.AddHeader("Authorization", "Bearer " + token);
            }
            
            if (data != null)
            {
                string _params;
                if (data.Length < 2048)
                {
                    _params = Hash.Encrypt(data);
                } 
                else
                {
                    _params = data;
                }
                request.AddParameter("params", _params);
            }
            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });
            return tcs.Task;
        }
    }
}
