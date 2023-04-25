namespace AIS_GLASS_PC_APP
{
    partial class frmBarcodeGeneration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBarcodeGeneration));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMini = new System.Windows.Forms.Button();
            this.gbPrintingParameter = new System.Windows.Forms.GroupBox();
            this.chkYearly = new System.Windows.Forms.CheckBox();
            this.chkMonthly = new System.Windows.Forms.CheckBox();
            this.chkDaily = new System.Windows.Forms.CheckBox();
            this.lblDateFormat = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIndex = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFieldName = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtPartVal = new System.Windows.Forms.TextBox();
            this.lblPartNo = new System.Windows.Forms.Label();
            this.cmbAISPartNo = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvShowSaveDetails = new System.Windows.Forms.DataGridView();
            this.AIS_PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeletePart = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.IsValid = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AISPartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAdd = new System.Windows.Forms.DataGridView();
            this.Field_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Field_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value_Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value_Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbPrintingParameter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowSaveDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReset
            // 
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
            this.btnReset.Location = new System.Drawing.Point(90, 8);
            this.btnReset.Margin = new System.Windows.Forms.Padding(5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(66, 47);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "R&eset";
            this.btnReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnClose
            // 
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
            this.btnClose.Location = new System.Drawing.Point(166, 8);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 47);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.btnMini.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnMini.Image = ((System.Drawing.Image)(resources.GetObject("btnMini.Image")));
            this.btnMini.Location = new System.Drawing.Point(1137, 3);
            this.btnMini.Name = "btnMini";
            this.btnMini.Size = new System.Drawing.Size(29, 22);
            this.btnMini.TabIndex = 210;
            this.btnMini.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMini.UseVisualStyleBackColor = false;
            this.btnMini.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // gbPrintingParameter
            // 
            this.gbPrintingParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPrintingParameter.Controls.Add(this.chkYearly);
            this.gbPrintingParameter.Controls.Add(this.chkMonthly);
            this.gbPrintingParameter.Controls.Add(this.chkDaily);
            this.gbPrintingParameter.Controls.Add(this.lblDateFormat);
            this.gbPrintingParameter.Controls.Add(this.label4);
            this.gbPrintingParameter.Controls.Add(this.label2);
            this.gbPrintingParameter.Controls.Add(this.txtIndex);
            this.gbPrintingParameter.Controls.Add(this.txtLength);
            this.gbPrintingParameter.Controls.Add(this.label1);
            this.gbPrintingParameter.Controls.Add(this.cmbFieldName);
            this.gbPrintingParameter.Controls.Add(this.btnAdd);
            this.gbPrintingParameter.Controls.Add(this.txtPartVal);
            this.gbPrintingParameter.Controls.Add(this.lblPartNo);
            this.gbPrintingParameter.Controls.Add(this.cmbAISPartNo);
            this.gbPrintingParameter.Controls.Add(this.panel2);
            this.gbPrintingParameter.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.gbPrintingParameter.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gbPrintingParameter.Location = new System.Drawing.Point(3, 3);
            this.gbPrintingParameter.Name = "gbPrintingParameter";
            this.gbPrintingParameter.Size = new System.Drawing.Size(1164, 155);
            this.gbPrintingParameter.TabIndex = 193;
            this.gbPrintingParameter.TabStop = false;
            this.gbPrintingParameter.Text = "Details";
            // 
            // chkYearly
            // 
            this.chkYearly.AutoSize = true;
            this.chkYearly.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this.chkYearly.Location = new System.Drawing.Point(798, 57);
            this.chkYearly.Name = "chkYearly";
            this.chkYearly.Size = new System.Drawing.Size(67, 23);
            this.chkYearly.TabIndex = 246;
            this.chkYearly.Text = "Yearly";
            this.chkYearly.UseVisualStyleBackColor = true;
            this.chkYearly.Visible = false;
            this.chkYearly.Click += new System.EventHandler(this.chkYearly_Click);
            // 
            // chkMonthly
            // 
            this.chkMonthly.AutoSize = true;
            this.chkMonthly.Checked = true;
            this.chkMonthly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMonthly.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this.chkMonthly.Location = new System.Drawing.Point(710, 57);
            this.chkMonthly.Name = "chkMonthly";
            this.chkMonthly.Size = new System.Drawing.Size(82, 23);
            this.chkMonthly.TabIndex = 245;
            this.chkMonthly.Text = "Monthly";
            this.chkMonthly.UseVisualStyleBackColor = true;
            this.chkMonthly.Visible = false;
            this.chkMonthly.Click += new System.EventHandler(this.chkMonthly_Click);
            // 
            // chkDaily
            // 
            this.chkDaily.AutoSize = true;
            this.chkDaily.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic);
            this.chkDaily.Location = new System.Drawing.Point(643, 56);
            this.chkDaily.Name = "chkDaily";
            this.chkDaily.Size = new System.Drawing.Size(61, 23);
            this.chkDaily.TabIndex = 244;
            this.chkDaily.Text = "Daily";
            this.chkDaily.UseVisualStyleBackColor = true;
            this.chkDaily.Visible = false;
            this.chkDaily.Click += new System.EventHandler(this.chkDaily_Click);
            // 
            // lblDateFormat
            // 
            this.lblDateFormat.AutoSize = true;
            this.lblDateFormat.Font = new System.Drawing.Font("Calibri", 12F);
            this.lblDateFormat.Location = new System.Drawing.Point(538, 56);
            this.lblDateFormat.Name = "lblDateFormat";
            this.lblDateFormat.Size = new System.Drawing.Size(94, 19);
            this.lblDateFormat.TabIndex = 247;
            this.lblDateFormat.Text = "Mfg format*:";
            this.lblDateFormat.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1009, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 19);
            this.label4.TabIndex = 243;
            this.label4.Text = "Index  *:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(880, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 19);
            this.label2.TabIndex = 242;
            this.label2.Text = "Length  *:";
            // 
            // txtIndex
            // 
            this.txtIndex.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtIndex.Location = new System.Drawing.Point(1081, 54);
            this.txtIndex.MaxLength = 1;
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.Size = new System.Drawing.Size(30, 27);
            this.txtIndex.TabIndex = 4;
            this.txtIndex.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIndex_KeyPress);
            // 
            // txtLength
            // 
            this.txtLength.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtLength.Location = new System.Drawing.Point(962, 51);
            this.txtLength.MaxLength = 2;
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(30, 27);
            this.txtLength.TabIndex = 3;
            this.txtLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLength_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(428, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 19);
            this.label1.TabIndex = 238;
            this.label1.Text = "Fields Name  *:";
            // 
            // cmbFieldName
            // 
            this.cmbFieldName.FormattingEnabled = true;
            this.cmbFieldName.Location = new System.Drawing.Point(546, 19);
            this.cmbFieldName.Name = "cmbFieldName";
            this.cmbFieldName.Size = new System.Drawing.Size(294, 27);
            this.cmbFieldName.TabIndex = 1;
            this.cmbFieldName.SelectedIndexChanged += new System.EventHandler(this.cmbFieldName_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdd.Location = new System.Drawing.Point(432, 46);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 47);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtPartVal
            // 
            this.txtPartVal.Location = new System.Drawing.Point(846, 19);
            this.txtPartVal.Name = "txtPartVal";
            this.txtPartVal.Size = new System.Drawing.Size(312, 27);
            this.txtPartVal.TabIndex = 2;
            this.txtPartVal.Leave += new System.EventHandler(this.txtPartVal_Leave);
            // 
            // lblPartNo
            // 
            this.lblPartNo.AutoSize = true;
            this.lblPartNo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartNo.Location = new System.Drawing.Point(6, 27);
            this.lblPartNo.Name = "lblPartNo";
            this.lblPartNo.Size = new System.Drawing.Size(112, 19);
            this.lblPartNo.TabIndex = 212;
            this.lblPartNo.Text = "AIS Part No.  *:";
            // 
            // cmbAISPartNo
            // 
            this.cmbAISPartNo.FormattingEnabled = true;
            this.cmbAISPartNo.Location = new System.Drawing.Point(124, 22);
            this.cmbAISPartNo.Name = "cmbAISPartNo";
            this.cmbAISPartNo.Size = new System.Drawing.Size(294, 27);
            this.cmbAISPartNo.TabIndex = 0;
            this.cmbAISPartNo.SelectedIndexChanged += new System.EventHandler(this.cmbPartNo_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnReset);
            this.panel2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.panel2.Location = new System.Drawing.Point(3, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(237, 64);
            this.panel2.TabIndex = 195;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(146)))));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(15, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 47);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.AliceBlue;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1184, 41);
            this.lblHeader.TabIndex = 212;
            this.lblHeader.Text = "BARCODE GENERATION";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHeader.Click += new System.EventHandler(this.lblHeader_Click);
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
            this.btnMinimize.Location = new System.Drawing.Point(1146, -72);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(28, 32);
            this.btnMinimize.TabIndex = 211;
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMinimize.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dgvShowSaveDetails);
            this.panel1.Controls.Add(this.lblCount);
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Controls.Add(this.dgvAdd);
            this.panel1.Controls.Add(this.gbPrintingParameter);
            this.panel1.Location = new System.Drawing.Point(4, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 514);
            this.panel1.TabIndex = 213;
            // 
            // dgvShowSaveDetails
            // 
            this.dgvShowSaveDetails.AllowUserToAddRows = false;
            this.dgvShowSaveDetails.AllowUserToDeleteRows = false;
            this.dgvShowSaveDetails.AllowUserToResizeRows = false;
            this.dgvShowSaveDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShowSaveDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShowSaveDetails.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dgvShowSaveDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Garamond", 12.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShowSaveDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvShowSaveDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowSaveDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AIS_PartNo,
            this.FieldValue,
            this.FieldName,
            this.ValueLength,
            this.ValueIndex,
            this.DeletePart});
            this.dgvShowSaveDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvShowSaveDetails.EnableHeadersVisualStyles = false;
            this.dgvShowSaveDetails.GridColor = System.Drawing.Color.AliceBlue;
            this.dgvShowSaveDetails.Location = new System.Drawing.Point(3, 184);
            this.dgvShowSaveDetails.MultiSelect = false;
            this.dgvShowSaveDetails.Name = "dgvShowSaveDetails";
            this.dgvShowSaveDetails.ReadOnly = true;
            this.dgvShowSaveDetails.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dgvShowSaveDetails.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvShowSaveDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShowSaveDetails.Size = new System.Drawing.Size(1162, 202);
            this.dgvShowSaveDetails.StandardTab = true;
            this.dgvShowSaveDetails.TabIndex = 222;
            this.dgvShowSaveDetails.TabStop = false;
            this.dgvShowSaveDetails.Visible = false;
            this.dgvShowSaveDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowSaveDetails_CellContentClick);
            this.dgvShowSaveDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowPickDetails_CellDoubleClick);
            // 
            // AIS_PartNo
            // 
            this.AIS_PartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AIS_PartNo.DataPropertyName = "AIS_PartNo";
            this.AIS_PartNo.HeaderText = "AIS Part No.";
            this.AIS_PartNo.Name = "AIS_PartNo";
            this.AIS_PartNo.ReadOnly = true;
            this.AIS_PartNo.Width = 200;
            // 
            // FieldValue
            // 
            this.FieldValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FieldValue.DataPropertyName = "FieldValue";
            this.FieldValue.HeaderText = "Field Value";
            this.FieldValue.Name = "FieldValue";
            this.FieldValue.ReadOnly = true;
            this.FieldValue.Width = 200;
            // 
            // FieldName
            // 
            this.FieldName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FieldName.DataPropertyName = "FieldName";
            this.FieldName.HeaderText = "Field Name";
            this.FieldName.Name = "FieldName";
            this.FieldName.ReadOnly = true;
            this.FieldName.Width = 200;
            // 
            // ValueLength
            // 
            this.ValueLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ValueLength.DataPropertyName = "ValueLength";
            this.ValueLength.HeaderText = "Length";
            this.ValueLength.Name = "ValueLength";
            this.ValueLength.ReadOnly = true;
            this.ValueLength.Width = 200;
            // 
            // ValueIndex
            // 
            this.ValueIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ValueIndex.DataPropertyName = "ValueIndex";
            this.ValueIndex.HeaderText = "Index";
            this.ValueIndex.Name = "ValueIndex";
            this.ValueIndex.ReadOnly = true;
            this.ValueIndex.Width = 200;
            // 
            // DeletePart
            // 
            this.DeletePart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DeletePart.DataPropertyName = "DeletePart";
            this.DeletePart.HeaderText = "Delete";
            this.DeletePart.Name = "DeletePart";
            this.DeletePart.ReadOnly = true;
            this.DeletePart.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DeletePart.Text = "Delete";
            this.DeletePart.UseColumnTextForButtonValue = true;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(11, 159);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(106, 19);
            this.lblCount.TabIndex = 221;
            this.lblCount.Text = "Rows Count :0";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Garamond", 12.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsValid,
            this.AISPartNo});
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.AliceBlue;
            this.dgv.Location = new System.Drawing.Point(1, 392);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(1162, 117);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 195;
            this.dgv.TabStop = false;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // IsValid
            // 
            this.IsValid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IsValid.DataPropertyName = "IsValid";
            this.IsValid.HeaderText = "IsValid";
            this.IsValid.Name = "IsValid";
            this.IsValid.ReadOnly = true;
            this.IsValid.Visible = false;
            this.IsValid.Width = 70;
            // 
            // AISPartNo
            // 
            this.AISPartNo.DataPropertyName = "AISPartNo";
            this.AISPartNo.HeaderText = "AIS Part No.";
            this.AISPartNo.Name = "AISPartNo";
            this.AISPartNo.ReadOnly = true;
            // 
            // dgvAdd
            // 
            this.dgvAdd.AllowUserToAddRows = false;
            this.dgvAdd.AllowUserToDeleteRows = false;
            this.dgvAdd.AllowUserToResizeRows = false;
            this.dgvAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAdd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAdd.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dgvAdd.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Garamond", 12.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAdd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Field_Name,
            this.Field_Value,
            this.Value_Length,
            this.Value_Index,
            this.Delete});
            this.dgvAdd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvAdd.EnableHeadersVisualStyles = false;
            this.dgvAdd.GridColor = System.Drawing.Color.AliceBlue;
            this.dgvAdd.Location = new System.Drawing.Point(3, 183);
            this.dgvAdd.MultiSelect = false;
            this.dgvAdd.Name = "dgvAdd";
            this.dgvAdd.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dgvAdd.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAdd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdd.Size = new System.Drawing.Size(1162, 203);
            this.dgvAdd.StandardTab = true;
            this.dgvAdd.TabIndex = 194;
            this.dgvAdd.TabStop = false;
            this.dgvAdd.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdd_CellContentClick);
            // 
            // Field_Name
            // 
            this.Field_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Field_Name.DataPropertyName = "Field_Name";
            this.Field_Name.HeaderText = "Field Name";
            this.Field_Name.Name = "Field_Name";
            this.Field_Name.Width = 200;
            // 
            // Field_Value
            // 
            this.Field_Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Field_Value.DataPropertyName = "Field_Value";
            this.Field_Value.HeaderText = "Field Value";
            this.Field_Value.Name = "Field_Value";
            this.Field_Value.Width = 200;
            // 
            // Value_Length
            // 
            this.Value_Length.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Value_Length.DataPropertyName = "Value_Length";
            this.Value_Length.HeaderText = "Length";
            this.Value_Length.Name = "Value_Length";
            this.Value_Length.ReadOnly = true;
            this.Value_Length.Width = 200;
            // 
            // Value_Index
            // 
            this.Value_Index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Value_Index.DataPropertyName = "Value_Index";
            this.Value_Index.HeaderText = "Index";
            this.Value_Index.Name = "Value_Index";
            this.Value_Index.Width = 200;
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Delete.DataPropertyName = "Delete";
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.Text = "Delete";
            this.Delete.UseColumnTextForButtonValue = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1126, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 214;
            this.label5.Text = "Minimize";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(115, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 262;
            this.pictureBox1.TabStop = false;
            // 
            // frmBarcodeGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(194)))), ((int)(((byte)(191)))));
            this.ClientSize = new System.Drawing.Size(1184, 560);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnMini);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmBarcodeGeneration";
            this.Text = "Reprinting";
            this.Load += new System.EventHandler(this.frmRMPicklistGeneration_Load);
            this.gbPrintingParameter.ResumeLayout(false);
            this.gbPrintingParameter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowSaveDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMini;
        private System.Windows.Forms.GroupBox gbPrintingParameter;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblPartNo;
        private System.Windows.Forms.ComboBox cmbAISPartNo;
        private System.Windows.Forms.TextBox txtPartVal;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvShowSaveDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFieldName;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Field_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Field_Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value_Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value_Index;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn AIS_PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueIndex;
        private System.Windows.Forms.DataGridViewButtonColumn DeletePart;
        private System.Windows.Forms.CheckBox chkYearly;
        private System.Windows.Forms.CheckBox chkMonthly;
        private System.Windows.Forms.CheckBox chkDaily;
        private System.Windows.Forms.Label lblDateFormat;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsValid;
        private System.Windows.Forms.DataGridViewTextBoxColumn AISPartNo;
    }
}