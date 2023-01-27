using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCYDisLabServer
{
    public class ClientInfo
    {
        public ClientInfo(DisLabComm.Address address, DisLabComm.CommandDefine status, byte[] sensors)
        {
            _address = address;
            _status = status;
            _sensors = sensors;
            _lastFresh = DateTime.Now;
        }
        public ClientInfo(byte[] pack)
        {
            Initialize(pack);
        }
        #region Props
        DateTime _lastFresh;
        DisLabComm.Address _address;
        DisLabComm.CommandDefine _status;
        byte[] _sensors;
        string _filename;
        System.IO.FileStream _writer= null;
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        public System.IO.FileStream Writer
        {
            get { return _writer; }
            set { _writer = value; }
        }
        public DisLabComm.Address Address
        {
            get { return _address; }
        }
        public DisLabComm.CommandDefine Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string StatusString
        {
            get
            {
                string str = "";
                switch (_status)
                {
                    case DisLabComm.CommandDefine.Connect:
                        str = "空闲";
                        break;
                    case DisLabComm.CommandDefine.GetData:
                        str = "获取数据";
                        break;
                    case DisLabComm.CommandDefine.None:
                        str = "其他";
                        break;
                    case DisLabComm.CommandDefine.EndSend:
                        str = "接收完毕";
                        break;
                    case DisLabComm.CommandDefine.StartSend:
                        str = "开始接收";
                        break;
                    case DisLabComm.CommandDefine.SendData:
                        str = "正在接收";
                        break;
                    case DisLabComm.CommandDefine.Working:
                        str = "正在采集";
                        break;
                }
                return str;
            }
        }
        public byte[] Sensors
        {
            get { return _sensors; }
        }
        public string SensorsString
        {
            get
            {
                string[] strList = new string[_sensors.Length];
                for (int i = 0; i < _sensors.Length; i++)
                {
                    strList[i] = _sensors[i].ToString();
                        
                }
                return string.Join(" ", strList);

            }
        }
        public DateTime LastRefresh
        {
            get { return _lastFresh; }
        }
        #endregion

        public void Initialize(byte[] pack)
        {
            if (pack.Length >= 9)
            {
                _lastFresh = DateTime.Now;
                _status = (DisLabComm.CommandDefine)pack[0];
                byte[] portPack = new byte[]{pack[5],pack[6],pack[7],pack[8]};
                int port = DisLabComm.CommandPack.BytesToInt( portPack);
                _address = new DisLabComm.Address(pack[1], pack[2], pack[3], pack[4], port);
                int sensorCount = pack.Length - 9;
                if (sensorCount > 0)
                {
                    _sensors = new byte[sensorCount];
                    for (int i = 0; i < sensorCount; i++)
                    {
                        _sensors[i] = pack[9 + i];
                    }
                }
                else
                {
                    _sensors = new byte[]{};
                }
            }
        }
        public void Refresh()
        {
            _lastFresh= DateTime.Now;
        }
        public bool Match(ClientInfo client)
        {
            return string.Equals( client.Address.IPString,this.Address.IPString ) && client.Address.Port == this.Address.Port ;
        }
    }
}
