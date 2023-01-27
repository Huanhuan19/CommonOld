using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace DisLabComm
{
    public class Address
    {
        private byte[] _ip = new byte[4];

        private int _port;

        public byte[] IP => _ip;

        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        public string IPString => _ip[0].ToString() + "." + _ip[1].ToString() + "." + _ip[2].ToString() + "." + _ip[3].ToString();

        public Address(byte seg1, byte seg2, byte seg3, byte seg4, int port)
        {
            _ip[0] = seg1;
            _ip[1] = seg2;
            _ip[2] = seg3;
            _ip[3] = seg4;
            _port = port;
        }

        public Address(string value)
        {
            Parse(value);
        }

        public Address()
        {
            Clear();
            GetLocalIPV4Address();
        }

        public void Clear()
        {
            byte[] array = _ip = new byte[4];
            _port = 0;
        }

        public void GetLocalIPV4Address()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i != hostEntry.AddressList.Length; i++)
            {
                if (hostEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    _ip = hostEntry.AddressList[i].GetAddressBytes();
                }
            }
        }

        public override string ToString()
        {
            return _ip[0].ToString() + "|" + _ip[1].ToString() + "|" + _ip[2].ToString() + "|" + _ip[3].ToString() + "|" + _port.ToString();
        }

        public void Parse(string value)
        {
            Clear();
            string[] array = value.Split('|');
            if (array.Length == 5)
            {
                byte.TryParse(array[0], out _ip[0]);
                byte.TryParse(array[1], out _ip[1]);
                byte.TryParse(array[2], out _ip[2]);
                byte.TryParse(array[3], out _ip[3]);
                int.TryParse(array[4], out _port);
            }
        }
    }
    public enum CommandDefine : byte
    {
        None,
        Connect,
        Working,
        GetData,
        SendData,
        StartSend,
        EndSend
    }
}
