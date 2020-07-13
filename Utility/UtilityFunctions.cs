using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusOperationsApp.Utility
{
    public class UtilityFunctions
    {
        public byte[] GetRegisterBytes(ushort[] registers)
        {
            byte[] byteData;
            List<byte> byteDataList = new List<byte>();
            for (int i = 0; i < registers.Length; i++)
            {
                byteData = BitConverter.GetBytes(registers[i]);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(byteData);
                byteDataList.AddRange(byteData);
            }

            return byteDataList.ToArray();
        }

        public string ByteArrayToString(byte[] dataList)
        {
            StringBuilder hex = new StringBuilder(dataList.Length * 3);
            foreach (byte data in dataList)
                hex.AppendFormat("{0:X2}-", data);
            return hex.ToString().Trim('-');
        }

        public string RegisterArrayToString(ushort[] dataList)
        {
            string dataText;
            StringBuilder hex = new StringBuilder();
            foreach (ushort data in dataList)
            {
                dataText = string.Format("{0:X4}", data);
                dataText = dataText.Insert(2, "-");
                hex.AppendFormat(dataText + "-");
            }
            return hex.ToString().Trim('-');
        }

        public T GetValue<T>(byte[] byteData, ByteOrder byteOrder, out byte[] orderedByteData)
        {
            T value = default(T);

            DataType dataType;
            string dataTypeText = typeof(T).Name.Replace("Single", "Float");

            System.Enum.TryParse<DataType>(dataTypeText, out dataType);
            string format = Enum.GetName(typeof(ByteOrder), byteOrder);
            orderedByteData = OrderByteArray(byteData, format);
            byte[] hexData = new byte[orderedByteData.Length];
            Array.Copy(orderedByteData, 0, hexData, 0, hexData.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(hexData);

            #region Byte Order
            //byte tmp, tmp2;
            //switch (byteOrder)
            //{
            //    default:
            //    case Utility.ByteOrder.ABCD:
            //        break;
            //    case Utility.ByteOrder.BADC:
            //        #region Byte Swap
            //        for (int i = 0; i <= byteData.Length - 2; i = i + 2)
            //        {
            //            tmp = byteData[i];
            //            byteData[i] = byteData[i + 1];
            //            byteData[i + 1] = tmp;
            //        }
            //        #endregion
            //        break;
            //    case Utility.ByteOrder.CDAB:
            //        #region Word Swap
            //        for (int i = 0; i <= byteData.Length - 4; i = i + 4)
            //        {
            //            tmp = byteData[i];
            //            tmp2 = byteData[i + 1];
            //            byteData[i] = byteData[i + 2];
            //            byteData[i + 1] = byteData[i + 3];
            //            byteData[i + 2] = tmp;
            //            byteData[i + 3] = tmp2;
            //        }
            //        #endregion
            //        break;
            //    case Utility.ByteOrder.DCBA:
            //        #region Word And Byte Swap == Reverse                    
            //        for (int i = 0; i <= byteData.Length - 4; i = i + 4)
            //        {
            //            Array.Reverse(byteData, i, 4);
            //        }
            //        #endregion
            //        break;
            //}
            #endregion

            #region Value Type Convert

            switch (dataType)
            {
                case Utility.DataType.Int16:
                    value = (T)Convert.ChangeType(BitConverter.ToInt16(hexData, 0), typeof(T));
                    break;
                case Utility.DataType.UInt16:
                    value = (T)Convert.ChangeType(BitConverter.ToUInt16(hexData, 0), typeof(T));
                    break;
                case Utility.DataType.Int32:
                    value = (T)Convert.ChangeType(BitConverter.ToInt32(hexData, 0), typeof(T));
                    break;
                case Utility.DataType.UInt32:
                    value = (T)Convert.ChangeType(BitConverter.ToUInt32(hexData, 0), typeof(T));
                    break;
                case Utility.DataType.Int64:
                    value = (T)Convert.ChangeType(BitConverter.ToInt64(hexData, 0), typeof(T));
                    break;
                case Utility.DataType.UInt64:
                    value = (T)Convert.ChangeType(BitConverter.ToUInt64(hexData, 0), typeof(T));
                    break;
                case Utility.DataType.Double:
                    value = (T)Convert.ChangeType(BitConverter.ToDouble(hexData, 0), typeof(T));

                    //using (MemoryStream m2 = new MemoryStream(byteData))
                    //{
                    //    using (BinaryReader reader = new BinaryReader(m2))
                    //    {
                    //        double dcmValue = reader.ReadDouble();
                    //        value = (T)Convert.ChangeType(dcmValue, typeof(T));
                    //    }
                    //}
                    break;
                case Utility.DataType.Float:
                    value = (T)Convert.ChangeType(BitConverter.ToSingle(hexData, 0), typeof(T));

                    //using (MemoryStream m2 = new MemoryStream(byteData))
                    //{
                    //    using (BinaryReader reader = new BinaryReader(m2))
                    //    {
                    //        float fltValue = reader.ReadSingle();
                    //        value = (T)Convert.ChangeType(fltValue, typeof(T));
                    //    }
                    //}
                    break;
                case Utility.DataType.String:
                    value = (T)Convert.ChangeType(Encoding.UTF8.GetString(byteData), typeof(T));
                    break;
                default:
                    value = default(T);
                    break;
            }
            #endregion

            return value;
        }
    
        public byte[] GetBytesByDataType(string valueText, DataType dataType)
        {
            byte[] data = null;
            switch (dataType)
            {
                case Utility.DataType.Int16:
                    {
                        Int16 value;
                        if (Int16.TryParse(valueText, out value))
                        {
                            data = BitConverter.GetBytes(value);
                        }
                    }
                    break;
                case Utility.DataType.UInt16:
                    {
                        UInt16 value;
                        if (UInt16.TryParse(valueText, out value))
                        {
                            data = BitConverter.GetBytes(value);
                        }
                    }
                    break;
                case Utility.DataType.Int32:
                    {
                        Int32 value;
                        if (Int32.TryParse(valueText, out value))
                        {
                            data = BitConverter.GetBytes(value);
                        }
                    }
                    break;
                case Utility.DataType.UInt32:
                    {
                        UInt32 value;
                        if (UInt32.TryParse(valueText, out value))
                        {
                            data = BitConverter.GetBytes(value);
                        }
                    }
                    break;
                case Utility.DataType.Int64:
                    {
                        Int64 value;
                        if (Int64.TryParse(valueText, out value))
                        {
                            data = BitConverter.GetBytes(value);
                        }
                    }
                    break;
                case Utility.DataType.UInt64:
                    {
                        UInt64 value;
                        if (UInt64.TryParse(valueText, out value))
                        {
                            data = BitConverter.GetBytes(value);
                        }
                    }
                    break;
                case Utility.DataType.Double:
                    {
                        Double value;
                        valueText = valueText.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                                             .Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                        if (Double.TryParse(valueText, out value))
                        {
                            data = BitConverter.GetBytes(value);
                        }
                    }
                    break;
                case Utility.DataType.Float:
                    {
                        float value;
                        valueText = valueText.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                                             .Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                        if (Single.TryParse(valueText, out value))
                        {
                            data = BitConverter.GetBytes(value);
                        }
                    }
                    break;
                case Utility.DataType.String:
                    data = Encoding.UTF8.GetBytes(valueText);
                    break;
                default:
                    break;
            }

            for (int i = 0; i < data.Length; i = i + 2)
            {
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(data, i, 2);
            }

            return data;
        }

        public ushort[] GetRegisterValues(byte[] bytes)
        {
            int i, j;
            int arrayLength;
            ushort tempValue;
            byte[] tempBytes;
            List<ushort> values = new List<ushort>();

            if (bytes.Length % 2 != 0)
            {
                List<byte> byteList = new List<byte>();
                for (i = 0; i < bytes.Length; i++)
                    byteList.Add(bytes[i]);

                byteList.Add(0);

                bytes = byteList.ToArray();
            }

            for (i = 0; i < bytes.Length; i = i + 2)
            {
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(bytes, i, 2);
            }

            // Converting to ushort list
            for (i = 0; i < bytes.Length; i = i + 2)
            {
                values.Add(BitConverter.ToUInt16(bytes, i));
            }

            //byte tmp, tmp2;
            //switch (byteOrder)
            //{
            //    case ByteOrder.AB:
            //    case ByteOrder.ABCD:
            //        // Converting to ushort list
            //        for (i = 0; i < bytes.Length; i = i + 2)
            //        {
            //            values.Add(BitConverter.ToUInt16(bytes, i));
            //        }
            //        break;
            //    // Word Swap
            //    case ByteOrder.CDAB:
            //        for (i = 0; i <= bytes.Length - 4; i = i + 4)
            //        {
            //            tmp = bytes[i];
            //            tmp2 = bytes[i + 1];
            //            bytes[i] = bytes[i + 2];
            //            bytes[i + 1] = bytes[i + 3];
            //            bytes[i + 2] = tmp;
            //            bytes[i + 3] = tmp2;
            //        }
            //        // Converting to ushort list
            //        for (i = 0; i < bytes.Length; i = i + 2)
            //        {
            //            values.Add(BitConverter.ToUInt16(bytes, i));
            //        }
            //        break;
            //    // Byte Swap 
            //    case ByteOrder.BA:
            //    case ByteOrder.BADC: 
            //        for (i = 0; i < bytes.Length; i = i + 2)
            //        {
            //            tmp = bytes[i];
            //            bytes[i] = bytes[i + 1];
            //            bytes[i + 1] = tmp;
            //        }
            //        // Converting to ushort list
            //        for (i = 0; i < bytes.Length; i = i + 2)
            //        {
            //            values.Add(BitConverter.ToUInt16(bytes, i));
            //        }
            //        break;
            //    // Byte Swap And Word Swap == Reverse
            //    case ByteOrder.DCBA:
            //        // Byte Swap 
            //        for (i = 0; i < bytes.Length; i = i + 2)
            //        {
            //            tmp = bytes[i];
            //            bytes[i] = bytes[i + 1];
            //            bytes[i + 1] = tmp;
            //        }
            //        // Word Swap
            //        for (i = 0; i <= bytes.Length - 4; i = i + 4)
            //        {
            //            tmp = bytes[i];
            //            tmp2 = bytes[i + 1];
            //            bytes[i] = bytes[i + 2];
            //            bytes[i + 1] = bytes[i + 3];
            //            bytes[i + 2] = tmp;
            //            bytes[i + 3] = tmp2;
            //        }
            //        // Converting to ushort list
            //        for (i = 0; i < bytes.Length; i = i + 2)
            //        {
            //            values.Add(BitConverter.ToUInt16(bytes, i));
            //        }
            //        break;
            //}

            return values.ToArray();
        }

        public byte[] OrderByteArray(byte[] hexData, string format)
        {
            byte[] hexDataNew = new byte[hexData.Length];

            if (hexData.Length == 2)
                format = format.Substring(0, 2);

            switch (format)
            {
                case "ABCD":
                    Array.Copy(hexData, 0, hexDataNew, 0, hexData.Length);
                    break;
                case "BADC":
                    #region Byte Swap
                    if (hexData.Length == 4)
                    {
                        hexDataNew[0] = hexData[1];
                        hexDataNew[1] = hexData[0];
                        hexDataNew[2] = hexData[3];
                        hexDataNew[3] = hexData[2];
                    }
                    else if (hexData.Length == 8)
                    {
                        hexDataNew[0] = hexData[5];
                        hexDataNew[1] = hexData[4];
                        hexDataNew[2] = hexData[7];
                        hexDataNew[3] = hexData[6];

                        hexDataNew[4] = hexData[1];
                        hexDataNew[5] = hexData[0];
                        hexDataNew[6] = hexData[3];
                        hexDataNew[7] = hexData[2];
                    }
                    #endregion
                    break;
                case "CDAB":
                    #region Word Swap
                    if (hexData.Length == 4)
                    {
                        hexDataNew[0] = hexData[2];
                        hexDataNew[1] = hexData[3];
                        hexDataNew[2] = hexData[0];
                        hexDataNew[3] = hexData[1];
                    }
                    else if (hexData.Length == 8)
                    {
                        hexDataNew[0] = hexData[6];
                        hexDataNew[1] = hexData[7];
                        hexDataNew[2] = hexData[4];
                        hexDataNew[3] = hexData[5];

                        hexDataNew[4] = hexData[2];
                        hexDataNew[5] = hexData[3];
                        hexDataNew[6] = hexData[0];
                        hexDataNew[7] = hexData[1];
                    }
                    #endregion
                    break;
                case "DCBA":
                    #region Byte & Word Swap => Reverse
                    if (hexData.Length == 4)
                    {
                        hexDataNew[0] = hexData[3];
                        hexDataNew[1] = hexData[2];
                        hexDataNew[2] = hexData[1];
                        hexDataNew[3] = hexData[0];
                    }
                    else if (hexData.Length == 8)
                    {
                        hexDataNew[0] = hexData[7];
                        hexDataNew[1] = hexData[6];
                        hexDataNew[2] = hexData[5];
                        hexDataNew[3] = hexData[4];

                        hexDataNew[4] = hexData[3];
                        hexDataNew[5] = hexData[2];
                        hexDataNew[6] = hexData[1];
                        hexDataNew[7] = hexData[0];
                    }
                    #endregion
                    break;

                case "AB":
                    Array.Copy(hexData, 0, hexDataNew, 0, hexData.Length);
                    break;
                case "BA":
                    #region Byte Swap
                    hexDataNew[0] = hexData[1];
                    hexDataNew[1] = hexData[0];
                    #endregion
                    break;
            }

            return hexDataNew;
        }

    }
}
