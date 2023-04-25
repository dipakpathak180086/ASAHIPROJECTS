namespace AIS_GLASS_PC_APP
{
    partial class frmMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbReport = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblBarcodeGeneration = new System.Windows.Forms.Label();
            this.picBarcodeGeneration = new System.Windows.Forms.PictureBox();
            this.lblPartMaster = new System.Windows.Forms.Label();
            this.picPartMaster = new System.Windows.Forms.PictureBox();
            this.lblGroupMaster = new System.Windows.Forms.Label();
            this.picGroupMaster = new System.Windows.Forms.PictureBox();
            this.lblUserMaster = new System.Windows.Forms.Label();
            this.picUserMaster = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblReportPalletMapping = new System.Windows.Forms.Label();
            this.picReportPalletMapping = new System.Windows.Forms.PictureBox();
            this.lblReportLabelPrinting = new System.Windows.Forms.Label();
            this.picReportLabelPrinting = new System.Windows.Forms.PictureBox();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMini = new System.Windows.Forms.Button();
            this.picChangePassword = new System.Windows.Forms.PictureBox();
            this.picLogOut = new System.Windows.Forms.PictureBox();
            this.timerAutoLogOut = new System.Windows.Forms.Timer(this.components);
            this.timerReOiling = new System.Windows.Forms.Timer(this.components);
            this.lblReopenCompletedPallet = new System.Windows.Forms.Label();
            this.picReopenCompletedPallet = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.tbReport.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcodeGeneration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPartMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUserMaster)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReportPalletMapping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReportLabelPrinting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChangePassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReopenCompletedPallet)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tbReport);
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Location = new System.Drawing.Point(8, 56);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1073, 491);
            this.panel1.TabIndex = 8;
            // 
            // tbReport
            // 
            this.tbReport.Controls.Add(this.tabPage1);
            this.tbReport.Controls.Add(this.tabPage3);
            this.tbReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbReport.Location = new System.Drawing.Point(0, 0);
            this.tbReport.Name = "tbReport";
            this.tbReport.SelectedIndex = 0;
            this.tbReport.Size = new System.Drawing.Size(1071, 471);
            this.tbReport.TabIndex = 140;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblReopenCompletedPallet);
            this.tabPage1.Controls.Add(this.picReopenCompletedPallet);
            this.tabPage1.Controls.Add(this.lblBarcodeGeneration);
            this.tabPage1.Controls.Add(this.picBarcodeGeneration);
            this.tabPage1.Controls.Add(this.lblPartMaster);
            this.tabPage1.Controls.Add(this.picPartMaster);
            this.tabPage1.Controls.Add(this.lblGroupMaster);
            this.tabPage1.Controls.Add(this.picGroupMaster);
            this.tabPage1.Controls.Add(this.lblUserMaster);
            this.tabPage1.Controls.Add(this.picUserMaster);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1063, 439);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Master";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblBarcodeGeneration
            // 
            this.lblBarcodeGeneration.AutoSize = true;
            this.lblBarcodeGeneration.Enabled = false;
            this.lblBarcodeGeneration.Location = new System.Drawing.Point(387, 109);
            this.lblBarcodeGeneration.Name = "lblBarcodeGeneration";
            this.lblBarcodeGeneration.Size = new System.Drawing.Size(138, 19);
            this.lblBarcodeGeneration.TabIndex = 7;
            this.lblBarcodeGeneration.Text = "Barcode Generation";
            // 
            // picBarcodeGeneration
            // 
            this.picBarcodeGeneration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBarcodeGeneration.Enabled = false;
            this.picBarcodeGeneration.Image = ((System.Drawing.Image)(resources.GetObject("picBarcodeGeneration.Image")));
            this.picBarcodeGeneration.Location = new System.Drawing.Point(398, 6);
            this.picBarcodeGeneration.Name = "picBarcodeGeneration";
            this.picBarcodeGeneration.Size = new System.Drawing.Size(100, 100);
            this.picBarcodeGeneration.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBarcodeGeneration.TabIndex = 6;
            this.picBarcodeGeneration.TabStop = false;
            this.picBarcodeGeneration.Tag = "101";
            this.picBarcodeGeneration.Click += new System.EventHandler(this.picBarcodeGeneration_Click);
            // 
            // lblPartMaster
            // 
            this.lblPartMaster.AutoSize = true;
            this.lblPartMaster.Enabled = false;
            this.lblPartMaster.Location = new System.Drawing.Point(254, 109);
            this.lblPartMaster.Name = "lblPartMaster";
            this.lblPartMaster.Size = new System.Drawing.Size(85, 19);
            this.lblPartMaster.TabIndex = 5;
            this.lblPartMaster.Text = "Part Master";
            // 
            // picPartMaster
            // 
            this.picPartMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picPartMaster.Enabled = false;
            this.picPartMaster.Image = ((System.Drawing.Image)(resources.GetObject("picPartMaster.Image")));
            this.picPartMaster.Location = new System.Drawing.Point(249, 6);
            this.picPartMaster.Name = "picPartMaster";
            this.picPartMaster.Size = new System.Drawing.Size(100, 100);
            this.picPartMaster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPartMaster.TabIndex = 4;
            this.picPartMaster.TabStop = false;
            this.picPartMaster.Tag = "101";
            this.picPartMaster.Click += new System.EventHandler(this.picPartMaster_Click);
            // 
            // lblGroupMaster
            // 
            this.lblGroupMaster.AutoSize = true;
            this.lblGroupMaster.Enabled = false;
            this.lblGroupMaster.Location = new System.Drawing.Point(16, 109);
            this.lblGroupMaster.Name = "lblGroupMaster";
            this.lblGroupMaster.Size = new System.Drawing.Size(98, 19);
            this.lblGroupMaster.TabIndex = 3;
            this.lblGroupMaster.Text = "Group Master";
            // 
            // picGroupMaster
            // 
            this.picGroupMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picGroupMaster.Enabled = false;
            this.picGroupMaster.Image = ((System.Drawing.Image)(resources.GetObject("picGroupMaster.Image")));
            this.picGroupMaster.Location = new System.Drawing.Point(15, 6);
            this.picGroupMaster.Name = "picGroupMaster";
            this.picGroupMaster.Size = new System.Drawing.Size(100, 100);
            this.picGroupMaster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picGroupMaster.TabIndex = 2;
            this.picGroupMaster.TabStop = false;
            this.picGroupMaster.Tag = "101";
            this.picGroupMaster.Click += new System.EventHandler(this.picGroupMaster_Click);
            // 
            // lblUserMaster
            // 
            this.lblUserMaster.AutoSize = true;
            this.lblUserMaster.Enabled = false;
            this.lblUserMaster.Location = new System.Drawing.Point(135, 109);
            this.lblUserMaster.Name = "lblUserMaster";
            this.lblUserMaster.Size = new System.Drawing.Size(89, 19);
            this.lblUserMaster.TabIndex = 1;
            this.lblUserMaster.Text = "User Master";
            // 
            // picUserMaster
            // 
            this.picUserMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picUserMaster.Enabled = false;
            this.picUserMaster.Image = ((System.Drawing.Image)(resources.GetObject("picUserMaster.Image")));
            this.picUserMaster.Location = new System.Drawing.Point(132, 6);
            this.picUserMaster.Name = "picUserMaster";
            this.picUserMaster.Size = new System.Drawing.Size(100, 100);
            this.picUserMaster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUserMaster.TabIndex = 0;
            this.picUserMaster.TabStop = false;
            this.picUserMaster.Tag = "102";
            this.picUserMaster.Click += new System.EventHandler(this.picUserMaster_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblReportPalletMapping);
            this.tabPage3.Controls.Add(this.picReportPalletMapping);
            this.tabPage3.Controls.Add(this.lblReportLabelPrinting);
            this.tabPage3.Controls.Add(this.picReportLabelPrinting);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1063, 439);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Report";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblReportPalletMapping
            // 
            this.lblReportPalletMapping.AutoSize = true;
            this.lblReportPalletMapping.Enabled = false;
            this.lblReportPalletMapping.Location = new System.Drawing.Point(162, 92);
            this.lblReportPalletMapping.Name = "lblReportPalletMapping";
            this.lblReportPalletMapping.Size = new System.Drawing.Size(107, 19);
            this.lblReportPalletMapping.TabIndex = 40;
            this.lblReportPalletMapping.Text = "Pallet Mapping";
            // 
            // picReportPalletMapping
            // 
            this.picReportPalletMapping.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picReportPalletMapping.Enabled = false;
            this.picReportPalletMapping.Image = ((System.Drawing.Image)(resources.GetObject("picReportPalletMapping.Image")));
            this.picReportPalletMapping.Location = new System.Drawing.Point(176, 6);
            this.picReportPalletMapping.Name = "picReportPalletMapping";
            this.picReportPalletMapping.Size = new System.Drawing.Size(93, 79);
            this.picReportPalletMapping.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picReportPalletMapping.TabIndex = 39;
            this.picReportPalletMapping.TabStop = false;
            this.picReportPalletMapping.Tag = "101";
            this.picReportPalletMapping.Click += new System.EventHandler(this.picReportPalletMapping_Click);
            // 
            // lblReportLabelPrinting
            // 
            this.lblReportLabelPrinting.AutoSize = true;
            this.lblReportLabelPrinting.Enabled = false;
            this.lblReportLabelPrinting.Location = new System.Drawing.Point(2, 92);
            this.lblReportLabelPrinting.Name = "lblReportLabelPrinting";
            this.lblReportLabelPrinting.Size = new System.Drawing.Size(98, 19);
            this.lblReportLabelPrinting.TabIndex = 38;
            this.lblReportLabelPrinting.Text = "Label Printing";
            // 
            // picReportLabelPrinting
            // 
            this.picReportLabelPrinting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picReportLabelPrinting.Enabled = false;
            this.picReportLabelPrinting.Image = ((System.Drawing.Image)(resources.GetObject("picReportLabelPrinting.Image")));
            this.picReportLabelPrinting.Location = new System.Drawing.Point(16, 6);
            this.picReportLabelPrinting.Name = "picReportLabelPrinting";
            this.picReportLabelPrinting.Size = new System.Drawing.Size(93, 79);
            this.picReportLabelPrinting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picReportLabelPrinting.TabIndex = 37;
            this.picReportLabelPrinting.TabStop = false;
            this.picReportLabelPrinting.Tag = "101";
            this.picReportLabelPrinting.Click += new System.EventHandler(this.picReportLabelPrinting_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblWelcome.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.Purple;
            this.lblWelcome.Location = new System.Drawing.Point(0, 471);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(1071, 18);
            this.lblWelcome.TabIndex = 139;
            this.lblWelcome.Text = "test";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(194)))), ((int)(((byte)(191)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.AliceBlue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1089, 51);
            this.label1.TabIndex = 180;
            this.label1.Text = "MAIN MENU";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(861, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Change Password";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(962, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 182;
            this.label3.Text = "Minimize";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1048, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 183;
            this.label4.Text = "Stop";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 262;
            this.pictureBox1.TabStop = false;
            // 
            // btnMini
            // 
            this.btnMini.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMini.BackColor = System.Drawing.Color.Transparent;
            this.btnMini.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMini.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnMini.FlatAppearance.BorderSize = 0;
            this.btnMini.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnMini.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMini.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMini.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMini.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(197)))), ((int)(((byte)(0)))));
            this.btnMini.Image = ((System.Drawing.Image)(resources.GetObject("btnMini.Image")));
            this.btnMini.Location = new System.Drawing.Point(966, 7);
            this.btnMini.Name = "btnMini";
            this.btnMini.Size = new System.Drawing.Size(39, 33);
            this.btnMini.TabIndex = 181;
            this.btnMini.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMini.UseVisualStyleBackColor = false;
            this.btnMini.Click += new System.EventHandler(this.btnMini_Click);
            // 
            // picChangePassword
            // 
            this.picChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picChangePassword.Image = global::AIS_GLASS_PC_APP.Properties.Resources.iconfinder_change_password_63985;
            this.picChangePassword.Location = new System.Drawing.Point(887, 7);
            this.picChangePassword.Name = "picChangePassword";
            this.picChangePassword.Size = new System.Drawing.Size(39, 33);
            this.picChangePassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picChangePassword.TabIndex = 18;
            this.picChangePassword.TabStop = false;
            this.picChangePassword.Visible = false;
            this.picChangePassword.Click += new System.EventHandler(this.picChangePassword_Click);
            // 
            // picLogOut
            // 
            this.picLogOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogOut.Image = ((System.Drawing.Image)(resources.GetObject("picLogOut.Image")));
            this.picLogOut.Location = new System.Drawing.Point(1045, 7);
            this.picLogOut.Name = "picLogOut";
            this.picLogOut.Size = new System.Drawing.Size(39, 33);
            this.picLogOut.TabIndex = 16;
            this.picLogOut.TabStop = false;
            this.picLogOut.Click += new System.EventHandler(this.picLogOut_Click);
            // 
            // lblReopenCompletedPallet
            // 
            this.lblReopenCompletedPallet.AutoSize = true;
            this.lblReopenCompletedPallet.Enabled = false;
            this.lblReopenCompletedPallet.Location = new System.Drawing.Point(543, 109);
            this.lblReopenCompletedPallet.Name = "lblReopenCompletedPallet";
            this.lblReopenCompletedPallet.Size = new System.Drawing.Size(173, 19);
            this.lblReopenCompletedPallet.TabIndex = 9;
            this.lblReopenCompletedPallet.Text = "Reopen Completed Pallet";
            // 
            // picReopenCompletedPallet
            // 
            this.picReopenCompletedPallet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picReopenCompletedPallet.Enabled = false;
            this.picReopenCompletedPallet.Image = ((System.Drawing.Image)(resources.GetObject("picReopenCompletedPallet.Image")));
            this.picReopenCompletedPallet.Location = new System.Drawing.Point(568, 6);
            this.picReopenCompletedPallet.Name = "picReopenCompletedPallet";
            this.picReopenCompletedPallet.Size = new System.Drawing.Size(100, 100);
            this.picReopenCompletedPallet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picReopenCompletedPallet.TabIndex = 8;
            this.picReopenCompletedPallet.TabStop = false;
            this.picReopenCompletedPallet.Tag = "101";
            this.picReopenCompletedPallet.Click += new System.EventHandler(this.picReopenCompletedPallet_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(194)))), ((int)(((byte)(191)))));
            this.ClientSize = new System.Drawing.Size(1089, 552);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnMini);
            this.Controls.Add(this.picChangePassword);
            this.Controls.Add(this.picLogOut);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LABEL PRINTING";
            this.Load += new System.EventHandler(this.frmModelMaster_Load);
            this.panel1.ResumeLayout(false);
            this.tbReport.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcodeGeneration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPartMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGroupMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUserMaster)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReportPalletMapping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReportLabelPrinting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChangePassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReopenCompletedPallet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.TabControl tbReport;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblGroupMaster;
        private System.Windows.Forms.PictureBox picGroupMaster;
        private System.Windows.Forms.Label lblUserMaster;
        private System.Windows.Forms.PictureBox picUserMaster;
        private System.Windows.Forms.PictureBox picLogOut;
        private System.Windows.Forms.PictureBox picChangePassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMini;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPartMaster;
        private System.Windows.Forms.PictureBox picPartMaster;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblReportLabelPrinting;
        private System.Windows.Forms.PictureBox picReportLabelPrinting;
        private System.Windows.Forms.Timer timerAutoLogOut;
        private System.Windows.Forms.Timer timerReOiling;
        private System.Windows.Forms.Label lblReportPalletMapping;
        private System.Windows.Forms.PictureBox picReportPalletMapping;
        private System.Windows.Forms.Label lblBarcodeGeneration;
        private System.Windows.Forms.PictureBox picBarcodeGeneration;
        private System.Windows.Forms.Label lblReopenCompletedPallet;
        private System.Windows.Forms.PictureBox picReopenCompletedPallet;
    }
}