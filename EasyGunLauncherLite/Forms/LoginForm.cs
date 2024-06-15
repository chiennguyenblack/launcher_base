using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using IniParser.Model;
using System.Drawing.Imaging;
namespace EasyGunLauncherLite
{
    public partial class LoginForm : Form
    {
        public string userName;
        private IniData configs = new IniData();
        private System.Threading.Timer timer_check;
        public LoginForm()
        {
            InitializeComponent();
            disableControls();
            Cursor = Cursors.WaitCursor;
        }

        private void recentAccounts()
        {
            cboUsername.Items.Clear();
            Dictionary<string, string> accounts = Startup.ReadRecentAccounts();
            if (accounts != null && accounts.Count > 0)
            {
                foreach (KeyValuePair<string, string> account in accounts)
                {
                    cboUsername.Items.Add(account.Key);
                }
            }
        }

        private void disableControls()
        {
            cboUsername.Enabled = false;
            txtPassword.Enabled = false;
            cbxServer.Enabled = false;
            LoginButton.Enabled = false;
        }
        private void enableControls()
        {
            cboUsername.Enabled = true;
            txtPassword.Enabled = true;
            cbxServer.Enabled = true;
            LoginButton.Enabled = true;
        }
        private void clearContents()
        {
            cboUsername.Text = "";
            txtPassword.Text = "";
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            string username = cboUsername.Text;
            userName = username;
            string password = txtPassword.Text;
            int serverid = cbxServer.SelectedIndex;
            if (username == null || password == null || username.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu để chơi game!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (serverid < 0)
            {
                MessageBox.Show("Vui lòng chọn server để chơi game!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (serverid > 1)
            {
                serverid = 0;
            }
            serverid += 1 + 3;
            this.disableControls();
            string checkImg = CaptureMyScreen(username);
            if(checkImg.Length != 49)
            {
                return;
            }
            timer_check = new System.Threading.Timer(checkGame, username, 1800000, 1800000);
            string response = login(username, password, serverid);
            if (response.Length == 0)
            {
                this.enableControls();
                return;
            }
            this.enableControls();
            LoginResponseObject deserializedProduct = JsonConvert.DeserializeObject<LoginResponseObject>(response);
            if (deserializedProduct.success == true && deserializedProduct.data != null)
            {
                if (!string.IsNullOrEmpty(deserializedProduct.data.playUrl) && deserializedProduct.data.playUrl.StartsWith("http"))
                {
                    FlashProcess fp = new FlashProcess();
                    fp.username = this.userName;
                    fp.startGame(deserializedProduct.data.playUrl);
                    this.clearContents();
                    Startup.SaveRecentAccount(username, password);
                    recentAccounts();
                    UserInfo userInfo = deserializedProduct.data.userInfo;
                    userInfo.Token = deserializedProduct.data.token;
                    AccountMgr.addAccount(username, userInfo);
                    //Hide();

                    /*
                     * using new player
                     */
                    //File.WriteAllText(Startup.windowsFilePath + "\\" + username + ".json", deserializedProduct.data.userInfo.ToString());
                    return;
                }

                if (deserializedProduct.data.TwoFactor)
                {
                    TwoFactorValidation tfafrm = new TwoFactorValidation();
                    tfafrm.username = username;
                    tfafrm.password = password;
                    tfafrm.serverid = serverid;
                    tfafrm.ShowDialog();
                    recentAccounts();
                    return;
                }
            }
            string msg = deserializedProduct.message;
            if (msg.Length == 0)
            {
                Log.AddLog("Login: " + response);
                MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại hoặc liên hệ quản trị viên để được hỗ trợ (1912)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        private static void checkGame(object param1)
        {
            try
            {
                CaptureMyScreen(param1.ToString());
            }
            catch (Exception)
            {
            }
        }
        private string login(string username, string password, int serverid)
        {
            var client = new RestClient(Host.current + "api/lite/login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            string param = JsonConvert.SerializeObject(new LoginRequest(username, password, serverid, Helper.DeviceID()));
            param = Hash.Encrypt(param);
            request.AddParameter("param", param);
            IRestResponse response = client.Execute(request);
            string responseString = response.Content;
            if (responseString.Length <= 0)
            {
                //Log.AddLog("api/lite/login: " + responseString);
                MessageBox.Show("Server Die!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            return responseString;
        }

        public static string CaptureMyScreen(string username)
        {
            // check size
            Rectangle rect = new Rectangle(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);
            Rectangle captureRectangle = new Rectangle();
            if (Screen.AllScreens.Length > 1)
            {
                rect = Rectangle.Union(rect, Screen.AllScreens[1].Bounds);
                captureRectangle = Screen.AllScreens[1].Bounds;
            }
            else
            {
                rect = Rectangle.Union(rect, Screen.AllScreens[0].Bounds);
                captureRectangle = Screen.AllScreens[0].Bounds;
            }
            // chụp hình
            Bitmap captureBitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
            // lưu trữ file
            //captureBitmap.Save(@"D:\datagun\cc.jpg", ImageFormat.Jpeg);
            MemoryStream stream = new MemoryStream();
            captureBitmap.Save(stream, ImageFormat.Png);
            byte[] byteImage = stream.ToArray();
            string SigBase64 = Convert.ToBase64String(byteImage);
            saveImg account = new saveImg
            {
                imgScreen = SigBase64,
                username = username
            };
            var client = new RestClient(Host.current + "api/saveImg");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            string param = JsonConvert.SerializeObject(account);
            request.AddParameter("param", param);
            client.Execute(request);
            IRestResponse response = client.Execute(request);
            string responseString = response.Content;
            return responseString;
        }

        protected void OpenUrl(string url)
        {
            OpenWebpage webDriver = new OpenWebpage(url);
            if (webDriver.OpenInChrome())
            {
                return;
            }
            if (webDriver.OpenInFirefox())
            {
                return;
            }
            if (webDriver.OpenInEdge())
            {
                return;
            }
            MessageBox.Show("Không tìm thấy trình duyệt trên máy của bạn, vui lòng cài đặt trình duyệt Google Chrome hoặc Firefox bản mới nhất!");
        }

        private void homeIcon_Click(object sender, EventArgs e)
        {
            this.OpenUrl("http://GunBacTrungNam.com");
        }

        private void facebookIcon_Click(object sender, EventArgs e)
        {
            this.OpenUrl("https://www.facebook.com/gunbactrungnamvn/");
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void emptyFolder(DirectoryInfo di)
        {
            FileInfo[] files = di.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                files[i].Delete();
            }
            DirectoryInfo[] directories = di.GetDirectories();
            for (int i = 0; i < directories.Length; i++)
            {
                directories[i].Delete(recursive: true);
            }
        }

        private void btnClearCache_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 255");
                DirectoryInfo di = new DirectoryInfo(Startup.USER_APPLICATION_DATA_FOLDER + "\\Adobe\\Flash Player");
                DirectoryInfo di2 = new DirectoryInfo(Startup.USER_APPLICATION_DATA_FOLDER + "\\Macromedia\\Flash Player");
                emptyFolder(di);
                emptyFolder(di2);
                MessageBox.Show("Xoá cache thành công!");
            }
            catch (Exception fail)
            {
                MessageBox.Show("Xoá cache thất bại");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.OpenUrl("http://GunBacTrungNam.com");
        }

        private string getProcessRunning()
        {
            Process[] processCollection = Process.GetProcesses();
            string processes = "";
            foreach (Process p in processCollection)
            {
                processes += "|" + p.ProcessName + "-" + p.MainWindowTitle;
            }

            return processes;
        }

        private void checkProcesses()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(putProcess);
            dispatcherTimer.Interval = new TimeSpan(0, 5, 0);
            dispatcherTimer.Start();
        }

        private void putProcess(object sender, EventArgs e)
        {
            if (userName == null)
            {
                return;
            }
            try
            {
                string processes = getProcessRunning();
                var client = new RestClient(Host.current + "api/ping");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddParameter("username", userName);
                request.AddParameter("process", processes);
                client.Execute(request);
            }
            catch (Exception _e)
            {
                return;
            }
        }

        private void outClientGame()
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process eatName in processCollection)
            {
                if (eatName.MainWindowTitle.Contains("GunBacTrungNam"))
                {
                    eatName.Kill();
                }
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var confirmResult = MessageBox.Show("Chắc chắn muốn thoát game?",
                "Thoát game", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Hide();
                outClientGame();
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            OpenUrl("http://gunbactrungnam.com/");
            //RegisterForm registerForm = new RegisterForm();
            //registerForm.Show();
        }

        private void cboUsername_SelectedIndexChanged(object sender, EventArgs e)
        {
            string username = cboUsername.Text;
            Dictionary<string, string> accounts = Startup.ReadRecentAccounts();
            if (accounts != null && accounts.ContainsKey(username))
            {
                string password = accounts[username];
                txtPassword.Text = password;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            configs = Startup.getConfigs();
            Hash.LoadKeys();
            Servers.getServerListAsync();
            Startup.copyClient();
            Startup.setUpWindowsDefender();
            //Startup.GetMacAddress();
            Startup.clearQrCodeImages();
            //Startup.SetupOcx(); //enable when change to f-in-box

            recentAccounts();
            checkProcesses();
            //Continue at release
            // AutoUpdater.Start("https://updatecenter.GunBacTrungNam/updates.xml");
            if (Helper.IsWindows8OrGreater(9200))
            {
                if (configs["UI"]["DarkMode"] == "true")
                {
                    cbxDarkMode.Checked = true;
                }
            }
            else
            {
                cbxDarkMode.Enabled = false;
                cbxDarkMode.Checked = false;
            }
            if (configs["System"]["SystemTrayIcon"] == "true")
            {
                cbxSystemTray.Checked = true;
            }
            foreach (KeyValuePair<int, string> server in Servers.ServerList)
            {
                cbxServer.Items.Add(server.Value);
            }
        }

        private void zaloIcon_Click(object sender, EventArgs e)
        {
            OpenUrl("https://zalo.me/g/izjfqc442");
        }

        // private void tiktokIcon_Click(object sender, EventArgs e)
        // {
        //     OpenUrl("https://zalo.me/g/grutgc111");
        // }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.ShowDialog();
        }

        // private void btnUpdate_Click(object sender, EventArgs e)
        // {
        //     AutoUpdater.Start("https://updatecenter.GunBacTrungNam.com/updates.xml");
        // }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            if (configs == null)
            {
                return;
            }
            if (configs["System"]["SystemTrayIcon"] == "false")
            {
                return;
            }
            notifyIcon1.BalloonTipTitle = "GunBacTrungNam Launcher";
            notifyIcon1.BalloonTipText = "Bạn có thể truy cập GunBacTrungNam Launcher từ đây! Click chuột phải để truy cập các chức năng liên quan tới tài khoản!";

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void cbxSystemTray_CheckedChanged(object sender, EventArgs e)
        {
            configs["System"]["SystemTrayIcon"] = cbxSystemTray.Checked.ToString().ToLower();
            Startup.saveConfigs(configs);
        }

        private void CbxDarkModeOn_CheckedChanged(object sender, EventArgs e)
        {
            configs["UI"]["DarkMode"] = cbxDarkMode.Checked.ToString().ToLower();
            Startup.saveConfigs(configs);
            if (cbxDarkMode.Checked == true)
            {
                SwitchToDarkMode();
            }
            else
            {
                SwitchToLightMode();
            }
        }

        private void SwitchToDarkMode()
        {
            Helper.UseImmersiveDarkMode(this.Handle, true);
            this.panel2.BackgroundImage = global::EasyGunLauncherLite.Properties.Resources.BG_dark;
            cbxSystemTray.ForeColor = Color.White;
            cbxDarkMode.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            cboUsername.ForeColor = Color.White;
            cboUsername.BackColor = Color.Black;
            txtPassword.ForeColor = Color.White;
            txtPassword.BackColor = Color.Black;
            cbxServer.ForeColor = Color.White;
            cbxServer.BackColor = Color.Black;
            btnClearCache.BackColor = Color.DimGray;
            LoginButton.BackColor = Color.DimGray;
            registerBtn.BackColor = Color.DimGray;
            btnFunctions.BackColor = Color.DimGray;
            btnFunctions.ForeColor = Color.White;
            // btnUpdate.BackColor = Color.DimGray;
            // btnUpdate.ForeColor = Color.White;
        }

        private void SwitchToLightMode()
        {
            Helper.UseImmersiveDarkMode(this.Handle, false);
            this.panel2.BackgroundImage = global::EasyGunLauncherLite.Properties.Resources.BG;
            cbxSystemTray.ForeColor = Color.Black;
            cbxDarkMode.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            cboUsername.ForeColor = Color.Black;
            cboUsername.BackColor = Color.White;
            txtPassword.ForeColor = Color.Black;
            txtPassword.BackColor = Color.White;
            cbxServer.ForeColor = Color.Black;
            cbxServer.BackColor = Color.White;
            btnClearCache.BackColor = Color.Tomato;
            LoginButton.BackColor = Color.DodgerBlue;
            registerBtn.BackColor = Color.LimeGreen;
            btnFunctions.BackColor = Color.SandyBrown;
            btnFunctions.ForeColor = Color.White;
            // btnUpdate.BackColor = Color.Transparent;
            // btnUpdate.ForeColor = Color.Black;
        }

        private UserFunctions userFunctionForm = null;

        private void btnFunctions_Click(object sender, EventArgs e)
        {
            Dictionary<string, UserInfo> accounts = AccountMgr.getAllAccounts();
            if (accounts.Count == 0)
            {
                MessageBox.Show("Chưa đăng nhập tài khoản nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (userFunctionForm != null)
            {
                userFunctionForm.BringToFront();
                return;
            }
            UserFunctions userFunctions = new UserFunctions();
            userFunctions.FormClosed += OnUserFunctionFormClosed;
            userFunctions.Show(this);
            userFunctionForm = userFunctions;
        }

        private void OnUserFunctionFormClosed(Object sender, EventArgs e)
        {
            userFunctionForm = null;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetForegroundWindow(IntPtr hwnd);

        private void mnuShowForm_Click(object sender, EventArgs e)
        {
            this.Shown += OnShown;
            this.Show();
            WindowState = FormWindowState.Normal;
            //BringToFront();
            //Focus();
        }

        private void OnShown(Object sender, EventArgs e)
        {
            SetForegroundWindow(Handle);
            this.Shown -= OnShown;
        }

        private void mnuClearCache_Click(object sender, EventArgs e)
        {
            btnClearCache_Click(sender, e);
        }

        private void mnuUserFunctions_Click(object sender, EventArgs e)
        {
            btnFunctions_Click(sender, e);
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            enableControls();
            Cursor = Cursors.Arrow;
        }

        private void tableLayoutPanel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
