using IniParser.Model;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Dynamic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyGunLauncherLite
{
    public partial class ChangeEmail : Form
    {
        public string username;
        public string token;
        private string email;

        public ChangeEmail()
        {
            InitializeComponent();
            darkMode();
        }

        private static bool IsValidEmail(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        private void txtNewEmail_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsValidEmail(txtNewEmail.Text))
            {
                lblMessage.Text = "";
            }
            else
            {
                lblMessage.Text = "Email không đúng định dạng!";
            }
        }

        private void ChangeEmail_Load(object sender, System.EventArgs e)
        {
            Text = this.username + ": Đổi email";
        }

        private void darkMode()
        {
            IniData configs = Startup.getConfigs();
            if (configs["UI"]["DarkMode"] == "true")
            {
                Helper.UseImmersiveDarkMode(this.Handle, true);
                panel1.BackColor = Color.Black;
                panel1.ForeColor = Color.White;
                label1.BackColor = Color.DimGray;
                label2.BackColor = Color.DimGray;
                txtCurrentPassword.BackColor = Color.Black;
                txtCurrentPassword.ForeColor = Color.White;
                txtNewEmail.BackColor = Color.Black;
                txtNewEmail.ForeColor = Color.White;
                btnChangeEmail.BackColor = Color.DimGray;
                btnChangeEmail.ForeColor = Color.White;
                btnChangeEmail.FlatAppearance.BorderColor = Color.White;
            }
        }

        private void btnChangeEmail_Click(object sender, System.EventArgs e)
        {
            if (!IsValidEmail(txtNewEmail.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(txtCurrentPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.disableControls();
            dynamic data = new ExpandoObject();
            data.current_password = txtCurrentPassword.Text;
            data.new_email = txtNewEmail.Text;
            this.email = txtNewEmail.Text;
            Task<string> t = AsyncRequest.createRequestAsync("api/lite/changeEmail", this.token, JsonConvert.SerializeObject(data));
            string responseString = t.Result;
            if (responseString.Length <= 0)
            {
                MessageBox.Show("Lỗi hệ thống, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ! (355)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.AddLog("api/lite/changeEmail: " + responseString);
            }
            else
            {
                ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
                if (deserialized != null)
                {
                    if (deserialized.data != null && deserialized.data.TwoFactor)
                    {
                        ChangeEmailTwoFactorValidation changeEmailTwoFactorValidation = new ChangeEmailTwoFactorValidation();
                        changeEmailTwoFactorValidation.username = this.username;
                        changeEmailTwoFactorValidation.token = this.token;
                        changeEmailTwoFactorValidation.ChangeSuccess += TwoFactorValidateSuccess;
                        changeEmailTwoFactorValidation.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (deserialized.success)
                        {
                            OnChangeSuccess(null);
                            Close();
                        }
                    }
                }
            }
            this.enableControls();
        }

        private void disableControls()
        {
            txtCurrentPassword.Enabled = false;
            txtNewEmail.Enabled = false;
            btnChangeEmail.Enabled = false;
        }

        private void enableControls()
        {
            txtCurrentPassword.Enabled = true;
            txtNewEmail.Enabled = true;
            btnChangeEmail.Enabled = true;
        }

        public delegate void ChangeEmailEventHandler(object sender, EventArgs e, string email);
        /// <summary>
        /// Event to indicate new data is available
        /// </summary>
        public event ChangeEmailEventHandler ChangeSuccess;
        /// <summary>
        /// Called to signal to subscribers that new data is available
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnChangeSuccess(EventArgs e)
        {
            ChangeEmailEventHandler eh = ChangeSuccess;
            if (eh != null)
            {
                eh(this, e, this.email);
            }
        }

        private void TwoFactorValidateSuccess(object sender, EventArgs e)
        {
            OnChangeSuccess(null);
            Close();
        }
    }
}