namespace ModbusOperationsApp
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnWrite = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblIntervalUnit = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.chboxInterval = new System.Windows.Forms.CheckBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.gvList = new System.Windows.Forms.DataGridView();
            this.tmrIntervalControl = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialogForm = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogForm = new System.Windows.Forms.OpenFileDialog();
            this.pboxLoading = new System.Windows.Forms.PictureBox();
            this.Ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SlaveId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegisterType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ByteOrder = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RegisterLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsWritable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewImageColumn();
            this.DataValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnWrite);
            this.panel1.Controls.Add(this.lblResult);
            this.panel1.Controls.Add(this.lblIntervalUnit);
            this.panel1.Controls.Add(this.txtInterval);
            this.panel1.Controls.Add(this.chboxInterval);
            this.panel1.Controls.Add(this.btnRead);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 333);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(927, 48);
            this.panel1.TabIndex = 2;
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWrite.Location = new System.Drawing.Point(655, 3);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(79, 42);
            this.btnWrite.TabIndex = 5;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult.ForeColor = System.Drawing.Color.Red;
            this.lblResult.Location = new System.Drawing.Point(3, 4);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(571, 41);
            this.lblResult.TabIndex = 4;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIntervalUnit
            // 
            this.lblIntervalUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIntervalUnit.AutoSize = true;
            this.lblIntervalUnit.Location = new System.Drawing.Point(817, 29);
            this.lblIntervalUnit.Name = "lblIntervalUnit";
            this.lblIntervalUnit.Size = new System.Drawing.Size(24, 13);
            this.lblIntervalUnit.TabIndex = 3;
            this.lblIntervalUnit.Text = "(sn)";
            // 
            // txtInterval
            // 
            this.txtInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInterval.Location = new System.Drawing.Point(784, 25);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(31, 20);
            this.txtInterval.TabIndex = 2;
            this.txtInterval.Text = "10";
            // 
            // chboxInterval
            // 
            this.chboxInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chboxInterval.AutoSize = true;
            this.chboxInterval.Location = new System.Drawing.Point(784, 3);
            this.chboxInterval.Name = "chboxInterval";
            this.chboxInterval.Size = new System.Drawing.Size(68, 17);
            this.chboxInterval.TabIndex = 1;
            this.chboxInterval.Text = "Continue";
            this.chboxInterval.UseVisualStyleBackColor = true;
            this.chboxInterval.CheckedChanged += new System.EventHandler(this.chboxInterval_CheckedChanged);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRead.Location = new System.Drawing.Point(854, 3);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(70, 42);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // gvList
            // 
            this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ip,
            this.Port,
            this.SlaveId,
            this.Address,
            this.RegisterType,
            this.DataType,
            this.ByteOrder,
            this.RegisterLength,
            this.IsWritable,
            this.Status,
            this.DataValue,
            this.Description});
            this.gvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvList.Location = new System.Drawing.Point(0, 24);
            this.gvList.Name = "gvList";
            this.gvList.Size = new System.Drawing.Size(927, 309);
            this.gvList.TabIndex = 3;
            this.gvList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvList_CellContentDoubleClick);
            this.gvList.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gvList_DataError);
            this.gvList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvList_EditingControlShowing);
            // 
            // tmrIntervalControl
            // 
            this.tmrIntervalControl.Interval = 1000;
            this.tmrIntervalControl.Tick += new System.EventHandler(this.tmrIntervalControl_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(927, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // saveFileDialogForm
            // 
            this.saveFileDialogForm.DefaultExt = "json";
            this.saveFileDialogForm.FileName = "ModbusOperationsAppSettings";
            this.saveFileDialogForm.Filter = "Setting Files (*.json)|*.json";
            // 
            // openFileDialogForm
            // 
            this.openFileDialogForm.DefaultExt = "json";
            this.openFileDialogForm.FileName = "ModbusOperationsAppSettings";
            this.openFileDialogForm.Filter = "Setting Files (*.json)|*.json";
            // 
            // pboxLoading
            // 
            this.pboxLoading.BackColor = System.Drawing.Color.Transparent;
            this.pboxLoading.Image = global::ModbusOperationsApp.Properties.Resources.loading;
            this.pboxLoading.Location = new System.Drawing.Point(424, 124);
            this.pboxLoading.Name = "pboxLoading";
            this.pboxLoading.Size = new System.Drawing.Size(124, 96);
            this.pboxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxLoading.TabIndex = 6;
            this.pboxLoading.TabStop = false;
            // 
            // Ip
            // 
            this.Ip.HeaderText = "Ip";
            this.Ip.Name = "Ip";
            this.Ip.Width = 85;
            // 
            // Port
            // 
            this.Port.HeaderText = "Port";
            this.Port.Name = "Port";
            this.Port.Width = 40;
            // 
            // SlaveId
            // 
            this.SlaveId.HeaderText = "SlaveId";
            this.SlaveId.Name = "SlaveId";
            this.SlaveId.Width = 50;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.Width = 50;
            // 
            // RegisterType
            // 
            this.RegisterType.HeaderText = "RegisterType";
            this.RegisterType.Name = "RegisterType";
            this.RegisterType.Width = 85;
            // 
            // DataType
            // 
            this.DataType.HeaderText = "DataType";
            this.DataType.Name = "DataType";
            this.DataType.Width = 80;
            // 
            // ByteOrder
            // 
            this.ByteOrder.HeaderText = "ByteOrder";
            this.ByteOrder.Name = "ByteOrder";
            this.ByteOrder.Width = 80;
            // 
            // RegisterLength
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RegisterLength.DefaultCellStyle = dataGridViewCellStyle1;
            this.RegisterLength.HeaderText = "Length";
            this.RegisterLength.Name = "RegisterLength";
            this.RegisterLength.ReadOnly = true;
            this.RegisterLength.Width = 45;
            // 
            // IsWritable
            // 
            this.IsWritable.HeaderText = "IsWritable";
            this.IsWritable.Name = "IsWritable";
            this.IsWritable.ReadOnly = true;
            this.IsWritable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsWritable.Width = 65;
            // 
            // Status
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle2;
            this.Status.HeaderText = "Status";
            this.Status.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 50;
            // 
            // DataValue
            // 
            this.DataValue.HeaderText = "Value";
            this.DataValue.Name = "DataValue";
            this.DataValue.Width = 75;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 175;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 381);
            this.Controls.Add(this.pboxLoading);
            this.Controls.Add(this.gvList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Modbus Operations App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblIntervalUnit;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.CheckBox chboxInterval;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.DataGridView gvList;
        private System.Windows.Forms.Timer tmrIntervalControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogForm;
        private System.Windows.Forms.OpenFileDialog openFileDialogForm;
        private System.Windows.Forms.PictureBox pboxLoading;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlaveId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewComboBoxColumn RegisterType;
        private System.Windows.Forms.DataGridViewComboBoxColumn DataType;
        private System.Windows.Forms.DataGridViewComboBoxColumn ByteOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegisterLength;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsWritable;
        private System.Windows.Forms.DataGridViewImageColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}

