namespace AISCOMServer
{
    partial class frmServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServer));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.pnlClients = new System.Windows.Forms.Panel();
            this.lvClient = new System.Windows.Forms.ListView();
            this.colClientIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cloTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdClientBack = new System.Windows.Forms.Button();
            this.pnlLog = new System.Windows.Forms.Panel();
            this.lvLog = new System.Windows.Forms.ListView();
            this.colClient = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdLogBack = new System.Windows.Forms.Button();
            this.picDisconnect = new System.Windows.Forms.PictureBox();
            this.picConnect = new System.Windows.Forms.PictureBox();
            this.pnlDBSeting = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrinterName = new System.Windows.Forms.TextBox();
            this.lblString = new System.Windows.Forms.Label();
            this.txtConString = new System.Windows.Forms.TextBox();
            this.cmdTestCon = new System.Windows.Forms.Button();
            this.cmbSchema = new System.Windows.Forms.ComboBox();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdBack = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.lblPwd = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblConnect = new System.Windows.Forms.Label();
            this.pnlServer = new System.Windows.Forms.Panel();
            this.picViewConnection = new System.Windows.Forms.PictureBox();
            this.cmdHide = new System.Windows.Forms.Button();
            this.cmdDbServer = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.cmdClients = new System.Windows.Forms.Button();
            this.cmdLog = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdDisconnect = new System.Windows.Forms.Button();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlImage.SuspendLayout();
            this.pnlClients.SuspendLayout();
            this.pnlLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDisconnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConnect)).BeginInit();
            this.pnlDBSeting.SuspendLayout();
            this.pnlServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picViewConnection)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHeader.Controls.Add(this.pictureBox2);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(860, 69);
            this.pnlHeader.TabIndex = 28;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(664, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(196, 69);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 38;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Castellar", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(860, 69);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Communication Server";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlImage
            // 
            this.pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImage.Controls.Add(this.pnlClients);
            this.pnlImage.Controls.Add(this.pnlLog);
            this.pnlImage.Controls.Add(this.picDisconnect);
            this.pnlImage.Controls.Add(this.picConnect);
            this.pnlImage.Controls.Add(this.pnlDBSeting);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlImage.Location = new System.Drawing.Point(319, 69);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(541, 380);
            this.pnlImage.TabIndex = 34;
            // 
            // pnlClients
            // 
            this.pnlClients.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlClients.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClients.Controls.Add(this.lvClient);
            this.pnlClients.Controls.Add(this.cmdClientBack);
            this.pnlClients.Location = new System.Drawing.Point(3, 9);
            this.pnlClients.Name = "pnlClients";
            this.pnlClients.Size = new System.Drawing.Size(111, 347);
            this.pnlClients.TabIndex = 33;
            // 
            // lvClient
            // 
            this.lvClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colClientIP,
            this.cloTime});
            this.lvClient.GridLines = true;
            this.lvClient.HideSelection = false;
            this.lvClient.Location = new System.Drawing.Point(3, 3);
            this.lvClient.Name = "lvClient";
            this.lvClient.Size = new System.Drawing.Size(103, 293);
            this.lvClient.TabIndex = 39;
            this.lvClient.UseCompatibleStateImageBehavior = false;
            this.lvClient.View = System.Windows.Forms.View.Details;
            // 
            // colClientIP
            // 
            this.colClientIP.Text = "ClientIP";
            this.colClientIP.Width = 100;
            // 
            // cloTime
            // 
            this.cloTime.Text = "ConnectAt";
            this.cloTime.Width = 100;
            // 
            // cmdClientBack
            // 
            this.cmdClientBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClientBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClientBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClientBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClientBack.Location = new System.Drawing.Point(19, 302);
            this.cmdClientBack.Name = "cmdClientBack";
            this.cmdClientBack.Size = new System.Drawing.Size(87, 40);
            this.cmdClientBack.TabIndex = 38;
            this.cmdClientBack.Text = "Bac&k";
            this.cmdClientBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClientBack.UseVisualStyleBackColor = true;
            this.cmdClientBack.Click += new System.EventHandler(this.cmdClientBack_Click);
            // 
            // pnlLog
            // 
            this.pnlLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLog.Controls.Add(this.lvLog);
            this.pnlLog.Controls.Add(this.cmdLogBack);
            this.pnlLog.Location = new System.Drawing.Point(189, 5);
            this.pnlLog.Name = "pnlLog";
            this.pnlLog.Size = new System.Drawing.Size(122, 370);
            this.pnlLog.TabIndex = 40;
            // 
            // lvLog
            // 
            this.lvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colClient,
            this.colData,
            this.colDate});
            this.lvLog.GridLines = true;
            this.lvLog.HideSelection = false;
            this.lvLog.Location = new System.Drawing.Point(3, 3);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(114, 316);
            this.lvLog.TabIndex = 39;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            // 
            // colClient
            // 
            this.colClient.Text = "ClientIP";
            this.colClient.Width = 100;
            // 
            // colData
            // 
            this.colData.Text = "Req./Resp.";
            this.colData.Width = 227;
            // 
            // colDate
            // 
            this.colDate.Text = "ConnectAt";
            this.colDate.Width = 100;
            // 
            // cmdLogBack
            // 
            this.cmdLogBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLogBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdLogBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLogBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdLogBack.Location = new System.Drawing.Point(30, 325);
            this.cmdLogBack.Name = "cmdLogBack";
            this.cmdLogBack.Size = new System.Drawing.Size(87, 40);
            this.cmdLogBack.TabIndex = 38;
            this.cmdLogBack.Text = "Bac&k";
            this.cmdLogBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdLogBack.UseVisualStyleBackColor = true;
            this.cmdLogBack.Click += new System.EventHandler(this.cmdLogBack_Click);
            // 
            // picDisconnect
            // 
            this.picDisconnect.BackColor = System.Drawing.Color.Transparent;
            this.picDisconnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picDisconnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDisconnect.InitialImage = null;
            this.picDisconnect.Location = new System.Drawing.Point(0, 0);
            this.picDisconnect.Name = "picDisconnect";
            this.picDisconnect.Size = new System.Drawing.Size(539, 378);
            this.picDisconnect.TabIndex = 30;
            this.picDisconnect.TabStop = false;
            // 
            // picConnect
            // 
            this.picConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picConnect.InitialImage = null;
            this.picConnect.Location = new System.Drawing.Point(5, 2);
            this.picConnect.Name = "picConnect";
            this.picConnect.Size = new System.Drawing.Size(371, 340);
            this.picConnect.TabIndex = 30;
            this.picConnect.TabStop = false;
            // 
            // pnlDBSeting
            // 
            this.pnlDBSeting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDBSeting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDBSeting.Controls.Add(this.label1);
            this.pnlDBSeting.Controls.Add(this.txtPrinterName);
            this.pnlDBSeting.Controls.Add(this.lblString);
            this.pnlDBSeting.Controls.Add(this.txtConString);
            this.pnlDBSeting.Controls.Add(this.cmdTestCon);
            this.pnlDBSeting.Controls.Add(this.cmbSchema);
            this.pnlDBSeting.Controls.Add(this.cmbServer);
            this.pnlDBSeting.Controls.Add(this.cmdSave);
            this.pnlDBSeting.Controls.Add(this.cmdBack);
            this.pnlDBSeting.Controls.Add(this.txtPwd);
            this.pnlDBSeting.Controls.Add(this.lblPwd);
            this.pnlDBSeting.Controls.Add(this.txtUserID);
            this.pnlDBSeting.Controls.Add(this.lblUserID);
            this.pnlDBSeting.Controls.Add(this.lblDatabase);
            this.pnlDBSeting.Controls.Add(this.lblServer);
            this.pnlDBSeting.Location = new System.Drawing.Point(347, 5);
            this.pnlDBSeting.Name = "pnlDBSeting";
            this.pnlDBSeting.Size = new System.Drawing.Size(105, 360);
            this.pnlDBSeting.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 14);
            this.label1.TabIndex = 47;
            this.label1.Text = "Printer Name";
            // 
            // txtPrinterName
            // 
            this.txtPrinterName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrinterName.Location = new System.Drawing.Point(43, 282);
            this.txtPrinterName.MaxLength = 100;
            this.txtPrinterName.Name = "txtPrinterName";
            this.txtPrinterName.Size = new System.Drawing.Size(288, 22);
            this.txtPrinterName.TabIndex = 46;
            // 
            // lblString
            // 
            this.lblString.AutoSize = true;
            this.lblString.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblString.Location = new System.Drawing.Point(40, 218);
            this.lblString.Name = "lblString";
            this.lblString.Size = new System.Drawing.Size(124, 14);
            this.lblString.TabIndex = 45;
            this.lblString.Text = "Connection String";
            // 
            // txtConString
            // 
            this.txtConString.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConString.Location = new System.Drawing.Point(41, 235);
            this.txtConString.Name = "txtConString";
            this.txtConString.ReadOnly = true;
            this.txtConString.Size = new System.Drawing.Size(288, 22);
            this.txtConString.TabIndex = 44;
            // 
            // cmdTestCon
            // 
            this.cmdTestCon.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdTestCon.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTestCon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdTestCon.Location = new System.Drawing.Point(165, 178);
            this.cmdTestCon.Name = "cmdTestCon";
            this.cmdTestCon.Size = new System.Drawing.Size(164, 40);
            this.cmdTestCon.TabIndex = 43;
            this.cmdTestCon.Text = "Test Connection";
            this.cmdTestCon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdTestCon.UseVisualStyleBackColor = true;
            this.cmdTestCon.Click += new System.EventHandler(this.cmdTestCon_Click);
            // 
            // cmbSchema
            // 
            this.cmbSchema.FormattingEnabled = true;
            this.cmbSchema.Location = new System.Drawing.Point(114, 151);
            this.cmbSchema.Name = "cmbSchema";
            this.cmbSchema.Size = new System.Drawing.Size(215, 21);
            this.cmbSchema.TabIndex = 42;
            this.cmbSchema.Enter += new System.EventHandler(this.cmbSchema_Enter);
            // 
            // cmbServer
            // 
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Location = new System.Drawing.Point(114, 52);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(215, 21);
            this.cmbServer.TabIndex = 41;
            this.cmbServer.Enter += new System.EventHandler(this.cmbServer_Enter);
            // 
            // cmdSave
            // 
            this.cmdSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.Location = new System.Drawing.Point(41, 312);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(87, 40);
            this.cmdSave.TabIndex = 39;
            this.cmdSave.Text = "&Save";
            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdBack
            // 
            this.cmdBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdBack.Location = new System.Drawing.Point(138, 312);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(87, 40);
            this.cmdBack.TabIndex = 38;
            this.cmdBack.Text = "Bac&k";
            this.cmdBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdBack.UseVisualStyleBackColor = true;
            this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPwd.Location = new System.Drawing.Point(114, 115);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(215, 22);
            this.txtPwd.TabIndex = 7;
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPwd.Location = new System.Drawing.Point(38, 118);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(72, 14);
            this.lblPwd.TabIndex = 6;
            this.lblPwd.Text = "Password";
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserID.Location = new System.Drawing.Point(114, 85);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(215, 22);
            this.txtUserID.TabIndex = 5;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserID.Location = new System.Drawing.Point(38, 88);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(52, 14);
            this.lblUserID.TabIndex = 4;
            this.lblUserID.Text = "UserId";
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabase.Location = new System.Drawing.Point(38, 153);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(70, 14);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "DataBase";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(38, 52);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(52, 14);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server";
            // 
            // lblConnect
            // 
            this.lblConnect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConnect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblConnect.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnect.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblConnect.Location = new System.Drawing.Point(0, 449);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(860, 30);
            this.lblConnect.TabIndex = 31;
            this.lblConnect.Text = "Server Status";
            this.lblConnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlServer
            // 
            this.pnlServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlServer.Controls.Add(this.picViewConnection);
            this.pnlServer.Controls.Add(this.cmdHide);
            this.pnlServer.Controls.Add(this.cmdDbServer);
            this.pnlServer.Controls.Add(this.lblVersion);
            this.pnlServer.Controls.Add(this.cmdClients);
            this.pnlServer.Controls.Add(this.cmdLog);
            this.pnlServer.Controls.Add(this.cmdExit);
            this.pnlServer.Controls.Add(this.cmdDisconnect);
            this.pnlServer.Controls.Add(this.cmdConnect);
            this.pnlServer.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlServer.Location = new System.Drawing.Point(0, 69);
            this.pnlServer.Name = "pnlServer";
            this.pnlServer.Size = new System.Drawing.Size(313, 380);
            this.pnlServer.TabIndex = 35;
            // 
            // picViewConnection
            // 
            this.picViewConnection.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picViewConnection.Image = global::AISCOMServer.Properties.Resources.com_server_disconnect;
            this.picViewConnection.Location = new System.Drawing.Point(0, 174);
            this.picViewConnection.Name = "picViewConnection";
            this.picViewConnection.Size = new System.Drawing.Size(311, 182);
            this.picViewConnection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picViewConnection.TabIndex = 37;
            this.picViewConnection.TabStop = false;
            // 
            // cmdHide
            // 
            this.cmdHide.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdHide.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHide.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdHide.Location = new System.Drawing.Point(170, 97);
            this.cmdHide.Name = "cmdHide";
            this.cmdHide.Size = new System.Drawing.Size(118, 32);
            this.cmdHide.TabIndex = 5;
            this.cmdHide.Text = "&Hide Server";
            this.cmdHide.UseVisualStyleBackColor = true;
            this.cmdHide.Click += new System.EventHandler(this.cmdHide_Click);
            // 
            // cmdDbServer
            // 
            this.cmdDbServer.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdDbServer.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDbServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDbServer.Location = new System.Drawing.Point(15, 97);
            this.cmdDbServer.Name = "cmdDbServer";
            this.cmdDbServer.Size = new System.Drawing.Size(118, 32);
            this.cmdDbServer.TabIndex = 4;
            this.cmdDbServer.Text = "&DBSetting";
            this.cmdDbServer.UseVisualStyleBackColor = true;
            this.cmdDbServer.Visible = false;
            this.cmdDbServer.Click += new System.EventHandler(this.cmdDbServer_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblVersion.Location = new System.Drawing.Point(0, 356);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(311, 22);
            this.lblVersion.TabIndex = 36;
            this.lblVersion.Text = "Version:";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdClients
            // 
            this.cmdClients.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClients.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClients.Location = new System.Drawing.Point(170, 51);
            this.cmdClients.Name = "cmdClients";
            this.cmdClients.Size = new System.Drawing.Size(118, 32);
            this.cmdClients.TabIndex = 3;
            this.cmdClients.Text = "&View Clients";
            this.cmdClients.UseVisualStyleBackColor = true;
            this.cmdClients.Click += new System.EventHandler(this.cmdClients_Click);
            // 
            // cmdLog
            // 
            this.cmdLog.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLog.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdLog.Location = new System.Drawing.Point(15, 51);
            this.cmdLog.Name = "cmdLog";
            this.cmdLog.Size = new System.Drawing.Size(118, 32);
            this.cmdLog.TabIndex = 2;
            this.cmdLog.Text = "&View &Logs";
            this.cmdLog.UseVisualStyleBackColor = true;
            this.cmdLog.Click += new System.EventHandler(this.cmdLog_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExit.Location = new System.Drawing.Point(15, 136);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(273, 32);
            this.cmdExit.TabIndex = 6;
            this.cmdExit.Text = "E&xit Server";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdDisconnect
            // 
            this.cmdDisconnect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDisconnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDisconnect.Location = new System.Drawing.Point(170, 5);
            this.cmdDisconnect.Name = "cmdDisconnect";
            this.cmdDisconnect.Size = new System.Drawing.Size(118, 32);
            this.cmdDisconnect.TabIndex = 1;
            this.cmdDisconnect.Text = "&Disconnect";
            this.cmdDisconnect.UseVisualStyleBackColor = true;
            this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdConnect.Location = new System.Drawing.Point(15, 5);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(118, 32);
            this.cmdConnect.TabIndex = 0;
            this.cmdConnect.Text = "&Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(860, 479);
            this.ControlBox = false;
            this.Controls.Add(this.pnlServer);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.lblConnect);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Communication Server";
            this.Load += new System.EventHandler(this.frmServer_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlImage.ResumeLayout(false);
            this.pnlClients.ResumeLayout(false);
            this.pnlLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDisconnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConnect)).EndInit();
            this.pnlDBSeting.ResumeLayout(false);
            this.pnlDBSeting.PerformLayout();
            this.pnlServer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picViewConnection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox picConnect;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Button cmdDisconnect;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.Panel pnlServer;
        private System.Windows.Forms.Button cmdClients;
        private System.Windows.Forms.Button cmdLog;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button cmdDbServer;
        private System.Windows.Forms.Panel pnlDBSeting;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdBack;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.ComboBox cmbSchema;
        private System.Windows.Forms.Button cmdTestCon;
        private System.Windows.Forms.Label lblString;
        private System.Windows.Forms.TextBox txtConString;
        private System.Windows.Forms.Panel pnlClients;
        private System.Windows.Forms.ListView lvClient;
        private System.Windows.Forms.Button cmdClientBack;
        private System.Windows.Forms.ColumnHeader colClientIP;
        private System.Windows.Forms.ColumnHeader cloTime;
        private System.Windows.Forms.Panel pnlLog;
        private System.Windows.Forms.Button cmdLogBack;
        private System.Windows.Forms.Button cmdHide;
        private System.Windows.Forms.PictureBox picViewConnection;
        private System.Windows.Forms.PictureBox picDisconnect;
        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.ColumnHeader colClient;
        private System.Windows.Forms.ColumnHeader colData;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrinterName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

