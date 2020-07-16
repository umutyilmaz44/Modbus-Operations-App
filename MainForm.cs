using Modbus.Device;
using ModbusOperationsApp.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ModbusOperationsApp
{
    public partial class MainForm : Form
    {
        TagForm tagForm;
        List<ModbusInfo> modbusInfoList = new List<ModbusInfo>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
        }

        private void InitForm()
        {
            tagForm = null;
            btnSelectGridView_Click(null, null);

            foreach (string dataType in Enum.GetNames(typeof(DataType)))
            {
                this.DataType.Items.Add(dataType);
            }

            List<string> byteOrders = Enum.GetNames(typeof(Utility.ByteOrder)).ToList();
            byteOrders = byteOrders.Where(x => x != "AB" && x != "BA").ToList();
            foreach (string byteOrder in byteOrders)
            {
                this.ByteOrder.Items.Add(byteOrder);
            }

            foreach (string registerType in Enum.GetNames(typeof(RegisterType)))
            {
                this.RegisterType.Items.Add(registerType);
            }
        }


        public void AddModbusInfo(ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            modbusInfo.DataMapInfoList.Add(dataMapInfo);                
            modbusInfoList.Add(modbusInfo);

            int rowId;
            CheckModbusInfoOnGridView(modbusInfo, dataMapInfo, out rowId);
            CheckModbusInfoOnCardView(modbusInfo, dataMapInfo);

            dataMapInfo.rowIndex = rowId;
        }

        public void UpdateModbusInfo(ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            int rowId;
            CheckModbusInfoOnGridView(modbusInfo, dataMapInfo, out rowId);
            CheckModbusInfoOnCardView(modbusInfo, dataMapInfo);
            dataMapInfo.rowIndex = rowId;
        }
        public void RemoveModbusInfo(ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            ModbusInfo existModbusInfo = modbusInfoList.FirstOrDefault(x => x.ip == modbusInfo.ip && x.port == modbusInfo.port);
            if (existModbusInfo != null)
            {
                modbusInfoList.Remove(modbusInfo);
                DataMapInfo existedDataMapInfo = existModbusInfo.DataMapInfoList.FirstOrDefault(x => x.SlaveId == dataMapInfo.SlaveId && x.Address == dataMapInfo.Address);
                if (existedDataMapInfo != null)
                {
                    existModbusInfo.DataMapInfoList.Remove(existedDataMapInfo);
                }

                RemoveModbusInfoOnGridView(modbusInfo, dataMapInfo);
            }
        }

        public bool CheckExistModbusInfo(ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            ModbusInfo existModbusInfo = modbusInfoList.FirstOrDefault(x => x.ip == modbusInfo.ip && x.port == modbusInfo.port);
            if (existModbusInfo == null)
            {
                return false;
            }
            else 
            {
                return existModbusInfo.DataMapInfoList.Any(x => x.SlaveId == dataMapInfo.SlaveId && x.Address == dataMapInfo.Address);
            }
        }

        private async void tmrIntervalControl_Tick(object sender, EventArgs e)
        {
            await ReadModbusList();
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvList.Columns[e.ColumnIndex].DataPropertyName == "Description" && !string.IsNullOrEmpty(gvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString().Trim()))
            {
                MessageBox.Show(gvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString().Trim(), "Description Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string ip = gvList.Rows[e.RowIndex].Cells["Ip"].Value?.ToString().Trim();
                string portText = gvList.Rows[e.RowIndex].Cells["Port"].Value?.ToString().Trim();
                string slaveIdText = gvList.Rows[e.RowIndex].Cells["SlaveId"].Value?.ToString().Trim();
                string addressText = gvList.Rows[e.RowIndex].Cells["Address"].Value?.ToString().Trim();
                int port;
                ushort slaveId, address;

                if (!Int32.TryParse(portText, out port))
                    return;
                if (!ushort.TryParse(slaveIdText, out slaveId))
                    return;
                if (!ushort.TryParse(addressText, out address))
                    return;

                ModbusInfo existModbusInfo = modbusInfoList.FirstOrDefault(x => x.ip == ip && x.port == port);
                if (existModbusInfo != null)
                {
                    DataMapInfo existedDataMapInfo = existModbusInfo.DataMapInfoList.FirstOrDefault(x => x.SlaveId == slaveId && x.Address == address);
                    if (existedDataMapInfo != null)
                    {
                        ViewTagInfo(existModbusInfo, existedDataMapInfo);
                    }
                }                
            }
        }

        public void ViewTagInfo(ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            if (tagForm != null)
            {
                tagForm.Close();
                tagForm.Dispose();
            }

            tagForm = new TagForm(this, modbusInfo, dataMapInfo);
            tagForm.ShowDialog();
            tagForm.Close();
            tagForm.Dispose();
            tagForm = null;
        }

        private void gvList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void gvList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string ip = gvList.Rows[e.Row.Index].Cells["Ip"].Value?.ToString().Trim();
            string portText = gvList.Rows[e.Row.Index].Cells["Port"].Value?.ToString().Trim();
            string slaveIdText = gvList.Rows[e.Row.Index].Cells["SlaveId"].Value?.ToString().Trim();
            string addressText = gvList.Rows[e.Row.Index].Cells["Address"].Value?.ToString().Trim();
            int port;
            ushort slaveId, address;

            if (!Int32.TryParse(portText, out port))
                return;
            if (!ushort.TryParse(slaveIdText, out slaveId))
                return;
            if (!ushort.TryParse(addressText, out address))
                return;

            ModbusInfo existModbusInfo = modbusInfoList.FirstOrDefault(x => x.ip == ip && x.port == port);
            if (existModbusInfo != null)
            {
                DataMapInfo existedDataMapInfo = existModbusInfo.DataMapInfoList.FirstOrDefault(x => x.SlaveId == slaveId && x.Address == address);
                if (existedDataMapInfo != null)
                {
                    RemoveModbusInfoOnCardView(existModbusInfo, existedDataMapInfo);
                }
            }
        }

        private void gvList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gvList.CurrentCell.ColumnIndex == gvList.Columns["DataType"].Index && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= DataType_OnChange;
                comboBox.SelectedIndexChanged += DataType_OnChange;
            }
            else if (gvList.CurrentCell.ColumnIndex == gvList.Columns["RegisterType"].Index && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= RegisterType_OnChange;
                comboBox.SelectedIndexChanged += RegisterType_OnChange;
            }
        }

        private void DataType_OnChange(object sender, EventArgs e)
        {
            if (gvList.CurrentCellAddress.X == gvList.Columns["DataType"].Index)
            {
                DataType dataType;
                var currentcell = gvList.CurrentCellAddress;
                var cmbDataTypeControl = sender as DataGridViewComboBoxEditingControl;
                System.Enum.TryParse<DataType>(cmbDataTypeControl.EditingControlFormattedValue.ToString(), out dataType);

                CheckControl_OnChangeDataType(currentcell.Y, dataType);
            }
        }
        private void CheckControl_OnChangeDataType(int rowIndex, DataType dataType)
        {
            DataGridViewTextBoxCell lengthCell = (DataGridViewTextBoxCell)gvList.Rows[rowIndex].Cells["RegisterLength"];
            DataGridViewComboBoxCell byteOrderCell = (DataGridViewComboBoxCell)(gvList.Rows[rowIndex].Cells["ByteOrder"]);

            switch (dataType)
            {
                case Utility.DataType.Int16:
                case Utility.DataType.UInt16:
                    lengthCell.Value = "1";
                    lengthCell.ReadOnly = true;
                    break;
                case Utility.DataType.Int32:
                case Utility.DataType.UInt32:
                case Utility.DataType.Float:
                    lengthCell.Value = "2";
                    lengthCell.ReadOnly = true;
                    break;
                case Utility.DataType.Int64:
                case Utility.DataType.UInt64:
                case Utility.DataType.Double:
                    lengthCell.Value = "4";
                    lengthCell.ReadOnly = true;
                    break;
                case Utility.DataType.String:
                    lengthCell.Value = "";
                    lengthCell.ReadOnly = false;
                    break;
            }

            byteOrderCell.Items.Clear();
            switch (dataType)
            {
                case Utility.DataType.Int16:
                case Utility.DataType.UInt16:
                    {                        
                        Utility.ByteOrder[] byteOrders = new ByteOrder[] { Utility.ByteOrder.AB, Utility.ByteOrder.BA };
                        for (int i = 0; i < byteOrders.Length; i++)
                        {
                            byteOrderCell.Items.Add(Enum.GetName(typeof(ByteOrder), byteOrders[i]));
                        }
                    }
                    break;
                default:
                    {
                        List<string> byteOrders = Enum.GetNames(typeof(Utility.ByteOrder)).ToList();
                        byteOrders = byteOrders.Where(x => x != "AB" && x != "BA").ToList();
                        foreach (string byteOrder in byteOrders)
                        {
                            byteOrderCell.Items.Add(byteOrder);
                        }
                    }
                    break;
            }
        }
        
        private void RegisterType_OnChange(object sender, EventArgs e)
        {
            if (gvList.CurrentCellAddress.X == gvList.Columns["RegisterType"].Index)
            {
                var currentcell = gvList.CurrentCellAddress;
                RegisterType registerType;
                var cmbDataTypeControl = sender as DataGridViewComboBoxEditingControl;
                System.Enum.TryParse<RegisterType>(cmbDataTypeControl.EditingControlFormattedValue.ToString(), out registerType);

                CheckControl_OnChangeRegisterType(currentcell.Y, registerType);
            }
        }
        private void CheckControl_OnChangeRegisterType(int rowIndex, RegisterType registerType)
        {
            DataGridViewCheckBoxCell isWritableCell = (DataGridViewCheckBoxCell)gvList.Rows[rowIndex].Cells["IsWritable"];
            switch (registerType)
            {
                case Utility.RegisterType.Coil:
                case Utility.RegisterType.Holding_Register:
                    isWritableCell.ReadOnly = false;
                    break;
                default:
                    isWritableCell.Value = false;
                    isWritableCell.ReadOnly = true;
                    break;
            }
        }

        private void chboxInterval_CheckedChanged(object sender, EventArgs e)
        {
            if (chboxInterval.Checked)
            {
                btnRead.Enabled = false;
                btnWrite.Enabled = false;
                txtInterval.Enabled = false;                
            }
            else
            {
                btnRead.Enabled = true;
                btnWrite.Enabled = true;
                txtInterval.Enabled = true;
            }

            SetTimerStatus();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int port;
            bool isWritable;
            byte slaveId;
            ushort address, registerLength;
            string ip, portText, slaveIdText, addressText, lengthText, tagName;
            string dataTypeText, byteOrderText, registerTypeText, isWritableText;
            string errorText;
            DataType dataType;
            ByteOrder byteOrder;
            RegisterType registerType;
            IPAddress ipAddress;
            ModbusInfo modbusInfo;
            List<ModbusInfo> modbusInfoList = new List<ModbusInfo>();

            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                gvList.Rows[i].Cells["Status"].Value = ModbusOperationsApp.Properties.Resources.sandglass;

                #region Validation Control
                errorText = "";
                ip = gvList.Rows[i].Cells["Ip"].Value?.ToString();
                portText = gvList.Rows[i].Cells["Port"].Value?.ToString();
                tagName = gvList.Rows[i].Cells["TagName"].Value?.ToString();
                slaveIdText = gvList.Rows[i].Cells["SlaveId"].Value?.ToString();
                addressText = gvList.Rows[i].Cells["Address"].Value?.ToString();
                lengthText = gvList.Rows[i].Cells["RegisterLength"].Value?.ToString();
                dataTypeText = gvList.Rows[i].Cells["DataType"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["DataType"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["DataType"].Value?.ToString();

                byteOrderText = gvList.Rows[i].Cells["ByteOrder"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["ByteOrder"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["ByteOrder"].Value?.ToString();

                registerTypeText = gvList.Rows[i].Cells["RegisterType"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["RegisterType"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["RegisterType"].Value?.ToString();

                isWritableText = gvList.Rows[i].Cells["IsWritable"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["IsWritable"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["IsWritable"].Value?.ToString();

                System.Enum.TryParse<DataType>(dataTypeText, out dataType);
                System.Enum.TryParse<ByteOrder>(byteOrderText, out byteOrder);
                System.Enum.TryParse<RegisterType>(registerTypeText, out registerType);

                isWritable = isWritableText.ToLower() == "true";

                if (!Int32.TryParse(portText, out port))
                {
                    errorText = "Port not valid!";
                }
                if (!IPAddress.TryParse(ip, out ipAddress))
                {
                    errorText = "Ip address not valid!";
                }
                if (!Byte.TryParse(slaveIdText, out slaveId))
                {
                    errorText = "SlaveId not valid!";
                }
                if (!UInt16.TryParse(addressText, out address))
                {
                    errorText = "Address not valid!";
                }
                if (!UInt16.TryParse(lengthText, out registerLength))
                {
                    errorText = "Register lLength not valid!";
                }
                if (registerType == 0)
                {
                    errorText = "Register type not selected!";
                }

                if (!string.IsNullOrEmpty(errorText))
                {
                    gvList.Invoke((Action)(() =>
                    {
                        gvList.Rows[i].Cells["Status"].Value = ModbusOperationsApp.Properties.Resources.error;
                        gvList.Rows[i].Cells["Description"].Value = errorText;
                    }));
                    continue;
                }
                modbusInfo = modbusInfoList.FirstOrDefault(x => x.ip == ip && x.port == port);
                if (modbusInfo == null)
                {
                    modbusInfo = new ModbusInfo(ip, port);
                    modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, tagName, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                    modbusInfoList.Add(modbusInfo);
                }
                else if (!modbusInfo.DataMapInfoList.Any(x => x.SlaveId == slaveId && x.Address == address))
                {
                    modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, tagName, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                }
                #endregion
            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(modbusInfoList, Newtonsoft.Json.Formatting.Indented);

            try
            {
                if (saveFileDialogForm.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialogForm.FileName;
                    using (StreamWriter sw = new StreamWriter(saveFileDialogForm.FileName))
                    {
                        sw.WriteLine(json);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialogForm.FileName))
                    {
                        int rowId;
                        string dataText = sr.ReadToEnd();
                        this.modbusInfoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModbusInfo>>(dataText);

                        gvList.Rows.Clear();
                        foreach (ModbusInfo modbusInfo in modbusInfoList)
                        {
                            foreach (DataMapInfo dataMapInfo in modbusInfo.DataMapInfoList)
                            {
                                dataMapInfo.rowIndex = -1;
                                CheckModbusInfoOnGridView(modbusInfo, dataMapInfo, out rowId);
                                CheckModbusInfoOnCardView(modbusInfo, dataMapInfo);

                                dataMapInfo.rowIndex = rowId;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnRead_Click(object sender, EventArgs e)
        {   
            await ReadModbusList();

            SetTimerStatus();
        }

        private async void btnWrite_Click(object sender, EventArgs e)
        {
            btnRead.Enabled = false;
            btnWrite.Enabled = false;
            txtInterval.Enabled = false;

            await WriteModbusList();

            btnRead.Enabled = true;
            btnWrite.Enabled = true;
            txtInterval.Enabled = true;
        }

        private void btnAddTag_Click(object sender, EventArgs e)
        {
            if (tagForm != null)
            {
                tagForm.Close();
                tagForm.Dispose();
                tagForm = null;                
            }
            tagForm = new TagForm(this);
            tagForm.ShowDialog();            
        }

        private void btnSelectGridView_Click(object sender, EventArgs e)
        {
            gvList.Dock = DockStyle.Fill;
            gvList.Visible = true;

            flpnlCardViewList.Dock = DockStyle.None;
            flpnlCardViewList.Visible = false;
                        
            btnWrite.Visible = true;            
        }

        private void btnSelectCardView_Click(object sender, EventArgs e)
        {
            gvList.Dock = DockStyle.None;
            gvList.Visible = false;

            flpnlCardViewList.Dock = DockStyle.Fill;
            flpnlCardViewList.Visible = true;
                        
            btnWrite.Visible = false;            
        }

        private void SetTimerStatus()
        {
            int intervals = 1;
            Int32.TryParse(txtInterval.Text, out intervals);
            if (intervals < 100)
            {
                intervals = 100;
                txtInterval.Text = "100";
            }
            tmrIntervalControl.Interval = intervals;

            tmrIntervalControl.Enabled = chboxInterval.Checked;
        }

        private async Task ReadModbusList()
        {
            int port;
            bool isWritable;
            byte slaveId;
            ushort address, registerLength;
            string ip, portText, slaveIdText, addressText, lengthText, tagName;
            string dataTypeText, byteOrderText, registerTypeText, isWritableText;
            string errorText;
            DataType dataType;
            ByteOrder byteOrder;
            RegisterType registerType;
            IPAddress ipAddress;
            ModbusInfo modbusInfo;
            List<ModbusInfo> modbusInfoList = new List<ModbusInfo>();
            
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                gvList.Rows[i].Cells["Status"].Value = ModbusOperationsApp.Properties.Resources.sandglass;

                #region Validation Control
                errorText = "";
                ip = gvList.Rows[i].Cells["Ip"].Value?.ToString();
                portText = gvList.Rows[i].Cells["Port"].Value?.ToString();
                tagName = gvList.Rows[i].Cells["TagName"].Value?.ToString();
                slaveIdText = gvList.Rows[i].Cells["SlaveId"].Value?.ToString();
                addressText = gvList.Rows[i].Cells["Address"].Value?.ToString();
                lengthText = gvList.Rows[i].Cells["RegisterLength"].Value?.ToString();
                
                dataTypeText = gvList.Rows[i].Cells["DataType"].EditedFormattedValue != null
                               ? gvList.Rows[i].Cells["DataType"].EditedFormattedValue.ToString()
                               : gvList.Rows[i].Cells["DataType"].Value?.ToString();

                byteOrderText = gvList.Rows[i].Cells["ByteOrder"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["ByteOrder"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["ByteOrder"].Value?.ToString();

                registerTypeText = gvList.Rows[i].Cells["RegisterType"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["RegisterType"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["RegisterType"].Value?.ToString();

                isWritableText = gvList.Rows[i].Cells["IsWritable"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["IsWritable"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["IsWritable"].Value?.ToString();

                System.Enum.TryParse<DataType>(dataTypeText, out dataType);
                System.Enum.TryParse<ByteOrder>(byteOrderText, out byteOrder);
                System.Enum.TryParse<RegisterType>(registerTypeText, out registerType);

                isWritable = isWritableText.ToLower() == "true";

                if (!Int32.TryParse(portText, out port))
                {
                    errorText = "Port not valid!";
                }
                if (!IPAddress.TryParse(ip, out ipAddress))
                {
                    errorText = "Ip address not valid!";
                }
                if (!Byte.TryParse(slaveIdText, out slaveId))
                {
                    errorText = "SlaveId not valid!";
                }
                if (!UInt16.TryParse(addressText, out address))
                {
                    errorText = "Address not valid!";
                }
                if (!UInt16.TryParse(lengthText, out registerLength))
                {
                    errorText = "Register lLength not valid!";
                }
                if (dataType == 0)
                {
                    errorText = "Data Type not selected!";
                }
                if (byteOrder == 0)
                {
                    errorText = "Byte Order not selected!";
                }
                if (registerType == 0)
                {
                    errorText = "Register type not selected!";
                }

                if (!string.IsNullOrEmpty(errorText))
                {
                    gvList.Invoke((Action)(() =>
                    {
                        gvList.Rows[i].Cells["Status"].Value = ModbusOperationsApp.Properties.Resources.error;
                        gvList.Rows[i].Cells["Description"].Value = errorText;
                    }));
                    continue;
                }
                modbusInfo = modbusInfoList.FirstOrDefault(x => x.ip == ip && x.port == port);
                if (modbusInfo == null)
                {
                    modbusInfo = new ModbusInfo(ip, port);
                    modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, tagName, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                    modbusInfoList.Add(modbusInfo);
                }
                else if (!modbusInfo.DataMapInfoList.Any(x => x.SlaveId == slaveId && x.Address == address))
                {
                    modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, tagName, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                }
                #endregion
            }

            await Task.Factory.StartNew(() =>
                Parallel.ForEach(modbusInfoList, modbusInfoItem =>
                {
                    ReadModbus(modbusInfoItem);
                })
            );
        }

        private async Task WriteModbusList()
        {
            int port;
            bool isWritable;
            byte slaveId;
            ushort address, registerLength;
            string ip, portText, slaveIdText, addressText, lengthText, tagName;
            string dataTypeText, byteOrderText, registerTypeText, isWritableText;
            string errorText;
            DataType dataType;
            ByteOrder byteOrder;
            RegisterType registerType;
            IPAddress ipAddress;
            ModbusInfo modbusInfo;
            List<ModbusInfo> modbusInfoList = new List<ModbusInfo>();

            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                ip = gvList.Rows[i].Cells["Ip"].Value?.ToString();
                portText = gvList.Rows[i].Cells["Port"].Value?.ToString();
                tagName = gvList.Rows[i].Cells["TagName"].Value?.ToString();
                slaveIdText = gvList.Rows[i].Cells["SlaveId"].Value?.ToString();
                addressText = gvList.Rows[i].Cells["Address"].Value?.ToString();
                lengthText = gvList.Rows[i].Cells["RegisterLength"].Value?.ToString();

                dataTypeText = gvList.Rows[i].Cells["DataType"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["DataType"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["DataType"].Value?.ToString();

                byteOrderText = gvList.Rows[i].Cells["ByteOrder"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["ByteOrder"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["ByteOrder"].Value?.ToString();

                registerTypeText = gvList.Rows[i].Cells["RegisterType"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["RegisterType"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["RegisterType"].Value?.ToString();

                isWritableText = gvList.Rows[i].Cells["IsWritable"].EditedFormattedValue != null
                                ? gvList.Rows[i].Cells["IsWritable"].EditedFormattedValue.ToString()
                                : gvList.Rows[i].Cells["IsWritable"].Value?.ToString();

                System.Enum.TryParse<DataType>(dataTypeText, out dataType);
                System.Enum.TryParse<ByteOrder>(byteOrderText, out byteOrder);
                System.Enum.TryParse<RegisterType>(registerTypeText, out registerType);

                isWritable = isWritableText.ToLower() == "true";

                if (isWritable)
                {
                    gvList.Rows[i].Cells["Status"].Value = ModbusOperationsApp.Properties.Resources.sandglass;

                    #region Validation Control
                    errorText = "";

                    if (!Int32.TryParse(portText, out port))
                    {
                        errorText = "Port not valid!";
                    }
                    if (!IPAddress.TryParse(ip, out ipAddress))
                    {
                        errorText = "Ip address not valid!";
                    }
                    if (!Byte.TryParse(slaveIdText, out slaveId))
                    {
                        errorText = "SlaveId not valid!";
                    }
                    if (!UInt16.TryParse(addressText, out address))
                    {
                        errorText = "Address not valid!";
                    }
                    if (!UInt16.TryParse(lengthText, out registerLength))
                    {
                        errorText = "Register lLength not valid!";
                    }
                    if (dataType == 0)
                    {
                        errorText = "Data Type not selected!";
                    }
                    if (byteOrder == 0)
                    {
                        errorText = "Byte Order not selected!";
                    }
                    if (registerType == 0)
                    {
                        errorText = "Register type not selected!";
                    }

                    if (!string.IsNullOrEmpty(errorText))
                    {
                        gvList.Invoke((Action)(() =>
                        {
                            gvList.Rows[i].Cells["Status"].Value = ModbusOperationsApp.Properties.Resources.error;
                            gvList.Rows[i].Cells["Description"].Value = errorText;
                        }));
                        continue;
                    }
                    modbusInfo = modbusInfoList.FirstOrDefault(x => x.ip == ip && x.port == port);
                    if (modbusInfo == null)
                    {
                        modbusInfo = new ModbusInfo(ip, port);
                        modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, tagName, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                        modbusInfoList.Add(modbusInfo);
                    }
                    else if (!modbusInfo.DataMapInfoList.Any(x => x.SlaveId == slaveId && x.Address == address))
                    {
                        modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, tagName, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                    }
                    #endregion

                }
            }
            await Task.Factory.StartNew(() =>
                    Parallel.ForEach(modbusInfoList, modbusInfoItem =>
                    {
                        WriteModbus(modbusInfoItem);
                    })
                );
        }

        private void ReadModbus(ModbusInfo modbusInfo)
        {
            string result = "";
            object resultValue = "";
            byte[] byteDataArray;
            byte[] orderedByteData;

            Bitmap resultImage = null;
            ModbusIpMaster modbusIpMaster;
            UtilityFunctions utilityFunctions = new UtilityFunctions();

            foreach (DataMapInfo dataMapInfo in modbusInfo.DataMapInfoList)
            {
                result = "";
                resultValue = "";
                resultImage = null;

                try
                {
                    modbusIpMaster = ModbusIpMaster.CreateIp(new TcpClient(modbusInfo.ip, modbusInfo.port));

                    switch(dataMapInfo.RegisterType)
                    {
                        case Utility.RegisterType.Coil:
                            bool[] coils = modbusIpMaster.ReadCoils(dataMapInfo.SlaveId, dataMapInfo.Address, dataMapInfo.Length);
                            resultValue = string.Join(",", coils);
                            break;
                        case Utility.RegisterType.Discrete_Input:
                            bool[] discreteInputs = modbusIpMaster.ReadInputs(dataMapInfo.SlaveId, dataMapInfo.Address, dataMapInfo.Length);
                            resultValue = string.Join(",", discreteInputs);
                            break;
                        case Utility.RegisterType.Input_Register:
                            #region Input Register
                            ushort[] inputRegsters = modbusIpMaster.ReadInputRegisters(dataMapInfo.SlaveId, dataMapInfo.Address, dataMapInfo.Length);
                            
                            byteDataArray = utilityFunctions.GetRegisterBytes(inputRegsters);

                            switch (dataMapInfo.DataType)
                            {
                                case Utility.DataType.Int16:
                                    resultValue = utilityFunctions.GetValue<Int16>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.UInt16:
                                    resultValue = utilityFunctions.GetValue<UInt16>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.Int32:
                                    resultValue = utilityFunctions.GetValue<Int32>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.UInt32:
                                    resultValue = utilityFunctions.GetValue<UInt32>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.Int64:
                                    resultValue = utilityFunctions.GetValue<Int64>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.UInt64:
                                    resultValue = utilityFunctions.GetValue<UInt64>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.Double:
                                    resultValue = utilityFunctions.GetValue<Double>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.Float:
                                    resultValue = utilityFunctions.GetValue<float>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.String:
                                    resultValue = utilityFunctions.GetValue<String>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                default:
                                    orderedByteData = new byte[byteDataArray.Length];
                                    Array.Copy(byteDataArray, 0, orderedByteData, 0, byteDataArray.Length);
                                    break;
                            }

                            result = utilityFunctions.ByteArrayToString(byteDataArray.ToArray());
                            #endregion
                            break;
                        case Utility.RegisterType.Holding_Register:
                            #region Holding Register
                            ushort[] holdingRegisters = modbusIpMaster.ReadHoldingRegisters(dataMapInfo.SlaveId, dataMapInfo.Address, dataMapInfo.Length);

                            byteDataArray = utilityFunctions.GetRegisterBytes(holdingRegisters);

                            switch (dataMapInfo.DataType)
                            {
                                case Utility.DataType.Int16:
                                    resultValue = utilityFunctions.GetValue<Int16>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.UInt16:
                                    resultValue = utilityFunctions.GetValue<UInt16>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.Int32:
                                    resultValue = utilityFunctions.GetValue<Int32>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.UInt32:
                                    resultValue = utilityFunctions.GetValue<UInt32>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.Int64:
                                    resultValue = utilityFunctions.GetValue<Int64>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.UInt64:
                                    resultValue = utilityFunctions.GetValue<UInt64>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.Double:
                                    resultValue = utilityFunctions.GetValue<Double>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.Float:
                                    resultValue = utilityFunctions.GetValue<float>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                case Utility.DataType.String:
                                    resultValue = utilityFunctions.GetValue<String>(byteDataArray, dataMapInfo.ByteOrder, out orderedByteData);
                                    break;
                                default:
                                    orderedByteData = new byte[byteDataArray.Length];
                                    Array.Copy(byteDataArray, 0, orderedByteData, 0, byteDataArray.Length);
                                    break;
                            }

                            result = utilityFunctions.ByteArrayToString(byteDataArray.ToArray());
                            #endregion
                            break;
                    }
                    resultImage = ModbusOperationsApp.Properties.Resources.ok;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                    resultValue = "";
                    resultImage = ModbusOperationsApp.Properties.Resources.error;
                }
                finally
                {
                    DateTime updateTime = DateTime.Now;
                    gvList.Invoke((Action)(() =>
                    {
                        gvList.Rows[dataMapInfo.rowIndex].Cells["DataValue"].Value = resultValue;
                        gvList.Rows[dataMapInfo.rowIndex].Cells["Status"].Value = resultImage;
                        gvList.Rows[dataMapInfo.rowIndex].Cells["Description"].Value = result;
                        gvList.Rows[dataMapInfo.rowIndex].Cells["UpdateDate"].Value = updateTime.ToString("HH:mm:ss.fff yyyy.MM.dd");
                    }));

                    flpnlCardViewList.Invoke((Action)(() =>
                    {
                        foreach (CardViewItem item in flpnlCardViewList.Controls)
                        {
                            if (item.modbusInfo.ip == modbusInfo.ip && item.modbusInfo.port == modbusInfo.port &&
                               item.dataMapInfo.SlaveId == dataMapInfo.SlaveId && item.dataMapInfo.Address == dataMapInfo.Address)
                            {
                                item.SetValue(resultValue, updateTime);
                                break;
                            }
                        }
                    }));
                }
            }
        }

        private void WriteModbus(ModbusInfo modbusInfo)
        {
            bool isWritable;
            string result = "", valueText="";            
            Bitmap resultImage = null;
            ModbusIpMaster modbusIpMaster;
            UtilityFunctions utilityFunctions = new UtilityFunctions();

            foreach (DataMapInfo dataMapInfo in modbusInfo.DataMapInfoList)
            {
                result = "";
                resultImage = null;
                try
                {
                    isWritable = (((DataGridViewCheckBoxCell)gvList.Rows[dataMapInfo.rowIndex].Cells["IsWritable"]).Value != null) 
                                    ? (bool)((DataGridViewCheckBoxCell)gvList.Rows[dataMapInfo.rowIndex].Cells["IsWritable"]).Value 
                                    : false;

                    if(!isWritable)
                    {
                        result = null;
                        resultImage = null;
                        continue;
                    }

                    if (gvList.Rows[dataMapInfo.rowIndex].Cells["DataValue"].EditedFormattedValue != null)
                        valueText = gvList.Rows[dataMapInfo.rowIndex].Cells["DataValue"].EditedFormattedValue?.ToString();
                    else
                        valueText = gvList.Rows[dataMapInfo.rowIndex].Cells["DataValue"].Value?.ToString();

                    if (string.IsNullOrEmpty(valueText))
                    {
                        result = "Value not valid!";
                        resultImage = ModbusOperationsApp.Properties.Resources.error;
                        continue;
                    }

                    switch (dataMapInfo.RegisterType)
                    {
                        case Utility.RegisterType.Coil:
                            {
                                bool value;
                                if (!bool.TryParse(valueText, out value))
                                {
                                    result = "Value not valid!";
                                    resultImage = ModbusOperationsApp.Properties.Resources.error;
                                    continue;
                                }

                                modbusIpMaster = ModbusIpMaster.CreateIp(new TcpClient(modbusInfo.ip, modbusInfo.port));
                                modbusIpMaster.WriteSingleCoil(dataMapInfo.SlaveId, dataMapInfo.Address, value);

                                result = "";
                                resultImage = ModbusOperationsApp.Properties.Resources.ok;
                            }
                            break;
                        case Utility.RegisterType.Holding_Register:
                            {
                                #region Holding Register
                                byte[] dataList = utilityFunctions.GetBytesByDataType(valueText, dataMapInfo.DataType);
                                if (dataList == null || dataList.Length == 0)
                                {
                                    result = "Value not valid!";
                                    resultImage = ModbusOperationsApp.Properties.Resources.error;
                                    continue;
                                }

                                modbusIpMaster = ModbusIpMaster.CreateIp(new TcpClient(modbusInfo.ip, modbusInfo.port));

                                ushort[] registers = utilityFunctions.GetRegisterValues(dataList);
                                modbusIpMaster.WriteMultipleRegisters(dataMapInfo.SlaveId, dataMapInfo.Address, registers);
                                
                                result = utilityFunctions.RegisterArrayToString(registers);
                                resultImage = ModbusOperationsApp.Properties.Resources.ok;
                            }
                            #endregion
                            break;
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                    resultImage = ModbusOperationsApp.Properties.Resources.error;
                }
                finally
                {
                    if (result != null && resultImage != null)
                    {
                        gvList.Invoke((Action)(() =>
                        {
                            gvList.Rows[dataMapInfo.rowIndex].Cells["Status"].Value = resultImage;
                            gvList.Rows[dataMapInfo.rowIndex].Cells["Description"].Value = result;
                        }));
                    }
                }
            }
        }

        private void CheckModbusInfoOnGridView(ModbusInfo modbusInfo, DataMapInfo dataMapInfo, out int rowId)
        {
            if (dataMapInfo.rowIndex == -1)
            {
                rowId = gvList.Rows.Add();                
            }
            else
            {
                rowId = dataMapInfo.rowIndex;
            }

            gvList.Rows[rowId].Cells["Ip"].Value = modbusInfo.ip;
            gvList.Rows[rowId].Cells["Port"].Value = modbusInfo.port;
            gvList.Rows[rowId].Cells["SlaveId"].Value = dataMapInfo.SlaveId;
            gvList.Rows[rowId].Cells["Address"].Value = dataMapInfo.Address;
            gvList.Rows[rowId].Cells["RegisterLength"].Value = dataMapInfo.Length;
            gvList.Rows[rowId].Cells["RegisterType"].Value = Enum.GetName(typeof(RegisterType), dataMapInfo.RegisterType);
            gvList.Rows[rowId].Cells["DataType"].Value = Enum.GetName(typeof(DataType), dataMapInfo.DataType);
            gvList.Rows[rowId].Cells["ByteOrder"].Value = Enum.GetName(typeof(ByteOrder), dataMapInfo.ByteOrder);
            ((DataGridViewCheckBoxCell)gvList.Rows[rowId].Cells["IsWritable"]).Value = dataMapInfo.IsWritable;
            gvList.Rows[rowId].Cells["Status"].Value = ModbusOperationsApp.Properties.Resources.sandglass;
            gvList.Rows[rowId].Cells["TagName"].Value = dataMapInfo.TagName;
            gvList.Rows[rowId].Cells["DataValue"].Value = "";
            gvList.Rows[rowId].Cells["Description"].Value = "";

            CheckControl_OnChangeDataType(rowId, dataMapInfo.DataType);
            CheckControl_OnChangeRegisterType(rowId, dataMapInfo.RegisterType);
        }

        private void CheckModbusInfoOnCardView(ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            if (dataMapInfo.rowIndex == -1)
            {
                CardViewItem cardViewItem = new CardViewItem(modbusInfo, dataMapInfo);
                cardViewItem.OnRemove += CardViewItem_OnRemove;
                cardViewItem.OnEdit += CardViewItem_OnEdit;

                flpnlCardViewList.Controls.Add(cardViewItem);
            }
            else
            {
                foreach (CardViewItem item in flpnlCardViewList.Controls)
                {
                    if (item.modbusInfo.ip == modbusInfo.ip && item.modbusInfo.port == modbusInfo.port &&
                       item.dataMapInfo.SlaveId == dataMapInfo.SlaveId && item.dataMapInfo.Address == dataMapInfo.Address)
                    {
                        item.LoadTagInfo(modbusInfo, dataMapInfo);
                        break;
                    }
                }
            }
        }

        private void CardViewItem_OnEdit(object sender, OnCardViewEventArgs e)
        {
            ViewTagInfo(e.modbusInfo, e.dataMapInfo);
        }

        private void CardViewItem_OnRemove(object sender, OnCardViewEventArgs e)
        {
            RemoveModbusInfoOnGridView(e.modbusInfo, e.dataMapInfo);
            RemoveModbusInfoOnCardView(e.modbusInfo, e.dataMapInfo);
        }

        private void RemoveModbusInfoOnCardView(ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            foreach (CardViewItem item in flpnlCardViewList.Controls)
            {
                if(item.modbusInfo.ip == modbusInfo.ip && item.modbusInfo.port == modbusInfo.port &&
                   item.dataMapInfo.SlaveId == dataMapInfo.SlaveId && item.dataMapInfo.Address == dataMapInfo.Address)
                {
                    flpnlCardViewList.Controls.Remove(item);
                    break;
                }
            }

            RefreshGridViewIndex();
        }

        private void RemoveModbusInfoOnGridView(ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            if (dataMapInfo.rowIndex != -1)
            {
                gvList.Rows.RemoveAt(dataMapInfo.rowIndex);
            }
            else
            {
                foreach (DataGridViewRow row in gvList.Rows)
                {
                    if (row.Cells["Ip"].Value == modbusInfo.ip && row.Cells["Port"].Value.ToString() == modbusInfo.port.ToString() &&
                        row.Cells["SlaveId"].Value.ToString() == dataMapInfo.SlaveId.ToString() && row.Cells["Address"].Value.ToString() == dataMapInfo.Address.ToString())
                    {
                        gvList.Rows.RemoveAt(row.Index);
                    }
                }
            }

            RefreshGridViewIndex();
        }

        private void RefreshGridViewIndex()
        {
            int rowIndex = 0;
            int port;
            byte slaveId;
            ushort address;
            string ip;
            ModbusInfo existedModbusInfo;
            DataMapInfo existedDataMapInfo;
            gvList.Invoke((Action)(() =>
            {
                foreach (DataGridViewRow row in gvList.Rows)
                {
                    ip = row.Cells["Ip"].Value?.ToString();
                    if (string.IsNullOrEmpty(ip))
                        return;

                    port = (int)row.Cells["Port"].Value;
                    slaveId = (byte)row.Cells["SlaveId"].Value;
                    address = (ushort)row.Cells["Address"].Value;

                    existedModbusInfo = modbusInfoList.FirstOrDefault(mi => mi.ip == ip && mi.port == port);
                    if (existedModbusInfo != null)
                    {
                        existedDataMapInfo = existedModbusInfo.DataMapInfoList.FirstOrDefault(dmi => dmi.SlaveId == slaveId && dmi.Address == address);
                        if (existedDataMapInfo != null)
                        {
                            existedDataMapInfo.rowIndex = rowIndex;
                        }
                    }

                    rowIndex++;
                }
            }));
        }


    }
}
