namespace EasyGunLauncherLite
{
    public class LoginRequest
    {
        public string username;
        public string password;
        public int serverid;
        public string deviceid;

        public LoginRequest()
        {
            
        }
        
        public LoginRequest(string username, string password, int serverid, string deviceid)
        {
            this.username = username;
            this.password = password;
            this.serverid = serverid;
            this.deviceid = deviceid;
        }
    }
}