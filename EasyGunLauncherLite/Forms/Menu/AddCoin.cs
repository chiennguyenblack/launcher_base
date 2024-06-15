using Newtonsoft.Json;
using RestSharp;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp.Authenticators;
using IniParser.Model;

namespace EasyGunLauncherLite.Menu
{
    public partial class AddCoin : Form
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

        public AddCoin()
        {
            InitializeComponent();
            LoadingElements = new bool[3]
            {
                false,
                false,
                false
            };
            darkMode();
        }

        private void AddCoin_Load(object sender, System.EventArgs e)
        {
            this.Text = username + ": Nạp coin";
            this.tblMomo_PaymentInfo.CellPaint += tableLayoutPanel_CellPaint;
            this.tblACB_PaymentInfo.CellPaint += tableLayoutPanel_CellPaint;
            lblLoading.BringToFront();
            lblLoading.Visible = true;
        }

        private void darkMode()
        {
            IniData configs = Startup.getConfigs();
            if (configs["UI"]["DarkMode"] == "true")
            {
                Helper.UseImmersiveDarkMode(this.Handle, true);
                BackColor = Color.Black;
                mainPanel.BackColor = Color.Black;
                lblLoading.ForeColor = Color.White;
                lblLoading.BackColor = Color.Black;
                tabControl1.myBackColor = Color.Black;
                tabControl1.myBorderColor = Color.White;
                //tabPage1.BackColor = Color.Black;
                //tabPage1.ForeColor = Color.White;
                tabPage2.BackColor = Color.Black;
                tabPage2.ForeColor = Color.White;
                panel2.ForeColor = Color.White;
                panel6.ForeColor = Color.White;
                panel4.BackColor = Color.DimGray;
                panel4.ForeColor = Color.White;
            }
        }

        private void tableLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            var bottomLeft = new Point(e.CellBounds.Left, e.CellBounds.Top + e.CellBounds.Height);
            var bottomRight = new Point(e.CellBounds.Right, e.CellBounds.Bottom);

            IniData configs = Startup.getConfigs();
            if (configs["UI"]["DarkMode"] == "true")
            {
                e.Graphics.DrawLine(Pens.White, bottomLeft, bottomRight);
            }
            else
            {
                e.Graphics.DrawLine(Pens.LightGray, bottomLeft, bottomRight);
            }
        }

        private string retriveBase64FromWebBasedSrc(string src)
        {
            string str1 = "data:image/jpg;base64,";
            string str2 = "data:image/png;base64,";
            string str = src.Replace(str1, "");
            str = str.Replace(str2, "");
            return str;
        }

        private string LoadImageFromBase64(string base64)
        {
            if (string.IsNullOrEmpty(base64))
            {
                return "";
            }
            byte[] bytes = Convert.FromBase64String(retriveBase64FromWebBasedSrc(base64));

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            image = new Bitmap(image, new Size(459, 459));
            string path = Startup.windowsFilePath + "\\" + Helper.RandomString(10) + ".png";
            image.Save(path);
            return path;
        }
        
        private void getPaymentRateInfoAsync(Action callback = null)
        {
            Task<string> t = AsyncRequest.createRequestAsync("api/chargerateinfo", this.token);
            string responseString = t.Result;
            LoadingElements[0] = true;
            ResponseObject deserialized = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserialized.success == true && deserialized.data != null)
            {
                lblMomo_TiLeChuyenDoi.Text = deserialized.data.rateMomo + " coin";
                lblACB_TiLeChuyenDoi.Text = deserialized.data.rateATM + " coin";
            }
            else
            {
                Log.AddLog("api/chargerateinfo: " + responseString);
                lblMomo_TiLeChuyenDoi.Text = "Lỗi";
                lblACB_TiLeChuyenDoi.Text = "Lỗi";
            }

            if (callback != null)
            {
                callback();
            }
        }
        
        private void getPaymentMomoInfoAsync(Action callback = null)
        {
            Task<string> t = AsyncRequest.createRequestAsync("api/getMomoChargeQr", this.token);
            string responseString = t.Result;
            LoadingElements[1] = true;
            ResponseObject deserializedMomo = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserializedMomo.success == true && deserializedMomo.data != null)
            {
                pnlMomoQR.BackgroundImage = Image.FromFile(LoadImageFromBase64(deserializedMomo.data.src));
                lblMomo_ChuTaiKhoan.Text = deserializedMomo.data.accinfo.name;
                lblMomo_SoTaiKhoan.Text = deserializedMomo.data.accinfo.acc_num;
                lblMomo_NoiDungChuyen.Text = deserializedMomo.data.comment;
            }
            else
            {
                Log.AddLog("api/getMomoChargeQr: " + responseString);
                pnlMomoQR.BackgroundImage = null;
                lblMomo_ChuTaiKhoan.Text = "Lỗi kết nối";
                lblMomo_SoTaiKhoan.Text = "Lỗi kết nối";
                lblMomo_NoiDungChuyen.Text = "Lỗi kết nối";
            }
            
            if (callback != null)
            {
                callback();
            }
        }
        
        private void getPaymentBankInfoAsync(Action callback = null)
        {
            Task<string> t = AsyncRequest.createRequestAsync("api/getBankQrCode", this.token);
            string responseString = t.Result;
            LoadingElements[2] = true;
            ResponseObject deserializedBank = JsonConvert.DeserializeObject<ResponseObject>(responseString);
            if (deserializedBank.success == true && deserializedBank.data != null)
            {
                string path = LoadImageFromBase64(deserializedBank.data.src);
                if (!string.IsNullOrEmpty(path))
                {
                    pnlACBQr.BackgroundImage = Image.FromFile(path);
                }
                lblACB_ChuTaiKhoan.Text = deserializedBank.data.accinfo.name;
                lblACB_SoTaiKhoan.Text = deserializedBank.data.accinfo.acc_num;
                lblACB_NoiDungChuyen.Text = deserializedBank.data.comment;
            }
            else
            {
                Log.AddLog("api/getBankQrCode: " + responseString);
                pnlACBQr.BackgroundImage = null;
                lblACB_ChuTaiKhoan.Text = "Lỗi kết nối";
                lblACB_SoTaiKhoan.Text = "Lỗi kết nối";
                lblACB_NoiDungChuyen.Text = "Lỗi kết nối";
            }
            
            if (callback != null)
            {
                callback();
            }
        }

        private void showMainPanel()
        {
            if (isAllElementLoaded && lblLoading.Visible)
            {
                this.lblLoading.Visible = false;
            }
        }

        private void AddCoin_Shown(object sender, EventArgs e)
        {
            getPaymentRateInfoAsync(showMainPanel);
            getPaymentMomoInfoAsync(showMainPanel);
            getPaymentBankInfoAsync(showMainPanel);
        }
    }
}