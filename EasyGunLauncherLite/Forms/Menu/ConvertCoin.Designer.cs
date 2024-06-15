namespace EasyGunLauncherLite
{
    partial class ConvertCoin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnConvertCoin = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboCharacterList = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblXu = new System.Windows.Forms.Label();
            this.txtCoin = new System.Windows.Forms.NumericUpDown();
            this.cboServerList = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblExchangeRate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpdateCoinAmount = new System.Windows.Forms.Button();
            this.lblCoinAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLoading = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCoin)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 321);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnConvertCoin);
            this.panel5.Location = new System.Drawing.Point(117, 262);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(348, 49);
            this.panel5.TabIndex = 4;
            // 
            // btnConvertCoin
            // 
            this.btnConvertCoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvertCoin.Location = new System.Drawing.Point(3, 3);
            this.btnConvertCoin.Name = "btnConvertCoin";
            this.btnConvertCoin.Size = new System.Drawing.Size(342, 42);
            this.btnConvertCoin.TabIndex = 4;
            this.btnConvertCoin.Text = "Chuyển xu";
            this.btnConvertCoin.UseVisualStyleBackColor = true;
            this.btnConvertCoin.Click += new System.EventHandler(this.btnConvertCoin_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.6338F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.3662F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cboCharacterList, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cboServerList, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 72);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(568, 167);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "Số coin chuyển";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Chọn máy chủ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Chọn nhân vật";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCharacterList
            // 
            this.cboCharacterList.AllowDrop = true;
            this.cboCharacterList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboCharacterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCharacterList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCharacterList.FormattingEnabled = true;
            this.cboCharacterList.Location = new System.Drawing.Point(176, 86);
            this.cboCharacterList.Name = "cboCharacterList";
            this.cboCharacterList.Size = new System.Drawing.Size(358, 32);
            this.cboCharacterList.TabIndex = 2;
            this.cboCharacterList.SelectedIndexChanged += new System.EventHandler(this.cboCharacterList_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblXu);
            this.panel4.Controls.Add(this.txtCoin);
            this.panel4.Location = new System.Drawing.Point(176, 126);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(386, 35);
            this.panel4.TabIndex = 6;
            // 
            // lblXu
            // 
            this.lblXu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXu.Image = global::EasyGunLauncherLite.Properties.Resources.xu;
            this.lblXu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblXu.Location = new System.Drawing.Point(191, 4);
            this.lblXu.Name = "lblXu";
            this.lblXu.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblXu.Size = new System.Drawing.Size(195, 33);
            this.lblXu.TabIndex = 1;
            this.lblXu.Text = "      0 xu";
            this.lblXu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCoin
            // 
            this.txtCoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoin.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtCoin.Location = new System.Drawing.Point(0, 2);
            this.txtCoin.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.txtCoin.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtCoin.Name = "txtCoin";
            this.txtCoin.Size = new System.Drawing.Size(185, 29);
            this.txtCoin.TabIndex = 3;
            this.txtCoin.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtCoin.ValueChanged += new System.EventHandler(this.txtCoin_ValueChanged);
            this.txtCoin.EnabledChanged += new System.EventHandler(this.txtCoin_EnabledChanged);
            this.txtCoin.Click += new System.EventHandler(this.txtCoin_Click);
            this.txtCoin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCoin_KeyPress);
            this.txtCoin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCoin_KeyUp);
            // 
            // cboServerList
            // 
            this.cboServerList.AllowDrop = true;
            this.cboServerList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServerList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboServerList.FormattingEnabled = true;
            this.cboServerList.Location = new System.Drawing.Point(176, 4);
            this.cboServerList.Name = "cboServerList";
            this.cboServerList.Size = new System.Drawing.Size(358, 32);
            this.cboServerList.TabIndex = 1;
            this.cboServerList.SelectedIndexChanged += new System.EventHandler(this.cboServerList_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightCyan;
            this.panel3.Controls.Add(this.lblExchangeRate);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(176, 44);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(358, 35);
            this.panel3.TabIndex = 7;
            // 
            // lblExchangeRate
            // 
            this.lblExchangeRate.AutoSize = true;
            this.lblExchangeRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExchangeRate.ForeColor = System.Drawing.Color.Red;
            this.lblExchangeRate.Location = new System.Drawing.Point(243, 6);
            this.lblExchangeRate.Name = "lblExchangeRate";
            this.lblExchangeRate.Size = new System.Drawing.Size(46, 24);
            this.lblExchangeRate.TabIndex = 1;
            this.lblExchangeRate.Text = "0 xu";
            //this.lblExchangeRate.Click += new System.EventHandler(this.lblExchangeRate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(231, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tỉ lệ quy đổi: 1.000 coin = ";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightCyan;
            this.panel2.Controls.Add(this.btnUpdateCoinAmount);
            this.panel2.Controls.Add(this.lblCoinAmount);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(571, 37);
            this.panel2.TabIndex = 0;
            // 
            // btnUpdateCoinAmount
            // 
            this.btnUpdateCoinAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateCoinAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCoinAmount.Image = global::EasyGunLauncherLite.Properties.Resources.pngwing_com;
            this.btnUpdateCoinAmount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateCoinAmount.Location = new System.Drawing.Point(449, 3);
            this.btnUpdateCoinAmount.Name = "btnUpdateCoinAmount";
            this.btnUpdateCoinAmount.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnUpdateCoinAmount.Size = new System.Drawing.Size(119, 31);
            this.btnUpdateCoinAmount.TabIndex = 2;
            this.btnUpdateCoinAmount.Text = "   Cập nhật";
            this.btnUpdateCoinAmount.UseVisualStyleBackColor = true;
            this.btnUpdateCoinAmount.Click += new System.EventHandler(this.btnUpdateCoinAmount_Click);
            // 
            // lblCoinAmount
            // 
            this.lblCoinAmount.AutoSize = true;
            this.lblCoinAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoinAmount.ForeColor = System.Drawing.Color.Red;
            this.lblCoinAmount.Location = new System.Drawing.Point(90, 6);
            this.lblCoinAmount.Name = "lblCoinAmount";
            this.lblCoinAmount.Size = new System.Drawing.Size(46, 24);
            this.lblCoinAmount.TabIndex = 1;
            this.lblCoinAmount.Text = "coin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hiện có:";
            // 
            // lblLoading
            // 
            this.lblLoading.BackColor = System.Drawing.Color.White;
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.Location = new System.Drawing.Point(0, 0);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(577, 321);
            this.lblLoading.TabIndex = 5;
            this.lblLoading.Text = "Đang tải, xin chờ ...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConvertCoin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 321);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ConvertCoin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConvertCoin";
            this.Load += new System.EventHandler(this.ConvertCoin_Load);
            this.Shown += new System.EventHandler(this.ConvertCoin_Shown);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCoin)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnConvertCoin;

        private System.Windows.Forms.Label lblXu;

        private System.Windows.Forms.Panel panel4;

        private System.Windows.Forms.NumericUpDown txtCoin;

        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.ComboBox cboCharacterList;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.ComboBox cboServerList;

        private System.Windows.Forms.Label label2;

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCoinAmount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnUpdateCoinAmount;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblExchangeRate;
        private System.Windows.Forms.Label label3;
    }
}