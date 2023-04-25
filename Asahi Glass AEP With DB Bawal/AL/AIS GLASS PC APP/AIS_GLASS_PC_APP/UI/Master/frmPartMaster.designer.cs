namespace AIS_GLASS_PC_APP
{
    partial class frmPartMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPartMaster));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.InternalPartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InternalPartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerPartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsQAEnable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Separator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VendorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrintQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCustomerPartNo = new System.Windows.Forms.TextBox();
            this.lblCustomerPartNo = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtPartSearch = new System.Windows.Forms.TextBox();
            this.lblSearchPart = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLine = new System.Windows.Forms.Label();
            this.checkedListBoxLine = new System.Windows.Forms.CheckedListBox();
            this.txtDefaultPrintQty = new System.Windows.Forms.TextBox();
            this.lblDefaultPrintQty = new System.Windows.Forms.Label();
            this.chkQAEnable = new System.Windows.Forms.CheckBox();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.txtSeparator = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInternalPartName = new System.Windows.Forms.TextBox();
            this.lblInternalPartName = new System.Windows.Forms.Label();
            this.txtPackSize = new System.Windows.Forms.TextBox();
            this.lblPackSize = new System.Windows.Forms.Label();
            this.txtVendorCode = new System.Windows.Forms.TextBox();
            this.lblVendorCode = new System.Windows.Forms.Label();
            this.txtInternalPartNo = new System.Windows.Forms.TextBox();
            this.lblInternalPartNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMinimize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Garamond", 12.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InternalPartNo,
            this.InternalPartName,
            this.CustomerPartNo,
            this.CustomerCode,
            this.IsQAEnable,
            this.Separator,
            this.VendorCode,
            this.PackSize,
            this.PrintQty,
            this.CreatedBy,
            this.CreatedOn});
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.AliceBlue;
            this.dgv.Location = new System.Drawing.Point(10, 210);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(1109, 217);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 187;
            this.dgv.TabStop = false;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // InternalPartNo
            // 
            this.InternalPartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.InternalPartNo.DataPropertyName = "InternalPartNo";
            this.InternalPartNo.HeaderText = "Internal Part No.";
            this.InternalPartNo.Name = "InternalPartNo";
            this.InternalPartNo.ReadOnly = true;
            this.InternalPartNo.Width = 250;
            // 
            // InternalPartName
            // 
            this.InternalPartName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.InternalPartName.DataPropertyName = "InternalPartName";
            this.InternalPartName.HeaderText = "Internal Part Name";
            this.InternalPartName.Name = "InternalPartName";
            this.InternalPartName.ReadOnly = true;
            this.InternalPartName.Width = 200;
            // 
            // CustomerPartNo
            // 
            this.CustomerPartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CustomerPartNo.DataPropertyName = "CustomerPartNo";
            this.CustomerPartNo.HeaderText = "Customer Part No";
            this.CustomerPartNo.Name = "CustomerPartNo";
            this.CustomerPartNo.ReadOnly = true;
            this.CustomerPartNo.Width = 200;
            // 
            // CustomerCode
            // 
            this.CustomerCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CustomerCode.DataPropertyName = "CustomerCode";
            this.CustomerCode.HeaderText = "Customer";
            this.CustomerCode.Name = "CustomerCode";
            this.CustomerCode.ReadOnly = true;
            this.CustomerCode.Width = 150;
            // 
            // IsQAEnable
            // 
            this.IsQAEnable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IsQAEnable.DataPropertyName = "IsQAEnable";
            this.IsQAEnable.HeaderText = "QA Enable";
            this.IsQAEnable.Name = "IsQAEnable";
            this.IsQAEnable.ReadOnly = true;
            this.IsQAEnable.Width = 120;
            // 
            // Separator
            // 
            this.Separator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Separator.DataPropertyName = "Separator";
            this.Separator.HeaderText = "Separator";
            this.Separator.Name = "Separator";
            this.Separator.ReadOnly = true;
            // 
            // VendorCode
            // 
            this.VendorCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VendorCode.DataPropertyName = "VendorCode";
            this.VendorCode.HeaderText = "Vendor Code";
            this.VendorCode.Name = "VendorCode";
            this.VendorCode.ReadOnly = true;
            this.VendorCode.Width = 150;
            // 
            // PackSize
            // 
            this.PackSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PackSize.DataPropertyName = "PackSize";
            this.PackSize.HeaderText = "Pack Size";
            this.PackSize.Name = "PackSize";
            this.PackSize.ReadOnly = true;
            this.PackSize.Width = 120;
            // 
            // PrintQty
            // 
            this.PrintQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PrintQty.DataPropertyName = "PrintQty";
            this.PrintQty.HeaderText = "Print Qty";
            this.PrintQty.Name = "PrintQty";
            this.PrintQty.ReadOnly = true;
            // 
            // CreatedBy
            // 
            this.CreatedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CreatedBy.DataPropertyName = "CreatedBy";
            this.CreatedBy.HeaderText = "Created By";
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.ReadOnly = true;
            this.CreatedBy.Width = 150;
            // 
            // CreatedOn
            // 
            this.CreatedOn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CreatedOn.DataPropertyName = "CreatedOn";
            this.CreatedOn.HeaderText = "CreatedOn";
            this.CreatedOn.Name = "CreatedOn";
            this.CreatedOn.ReadOnly = true;
            this.CreatedOn.Width = 150;
            // 
            // txtCustomerPartNo
            // 
            this.txtCustomerPartNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCustomerPartNo.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtCustomerPartNo.Location = new System.Drawing.Point(162, 63);
            this.txtCustomerPartNo.MaxLength = 50;
            this.txtCustomerPartNo.Name = "txtCustomerPartNo";
            this.txtCustomerPartNo.Size = new System.Drawing.Size(300, 27);
            this.txtCustomerPartNo.TabIndex = 2;
            // 
            // lblCustomerPartNo
            // 
            this.lblCustomerPartNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCustomerPartNo.AutoSize = true;
            this.lblCustomerPartNo.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblCustomerPartNo.Location = new System.Drawing.Point(6, 67);
            this.lblCustomerPartNo.Name = "lblCustomerPartNo";
            this.lblCustomerPartNo.Size = new System.Drawing.Size(143, 19);
            this.lblCustomerPartNo.TabIndex = 183;
            this.lblCustomerPartNo.Text = "Customer Part No. *:";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblCount.ForeColor = System.Drawing.Color.Maroon;
            this.lblCount.Location = new System.Drawing.Point(13, 303);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(106, 19);
            this.lblCount.TabIndex = 182;
            this.lblCount.Text = "Rows Count : 0";
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.AliceBlue;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1139, 41);
            this.lblHeader.TabIndex = 212;
            this.lblHeader.Text = "PART MASTER";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtPartSearch);
            this.panel1.Controls.Add(this.lblSearchPart);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Controls.Add(this.lblCount);
            this.panel1.Location = new System.Drawing.Point(6, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1127, 500);
            this.panel1.TabIndex = 213;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnReset);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnExport);
            this.panel2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.panel2.Location = new System.Drawing.Point(767, 431);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 64);
            this.panel2.TabIndex = 248;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(6, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 47);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "&Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReset.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReset.Location = new System.Drawing.Point(210, 7);
            this.btnReset.Margin = new System.Windows.Forms.Padding(5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(67, 47);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "&Reset";
            this.btnReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnClose.Image = global::AIS_GLASS_PC_APP.Properties.Resources.Delete;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(278, 7);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 47);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(74, 7);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(67, 47);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExport.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExport.Location = new System.Drawing.Point(142, 7);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(67, 47);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "&Export";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtPartSearch
            // 
            this.txtPartSearch.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtPartSearch.Location = new System.Drawing.Point(773, 177);
            this.txtPartSearch.Name = "txtPartSearch";
            this.txtPartSearch.Size = new System.Drawing.Size(342, 27);
            this.txtPartSearch.TabIndex = 12;
            this.txtPartSearch.TextChanged += new System.EventHandler(this.txtPartSearch_TextChanged);
            // 
            // lblSearchPart
            // 
            this.lblSearchPart.AutoSize = true;
            this.lblSearchPart.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblSearchPart.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSearchPart.Location = new System.Drawing.Point(672, 181);
            this.lblSearchPart.Name = "lblSearchPart";
            this.lblSearchPart.Size = new System.Drawing.Size(96, 19);
            this.lblSearchPart.TabIndex = 191;
            this.lblSearchPart.Text = "Search Part.:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLine);
            this.groupBox1.Controls.Add(this.checkedListBoxLine);
            this.groupBox1.Controls.Add(this.txtDefaultPrintQty);
            this.groupBox1.Controls.Add(this.lblDefaultPrintQty);
            this.groupBox1.Controls.Add(this.chkQAEnable);
            this.groupBox1.Controls.Add(this.cmbCustomer);
            this.groupBox1.Controls.Add(this.txtSeparator);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtInternalPartName);
            this.groupBox1.Controls.Add(this.lblInternalPartName);
            this.groupBox1.Controls.Add(this.txtPackSize);
            this.groupBox1.Controls.Add(this.lblPackSize);
            this.groupBox1.Controls.Add(this.txtVendorCode);
            this.groupBox1.Controls.Add(this.lblVendorCode);
            this.groupBox1.Controls.Add(this.txtInternalPartNo);
            this.groupBox1.Controls.Add(this.lblInternalPartNo);
            this.groupBox1.Controls.Add(this.txtCustomerPartNo);
            this.groupBox1.Controls.Add(this.lblCustomerPartNo);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1117, 170);
            this.groupBox1.TabIndex = 193;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter Part Details:";
            // 
            // lblLine
            // 
            this.lblLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLine.AutoSize = true;
            this.lblLine.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblLine.Location = new System.Drawing.Point(945, 24);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(48, 19);
            this.lblLine.TabIndex = 251;
            this.lblLine.Text = "Line*:";
            // 
            // checkedListBoxLine
            // 
            this.checkedListBoxLine.FormattingEnabled = true;
            this.checkedListBoxLine.Location = new System.Drawing.Point(997, 22);
            this.checkedListBoxLine.Name = "checkedListBoxLine";
            this.checkedListBoxLine.ScrollAlwaysVisible = true;
            this.checkedListBoxLine.Size = new System.Drawing.Size(115, 114);
            this.checkedListBoxLine.TabIndex = 250;
            // 
            // txtDefaultPrintQty
            // 
            this.txtDefaultPrintQty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDefaultPrintQty.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtDefaultPrintQty.Location = new System.Drawing.Point(838, 134);
            this.txtDefaultPrintQty.MaxLength = 2;
            this.txtDefaultPrintQty.Name = "txtDefaultPrintQty";
            this.txtDefaultPrintQty.Size = new System.Drawing.Size(49, 27);
            this.txtDefaultPrintQty.TabIndex = 8;
            this.txtDefaultPrintQty.Text = "1";
            this.txtDefaultPrintQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefaultPrintQty_KeyPress);
            // 
            // lblDefaultPrintQty
            // 
            this.lblDefaultPrintQty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDefaultPrintQty.AutoSize = true;
            this.lblDefaultPrintQty.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblDefaultPrintQty.Location = new System.Drawing.Point(705, 137);
            this.lblDefaultPrintQty.Name = "lblDefaultPrintQty";
            this.lblDefaultPrintQty.Size = new System.Drawing.Size(126, 19);
            this.lblDefaultPrintQty.TabIndex = 249;
            this.lblDefaultPrintQty.Text = "Default Print Qty :";
            // 
            // chkQAEnable
            // 
            this.chkQAEnable.AutoSize = true;
            this.chkQAEnable.Checked = true;
            this.chkQAEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQAEnable.Location = new System.Drawing.Point(635, 98);
            this.chkQAEnable.Name = "chkQAEnable";
            this.chkQAEnable.Size = new System.Drawing.Size(108, 23);
            this.chkQAEnable.TabIndex = 5;
            this.chkQAEnable.Text = "QA Enable ?";
            this.chkQAEnable.UseVisualStyleBackColor = true;
            this.chkQAEnable.CheckedChanged += new System.EventHandler(this.chkQAEnable_CheckedChanged);
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(635, 59);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(300, 27);
            this.cmbCustomer.TabIndex = 3;
            // 
            // txtSeparator
            // 
            this.txtSeparator.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSeparator.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtSeparator.Location = new System.Drawing.Point(635, 131);
            this.txtSeparator.MaxLength = 1;
            this.txtSeparator.Name = "txtSeparator";
            this.txtSeparator.Size = new System.Drawing.Size(49, 27);
            this.txtSeparator.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F);
            this.label3.Location = new System.Drawing.Point(492, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 19);
            this.label3.TabIndex = 247;
            this.label3.Text = "Barcode Separator :";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F);
            this.label2.Location = new System.Drawing.Point(524, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 19);
            this.label2.TabIndex = 245;
            this.label2.Text = "IS QA Enable*:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F);
            this.label1.Location = new System.Drawing.Point(545, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 243;
            this.label1.Text = "Customer*:";
            // 
            // txtInternalPartName
            // 
            this.txtInternalPartName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtInternalPartName.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtInternalPartName.Location = new System.Drawing.Point(635, 21);
            this.txtInternalPartName.MaxLength = 50;
            this.txtInternalPartName.Name = "txtInternalPartName";
            this.txtInternalPartName.Size = new System.Drawing.Size(300, 27);
            this.txtInternalPartName.TabIndex = 1;
            // 
            // lblInternalPartName
            // 
            this.lblInternalPartName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInternalPartName.AutoSize = true;
            this.lblInternalPartName.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblInternalPartName.Location = new System.Drawing.Point(485, 24);
            this.lblInternalPartName.Name = "lblInternalPartName";
            this.lblInternalPartName.Size = new System.Drawing.Size(143, 19);
            this.lblInternalPartName.TabIndex = 241;
            this.lblInternalPartName.Text = "Internal Part Name*:";
            // 
            // txtPackSize
            // 
            this.txtPackSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPackSize.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtPackSize.Location = new System.Drawing.Point(162, 134);
            this.txtPackSize.MaxLength = 3;
            this.txtPackSize.Name = "txtPackSize";
            this.txtPackSize.Size = new System.Drawing.Size(133, 27);
            this.txtPackSize.TabIndex = 6;
            this.txtPackSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPackSize_KeyPress);
            // 
            // lblPackSize
            // 
            this.lblPackSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPackSize.AutoSize = true;
            this.lblPackSize.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblPackSize.Location = new System.Drawing.Point(74, 137);
            this.lblPackSize.Name = "lblPackSize";
            this.lblPackSize.Size = new System.Drawing.Size(80, 19);
            this.lblPackSize.TabIndex = 239;
            this.lblPackSize.Text = "Pack Size*:";
            // 
            // txtVendorCode
            // 
            this.txtVendorCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtVendorCode.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtVendorCode.Location = new System.Drawing.Point(162, 101);
            this.txtVendorCode.MaxLength = 20;
            this.txtVendorCode.Name = "txtVendorCode";
            this.txtVendorCode.Size = new System.Drawing.Size(300, 27);
            this.txtVendorCode.TabIndex = 4;
            // 
            // lblVendorCode
            // 
            this.lblVendorCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblVendorCode.AutoSize = true;
            this.lblVendorCode.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblVendorCode.Location = new System.Drawing.Point(49, 104);
            this.lblVendorCode.Name = "lblVendorCode";
            this.lblVendorCode.Size = new System.Drawing.Size(103, 19);
            this.lblVendorCode.TabIndex = 201;
            this.lblVendorCode.Text = "Vendor Code*:";
            // 
            // txtInternalPartNo
            // 
            this.txtInternalPartNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtInternalPartNo.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtInternalPartNo.Location = new System.Drawing.Point(162, 26);
            this.txtInternalPartNo.MaxLength = 50;
            this.txtInternalPartNo.Name = "txtInternalPartNo";
            this.txtInternalPartNo.Size = new System.Drawing.Size(300, 27);
            this.txtInternalPartNo.TabIndex = 0;
            // 
            // lblInternalPartNo
            // 
            this.lblInternalPartNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInternalPartNo.AutoSize = true;
            this.lblInternalPartNo.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblInternalPartNo.Location = new System.Drawing.Point(17, 29);
            this.lblInternalPartNo.Name = "lblInternalPartNo";
            this.lblInternalPartNo.Size = new System.Drawing.Size(131, 19);
            this.lblInternalPartNo.TabIndex = 195;
            this.lblInternalPartNo.Text = "Internal Part  No.*:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1085, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 214;
            this.label6.Text = "Minimize";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(115, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 215;
            this.pictureBox1.TabStop = false;
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnMinimize.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimize.Image")));
            this.btnMinimize.Location = new System.Drawing.Point(1098, 5);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(28, 20);
            this.btnMinimize.TabIndex = 0;
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // frmPartMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(151)))), ((int)(((byte)(195)))));
            this.ClientSize = new System.Drawing.Size(1139, 550);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmPartMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Part Master";
            this.Load += new System.EventHandler(this.frmModelMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtCustomerPartNo;
        private System.Windows.Forms.Label lblCustomerPartNo;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnMinimize;
        public System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPartSearch;
        private System.Windows.Forms.Label lblSearchPart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtInternalPartNo;
        private System.Windows.Forms.Label lblInternalPartNo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtVendorCode;
        private System.Windows.Forms.Label lblVendorCode;
        private System.Windows.Forms.TextBox txtPackSize;
        private System.Windows.Forms.Label lblPackSize;
        private System.Windows.Forms.TextBox txtInternalPartName;
        private System.Windows.Forms.Label lblInternalPartName;
        private System.Windows.Forms.TextBox txtSeparator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.CheckBox chkQAEnable;
        private System.Windows.Forms.TextBox txtDefaultPrintQty;
        private System.Windows.Forms.Label lblDefaultPrintQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn InternalPartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn InternalPartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerPartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsQAEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Separator;
        private System.Windows.Forms.DataGridViewTextBoxColumn VendorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrintQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedOn;
        private System.Windows.Forms.CheckedListBox checkedListBoxLine;
        private System.Windows.Forms.Label lblLine;
    }
}