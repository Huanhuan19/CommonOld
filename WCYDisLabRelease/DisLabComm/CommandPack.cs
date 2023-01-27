using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisLabComm
{
    public class CommandPack
    {
        public static byte[] IntToBytes(int value)
        {
            byte[] array = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                array[i] = (byte)(value >> 24 - i * 8);
            }
            return array;
        }

        public static int BytesToInt(byte[] b)
        {
            return (b[0] << 24) + (b[1] << 16) + (b[2] << 8) + b[3];
        }

        public static byte[] StringToBytes(string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        public static string BytesToString(byte[] values)
        {
            return Encoding.Default.GetString(values);
        }

        public static int[] ConnectCommand(Address clientAddress, int[] sensors)//由byte 都改成了Int  Address暂时屏蔽
        {
            //int[] array = Int(clientAddress.Port);
            //int[] array2 = new int[sensors.Length + 9];
            //int num = 0;
            //array2[num++] = 1;
            //array2[num++] = clientAddress.IP[0];
            //array2[num++] = clientAddress.IP[1];
            //array2[num++] = clientAddress.IP[2];
            //array2[num++] = clientAddress.IP[3];
            //array2[num++] = array[0];
            //array2[num++] = array[1];
            //array2[num++] = array[2];
            //array2[num++] = array[3];
            //for (int i = 0; i < sensors.Length; i++)
            //{
            //    array2[num++] = sensors[i];
            //}
            //return array2;
            return sensors;
        }

        public static byte[] WorkingCommand(Address clientAddress)
        {
            byte[] array = IntToBytes(clientAddress.Port);
            byte[] array2 = new byte[9];
            int num = 0;
            array2[num++] = 2;
            array2[num++] = clientAddress.IP[0];
            array2[num++] = clientAddress.IP[1];
            array2[num++] = clientAddress.IP[2];
            array2[num++] = clientAddress.IP[3];
            array2[num++] = array[0];
            array2[num++] = array[1];
            array2[num++] = array[2];
            array2[num++] = array[3];
            return array2;
        }

        public static byte[] GetDataCommand(Address serverAddress)
        {
            byte[] array = IntToBytes(serverAddress.Port);
            byte[] array2 = new byte[9];
            int num = 0;
            array2[num++] = 3;
            array2[num++] = serverAddress.IP[0];
            array2[num++] = serverAddress.IP[1];
            array2[num++] = serverAddress.IP[2];
            array2[num++] = serverAddress.IP[3];
            array2[num++] = array[0];
            array2[num++] = array[1];
            array2[num++] = array[2];
            array2[num++] = array[3];
            return array2;
        }

        public static byte[] StartSendDataCommand(Address clientAddress)
        {
            byte[] array = IntToBytes(clientAddress.Port);
            byte[] array2 = new byte[9];
            int num = 0;
            array2[num++] = 5;
            array2[num++] = clientAddress.IP[0];
            array2[num++] = clientAddress.IP[1];
            array2[num++] = clientAddress.IP[2];
            array2[num++] = clientAddress.IP[3];
            array2[num++] = array[0];
            array2[num++] = array[1];
            array2[num++] = array[2];
            array2[num++] = array[3];
            return array2;
        }

        public static byte[] EndSendDataCommand(Address clientAddress)
        {
            byte[] array = IntToBytes(clientAddress.Port);
            byte[] array2 = new byte[9];
            int num = 0;
            array2[num++] = 6;
            array2[num++] = clientAddress.IP[0];
            array2[num++] = clientAddress.IP[1];
            array2[num++] = clientAddress.IP[2];
            array2[num++] = clientAddress.IP[3];
            array2[num++] = array[0];
            array2[num++] = array[1];
            array2[num++] = array[2];
            array2[num++] = array[3];
            return array2;
        }

        public static byte[] SendDataCommand(Address clientAddress, byte[] buffer)
        {
            byte[] array = IntToBytes(clientAddress.Port);
            byte[] array2 = new byte[buffer.Length + 9];
            int num = 0;
            array2[num++] = 4;
            array2[num++] = clientAddress.IP[0];
            array2[num++] = clientAddress.IP[1];
            array2[num++] = clientAddress.IP[2];
            array2[num++] = clientAddress.IP[3];
            array2[num++] = array[0];
            array2[num++] = array[1];
            array2[num++] = array[2];
            array2[num++] = array[3];
            for (int i = 0; i < buffer.Length; i++)
            {
                array2[num++] = buffer[i];
            }
            return array2;
        }
    }
}
