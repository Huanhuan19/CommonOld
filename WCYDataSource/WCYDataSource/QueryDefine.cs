using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCYDataSource
{
    public class QueryDefine
    {
        static byte END_SIGNAL = 0xff;
        public static byte[] QueryType()
        {
            return new byte[] { 0x03,(byte)CommandDefine.QueryType,END_SIGNAL};
        }
        public static byte[] QueryShift(byte index)
        {
            return new byte[] { 0x04,(byte)CommandDefine.QueryShift,index,END_SIGNAL};
        }
        public static byte[] SetShift(byte index, byte shift)
        {
            return new byte[] { 0x05,(byte)CommandDefine.ShiftSet, index,shift,END_SIGNAL};
        }
        public static byte[] Start(byte a, byte b, byte c)
        {
            return new byte[] { 0x06,(byte)CommandDefine.Start,a,b,c,END_SIGNAL};
        }
        public static byte[] Wave(byte Wave, byte WaveType, byte value, byte time)//增加的波形类型的命令；
        {
            return new byte[] { 0x07, (byte)CommandDefine.Wave, Wave, WaveType, value, time, END_SIGNAL };
        }
        public static byte[] USBWave(byte Type,byte YY, byte XX, byte DD, byte PP)//增加的波形类型的命令；
        {
            return new byte[] { 0xa6, 0x05, 0x00, 0x00, 0x00, 0x00, Type, YY, XX, DD, PP };
        }
        public static byte[] Stop()
        {
            return new byte[] { 0x03,(byte)CommandDefine.Stop,END_SIGNAL};
        }
    }
}
