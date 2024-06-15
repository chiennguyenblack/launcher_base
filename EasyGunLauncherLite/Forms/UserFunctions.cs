using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyGunLauncherLite
{
    public partial class UserFunctions : Form
    {
        Menu.AddCoin addCoinForm = null;
        ChangeEmail changeEmailForm = null;
        ChangePassword changePasswordForm = null;
        Forms.Menu.ChargeHistory chargeHistoryForm = null;
        ConvertCoin convertCoinForm = null;
        Forms.Menu.TwoFactorSwitch twoFactorSwitchForm = null;
        ActiveEmailTwoFactorValidation activeEmailForm = null;

        UserInfo userInfo = null;

        public UserFunctions()
        {
            InitializeComponent();
        }

        private void UserFunctions_Load(object sender, EventArgs e)
        {
            accountList.Items.Clear();
            Dictionary<string, UserInfo> accounts = AccountMgr.getAllAccounts();
            foreach (KeyValuePair<string, UserInfo> account in accounts)
            {
                accountList.Items.Add(account.Key);
            }
            disableControls();
            darkMode();
        }

        private void darkMode()
        {
            IniData configs = Startup.getConfigs();
            if (configs["UI"]["DarkMode"] == "true")
            {
                Helper.UseImmersiveDarkMode(this.Handle, true);
                panel1.BackColor = Color.Black;
                panel1.ForeColor = Color.White;
                //label1.ForeColor = Color.White;
                //label2.ForeColor = Color.White;
                //label3.ForeColor = Color.White;
                //label5.ForeColor = Color.White;
                //label6.ForeColor = Color.White;
                //label7.ForeColor = Color.White;
                //label8.ForeColor = Color.White;
                //label10.ForeColor = Color.White;
                //label12.ForeColor = Color.White;
                //lblUsername.ForeColor = Color.White;
                //lblActiveEmail.ForeColor = Color.White;
                //lblCoin.ForeColor = Color.White;
                //lblEmail.ForeColor = Color.White;
                //lblTwoFactor.ForeColor = Color.White;
                //lblDateCreated.ForeColor = Color.White;
                panel2.ForeColor = Color.White;
                panel2.Paint += panel2_Paint;
                panel2.Invalidate();
                panel2.Update();
                //btnActiveEmail.FlatAppearance.BorderColor = Color.White;
                //btnActiveEmail.BackColor = Color.DimGray;
                btnAddCoin.FlatAppearance.BorderColor = Color.White;
                btnAddCoin.BackColor = Color.DimGray;
                //btnChangeEmail.FlatAppearance.BorderColor = Color.White;
                //btnChangeEmail.BackColor = Color.DimGray;
                btnExchange.FlatAppearance.BorderColor = Color.White;
                btnExchange.BackColor = Color.DimGray;
                btnHistory.FlatAppearance.BorderColor = Color.White;
                btnHistory.BackColor = Color.DimGray;
                //btnTwoFactor.FlatAppearance.BorderColor = Color.White;
                //btnTwoFactor.BackColor = Color.DimGray;
                btnChangePassword.FlatAppearance.BorderColor = Color.White;
                btnChangePassword.BackColor = Color.DimGray;
                accountList.BackColor = Color.Black;
                accountList.ForeColor = Color.White;
            }
        }

        private void accountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (accountList.SelectedItem == null)
            {
                return;
            }
            string username = accountList.SelectedItem.ToString();
            UserInfo userInfo = AccountMgr.getUserInfoByUsername(username);
            if (userInfo == null)
            {
                MessageBox.Show("Tài khoản không tồn tại hoặc đã thoát!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clearAccountInfo();
                disableControls();
                return;
            }
            lblUsername.Text = userInfo.UserName;
            lblCoin.Text = userInfo.Money.ToString("N0", new CultureInfo("vi-VN"));
            lblActiveEmail.Text = userInfo.VerifiedEmail ? "     Đã xác thực" : "     Chưa xác thực";
            lblActiveEmail.ImageList = imageList1;
            lblTwoFactor.Text = userInfo.TwoFactor ? "     Đã bật" : "     Đang tắt";
            lblTwoFactor.ImageList = imageList1;
            lblDateCreated.Text = userInfo.Date;
            lblEmail.Text = userInfo.Email;
            if (userInfo.VerifiedEmail)
            {
                lblActiveEmail.ImageIndex = 0;
            } else
            {
                lblActiveEmail.ImageIndex = 1;
            }
            if (userInfo.TwoFactor)
            {
                lblTwoFactor.ImageIndex = 0;
            }
            else
            {
                lblTwoFactor.ImageIndex = 1;
            }
            this.userInfo = userInfo;
            enableControls();
        }

        private void clearAccountInfo()
        {
            lblUsername.Text = "";
            lblEmail.Text = "";
            lblCoin.Text = "";
            lblActiveEmail.Text = "";
            lblTwoFactor.Text = "";
            lblDateCreated.Text = "";
            lblActiveEmail.ImageList = null;
            lblTwoFactor.ImageList = null;
        }

        private void enableControls()
        {
            //btnActiveEmail.Enabled = true;
            //btnTwoFactor.Enabled = true;
            btnAddCoin.Enabled = true;
            //btnChangeEmail.Enabled = true;
            btnChangePassword.Enabled = true;
            btnExchange.Enabled = true;
            btnHistory.Enabled = true;
        }

        private void disableControls()
        {
            //btnActiveEmail.Enabled = false;
            //btnTwoFactor.Enabled = false;
            btnAddCoin.Enabled = false;
            //btnChangeEmail.Enabled = false;
            btnChangePassword.Enabled = false;
            btnExchange.Enabled = false;
            btnHistory.Enabled = false;
        }

        private void btnAddCoin_Click(object sender, EventArgs e)
        {
            if (this.addCoinForm != null)
            {
                if (this.addCoinForm.Visible == false)
                {
                    this.addCoinForm.Show(this);
                }
                this.addCoinForm.BringToFront();
                return;
            }
            Menu.AddCoin addCoinForm = new Menu.AddCoin();
            addCoinForm.username = userInfo.UserName;
            addCoinForm.token = userInfo.Token;
            addCoinForm.FormClosed += AddCoinFormClosed;
            addCoinForm.Show(this);
            this.addCoinForm = addCoinForm;
        }

        private void AddCoinFormClosed(object sender, EventArgs e)
        {
            this.addCoinForm = null;
            GC.Collect();
        }

        private void menuConvertCoin_Click(object sender, EventArgs e)
        {
            if (this.convertCoinForm != null)
            {
                if (this.convertCoinForm.Visible == false)
                {
                    this.convertCoinForm.Show(this);
                }
                this.convertCoinForm.BringToFront();
                return;
            }
            ConvertCoin convertCoinForm = new ConvertCoin();
            convertCoinForm.username = userInfo.UserName;
            convertCoinForm.token = userInfo.Token;
            convertCoinForm.FormClosed += ConvertCoinFormClosed;
            convertCoinForm.Show(this);
            this.convertCoinForm = convertCoinForm;
        }

        private void ConvertCoinFormClosed(object sender, EventArgs e)
        {
            this.convertCoinForm = null;
            GC.Collect();
        }

        private void menuChargeHistory_Click(object sender, EventArgs e)
        {
            if (this.chargeHistoryForm != null)
            {
                if (this.chargeHistoryForm.Visible == false)
                {
                    this.chargeHistoryForm.Show(this);
                }
                this.chargeHistoryForm.BringToFront();
                return;
            }
            Forms.Menu.ChargeHistory chargeHistoryForm = new Forms.Menu.ChargeHistory();
            chargeHistoryForm.username = userInfo.UserName;
            chargeHistoryForm.token = userInfo.Token;
            chargeHistoryForm.FormClosed += ChargeHistoryFormClosed;
            chargeHistoryForm.Show(this);
            this.chargeHistoryForm = chargeHistoryForm;
        }

        private void ChargeHistoryFormClosed(object sender, EventArgs e)
        {
            this.chargeHistoryForm = null;
            GC.Collect();
        }

        private void menuChangePassword_Click(object sender, EventArgs e)
        {
            if (this.changePasswordForm != null)
            {
                if (this.changePasswordForm.Visible == false)
                {
                    this.changePasswordForm.Show(this);
                }
                this.changePasswordForm.BringToFront();
                return;
            }
            ChangePassword changePasswordForm = new ChangePassword();
            changePasswordForm.username = userInfo.UserName;
            changePasswordForm.token = userInfo.Token;
            changePasswordForm.userInfo = userInfo;
            changePasswordForm.FormClosed += ChangePasswordFormClosed;
            changePasswordForm.Show(this);
            this.changePasswordForm = changePasswordForm;
        }

        private void ChangePasswordFormClosed(object sender, EventArgs e)
        {
            this.changePasswordForm = null;
            GC.Collect();
        }

        private void menuActiveEmail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.userInfo.Email))
            {
                MessageBox.Show("Bạn chưa đăng ký email, vui lòng đổi email để thực hiện xác thực!");
                return;
            }
            if (this.activeEmailForm != null)
            {
                if (this.activeEmailForm.Visible == false)
                {
                    this.activeEmailForm.Show(this);
                }
                this.activeEmailForm.BringToFront();
                return;
            }
            ActiveEmailTwoFactorValidation activeEmailForm = new ActiveEmailTwoFactorValidation();
            activeEmailForm.username = userInfo.UserName;
            activeEmailForm.token = userInfo.Token;
            activeEmailForm.FormClosed += ActiveEmailFormClosed;
            activeEmailForm.ActiveSuccess += ActiveEmailSuccess;
            activeEmailForm.Show(this);
            this.activeEmailForm = activeEmailForm;
        }

        private void ActiveEmailFormClosed(object sender, EventArgs e)
        {
            this.activeEmailForm = null;
        }

        private void ActiveEmailSuccess(object sender, EventArgs e)
        {
            this.userInfo.VerifiedEmail = true;
            accountList_SelectedIndexChanged(sender, e);
        }

        private void menuChangeEmail_Click(object sender, EventArgs e)
        {
            if (this.changeEmailForm != null)
            {
                if (this.changeEmailForm.Visible == false)
                {
                    this.changeEmailForm.Show(this);
                }
                this.changeEmailForm.BringToFront();
                return;
            }
            ChangeEmail changeEmailForm = new ChangeEmail();
            changeEmailForm.username = userInfo.UserName;
            changeEmailForm.token = userInfo.Token;
            changeEmailForm.FormClosed += ChangeEmailFormClosed;
            changeEmailForm.ChangeSuccess += ChangeEmailSuccess;
            changeEmailForm.Show(this);
            this.changeEmailForm = changeEmailForm;
        }

        private void ChangeEmailFormClosed(object sender, EventArgs e)
        {
            this.changeEmailForm = null;
        }

        private void ChangeEmailSuccess(object sender, EventArgs e, string email)
        {
            this.userInfo.VerifiedEmail = false;
            this.userInfo.TwoFactor = false;
            this.userInfo.Email = email;
            accountList_SelectedIndexChanged(sender, e);
        }

        private void menuTwoFactorOnOff_Click(object sender, EventArgs e)
        {
            if (this.twoFactorSwitchForm != null)
            {
                if (this.twoFactorSwitchForm.Visible == false)
                {
                    this.twoFactorSwitchForm.Show(this);
                }
                this.twoFactorSwitchForm.BringToFront();
                return;
            }
            Forms.Menu.TwoFactorSwitch twoFactorSwitchForm = new Forms.Menu.TwoFactorSwitch();
            twoFactorSwitchForm.username = userInfo.UserName;
            twoFactorSwitchForm.token = userInfo.Token;
            twoFactorSwitchForm.userInfo = userInfo;
            twoFactorSwitchForm.FormClosed += TwoFactorSwitchFormClosed;
            twoFactorSwitchForm.ActiveChanged += TwoFactorActiveChanged;
            twoFactorSwitchForm.Show(this);
            this.twoFactorSwitchForm = twoFactorSwitchForm;
        }

        private void TwoFactorSwitchFormClosed(object sender, EventArgs e)
        {
            this.twoFactorSwitchForm = null;
        }

        private void TwoFactorActiveChanged(object sender, EventArgs e, bool isActive)
        {
            this.userInfo.TwoFactor = isActive;
            accountList_SelectedIndexChanged(sender, e);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel2.DisplayRectangle, Color.White, ButtonBorderStyle.Inset);
            panel2.Paint -= panel2_Paint;
        }
    }
}
