using System.ComponentModel;

namespace EasyGunLauncherLite.Forms.Menu
{
    partial class TwoFactorSwitch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TwoFactorSwitch));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxEmailActiveStatus = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnActiveNow = new System.Windows.Forms.Button();
            this.cbxStatus = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.cbxEmailActiveStatus);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnActiveNow);
            this.panel1.Controls.Add(this.cbxStatus);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 383);
            this.panel1.TabIndex = 0;
            // 
            // cbxEmailActiveStatus
            // 
            this.cbxEmailActiveStatus.AutoCheck = false;
            this.cbxEmailActiveStatus.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEmailActiveStatus.ForeColor = System.Drawing.Color.Red;
            this.cbxEmailActiveStatus.Location = new System.Drawing.Point(318, 209);
            this.cbxEmailActiveStatus.Name = "cbxEmailActiveStatus";
            this.cbxEmailActiveStatus.Size = new System.Drawing.Size(166, 29);
            this.cbxEmailActiveStatus.TabIndex = 9;
            this.cbxEmailActiveStatus.Text = "Chưa kích hoạt";
            this.cbxEmailActiveStatus.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(259, 29);
            this.label5.TabIndex = 8;
            this.label5.Text = "Trạng thái kích hoạt Email";
            // 
            // btnActiveNow
            // 
            this.btnActiveNow.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActiveNow.Location = new System.Drawing.Point(140, 323);
            this.btnActiveNow.Name = "btnActiveNow";
            this.btnActiveNow.Size = new System.Drawing.Size(316, 38);
            this.btnActiveNow.TabIndex = 7;
            this.btnActiveNow.Text = "Kích hoạt xác thực 2 lớp ngay";
            this.btnActiveNow.UseVisualStyleBackColor = true;
            this.btnActiveNow.Click += new System.EventHandler(this.btnActiveNow_Click);
            // 
            // cbxStatus
            // 
            this.cbxStatus.AutoCheck = false;
            this.cbxStatus.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxStatus.ForeColor = System.Drawing.Color.Red;
            this.cbxStatus.Location = new System.Drawing.Point(318, 262);
            this.cbxStatus.Name = "cbxStatus";
            this.cbxStatus.Size = new System.Drawing.Size(166, 29);
            this.cbxStatus.TabIndex = 6;
            this.cbxStatus.Text = "Chưa kích hoạt";
            this.cbxStatus.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(276, 56);
            this.label4.TabIndex = 5;
            this.label4.Text = "Trạng thái kích hoạt xác thực 2 lớp";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.LightCyan;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label3.ImageIndex = 0;
            this.label3.ImageList = this.imageList1;
            this.label3.Location = new System.Drawing.Point(10, 137);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(561, 55);
            this.label3.TabIndex = 4;
            this.label3.Text = "     Để sử dụng tính năng này, người dùng cần xác thực địa chỉ Email trước";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "pngwing.com (3).png");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.LightCyan;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.ImageIndex = 0;
            this.label1.ImageList = this.imageList1;
            this.label1.Location = new System.Drawing.Point(10, 73);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(561, 55);
            this.label1.TabIndex = 3;
            this.label1.Text = "     Khi xác thực 2 lớp được bật, mỗi lần đăng nhập, đổi mật khẩu hoặc chuyển xu " + "đều cần xác thực 2 lớp";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.LightCyan;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label2.ImageIndex = 0;
            this.label2.ImageList = this.imageList1;
            this.label2.Location = new System.Drawing.Point(10, 9);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(561, 55);
            this.label2.TabIndex = 2;
            this.label2.Text = "     Xác thực 2 lớp là chức năng nâng cao bảo mật cho tài khoản, tránh những phiê" + "n đăng nhập không mong muốn";
            // 
            // TwoFactorSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 383);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TwoFactorSwitch";
            this.Text = "TwoFacterSwitch";
            this.Load += new System.EventHandler(this.TwoFactorSwitch_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.CheckBox cbxEmailActiveStatus;
        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.Button btnActiveNow;

        private System.Windows.Forms.CheckBox cbxStatus;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList1;

        private System.Windows.Forms.Panel panel1;

        #endregion
    }
}