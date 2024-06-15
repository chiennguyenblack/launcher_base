using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace EasyGunLauncherLite
{
    public partial class ChangePasswordTwoFactorValidation : Form
    {
        public string username;
        public string token;
        
        int countdown = 120; //seconds
        public ChangePasswordTwoFactorValidation()
        {
            InitializeComponent();
        }

        private void TwoFactorValidation_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Text = this.username + ": Đổi mật khẩu - Xác thực 2 lớp";
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
        }
        
        private string validate(string code)
        {
            dynamic data = new ExpandoObject();
            data.code = code;
            Task<string> t = AsyncRequest.createRequestAsync("api/lite/changePassword/validatetfa", this.token, JsonConvert.SerializeObject(data));
            string responseString = t.Result;
            ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserialized != null)
            {
                if (deserialized.success)
                {
                    MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    OnChangedSuccess(null);
                    Close();
                }
                else
                {
                    MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                Log.AddLog("api/lite/changePassword/validatetfa: " + responseString);
                MessageBox.Show("Hệ thống gặp lỗi, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ! (380)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        
        public event EventHandler ChangedSuccess;
        protected virtual void OnChangedSuccess(EventArgs e)
        {
            EventHandler eh = ChangedSuccess;
            if (eh != null)
            {
                eh(this, e);
            }
        }
    }
}