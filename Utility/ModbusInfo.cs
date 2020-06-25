using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModbusOperationsApp.Utility
{
    public class ModbusInfo
    {
        public int port { get; set; }
        public string ip { get; set; }
        public List<DataMapInfo> DataMapInfoList { get; set; }
        public ModbusInfo(string Ip, int port)
        {
            IPAddress ipAddress;
            
            this.ip = Ip;
            this.port = port;
            
            DataMapInfoList = new List<DataMapInfo>();
        }
    }

    public class DataMapInfo
    {
        public int rowIndex;

        public byte SlaveId { get; set; }
        public ushort Address { get; set; }
        public ushort Length { get; set; }
        public DataType DataType { get; set; }
        public ByteOrder ByteOrder { get; set; }
        public RegisterType RegisterType { get; set; }
        public bool IsWritable { get; set; }

        public DataMapInfo(int rowIndex, byte slaveId, ushort address, ushort length, DataType dataType, ByteOrder byteOrder, RegisterType registerType, bool isWritable)
        {
            this.rowIndex = rowIndex;
            this.SlaveId = slaveId;
            this.Address = address;
            this.Length = length;
            this.DataType = dataType;
            this.ByteOrder = byteOrder;
            this.RegisterType = registerType;
            this.IsWritable = isWritable;
        }
    }

    public enum ModbusDataType
    {
        Int16,
        UInt16,
        Int32,
        UInt32,
        Int64,
        UInt64,
        Decimal,
        Float
    }
}
