using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EasyGunLauncherLite.Forms.Menu
{
    public partial class TwoFactorSwitch : Form
    {
        public string username;
        public string token;
        public UserInfo userInfo;

        public TwoFactorSwitch()
        {
            InitializeComponent();
        }

        private void TwoFactorSwitch_Load(object sender, System.EventArgs e)
        {
            Text = this.username + ": Xác thực 2 lớp";
            if (this.userInfo.VerifiedEmail)
            {
                cbxEmailActiveStatus.Checked = true;
                cbxEmailActiveStatus.ForeColor = System.Drawing.Color.Green;
                btnActiveNow.Enabled = true;
                cbxEmailActiveStatus.Text = "Đã kích hoạt";
            }
            else
            {
                cbxEmailActiveStatus.Checked = false;
                cbxEmailActiveStatus.ForeColor = System.Drawing.Color.Red;
                btnActiveNow.Enabled = false;
                cbxEmailActiveStatus.Text = "Chưa kích hoạt";
            }

            onChangeStatus();
        }

        private void onChangeStatus()
        {
            if (this.userInfo.TwoFactor)
            {
                cbxStatus.Checked = true;
                cbxStatus.ForeColor = System.Drawing.Color.Green;
                cbxStatus.Text = "Đã kích hoạt";
                btnActiveNow.Text = "Hủy kích hoạt xác thực 2 lớp";
            }
            else
            {
                cbxStatus.Checked = false;
                cbxStatus.ForeColor = System.Drawing.Color.Red;
                cbxStatus.Text = "Chưa kích hoạt";
                btnActiveNow.Text = "Kích hoạt xác thực 2 lớp ngay";
            }
        }

        private void btnActiveNow_Click(object sender, EventArgs e)
        {
            btnActiveNow.Enabled = false;
            Task<string> t = AsyncRequest.createRequestAsync("api/active2fa", this.token);
            string responseString = t.Result;
            ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserialized != null)
            {
                if (deserialized.success)
                {
                    TwoFactorActiveValidation twoFactorActiveValidation = new TwoFactorActiveValidation();
                    twoFactorActiveValidation.username = this.username;
                    twoFactorActiveValidation.token = this.token;
                    twoFactorActiveValidation.type = this.userInfo.TwoFactor ? 2 : 1; //2 = hủy, 1 = kích
                    twoFactorActiveValidation.ActiveSuccess += TwoFactorChanged;
                    twoFactorActiveValidation.ShowDialog();
                }
                else
                {
                    MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Log.AddLog("api/active2fa: " + responseString);
                MessageBox.Show("Hệ thống gặp lỗi, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ! (377)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnActiveNow.Enabled = true;
        }
        
        private void TwoFactorChanged(object sender, EventArgs e)
        {
            this.userInfo.TwoFactor = !this.userInfo.TwoFactor;
            onChangeStatus();
            OnActiveChanged(null);
        }

        
        public delegate void TwoFactorSwitchEventHandler(object sender, EventArgs e, bool isActive);
        public event TwoFactorSwitchEventHandler ActiveChanged;
        protected virtual void OnActiveChanged(EventArgs e)
        {
            TwoFactorSwitchEventHandler eh = ActiveChanged;
            if (eh != null)
            {
                eh(this, e, this.userInfo.TwoFactor);
            }
        }
    }
}