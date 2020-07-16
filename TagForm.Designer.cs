namespace ModbusOperationsApp
{
    partial class TagForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagForm));
            this.lblIp = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtSlaveId = new System.Windows.Forms.TextBox();
            this.lblSlaveId = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblRegisteryType = new System.Windows.Forms.Label();
            this.cmbRegisterType = new System.Windows.Forms.ComboBox();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.lblDataType = new System.Windows.Forms.Label();
            this.cmbByteOrder = new System.Windows.Forms.ComboBox();
            this.lblByteOrder = new System.Windows.Forms.Label();
            this.txtDataLength = new System.Windows.Forms.TextBox();
            this.lblDataLength = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chbIsWritable = new System.Windows.Forms.CheckBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtTagName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(12, 57);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(16, 13);
            this.lblIp.TabIndex = 0;
            this.lblIp.Text = "Ip";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(105, 54);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(121, 20);
            this.txtIp.TabIndex = 1;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(105, 76);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(121, 20);
            this.txtPort.TabIndex = 3;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(12, 79);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtSlaveId
            // 
            this.txtSlaveId.Location = new System.Drawing.Point(105, 98);
            this.txtSlaveId.Name = "txtSlaveId";
            this.txtSlaveId.Size = new System.Drawing.Size(121, 20);
            this.txtSlaveId.TabIndex = 5;
            // 
            // lblSlaveId
            // 
            this.lblSlaveId.AutoSize = true;
            this.lblSlaveId.Location = new System.Drawing.Point(12, 101);
            this.lblSlaveId.Name = "lblSlaveId";
            this.lblSlaveId.Size = new System.Drawing.Size(46, 13);
            this.lblSlaveId.TabIndex = 4;
            this.lblSlaveId.Text = "Slave Id";
            this.lblSlaveId.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(105, 120);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(121, 20);
            this.txtAddress.TabIndex = 7;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(12, 123);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 13);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Address";
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblRegisteryType
            // 
            this.lblRegisteryType.AutoSize = true;
            this.lblRegisteryType.Location = new System.Drawing.Point(12, 146);
            this.lblRegisteryType.Name = "lblRegisteryType";
            this.lblRegisteryType.Size = new System.Drawing.Size(78, 13);
            this.lblRegisteryType.TabIndex = 8;
            this.lblRegisteryType.Text = "Registery Type";
            this.lblRegisteryType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbRegisterType
            // 
            this.cmbRegisterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegisterType.FormattingEnabled = true;
            this.cmbRegisterType.Location = new System.Drawing.Point(105, 143);
            this.cmbRegisterType.Name = "cmbRegisterType";
            this.cmbRegisterType.Size = new System.Drawing.Size(121, 21);
            this.cmbRegisterType.TabIndex = 9;
            this.cmbRegisterType.SelectedIndexChanged += new System.EventHandler(this.cmbRegisterType_SelectedIndexChanged);
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(105, 167);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(121, 21);
            this.cmbDataType.TabIndex = 11;
            this.cmbDataType.SelectedIndexChanged += new System.EventHandler(this.cmbDataType_SelectedIndexChanged);
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.Location = new System.Drawing.Point(12, 170);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(57, 13);
            this.lblDataType.TabIndex = 10;
            this.lblDataType.Text = "Data Type";
            this.lblDataType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbByteOrder
            // 
            this.cmbByteOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbByteOrder.FormattingEnabled = true;
            this.cmbByteOrder.Location = new System.Drawing.Point(105, 192);
            this.cmbByteOrder.Name = "cmbByteOrder";
            this.cmbByteOrder.Size = new System.Drawing.Size(121, 21);
            this.cmbByteOrder.TabIndex = 13;
            // 
            // lblByteOrder
            // 
            this.lblByteOrder.AutoSize = true;
            this.lblByteOrder.Location = new System.Drawing.Point(12, 195);
            this.lblByteOrder.Name = "lblByteOrder";
            this.lblByteOrder.Size = new System.Drawing.Size(57, 13);
            this.lblByteOrder.TabIndex = 12;
            this.lblByteOrder.Text = "Byte Order";
            this.lblByteOrder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtDataLength
            // 
            this.txtDataLength.Location = new System.Drawing.Point(105, 217);
            this.txtDataLength.Name = "txtDataLength";
            this.txtDataLength.Size = new System.Drawing.Size(121, 20);
            this.txtDataLength.TabIndex = 15;
            // 
            // lblDataLength
            // 
            this.lblDataLength.AutoSize = true;
            this.lblDataLength.Location = new System.Drawing.Point(12, 220);
            this.lblDataLength.Name = "lblDataLength";
            this.lblDataLength.Size = new System.Drawing.Size(66, 13);
            this.lblDataLength.TabIndex = 14;
            this.lblDataLength.Text = "Data Length";
            this.lblDataLength.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Is Writable";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chbIsWritable
            // 
            this.chbIsWritable.AutoSize = true;
            this.chbIsWritable.Location = new System.Drawing.Point(105, 243);
            this.chbIsWritable.Name = "chbIsWritable";
            this.chbIsWritable.Size = new System.Drawing.Size(15, 14);
            this.chbIsWritable.TabIndex = 17;
            this.chbIsWritable.UseVisualStyleBackColor = true;
            // 
            // pnlActions
            // 
            this.pnlActions.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pnlActions.Controls.Add(this.btnClose);
            this.pnlActions.Controls.Add(this.btnAdd);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 271);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(239, 30);
            this.pnlActions.TabIndex = 20;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(123, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 23);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(178, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(48, 23);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "Save";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtTagName
            // 
            this.txtTagName.Location = new System.Drawing.Point(105, 28);
            this.txtTagName.Name = "txtTagName";
            this.txtTagName.Size = new System.Drawing.Size(121, 20);
            this.txtTagName.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Tag Name";
            // 
            // TagForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 301);
            this.ControlBox = false;
            this.Controls.Add(this.txtTagName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.chbIsWritable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDataLength);
            this.Controls.Add(this.lblDataLength);
            this.Controls.Add(this.cmbByteOrder);
            this.Controls.Add(this.lblByteOrder);
            this.Controls.Add(this.cmbDataType);
            this.Controls.Add(this.lblDataType);
            this.Controls.Add(this.cmbRegisterType);
            this.Controls.Add(this.lblRegisteryType);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtSlaveId);
            this.Controls.Add(this.lblSlaveId);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.lblIp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TagForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tag Info";
            this.Load += new System.EventHandler(this.TagForm_Load);
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtSlaveId;
        private System.Windows.Forms.Label lblSlaveId;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblRegisteryType;
        private System.Windows.Forms.ComboBox cmbRegisterType;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.ComboBox cmbByteOrder;
        private System.Windows.Forms.Label lblByteOrder;
        private System.Windows.Forms.TextBox txtDataLength;
        private System.Windows.Forms.Label lblDataLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbIsWritable;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtTagName;
        private System.Windows.Forms.Label label2;
    }
}