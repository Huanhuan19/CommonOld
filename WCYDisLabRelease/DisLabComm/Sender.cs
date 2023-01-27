using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace DisLabComm
{
    public class Sender
    {
        private static bool IsConnectionSuccessful = false;

        private static Exception socketexception;

        private static ManualResetEvent TimeoutObject = new ManualResetEvent(initialState: false);

        public bool Send(byte[] address, int port, byte[] datas)
        {
            bool result = false;
            TcpClient tcpClient = Connect(new IPEndPoint(new IPAddress(address), port), 1000);
            if (tcpClient != null && tcpClient.Connected)
            {
                result = true;
                NetworkStream stream = tcpClient.GetStream();
                stream.Write(datas, 0, datas.Length);
                stream.Close();
            }
            return result;
        }

        public static TcpClient Connect(IPEndPoint remoteEndPoint, int timeoutMSec)
        {
            TimeoutObject.Reset();
            socketexception = null;
            string host = Convert.ToString(remoteEndPoint.Address);
            int port = remoteEndPoint.Port;
            TcpClient tcpClient = new TcpClient();
            tcpClient.BeginConnect(host, port, CallBackMethod, tcpClient);
            if (TimeoutObject.WaitOne(timeoutMSec, exitContext: false))
            {
                if (IsConnectionSuccessful)
                {
                    return tcpClient;
                }
                return null;
            }
            tcpClient.Close();
            return null;
        }

        private static void CallBackMethod(IAsyncResult asyncresult)
        {
            try
            {
                IsConnectionSuccessful = false;
                TcpClient tcpClient = asyncresult.AsyncState as TcpClient;
                if (tcpClient.Client != null)
                {
                    tcpClient.EndConnect(asyncresult);
                    IsConnectionSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                IsConnectionSuccessful = false;
                socketexception = ex;
            }
            finally
            {
                TimeoutObject.Set();
            }
        }
    }
}
