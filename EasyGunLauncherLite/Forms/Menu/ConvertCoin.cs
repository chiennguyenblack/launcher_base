using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyGunLauncherLite
{
    public partial class ConvertCoin : Form
    {
        public string username;
        public string token;

        private bool[] LoadingElements = null;

        private bool isAllElementLoaded
        {
            get
            {
                foreach (bool element in LoadingElements)
                {
                    if (!element)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private float rate;

        private Dictionary<int, string> playerList = new Dictionary<int, string>();

        public ConvertCoin()
        {
            InitializeComponent();
            LoadingElements = new bool[1]
            {
                false
            };
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ConvertCoin_Load(object sender, EventArgs e)
        {
            Text = username + ": Chuyển xu vào game";
            foreach (KeyValuePair<int, string> item in Servers.ServerList)
            {
                cboServerList.Items.Add(item.Value);
            }
            cboCharacterList.Enabled = false;
            txtCoin.Enabled = false;
            lblLoading.Visible = true;
            lblLoading.BringToFront();
        }

        private void showMainPanel()
        {
            if (isAllElementLoaded && lblLoading.Visible)
            {
                this.lblLoading.Visible = false;
            }
        }

        private void ConvertCoin_Shown(object sender, EventArgs e)
        {
            getCoinInfoAsync(showMainPanel);
        }

        private void getConvertRateInfoAsync(Action callback = null)
        {
            KeyValuePair<int, string> server = Servers.ServerList.ElementAt(cboServerList.SelectedIndex);
            dynamic data = new ExpandoObject();
            data.serverid = server.Key;
            Task<string> t = AsyncRequest.createRequestAsync("api/lite/convertrateinfo", this.token, JsonConvert.SerializeObject(data));
            string responseString = t.Result;
            //LoadingElements[1] = true;
            ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserialized.success == true && deserialized.data != null)
            {
                lblExchangeRate.Text = (deserialized.data.rate * 1000).ToString("N0", new CultureInfo("vi-VN")) + " xu";
                rate = deserialized.data.rate;
            }
            else
            {
                Log.AddLog("api/lite/convertrateinfo: " + responseString);
                lblExchangeRate.Text = "Lỗi";
            }

            if (callback != null)
            {
                callback();
            }
        }

        private void getCoinInfoAsync(Action callback = null)
        {
            Task<string> t = AsyncRequest.createRequestAsync("api/coininfo", this.token);
            string responseString = t.Result;
            LoadingElements[0] = true;
            ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserialized.success == true && deserialized.data != null)
            {
                lblCoinAmount.Text = (deserialized.data.coin).ToString("N0", new CultureInfo("vi-VN")) + " coin";
            }
            else
            {
                Log.AddLog("api/coininfo: " + responseString);
                lblCoinAmount.Text = "Lỗi";
            }
            btnUpdateCoinAmount.Enabled = true;

            if (callback != null)
            {
                callback();
            }
        }

        private void btnUpdateCoinAmount_Click(object sender, EventArgs e)
        {
            btnUpdateCoinAmount.Enabled = false;
            getCoinInfoAsync();
        }

        private void cboServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboServerList.SelectedIndex >= 0)
            {
                getConvertRateInfoAsync();
                cboCharacterList.Enabled = true;
                KeyValuePair<int, string> server = Servers.ServerList.ElementAt(cboServerList.SelectedIndex);
                dynamic data = new ExpandoObject();
                data.server_id = server.Key;
                Task<string> t = AsyncRequest.createRequestAsync("api/lite/playerlist", this.token, JsonConvert.SerializeObject(data));
                string responseString = t.Result;
                PlayerListResponseObject deserialized = JsonConvert.DeserializeObject<PlayerListResponseObject>(responseString);
                playerList.Clear();
                cboCharacterList.Items.Clear();
                if (deserialized != null)
                {
                    if (deserialized.success && deserialized.data != null)
                    {
                        foreach (Player player in deserialized.data)
                        {
                            cboCharacterList.Items.Add(player.NickName);
                            playerList.Add(player.UserID, player.NickName);
                        }
                    } else
                    {
                        MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Log.AddLog("api/lite/playerlist: " + responseString);
                    MessageBox.Show("Hệ thống gặp lỗi, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ! (311)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            } else
            {
                cboCharacterList.Items.Clear();
                cboCharacterList.Enabled = false;
                txtCoin.ResetText();
                txtCoin.Enabled = false;
            }
        }

        private void cboCharacterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCharacterList.SelectedIndex >= 0)
            {
                txtCoin.Enabled = true;
            }
            else
            {
                txtCoin.Enabled = false;
            }
        }

        private void txtCoin_ValueChanged(object sender, EventArgs e)
        {
            if (txtCoin.Value != 0)
            {
                if (txtCoin.Value > 10000000)
                {
                    MessageBox.Show("Vui lòng nhập giá trị từ 100 -> 10.000.000", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnConvertCoin.Enabled = false;
                    return;
                }
                lblXu.Text = "      " + ((float)txtCoin.Value * rate).ToString("N0", new CultureInfo("vi-VN")) + " xu";
            }
            if (txtCoin.Value >= 100)
            {
                btnConvertCoin.Enabled = true;
            }
            else
            {
                btnConvertCoin.Enabled = false;
            }
        }

        private void txtCoin_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtCoin_EnabledChanged(object sender, EventArgs e)
        {
            if (!txtCoin.Enabled)
            {
                btnConvertCoin.Enabled = false;
            }
            else
            {
                if (txtCoin.Value >= 100)
                {
                    btnConvertCoin.Enabled = true;
                } else
                {
                    btnConvertCoin.Enabled = false;
                }
            }
        }

        private void btnConvertCoin_Click(object sender, EventArgs e)
        {
            cboCharacterList.Enabled = false;
            cboServerList.Enabled = false;
            txtCoin.Enabled = false;
            btnUpdateCoinAmount.Enabled = false;
            btnConvertCoin.Enabled = false;
            KeyValuePair<int, string> server = Servers.ServerList.ElementAt(cboServerList.SelectedIndex);
            KeyValuePair<int, string> player = playerList.ElementAt(cboCharacterList.SelectedIndex);
            dynamic data = new ExpandoObject();
            data.server_id = server.Key;
            data.player_id = player.Key;
            data.coin = txtCoin.Text;
            Task<string> t = AsyncRequest.createRequestAsync("api/lite/convertCoin", this.token, JsonConvert.SerializeObject(data));
            string responseString = t.Result;
            ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserialized != null)
            {
                if (deserialized.success)
                {
                    if (deserialized.data.TwoFactor)
                    {
                        ConvertCoinTwoFactorValidation tfafrm = new ConvertCoinTwoFactorValidation();
                        tfafrm.username = this.username;
                        tfafrm.token = this.token;
                        tfafrm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(deserialized.message, "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                Log.AddLog("api/lite/convertCoin: " + responseString);
                MessageBox.Show("Hệ thống gặp lỗi, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ! (312)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cboCharacterList.Enabled = true;
            cboServerList.Enabled = true;
            txtCoin.Enabled = true;
            btnUpdateCoinAmount.Enabled = true;
            btnConvertCoin.Enabled = true;
        }

        private void txtCoin_KeyUp(object sender, KeyEventArgs e)
        {
            txtCoin_ValueChanged(sender, e);
        }

        private void txtCoin_Click(object sender, EventArgs e)
        {
        }


    }
}
