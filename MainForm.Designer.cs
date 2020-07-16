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
            this.btnAddTag = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.lblIntervalUnit = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.chboxInterval = new System.Windows.Forms.CheckBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.gvList = new System.Windows.Forms.DataGridView();
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
            this.TagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmrIntervalControl = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelectGridView = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelectCardView = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialogForm = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogForm = new System.Windows.Forms.OpenFileDialog();
            this.flpnlCardViewList = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.btnAddTag);
            this.panel1.Controls.Add(this.btnWrite);
            this.panel1.Controls.Add(this.lblIntervalUnit);
            this.panel1.Controls.Add(this.txtInterval);
            this.panel1.Controls.Add(this.chboxInterval);
            this.panel1.Controls.Add(this.btnRead);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 376);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 48);
            this.panel1.TabIndex = 2;
            // 
            // btnAddTag
            // 
            this.btnAddTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTag.Location = new System.Drawing.Point(3, 3);
            this.btnAddTag.Name = "btnAddTag";
            this.btnAddTag.Size = new System.Drawing.Size(86, 42);
            this.btnAddTag.TabIndex = 6;
            this.btnAddTag.Text = "Add Tag";
            this.btnAddTag.UseVisualStyleBackColor = true;
            this.btnAddTag.Click += new System.EventHandler(this.btnAddTag_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWrite.Location = new System.Drawing.Point(720, 3);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(86, 42);
            this.btnWrite.TabIndex = 5;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // lblIntervalUnit
            // 
            this.lblIntervalUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIntervalUnit.AutoSize = true;
            this.lblIntervalUnit.Location = new System.Drawing.Point(874, 28);
            this.lblIntervalUnit.Name = "lblIntervalUnit";
            this.lblIntervalUnit.Size = new System.Drawing.Size(26, 13);
            this.lblIntervalUnit.TabIndex = 3;
            this.lblIntervalUnit.Text = "(ms)";
            // 
            // txtInterval
            // 
            this.txtInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInterval.Location = new System.Drawing.Point(841, 24);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(31, 20);
            this.txtInterval.TabIndex = 2;
            this.txtInterval.Text = "300";
            // 
            // chboxInterval
            // 
            this.chboxInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chboxInterval.AutoSize = true;
            this.chboxInterval.Location = new System.Drawing.Point(841, 5);
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
            this.btnRead.Location = new System.Drawing.Point(911, 3);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(70, 42);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // gvList
            // 
            this.gvList.AllowUserToAddRows = false;
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
            this.TagName,
            this.DataValue,
            this.Description,
            this.UpdateDate});
            this.gvList.Dock = System.Windows.Forms.DockStyle.Top;
            this.gvList.Location = new System.Drawing.Point(0, 24);
            this.gvList.MultiSelect = false;
            this.gvList.Name = "gvList";
            this.gvList.ReadOnly = true;
            this.gvList.Size = new System.Drawing.Size(984, 151);
            this.gvList.TabIndex = 3;
            this.gvList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvList_CellContentDoubleClick);
            this.gvList.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gvList_DataError);
            this.gvList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvList_EditingControlShowing);
            this.gvList.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.gvList_UserDeletingRow);
            // 
            // Ip
            // 
            this.Ip.HeaderText = "Ip";
            this.Ip.Name = "Ip";
            this.Ip.ReadOnly = true;
            this.Ip.Width = 85;
            // 
            // Port
            // 
            this.Port.HeaderText = "Port";
            this.Port.Name = "Port";
            this.Port.ReadOnly = true;
            this.Port.Width = 40;
            // 
            // SlaveId
            // 
            this.SlaveId.HeaderText = "SlaveId";
            this.SlaveId.Name = "SlaveId";
            this.SlaveId.ReadOnly = true;
            this.SlaveId.Width = 45;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 50;
            // 
            // RegisterType
            // 
            this.RegisterType.HeaderText = "RegisterType";
            this.RegisterType.Name = "RegisterType";
            this.RegisterType.ReadOnly = true;
            this.RegisterType.Width = 85;
            // 
            // DataType
            // 
            this.DataType.HeaderText = "DataType";
            this.DataType.Name = "DataType";
            this.DataType.ReadOnly = true;
            this.DataType.Width = 70;
            // 
            // ByteOrder
            // 
            this.ByteOrder.HeaderText = "ByteOrder";
            this.ByteOrder.Name = "ByteOrder";
            this.ByteOrder.ReadOnly = true;
            this.ByteOrder.Width = 70;
            // 
            // RegisterLength
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RegisterLength.DefaultCellStyle = dataGridViewCellStyle1;
            this.RegisterLength.HeaderText = "Length";
            this.RegisterLength.Name = "RegisterLength";
            this.RegisterLength.ReadOnly = true;
            this.RegisterLength.Width = 42;
            // 
            // IsWritable
            // 
            this.IsWritable.HeaderText = "Writable";
            this.IsWritable.Name = "IsWritable";
            this.IsWritable.ReadOnly = true;
            this.IsWritable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsWritable.Width = 50;
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
            // TagName
            // 
            this.TagName.HeaderText = "Tag";
            this.TagName.Name = "TagName";
            this.TagName.ReadOnly = true;
            // 
            // DataValue
            // 
            this.DataValue.HeaderText = "Value";
            this.DataValue.Name = "DataValue";
            this.DataValue.ReadOnly = true;
            this.DataValue.Width = 75;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 150;
            // 
            // UpdateDate
            // 
            this.UpdateDate.HeaderText = "Date";
            this.UpdateDate.Name = "UpdateDate";
            this.UpdateDate.ReadOnly = true;
            this.UpdateDate.Width = 125;
            // 
            // tmrIntervalControl
            // 
            this.tmrIntervalControl.Interval = 1000;
            this.tmrIntervalControl.Tick += new System.EventHandler(this.tmrIntervalControl_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.modeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
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
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelectGridView,
            this.btnSelectCardView});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // btnSelectGridView
            // 
            this.btnSelectGridView.Name = "btnSelectGridView";
            this.btnSelectGridView.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.G)));
            this.btnSelectGridView.Size = new System.Drawing.Size(201, 22);
            this.btnSelectGridView.Text = "Grid View";
            this.btnSelectGridView.Click += new System.EventHandler(this.btnSelectGridView_Click);
            // 
            // btnSelectCardView
            // 
            this.btnSelectCardView.Name = "btnSelectCardView";
            this.btnSelectCardView.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.btnSelectCardView.Size = new System.Drawing.Size(201, 22);
            this.btnSelectCardView.Text = "Card View";
            this.btnSelectCardView.Click += new System.EventHandler(this.btnSelectCardView_Click);
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
            // flpnlCardViewList
            // 
            this.flpnlCardViewList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpnlCardViewList.Location = new System.Drawing.Point(0, 175);
            this.flpnlCardViewList.Name = "flpnlCardViewList";
            this.flpnlCardViewList.Size = new System.Drawing.Size(984, 201);
            this.flpnlCardViewList.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 424);
            this.Controls.Add(this.flpnlCardViewList);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.FlowLayoutPanel flpnlCardViewList;
        private System.Windows.Forms.Button btnAddTag;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnSelectGridView;
        private System.Windows.Forms.ToolStripMenuItem btnSelectCardView;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn TagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateDate;
    }
}

