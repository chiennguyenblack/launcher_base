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
    public partial class ActiveEmailTwoFactorValidation : Form
    {
        public string username;
        public string token;

        int countdown = 120; //seconds
        public ActiveEmailTwoFactorValidation()
        {
            InitializeComponent();
        }

        private void TwoFactorValidation_Load(object sender, EventArgs e)
        {
            //timer1.Start();
            Text = this.username + ": Kích hoạt Email";
            lblLoading.Visible = true;
            lblLoading.BringToFront();
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
            ResponseObject deserializedProduct = JsonConvert.DeserializeObject<ResponseObject>(response);
            string msg = "";
            if (deserializedProduct != null)
            {
                if (deserializedProduct.success)
                {
                    MessageBox.Show(deserializedProduct.message, "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    OnActiveSuccess(null);
                    Close();
                    return;
                }
                msg = deserializedProduct.message;
            }
            if (msg.Length == 0)
            {
                Log.AddLog("api/lite/validateVerifyEmail: " + response);
                MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ! (341)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        private string validate(string code)
        {
            dynamic data = new ExpandoObject();
            data.code = code;
            Task<string> t = AsyncRequest.createRequestAsync("api/lite/validateVerifyEmail", this.token, JsonConvert.SerializeObject(data));
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

        /// <summary>
        /// Event to indicate new data is available
        /// </summary>
        public event EventHandler ActiveSuccess;
        /// <summary>
        /// Called to signal to subscribers that new data is available
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnActiveSuccess(EventArgs e)
        {
            EventHandler eh = ActiveSuccess;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        private void ActiveEmailTwoFactorValidation_Shown(object sender, EventArgs e)
        {
            Task<string> t = AsyncRequest.createRequestAsync("api/verifyEmail", this.token);
            string responseString = t.Result;
            ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserialized != null)
            {
                if (deserialized.success)
                {
                    lblLoading.Visible = false;
                    timer1.Start();
                }
                else
                {
                    MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                Log.AddLog("api/verifyEmail: " + responseString);
                MessageBox.Show("Hệ thống gặp lỗi, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ! (331)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}