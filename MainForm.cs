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
            SetLoadingPictureLocation();
        }

        private void InitForm()
        {
            pboxLoading.Visible = false;

            foreach (string dataType in Enum.GetNames(typeof(DataType)))
            {
                this.DataType.Items.Add(dataType);
            }

            foreach (string byteOrder in Enum.GetNames(typeof(ByteOrder)))
            {
                this.ByteOrder.Items.Add(byteOrder);
            }

            foreach (string registerType in Enum.GetNames(typeof(RegisterType)))
            {
                this.RegisterType.Items.Add(registerType);
            }

            SetLoadingPictureLocation();
        }

        private void SetLoadingPictureLocation()
        {
            pboxLoading.Location = new Point(((this.Width - pboxLoading.Width) / 2), ((this.Height - pboxLoading.Height) / 2));
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
        }
        private void gvList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

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
            DataType dataType;
            var currentcell = gvList.CurrentCellAddress;
            var cmbDataTypeControl = sender as DataGridViewComboBoxEditingControl;
            System.Enum.TryParse<DataType>(cmbDataTypeControl.EditingControlFormattedValue.ToString(), out dataType);

            CheckControl_OnChangeDataType(currentcell.Y, dataType);
        }
        private void CheckControl_OnChangeDataType(int rowIndex, DataType dataType)
        {
            DataGridViewTextBoxCell lengthCell = (DataGridViewTextBoxCell)gvList.Rows[rowIndex].Cells["RegisterLength"];

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
        }
        
        private void RegisterType_OnChange(object sender, EventArgs e)
        {
            var currentcell = gvList.CurrentCellAddress;
            RegisterType registerType;
            var cmbDataTypeControl = sender as DataGridViewComboBoxEditingControl;
            System.Enum.TryParse<RegisterType>(cmbDataTypeControl.EditingControlFormattedValue.ToString(), out registerType);

            CheckControl_OnChangeRegisterType(currentcell.Y, registerType);
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
            string ip, portText, slaveIdText, addressText, lengthText;
            string dataTypeText, byteOrderText, registerTypeText, isWritableText;
            string errorText;
            DataType dataType;
            ByteOrder byteOrder;
            RegisterType registerType;
            IPAddress ipAddress;
            ModbusInfo modbusInfo;
            List<ModbusInfo> modbusInfoList = new List<ModbusInfo>();

            for (int i = 0; i < gvList.Rows.Count - 1; i++)
            {
                gvList.Rows[i].Cells["Status"].Value = ModbusOperationsApp.Properties.Resources.sandglass;

                #region Validation Control
                errorText = "";
                ip = gvList.Rows[i].Cells["Ip"].Value?.ToString();
                portText = gvList.Rows[i].Cells["Port"].Value?.ToString();
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
                    modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                    modbusInfoList.Add(modbusInfo);
                }
                else if (!modbusInfo.DataMapInfoList.Any(x => x.SlaveId == slaveId && x.Address == address))
                {
                    modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                }
                #endregion
            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(modbusInfoList);

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
                        string dataText = sr.ReadToEnd();
                        List<ModbusInfo> modbusInfoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModbusInfo>>(dataText);

                        int rowId = -1;
                        gvList.Rows.Clear();
                        foreach (ModbusInfo modbusInfo in modbusInfoList)
                        {
                            foreach (DataMapInfo dataMapInfo in modbusInfo.DataMapInfoList)
                            {
                                rowId = gvList.Rows.Add();

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
                                gvList.Rows[rowId].Cells["DataValue"].Value = "";
                                gvList.Rows[rowId].Cells["Description"].Value = "";

                                CheckControl_OnChangeDataType(rowId, dataMapInfo.DataType);
                                CheckControl_OnChangeRegisterType(rowId, dataMapInfo.RegisterType);
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

        private void SetTimerStatus()
        {
            int intervals = 1;
            Int32.TryParse(txtInterval.Text, out intervals);
            tmrIntervalControl.Interval = 1000 * intervals;

            tmrIntervalControl.Enabled = chboxInterval.Checked;
        }

        private async Task ReadModbusList()
        {
            int port;
            bool isWritable;
            byte slaveId;
            ushort address, registerLength;
            string ip, portText, slaveIdText, addressText, lengthText;
            string dataTypeText, byteOrderText, registerTypeText, isWritableText;
            string errorText;
            DataType dataType;
            ByteOrder byteOrder;
            RegisterType registerType;
            IPAddress ipAddress;
            ModbusInfo modbusInfo;
            List<ModbusInfo> modbusInfoList = new List<ModbusInfo>();
            
            pboxLoading.Invoke((Action)(() =>
            {
                pboxLoading.Visible = true;
            }));
            
            for (int i = 0; i < gvList.Rows.Count - 1; i++)
            {
                gvList.Rows[i].Cells["Status"].Value = ModbusOperationsApp.Properties.Resources.sandglass;

                #region Validation Control
                errorText = "";
                ip = gvList.Rows[i].Cells["Ip"].Value?.ToString();
                portText = gvList.Rows[i].Cells["Port"].Value?.ToString();
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
                    modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                    modbusInfoList.Add(modbusInfo);
                }
                else if (!modbusInfo.DataMapInfoList.Any(x => x.SlaveId == slaveId && x.Address == address))
                {
                    modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                }
                #endregion
            }

            await Task.Factory.StartNew(() =>
                Parallel.ForEach(modbusInfoList, modbusInfoItem =>
                {
                    ReadModbus(modbusInfoItem);
                })
            );

            pboxLoading.Invoke((Action)(() =>
            {
                pboxLoading.Visible = false;
            }));
        }

        private async Task WriteModbusList()
        {
            int port;
            bool isWritable;
            byte slaveId;
            ushort address, registerLength;
            string ip, portText, slaveIdText, addressText, lengthText;
            string dataTypeText, byteOrderText, registerTypeText, isWritableText;
            string errorText;
            DataType dataType;
            ByteOrder byteOrder;
            RegisterType registerType;
            IPAddress ipAddress;
            ModbusInfo modbusInfo;
            List<ModbusInfo> modbusInfoList = new List<ModbusInfo>();

            pboxLoading.Invoke((Action)(() =>
            {
                pboxLoading.Visible = true;
            }));

            for (int i = 0; i < gvList.Rows.Count - 1; i++)
            {
                ip = gvList.Rows[i].Cells["Ip"].Value?.ToString();
                portText = gvList.Rows[i].Cells["Port"].Value?.ToString();
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
                        modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
                        modbusInfoList.Add(modbusInfo);
                    }
                    else if (!modbusInfo.DataMapInfoList.Any(x => x.SlaveId == slaveId && x.Address == address))
                    {
                        modbusInfo.DataMapInfoList.Add(new DataMapInfo(i, slaveId, address, registerLength, dataType, byteOrder, registerType, isWritable));
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

            pboxLoading.Invoke((Action)(() =>
            {
                pboxLoading.Visible = false;
            }));
        }

        private void ReadModbus(ModbusInfo modbusInfo)
        {
            string result = "";
            string resultValue = "";
            Bitmap resultImage = null;
            ModbusIpMaster modbusIpMaster;
            UtilityFunctions utilityFunctions = new UtilityFunctions();

            foreach (DataMapInfo dataMapInfo in modbusInfo.DataMapInfoList)
            {
                result = "";
                resultValue = "";
                resultImage = null;
                byte[] byteDataArray;

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

                            result = utilityFunctions.ByteArrayToString(byteDataArray.ToArray());

                            switch (dataMapInfo.DataType)
                            {
                                case Utility.DataType.Int16:
                                    resultValue = utilityFunctions.GetValue<Int16>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.UInt16:
                                    resultValue = utilityFunctions.GetValue<UInt16>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.Int32:
                                    resultValue = utilityFunctions.GetValue<Int32>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.UInt32:
                                    resultValue = utilityFunctions.GetValue<UInt32>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.Int64:
                                    resultValue = utilityFunctions.GetValue<Int64>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.UInt64:
                                    resultValue = utilityFunctions.GetValue<UInt64>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.Double:
                                    resultValue = utilityFunctions.GetValue<Double>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.Float:
                                    resultValue = utilityFunctions.GetValue<float>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.String:
                                    resultValue = utilityFunctions.GetValue<String>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case Utility.RegisterType.Holding_Register:
                            #region Holding Register
                            ushort[] holdingRegisters = modbusIpMaster.ReadHoldingRegisters(dataMapInfo.SlaveId, dataMapInfo.Address, dataMapInfo.Length);

                            byteDataArray = utilityFunctions.GetRegisterBytes(holdingRegisters);

                            result = utilityFunctions.ByteArrayToString(byteDataArray.ToArray());

                            switch (dataMapInfo.DataType)
                            {
                                case Utility.DataType.Int16:
                                    resultValue = utilityFunctions.GetValue<Int16>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.UInt16:
                                    resultValue = utilityFunctions.GetValue<UInt16>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.Int32:
                                    resultValue = utilityFunctions.GetValue<Int32>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.UInt32:
                                    resultValue = utilityFunctions.GetValue<UInt32>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.Int64:
                                    resultValue = utilityFunctions.GetValue<Int64>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.UInt64:
                                    resultValue = utilityFunctions.GetValue<UInt64>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                case Utility.DataType.Double:
                                    resultValue = utilityFunctions.GetValue<Double>(byteDataArray, dataMapInfo.ByteOrder).ToString("0.0000000000").TrimEnd('0');
                                    break;
                                case Utility.DataType.Float:
                                    resultValue = utilityFunctions.GetValue<float>(byteDataArray, dataMapInfo.ByteOrder).ToString("0.0000000000").TrimEnd('0');
                                    break;
                                case Utility.DataType.String:
                                    resultValue = utilityFunctions.GetValue<String>(byteDataArray, dataMapInfo.ByteOrder).ToString();
                                    break;
                                default:                                    
                                    break;
                            }
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
                    gvList.Invoke((Action)(() =>
                    {
                        gvList.Rows[dataMapInfo.rowIndex].Cells["DataValue"].Value = resultValue;
                        gvList.Rows[dataMapInfo.rowIndex].Cells["Status"].Value = resultImage;
                        gvList.Rows[dataMapInfo.rowIndex].Cells["Description"].Value = result;
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

                                ushort[] registers = utilityFunctions.GetRegisterValues(dataList, dataMapInfo.ByteOrder);
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
    }
}
