using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace DisLabComm
{
    public class Listener
    {
        private TcpListener tcpListen;

        private byte[] _address;

        private int _port;

        public TcpListener TcpListener => tcpListen;

        public event ListenerHandler DataRecieved;

        public bool Start(byte[] address, int port)
        {
            _address = address;
            _port = port;
            return listenThread();
        }

        public void Stop()
        {
            if (tcpListen != null)
            {
                tcpListen.Stop();
                tcpListen = null;
            }
        }

        private bool listenThread()
        {
            bool flag = false;
            try
            {
                flag = true;
                tcpListen = new TcpListener(new IPEndPoint(new IPAddress(_address), _port));
                tcpListen.Start();
                tcpListen.BeginAcceptTcpClient(processEvents, tcpListen);
                return flag;
            }
            catch
            {
                return false;
            }
        }

        private void processEvents(IAsyncResult asyn)
        {
            try
            {
                TcpListener tcpListener = (TcpListener)asyn.AsyncState;
                TcpClient tcpClient = tcpListener.EndAcceptTcpClient(asyn);
                NetworkStream stream = tcpClient.GetStream();
                if (stream.CanRead)
                {
                    byte[] array = new byte[65535];
                    int num = stream.Read(array, 0, array.Length);
                    if (num > 0)
                    {
                        Array.Resize(ref array, num);
                        ListenerEventArgs e = new ListenerEventArgs(array, num);
                        OnDataRecieved(e);
                    }
                }
                stream.Close();
                tcpClient.Close();
                tcpListen.BeginAcceptTcpClient(processEvents, tcpListen);
            }
            catch
            {
            }
        }

        protected void OnDataRecieved(ListenerEventArgs e)
        {
            if (this.DataRecieved != null)
            {
                this.DataRecieved(this, e);
            }
        }
    }
    public class ListenerEventArgs : EventArgs
    {
        private byte[] _buffer;

        private int _count;

        public byte[] Buffer => _buffer;

        public int Count => _count;

        public ListenerEventArgs(byte[] buffer, int count)
        {
            _buffer = buffer;
            _count = count;
        }
    }
    public delegate void ListenerHandler(object sender, ListenerEventArgs e);
}
