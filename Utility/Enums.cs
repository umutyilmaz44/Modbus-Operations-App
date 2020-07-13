using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusOperationsApp.Utility
{
    public enum ByteOrder
    {
        ABCD=1,
        CDAB,
        BADC,
        DCBA,
        AB,
        BA
    }

    public enum DataType
    {
        Int16=1,
        UInt16,
        Int32,
        UInt32,
        Int64,
        UInt64,
        Double,
        Float,
        String
    }

    public enum RegisterType
    {
        Coil = 1,
        Discrete_Input,
        Input_Register,
        Holding_Register
    }
}
