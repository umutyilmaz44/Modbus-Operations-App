using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModbusOperationsApp.Utility;

namespace ModbusOperationsApp
{
    public partial class CardViewItem : UserControl
    {
        public ModbusInfo modbusInfo { get; }
        public DataMapInfo dataMapInfo { get; }
        public delegate void OnRemoveEventHandler(object sender, OnCardViewEventArgs e);
        public event OnRemoveEventHandler OnRemove;

        public delegate void OnEditEventHandler(object sender, OnCardViewEventArgs e);
        public event OnEditEventHandler OnEdit;

        public delegate void OnWriteRequestEventHandler(object sender, OnCardViewEventArgs e);
        public event OnWriteRequestEventHandler OnWriteRequest;

        public CardViewItem(ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            InitializeComponent();

            this.modbusInfo = modbusInfo;
            this.dataMapInfo = dataMapInfo;
        }

        private void CardViewItem_Load(object sender, EventArgs e)
        {
            LoadTagInfo(this.modbusInfo, this.dataMapInfo);
        }

        public void LoadTagInfo(ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            btnWrite.Enabled = false;

            lblTagName.Text = dataMapInfo.TagName;
            lblDatetime.Text = "";
            lblInfo.Text = string.Format("{0} : {1}\r\nSlave Id: {2}\r\nAddress: {3} : {4}", modbusInfo.ip, modbusInfo.port, dataMapInfo.SlaveId, dataMapInfo.Address, dataMapInfo.DataType.ToString());

            chbWritable.Enabled = dataMapInfo.IsWritable;
            btnWrite.Enabled = dataMapInfo.IsWritable && chbWritable.Checked;
            txtValue.Enabled = dataMapInfo.IsWritable && chbWritable.Checked;
        }

        private void chbWritable_CheckedChanged(object sender, EventArgs e)
        {
            btnWrite.Enabled = chbWritable.Checked;
            txtValue.Enabled = chbWritable.Checked;
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            string valueText = txtValue.Text.Trim().Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                                                   .Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                        
            if (OnWriteRequest != null)
                OnWriteRequest(this, new OnCardViewEventArgs(this.modbusInfo, this.dataMapInfo, valueText));
        }

        private void btnRemoveTag_Click(object sender, EventArgs e)
        {
            if (OnRemove != null)
                OnRemove(this, new OnCardViewEventArgs(this.modbusInfo, this.dataMapInfo));
        }

        public void SetValue(object value, DateTime dateTime)
        {
            txtValue.Text = value.ToString();
            lblDatetime.Text = dateTime.ToString("yyyy.MM.dd HH:mm:ss.fff");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (OnEdit != null)
                OnEdit(this, new OnCardViewEventArgs(this.modbusInfo, this.dataMapInfo));
        }
    }

    public class OnCardViewEventArgs : EventArgs
    {
        public ModbusInfo modbusInfo { get; private set; }
        public DataMapInfo dataMapInfo { get; private set; }

        public string Value { get; private set; }

        public OnCardViewEventArgs(ModbusInfo modbusInfo, DataMapInfo dataMapInfo, string value = "")
        {
            this.modbusInfo = modbusInfo;
            this.dataMapInfo = dataMapInfo;
            this.Value = value;
        }
    }
}
