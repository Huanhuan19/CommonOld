using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace WCYDisLab
{
    public partial class FormSendData : Form
    {
        public FormSendData(string filename,DisLabComm.Address serverAddress,DisLabComm.Address localAddress)
        {
            InitializeComponent();
            _filename = filename;
            _serverAddress = serverAddress;
            _localAddress = localAddress;
            InitNet();
            timer1.Start();
        }

        #region Props
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.FormSendData", Assembly.GetExecutingAssembly());
        string _filename;
        DisLabComm.Address _serverAddress;
        DisLabComm.Address _localAddress;
        DisLabComm.Sender _sender;
        System.IO.FileStream _reader;
        long _length;
        long _position;
        bool _needOpenFile = true, _needStart = false ,_sending= false,_fileOpen = false,_needStop = false,_needPopMessage = false,_needClose= false;
        string _message = "";
        static int MAX_LENGTH = 1024;
        #endregion

        #region Methods
        void InitNet()
        {
            _sender = new DisLabComm.Sender();
            
        }
        bool OpenFile()
        {
            bool success = false;
            if (System.IO.File.Exists(_filename))
            {
                _reader= System.IO.File.OpenRead(_filename);
                _length = _reader.Length;
                _position = 0;
                success = true;
            }
            return success;
        }
        void SendStart()
        {
            _sender.Send(_serverAddress.IP, _serverAddress.Port, DisLabComm.CommandPack.StartSendDataCommand(_localAddress));
        }
        void SendEnd()
        {
            _sender.Send(_serverAddress.IP, _serverAddress.Port, DisLabComm.CommandPack.EndSendDataCommand(_localAddress));
        }
        bool SendData()
        {
            bool haveData = false;
            long bufferLength;
            if (_length - _position <= MAX_LENGTH)
            {
                bufferLength = (long)(_length - _position);
                haveData = false;
            }
            else
            {
                bufferLength = MAX_LENGTH;
                haveData = true;

            }
            byte[] buffer = new byte[bufferLength];
            _reader.Read(buffer, 0, (int)buffer.Length);
            _position = _reader.Position;
            _sender.Send(_serverAddress.IP, _serverAddress.Port,DisLabComm.CommandPack.SendDataCommand(_localAddress,buffer ));
            return haveData;
        }
        void CloseFile()
        {
            _reader.Close();
        }
        void PopMessage()
        {
            MessageBox.Show(_message);
            _needClose = true;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (_needStop)
            {
                _needStop = false;
                SendEnd();
                //_message = "发送完成！";
                //_needPopMessage = true;
                _needClose = true;
            }
            if (_sending)
            {
                int index = 0;
                while (_sending && index < 100)
                {
                    _sending = SendData();
                    index++;
                }
                if (!_sending)
                {
                    _needStop = true;
                }
                int rate =(int)Math.Round( (double)_position / (double)_length *100,0);
                progressBar1.Value = (rate >= progressBar1.Minimum && rate <= progressBar1.Maximum) ? rate : progressBar1.Maximum;
            }
            if (_needStart)
            {
                _needStart = false;
                SendStart();
                _sending = true;                
            }
            if (_fileOpen)
            {
                _fileOpen = false;
                _needStart = true;
            }
            if (_needOpenFile)
            {
                _needOpenFile = false;
                _fileOpen = OpenFile();
                if (!_fileOpen)
                {
                    _message = _filename +_resourceManager.GetString("OpenFileErrorMSG") ;
                    _needPopMessage = true;
                }
            }
            if (_needPopMessage)
            {
                _needPopMessage = false;
                PopMessage();
            }
            if (_needClose)
            {
                _needClose = false;
                _sending = false;
                CloseFile();

                this.Close();
            }
            
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            _needClose = true;
        }
    }
}
