using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EasyGunLauncherLite.Forms.Menu
{
    public partial class ChargeHistory : Form
    {
        public string username;
        public string token;
        
        public ChargeHistory()
        {
            InitializeComponent();
        }

        private void ChargeHistory_Load(object sender, EventArgs e)
        {
            this.Text = username + ": Lịch sử nạp & chuyển xu";
            lblLoading.Visible = true;
            lblLoading.BringToFront();
        }
        
        private void getChargeHistoryAsync(Action callback = null)
        {
            Task<string> t = AsyncRequest.createRequestAsync("api/chargeHistory", this.token);
            string responseString = t.Result;
            ChargeHistoryResponseObject deserialized = JsonConvert.DeserializeObject<ChargeHistoryResponseObject>(responseString);
            dataGridView1.Rows.Clear();
            if (deserialized.success && deserialized.data != null)
            {
                if (deserialized.data.Count > 0)
                {
                    for (int i = 0; i < deserialized.data.Count; i++)
                    {
                        EasyGunLauncherLite.ChargeHistory chargeHistory = deserialized.data[i];
                        string[] row =
                        {
                            (i + 1).ToString(),
                            chargeHistory.TimeCreate,
                            chargeHistory.Value.ToString(),
                            chargeHistory.Content
                        };
                        dataGridView1.Rows.Add(row);
                    }
                }
            }
            else
            {
                Log.AddLog("api/chargeHistory: " + responseString);
            }
            button1.Enabled = true;
            if (callback != null)
            {
                callback();
            }
        }
        
        private void showMainPanel()
        {
            if (lblLoading.Visible)
            {
                this.lblLoading.Visible = false;
            }
        }

        private void ChargeHistory_Shown(object sender, EventArgs e)
        {
            getChargeHistoryAsync(showMainPanel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            getChargeHistoryAsync();
        }
    }
}