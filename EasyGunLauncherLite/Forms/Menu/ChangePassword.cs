using System;
using System.Drawing;
using Newtonsoft.Json;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyGunLauncherLite
{
    public partial class ChangePassword : Form
    {
        public string username;
        public string token;
        public UserInfo userInfo;
        
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void txtReNewPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtReNewPassword.Text != txtNewPassword.Text)
            {
                lblMessage.Text = "Xác nhận mật khẩu không đúng!";
            }
            else
            {
                lblMessage.Text = "";
            }
        }

        private void btnChangePass_Click(object sender, System.EventArgs e)
        {
            if (txtReNewPassword.Text != txtNewPassword.Text)
            {
                return;
            }
            if (string.IsNullOrEmpty(txtCurrentPassword.Text) || string.IsNullOrEmpty(txtNewPassword.Text) || string.IsNullOrEmpty(txtReNewPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dynamic data = new ExpandoObject();
            data.current_password = txtCurrentPassword.Text;
            data.new_password = txtNewPassword.Text;
            data.re_new_password = txtReNewPassword.Text;
            Task<string> t = AsyncRequest.createRequestAsync("api/lite/changePassword", this.token, JsonConvert.SerializeObject(data));
            string responseString = t.Result;
            ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserialized != null)
            {
                if (deserialized.data != null && deserialized.data.TwoFactor)
                {
                    ChangePasswordTwoFactorValidation twoFactor = new ChangePasswordTwoFactorValidation();
                    twoFactor.username = username;
                    twoFactor.token = token;
                    twoFactor.ChangedSuccess += PasswordChanged;
                    twoFactor.ShowDialog();
                    return;
                }
                MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (deserialized.success)
                {
                    Close();
                    return;
                }
            }
            else
            {
                Log.AddLog("api/lite/changePassword: " + responseString);
                MessageBox.Show("Hệ thống gặp lỗi, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ! (321)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.enableControls();
        }

        private void disableControls()
        {
            this.txtCurrentPassword.Enabled = false;
            this.txtNewPassword.Enabled = false;
            this.txtReNewPassword.Enabled = false;
            btnChangePass.Enabled = false;
        }

        private void enableControls()
        {
            btnChangePass.Enabled = true;
            this.txtCurrentPassword.Enabled = true;
            this.txtNewPassword.Enabled = true;
            this.txtReNewPassword.Enabled = true;
        }

        private void ChangePassword_Load(object sender, System.EventArgs e)
        {
            Text = this.username + ": Đổi mật khẩu";
            if (userInfo.TwoFactor)
            {
                lblTwoFactorStatus.Text = "    Xác thực 2 lớp đã bật";
                lblTwoFactorStatus.BackColor = Color.Honeydew;
                lblTwoFactorStatus.ForeColor = Color.LimeGreen;
                lblTwoFactorStatus.ImageIndex = 0;
            }
            else
            {
                lblTwoFactorStatus.Text = "    Xác thực 2 lớp đang tắt";
                lblTwoFactorStatus.BackColor = Color.MistyRose;
                lblTwoFactorStatus.ForeColor = Color.Red;
                lblTwoFactorStatus.ImageIndex = 1;
            }
        }
        
        private void PasswordChanged(object sender, EventArgs e)
        {
            Close();
        }
    }
}