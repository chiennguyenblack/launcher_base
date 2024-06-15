using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace EasyGunLauncherLite
{
    public partial class TwoFactorValidation : Form
    {
        public string username;
        public string password;
        public int serverid;
        
        int countdown = 120; //seconds
        public TwoFactorValidation()
        {
            InitializeComponent();
        }

        private void TwoFactorValidation_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Text = this.username + ": Đăng nhập - Xác thực 2 lớp";
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            countdown--;
            displayText();
            if (countdown <= 0)
            {
                timer1.Stop();
                MessageBox.Show("Quá thời gian xác thực!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void displayText()
        {
            TimeSpan t = TimeSpan.FromSeconds( countdown );

            string answer = string.Format("{0:D2}:{1:D2}",
                t.Minutes, 
                t.Seconds);
            label1.Text = "Nhập mã xác thực được gửi tới Email đã đăng ký (" + answer + ")";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string code = textBox1.Text;
            if (code == null || code.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập mã xác thực!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.disableControls();
            string response = validate(code);
            if (response.Length == 0)
            {
                return;
            }
            LoginResponseObject deserializedProduct = JsonConvert.DeserializeObject<LoginResponseObject>(response);
            string msg = "";
            if (deserializedProduct != null)
            {
                if (deserializedProduct.success && deserializedProduct.data != null && !string.IsNullOrEmpty(deserializedProduct.data.playUrl) && deserializedProduct.data.playUrl.StartsWith("http"))
                {
                    //PlayWindow playWindow = new PlayWindow();
                    //playWindow.URL = deserializedProduct.data.playUrl;
                    //playWindow.username = this.username;
                    //playWindow.token = deserializedProduct.data.token;
                    //playWindow.userInfo = deserializedProduct.data.userInfo;
                    //playWindow.Show();
                    FlashProcess fp = new FlashProcess();
                    fp.username = this.username;
                    fp.startGame(deserializedProduct.data.playUrl);
                    Startup.SaveRecentAccount(username, password);
                    UserInfo userInfo = deserializedProduct.data.userInfo;
                    userInfo.Token = deserializedProduct.data.token;
                    AccountMgr.addAccount(username, userInfo);
                    Close();
                    return;
                }
                msg = deserializedProduct.message;
            }
            this.enableControls();
            if (msg.Length == 0)
            {
                Log.AddLog("api/lite/validate: " + response);
                MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ! (301)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        private string validate(string code)
        {
            dynamic data = new ExpandoObject();
            data.code = code;
            data.serverid = serverid;
            data.deviceid = Helper.DeviceID();
            Task<string> t = AsyncRequest.createRequestAsync("api/lite/validate", null, JsonConvert.SerializeObject(data));
            string responseString = t.Result;
            return responseString;
        }
        
        private void disableControls()
        {
            textBox1.Enabled = false;
            button1.Enabled = false;
        }
        private void enableControls()
        {
            textBox1.Enabled = true;
            button1.Enabled = true;
        }

        private void TwoFactorValidation_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }
    }
}