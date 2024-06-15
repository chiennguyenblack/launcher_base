using System;
using System.Drawing;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser.Model;
using Newtonsoft.Json;

namespace EasyGunLauncherLite
{
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
            darkMode();
        }

        private void darkMode()
        {
            IniData configs = Startup.getConfigs();
            if (configs["UI"]["DarkMode"] == "true")
            {
                Helper.UseImmersiveDarkMode(this.Handle, true);
                BackColor = Color.Black;
                panel1.BackColor = Color.Black;
                panel1.ForeColor = Color.White;
                label1.BackColor = Color.DimGray;
                label2.BackColor = Color.DimGray;
                txtUsername.BackColor = Color.Black;
                txtUsername.ForeColor = Color.White;
                button1.BackColor = Color.DimGray;
                button1.ForeColor = Color.White;
                button1.FlatAppearance.BorderColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản hoặc địa chỉ email để khôi phục mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtUsername.Enabled = false;
            button1.Enabled = false;
            
            dynamic data = new ExpandoObject();
            data.account = txtUsername.Text;
            Task<string> t = AsyncRequest.createRequestAsync("api/lite/forgotPassword", null, JsonConvert.SerializeObject(data));
            string responseString = t.Result;
            ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserialized != null)
            {
                if (deserialized.data != null && deserialized.data.TwoFactor)
                {
                    ForgotPasswordTwoFactorValidation twoFactor = new ForgotPasswordTwoFactorValidation();
                    twoFactor.account = deserialized.message;
                    twoFactor.ChangedSuccess += PasswordChanged;
                    twoFactor.ShowDialog();
                    return;
                }
                MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Log.AddLog("api/lite/forgotPassword: " + responseString);
                MessageBox.Show("Hệ thống gặp lỗi, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ! (401)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtUsername.Enabled = true;
            button1.Enabled = true;
        }
        
        private void PasswordChanged(object sender, EventArgs e)
        {
            Close();
        }
    }
}