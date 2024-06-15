using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser.Model;
using Newtonsoft.Json;
using RestSharp;

namespace EasyGunLauncherLite
{
    public partial class RegisterForm : Form
    {
        private FormError formError = new FormError();
        public RegisterForm()
        {
            InitializeComponent();

            IniData configs = Startup.getConfigs();
            if (configs["UI"]["DarkMode"] == "true")
            {
                Helper.UseImmersiveDarkMode(this.Handle, true);
                panel1.BackColor = Color.Black;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                label7.ForeColor = Color.White;
                txtUsername.BackColor = Color.Black;
                txtUsername.ForeColor = Color.White;
                txtPassword.BackColor = Color.Black;
                txtPassword.ForeColor = Color.White;
                txtRePassword.BackColor = Color.Black;
                txtRePassword.ForeColor = Color.White;
                txtEmail.BackColor = Color.Black;
                txtEmail.ForeColor = Color.White;
                registerBtn.BackColor = Color.DimGray;
                registerBtn.ForeColor = Color.White;
                resetBtn.BackColor = Color.DimGray;
                resetBtn.ForeColor = Color.White;
            }
        }

        private void showErrorMessage()
        {
            if (formError.usernameError != "")
            {
                lblMessage.Text = formError.usernameError;
                return;
            }
            if (formError.passwordError != "")
            {
                lblMessage.Text = formError.passwordError;
                return;
            }
            if (formError.repasswordError != "")
            {
                lblMessage.Text = formError.repasswordError;
                return;
            }
            if (formError.emailError != "")
            {
                lblMessage.Text = formError.emailError;
                return;
            }

            lblMessage.Text = "";
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

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (IsValidEmail(txtEmail.Text))
            {
                formError.emailError = "";
            }
            else
            {
                formError.emailError = "Email không đúng định dạng!";
            }
            showErrorMessage();
        }

        private void txtRePassword_TextChanged(object sender, EventArgs e)
        {
            if (txtRePassword.Text == txtPassword.Text)
            {
                formError.repasswordError = "";
            }
            else
            {
                formError.repasswordError = "Xác nhận mật khẩu không đúng, vui lòng kiểm tra lại!";
            }
            showErrorMessage();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            var regexItem = new Regex("^[a-z0-9]*$");

            if (!regexItem.IsMatch(username))
            {
                formError.usernameError = "Tên tài khoản không đúng!";
            }
            else
            {
                formError.usernameError = "";
            }
            showErrorMessage();
        }
        
        private void disableControls()
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtRePassword.Enabled = false;
            txtEmail.Enabled = false;
            registerBtn.Enabled = false;
            resetBtn.Enabled = false;
        }
        private void enableControls()
        {
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtRePassword.Enabled = true;
            txtEmail.Enabled = true;
            registerBtn.Enabled = true;
            resetBtn.Enabled = true;
        }
        private void clearContents()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtRePassword.Text = "";
            txtEmail.Text = "";
        }

        private void regsiterBtn_Click(object sender, EventArgs e)
        {
            if (formError.hasError())
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.disableControls();
            Task<string> t = AsyncRequest.createRequestAsync("api/lite/register", null, JsonConvert.SerializeObject(new RegisterRequest(txtUsername.Text, txtPassword.Text, txtRePassword.Text, txtEmail.Text)));
            string responseString = t.Result;
            if (responseString.Length <= 0)
            {
                MessageBox.Show("Server Die!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.AddLog("api/lite/register: " + responseString);
            }
            else
            {
                ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
                if (deserialized != null)
                {
                    this.clearContents();
                    MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lỗi hệ thống, vui lòng thử lại hoặc liên hệ quản trị viên để được trợ giúp (11256)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log.AddLog("api/lite/register: " + responseString);
                }
            }
            this.enableControls();
            return;
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            this.clearContents();
        }
    }

    public class FormError
    {
        public string usernameError = "";
        public string passwordError = "";
        public string repasswordError = "";
        public string emailError = "";

        public bool hasError()
        {
            return usernameError != ""
                   || passwordError != ""
                   || repasswordError != ""
                   || emailError != "";
        }
    }
}
