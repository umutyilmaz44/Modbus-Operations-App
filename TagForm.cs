using ModbusOperationsApp.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusOperationsApp
{
    public partial class TagForm : Form
    {
        MainForm mainForm;
        public ModbusInfo modbusInfo { get; }
        public DataMapInfo dataMapInfo { get; }

        public TagForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        public TagForm(MainForm mainForm, ModbusInfo modbusInfo, DataMapInfo dataMapInfo)
        {
            this.mainForm = mainForm;
            this.modbusInfo = modbusInfo;
            this.dataMapInfo = dataMapInfo;

            InitializeComponent();
        }

        private void TagForm_Load(object sender, EventArgs e)
        {
            txtDataLength.Enabled = false;

            foreach (string dataType in Enum.GetNames(typeof(DataType)))
            {
                this.cmbDataType.Items.Add(dataType);
            }
            foreach (string registerType in Enum.GetNames(typeof(RegisterType)))
            {
                this.cmbRegisterType.Items.Add(registerType);
            }

            cmbRegisterType.SelectedItem = RegisterType.Holding_Register.ToString();
            cmbDataType.SelectedItem = DataType.UInt32.ToString();
            cmbByteOrder.SelectedItem = ByteOrder.ABCD.ToString();

            if (this.modbusInfo != null)
            {
                txtTagName.Text = dataMapInfo.TagName;
                txtIp.Text = modbusInfo.ip;
                txtPort.Text = modbusInfo.port.ToString();
                txtSlaveId.Text = dataMapInfo.SlaveId.ToString();
                txtAddress.Text = dataMapInfo.Address.ToString();
                cmbRegisterType.SelectedItem = dataMapInfo.RegisterType.ToString();
                cmbDataType.SelectedItem = dataMapInfo.DataType.ToString();
                cmbByteOrder.SelectedItem = dataMapInfo.ByteOrder.ToString();
                txtDataLength.Text = dataMapInfo.Length.ToString();
                chbIsWritable.Checked = dataMapInfo.IsWritable;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SaveTag();
        }

        private void cmbRegisterType_SelectedIndexChanged(object sender, EventArgs e)
        {            
            RegisterType registerType;
         
            System.Enum.TryParse<RegisterType>(cmbRegisterType.SelectedItem.ToString(), out registerType);

            CheckControl_OnChangeRegisterType(registerType);
        }

        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataType dataType;
            System.Enum.TryParse<DataType>(cmbDataType.SelectedItem.ToString(), out dataType);

            CheckControl_OnChangeDataType(dataType);
        }

        private void CheckControl_OnChangeRegisterType(RegisterType registerType)
        {
            bool isWritable = false;
            switch (registerType)
            {
                case Utility.RegisterType.Coil:
                case Utility.RegisterType.Holding_Register:
                    isWritable = true;
                    break;
                default:
                    isWritable = false;                    
                    break;
            }

            chbIsWritable.Enabled = isWritable;
        }

        private void CheckControl_OnChangeDataType(DataType dataType)
        {
            bool isEnabled = false;
            string length = "";

            switch (dataType)
            {
                case Utility.DataType.Int16:
                case Utility.DataType.UInt16:
                    length = "1";
                    isEnabled = false;
                    break;
                case Utility.DataType.Int32:
                case Utility.DataType.UInt32:
                case Utility.DataType.Float:
                    length = "2";
                    isEnabled = false;
                    break;
                case Utility.DataType.Int64:
                case Utility.DataType.UInt64:
                case Utility.DataType.Double:
                    length = "4";
                    isEnabled = false;
                    break;
                case Utility.DataType.String:
                    length = "";
                    isEnabled = true;
                    break;
            }

            txtDataLength.Text = length;
            txtDataLength.Enabled = isEnabled;

            LoadByteOrderList();
        }

        private void LoadByteOrderList()
        {
            DataType dataType;
            System.Enum.TryParse<DataType>(cmbDataType.SelectedItem.ToString(), out dataType);

            cmbByteOrder.Items.Clear();
            switch (dataType)
            {
                case Utility.DataType.Int16:
                case Utility.DataType.UInt16:
                    {
                        Utility.ByteOrder[] byteOrders = new ByteOrder[] { Utility.ByteOrder.AB, Utility.ByteOrder.BA };
                        for (int i = 0; i < byteOrders.Length; i++)
                        {
                            cmbByteOrder.Items.Add(Enum.GetName(typeof(ByteOrder), byteOrders[i]));
                        }
                    }
                    break;
                default:
                    {
                        List<string> byteOrders = Enum.GetNames(typeof(Utility.ByteOrder)).ToList();
                        byteOrders = byteOrders.Where(x => x != "AB" && x != "BA").ToList();
                        foreach (string byteOrder in byteOrders)
                        {
                            cmbByteOrder.Items.Add(byteOrder);
                        }
                    }
                    break;
            }
        }

        private void SaveTag()
        {
            int port=0;
            byte slaveId = 0;
            ushort address = 0, dataLength = 0;

            IPAddress ipAddress;

            if (string.IsNullOrEmpty(txtTagName.Text))
            {
                MessageBox.Show("Tag name is empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtIp.Text))
            {
                MessageBox.Show("Ip value is empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(!IPAddress.TryParse(txtIp.Text.Trim(), out ipAddress))
            {
                MessageBox.Show("Ip value is valid!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtPort.Text))
            {
                MessageBox.Show("Port value is empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!Int32.TryParse(txtPort.Text.Trim(), out port))
            {
                MessageBox.Show("Port value is valid!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtSlaveId.Text))
            {
                MessageBox.Show("SlaveId value is empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!byte.TryParse(txtSlaveId.Text.Trim(), out slaveId))
            {
                MessageBox.Show("SlaveId value is valid!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Address value is empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!ushort.TryParse(txtAddress.Text.Trim(), out address))
            {
                MessageBox.Show("Address value is valid!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(cmbRegisterType.SelectedItem == null)
            {
                MessageBox.Show("Register type not selected!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (cmbDataType.SelectedItem == null)
            {
                MessageBox.Show("Data type not selected!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (cmbByteOrder.SelectedItem == null)
            {
                MessageBox.Show("Byte order not selected!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtDataLength.Text))
            {
                MessageBox.Show("Data length not selected!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!ushort.TryParse(txtDataLength.Text.Trim(), out dataLength))
            {
                MessageBox.Show("Data length value is valid!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataType dataType ;
            ByteOrder byteOrder;
            RegisterType registerType;
            System.Enum.TryParse<DataType>(cmbDataType.SelectedItem.ToString(), out dataType);
            System.Enum.TryParse<ByteOrder>(cmbByteOrder.SelectedItem.ToString(), out byteOrder);
            System.Enum.TryParse<RegisterType>(cmbRegisterType.SelectedItem.ToString(), out registerType);

            bool isWritable = chbIsWritable.Checked;
            string ip = txtIp.Text.Trim();
            string tagName = txtTagName.Text.Trim();

            if (this.modbusInfo == null)
            {
                ModbusInfo modbusInfo = new ModbusInfo(ip, port);
                DataMapInfo dataMapInfo = new DataMapInfo(-1, tagName, slaveId, address, dataLength, dataType, byteOrder, registerType, isWritable);

                bool existControl = mainForm.CheckExistModbusInfo(modbusInfo, dataMapInfo);
                if (existControl)
                {
                    MessageBox.Show("The defined values are already registered.", "Tag Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    mainForm.AddModbusInfo(modbusInfo, dataMapInfo);
                    this.Close();
                }
            }
            else
            {
                this.modbusInfo.ip = ip;
                this.modbusInfo.port = port;
                this.dataMapInfo.TagName = tagName;
                this.dataMapInfo.Address = address;
                this.dataMapInfo.SlaveId = slaveId;
                this.dataMapInfo.Address = address;
                this.dataMapInfo.Length = dataLength;
                this.dataMapInfo.DataType = dataType;
                this.dataMapInfo.ByteOrder = byteOrder;
                this.dataMapInfo.RegisterType = registerType;
                this.dataMapInfo.IsWritable = isWritable;

                mainForm.UpdateModbusInfo(modbusInfo, dataMapInfo);
                this.Close();
            }
        }
    }
}
