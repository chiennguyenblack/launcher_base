namespace EasyGunLauncherLite
{
    public class RegisterRequest
    {
        public string username;
        public string password;
        public string repassword;
        public string email;

        public RegisterRequest()
        {
            
        }
        
        public RegisterRequest(string username, string password, string repassword, string email)
        {
            this.username = username;
            this.password = password;
            this.repassword = repassword;
            this.email = email;
        }
    }
}