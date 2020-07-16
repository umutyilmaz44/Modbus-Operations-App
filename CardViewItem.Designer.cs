namespace ModbusOperationsApp
{
    partial class CardViewItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTagName = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.chbWritable = new System.Windows.Forms.CheckBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.lblDatetime = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemoveTag = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTagName
            // 
            this.lblTagName.AutoSize = true;
            this.lblTagName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagName.Location = new System.Drawing.Point(39, 22);
            this.lblTagName.Name = "lblTagName";
            this.lblTagName.Size = new System.Drawing.Size(81, 16);
            this.lblTagName.TabIndex = 0;
            this.lblTagName.Text = "Tag Name";
            // 
            // txtValue
            // 
            this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.Location = new System.Drawing.Point(5, 44);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(153, 31);
            this.txtValue.TabIndex = 1;
            // 
            // chbWritable
            // 
            this.chbWritable.AutoSize = true;
            this.chbWritable.Location = new System.Drawing.Point(5, 146);
            this.chbWritable.Name = "chbWritable";
            this.chbWritable.Size = new System.Drawing.Size(65, 17);
            this.chbWritable.TabIndex = 2;
            this.chbWritable.Text = "Writable";
            this.chbWritable.UseVisualStyleBackColor = true;
            this.chbWritable.CheckedChanged += new System.EventHandler(this.chbWritable_CheckedChanged);
            // 
            // btnWrite
            // 
            this.btnWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWrite.Location = new System.Drawing.Point(103, 139);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(56, 23);
            this.btnWrite.TabIndex = 3;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // lblDatetime
            // 
            this.lblDatetime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatetime.Location = new System.Drawing.Point(5, 79);
            this.lblDatetime.Name = "lblDatetime";
            this.lblDatetime.Size = new System.Drawing.Size(157, 16);
            this.lblDatetime.TabIndex = 4;
            this.lblDatetime.Text = "Date time";
            this.lblDatetime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(5, 99);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(157, 39);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "192.168.0.2 : 502\r\nSlave Id: 1\r\nAddress: 12336 : Float";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = global::ModbusOperationsApp.Properties.Resources.editx16;
            this.btnEdit.Location = new System.Drawing.Point(122, 1);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(20, 20);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRemoveTag
            // 
            this.btnRemoveTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveTag.Image = global::ModbusOperationsApp.Properties.Resources.removex16;
            this.btnRemoveTag.Location = new System.Drawing.Point(144, 1);
            this.btnRemoveTag.Name = "btnRemoveTag";
            this.btnRemoveTag.Size = new System.Drawing.Size(20, 20);
            this.btnRemoveTag.TabIndex = 6;
            this.btnRemoveTag.UseVisualStyleBackColor = true;
            this.btnRemoveTag.Click += new System.EventHandler(this.btnRemoveTag_Click);
            // 
            // CardViewItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnRemoveTag);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblDatetime);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.chbWritable);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblTagName);
            this.Name = "CardViewItem";
            this.Size = new System.Drawing.Size(163, 164);
            this.Load += new System.EventHandler(this.CardViewItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTagName;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.CheckBox chbWritable;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label lblDatetime;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnRemoveTag;
        private System.Windows.Forms.Button btnEdit;
    }
}
