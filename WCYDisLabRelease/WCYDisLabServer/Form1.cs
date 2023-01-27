using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WCYDisLabServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Initialize();
            timer1.Start();
            listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
            FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        void listView1_DoubleClick(object sender, EventArgs e)
        {
            GetData();
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.None:
                case CloseReason.UserClosing:
                    if (_waiting)
                    {
                        if (MessageBox.Show("正在侦听来自客户端的数据访问，确定要停止侦听吗？", "停止侦听", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            _listener.Stop();
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    break;
            }
        }

        #region Props
        ClientManager _clientManager;
        DisLabComm.Listener _listener;
        DisLabComm.Sender _sender;
        DisLabComm.Address _address;
        string _propsFilename = "\\Properties.set";
        bool _needRefreshStatus = false,_needRefreshList = false;
        bool _waiting = false;
        #endregion

        #region Methods
        void Initialize()
        {
            _clientManager = new ClientManager();
            LoadProperties();
            _listener = new DisLabComm.Listener();
            _listener.DataRecieved += new DisLabComm.ListenerHandler(_listener_DataRecieved);
            _sender = new DisLabComm.Sender();
            
            _needRefreshStatus = true;
        }

        void _listener_DataRecieved(object sender, DisLabComm.ListenerEventArgs e)
        {
            Unpack(e.Buffer);
        }
        void Unpack(byte[] buffer)
        {
            if (buffer.Length > 1)
            {
                byte command = buffer[0];
                //byte[] addressPack = new byte[8];
                switch (command)
                {
                    case (byte)DisLabComm.CommandDefine.Connect:
                    case (byte)DisLabComm.CommandDefine.Working:
                        _clientManager.Add(buffer);
                        _needRefreshList = true;
                        break;
                    case (byte)DisLabComm.CommandDefine.GetData:
                        break;
                    case (byte)DisLabComm.CommandDefine.None:
                        break;
                    case (byte)DisLabComm.CommandDefine.SendData:
                        UnpackSendDataPack(buffer);
                        break;
                    case (byte)DisLabComm.CommandDefine.EndSend:
                        UnpackEndSendPack(buffer);
                        break;
                    case (byte) DisLabComm.CommandDefine.StartSend:
                        UnpackStartSendPack(buffer);
                        break;
                }
            }
        }
        void UnpackSendDataPack(byte[] buffer)
        {
            int index = _clientManager.Get(buffer);
            if (index >= 0)
            {
                _clientManager.Clients[index].Status = DisLabComm.CommandDefine.SendData;
                _needRefreshList = true;

                byte[] fileBuffer = new byte[buffer.Length - 9];
                for (int i = 9; i < buffer.Length; i++)
                {
                    fileBuffer[i - 9] = buffer[i];
                }
                if (_clientManager.Clients[index].Writer != null)
                {
                    _clientManager.Clients[index].Writer.Write(fileBuffer, 0, fileBuffer.Length);
                }

            }
        }
        void UnpackEndSendPack(byte[] buffer)
        {
            int index = _clientManager.Get(buffer);
            if (index >= 0)
            {
                _clientManager.Clients[index].Status = DisLabComm.CommandDefine.EndSend;
                _needRefreshList = true;
                if (_clientManager.Clients[index].Writer != null)
                {
                    _clientManager.Clients[index].Writer.Close();
                    ProcessStartInfo startInfo = new ProcessStartInfo(Application.StartupPath + "\\Client\\WCYDisLab.exe", _clientManager.Clients[index].Address.IPString + " " + _clientManager.Clients[index].Filename);
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;

                    Process.Start(startInfo);

                }
            }
        }
        void UnpackStartSendPack(byte[] buffer)
        {
            int index = _clientManager.Get(buffer);
            if (index >= 0)
            {
                _clientManager.Clients[index].Status = DisLabComm.CommandDefine.StartSend;
                _needRefreshList = true;
                if (_clientManager.Clients[index].Writer != null)
                {
                    _clientManager.Clients[index].Writer.Close();
                }

                _clientManager.Clients[index].Filename = System.IO.Path.GetTempFileName();
                _clientManager.Clients[index].Writer = System.IO.File.OpenWrite(_clientManager.Clients[index].Filename);
            }
        }
        bool LoadProperties()
        {
            string filename = Application.StartupPath + _propsFilename;
            bool success = false;
            try
            {
                System.IO.StreamReader reader = System.IO.File.OpenText(filename);
                _address = new DisLabComm.Address(reader.ReadToEnd());
                reader.Close();
                //RefreshDataSourceSensorTypeClassList();
                success = true;
            }
            catch
            {
                _address = new DisLabComm.Address();
                success = false;
            }
            return success;

        }
        public bool SaveProperties()
        {
            string filename = Application.StartupPath + _propsFilename;
            bool success = false;
            try
            {
                System.IO.StreamWriter writer = System.IO.File.CreateText(filename);
                if (writer != null)
                {
                    writer.Write(_address.ToString());
                    writer.Close();
                    //RefreshDataSourceSensorTypeClassList();
                    success = true;

                }
            }
            catch
            {
                success = false;

            }
            return success;
        }

        void RefreshStatus()
        {
            this.toolStripStatusLabel_ip.Text = _address.IPString;
            this.toolStripStatusLabel_port.Text = _address.Port.ToString();
            this.toolStripButton_props.Enabled = !_waiting;
            this.toolStripButton_start.Enabled = !_waiting;
            this.toolStripButton_stop.Enabled = _waiting;
            this.toolStripButton_view.Enabled = _waiting;
            this.toolStripProgressBar1.Visible = _waiting;
            this.toolStripStatusLabel_status.Text = _waiting ? "正在侦听..." : "停止侦听";
        }
        void RefreshList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _clientManager.Clients.Count; i++)
            {
                ListViewItem item = new ListViewItem(_clientManager.Clients[i].Address.IPString + " " + _clientManager.Clients[i].Address.Port.ToString());
                item.SubItems.Add(_clientManager.Clients[i].StatusString);
                item.SubItems.Add(_clientManager.Clients[i].SensorsString);
                switch (_clientManager.Clients[i].Status)
                {
                    case DisLabComm.CommandDefine.Connect:
                        item.ImageKey = "Connect";
                        break;
                    case DisLabComm.CommandDefine.GetData:
                        item.ImageKey = "Transfer";
                        break;
                    case DisLabComm.CommandDefine.None:
                        item.ImageKey = "Unknown";
                        break;
                    case DisLabComm.CommandDefine.SendData:
                        item.ImageKey = "Transfer";
                        break;
                    case DisLabComm.CommandDefine.Working:
                        item.ImageKey = "Working";
                        break;
                }
                listView1.Items.Add(item);
            }
        }
        void CheckLastRefreshTime()
        {
            for (int i = _clientManager.Clients.Count - 1; i >= 0; i--)
            {
                if (DateTime.Now - _clientManager.Clients[i].LastRefresh >= new TimeSpan(0, 0, 30))
                {
                    _clientManager.Remove(i);
                    _needRefreshList = true;
                }
            }
        }
        void SendRequest(DisLabComm.Address remoteAddress)
        {
            DisLabComm.Sender sender = new DisLabComm.Sender();
            sender.Send(remoteAddress.IP, remoteAddress.Port, DisLabComm.CommandPack.GetDataCommand(_address));
        } 
        void GetData()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                if (index >= 0 && index < _clientManager.Clients.Count)
                {
                    SendRequest(_clientManager.Clients[index].Address);
                }
            }
        }
        #endregion

        private void toolStripButton_start_Click(object sender, EventArgs e)
        {
            _waiting = true;
            _needRefreshStatus = true;
            _listener.Start(_address.IP, _address.Port);
        }

        private void toolStripButton_stop_Click(object sender, EventArgs e)
        {
            _waiting = false;
            _needRefreshStatus = true;
            _listener.Stop();
        }

        private void toolStripButton_view_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void toolStripButton_props_Click(object sender, EventArgs e)
        {
            FormServerProps f = new FormServerProps(_address.ToString());
            if (f.ShowDialog() == DialogResult.OK)
            {
                _address = new DisLabComm.Address(f.SelectedAddress.ToString());
                SaveProperties();
                
                _needRefreshStatus = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshStatus)
            {
                _needRefreshStatus = false;
                RefreshStatus();
            }
            if (_needRefreshList)
            {
                _needRefreshList = false;
                RefreshList();
            }
            if (_waiting)
            {
                CheckLastRefreshTime();
            }
        }

    }
}
