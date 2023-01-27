using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCYDataSource
{
    public class PortCollection
    {
        public PortCollection()
        {
            _ports = new PortInfo[DEFAULT_PORTCOUNT];
            InitPorts();
        }
        public PortCollection(int count)
        {
            _ports = new PortInfo[count];
            InitPorts();
        }
        #region Props
        static int DEFAULT_PORTCOUNT = 256;
        PortInfo[] _ports;
        public PortInfo[] Ports
        {
            get { return _ports; }
        }
        public int[] SensorTypes
        {
            get
            {
                List<int> types = new List<int>();
                for (int i = 0; i < _ports.Length; i++)
                {
                    if (_ports[i].PortAvailable)
                    {
                        types.Add(_ports[i].PortIndex);
                    }
                }
                return types.ToArray();
            }
        }
        public int PortCount
        {
            get { return _ports.Length; }
        }
        public int AvailablePortCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < _ports.Length; i++)
                {
                    if (_ports[i] != null && _ports[i].PortAvailable)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public List<int> AvailablePortIndexes
        {
            get
            {
                List<int> list = new List<int>();
                for (int i = 0; i < _ports.Length; i++)
                {
                    if (_ports[i] != null && _ports[i].PortAvailable)
                    {
                        list.Add(i);
                    }
                }
                return list;
            }
        }
        public int PlugInPortCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < _ports.Length; i++)
                {
                    if (_ports[i] != null && _ports[i].PortAvailable&&_ports[i].IsPlugIn)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public List<int> PlugInPortIndexes
        {
            get
            {
                List<int> list = new List<int>();
                for (int i = 0; i < _ports.Length; i++)
                {
                    if (_ports[i] != null && _ports[i].PortAvailable && _ports[i].IsPlugIn)
                    {
                        list.Add(i);
                    }
                }
                return list;
            }
        }
        #endregion

        #region Methods
        void InitPorts()
        {
            for (int i = 0; i < _ports.Length; i++)
            {
                _ports[i] = new PortInfo();
            }
        }
        public void PlugIn( int index,int portIndex,SensorTypeDefine sensorType,float k ,float b)
        {
            if (index >= 0 && index < _ports.Length)
            {
                if( _ports[index] == null )
                {
                    _ports[index ] = new PortInfo(portIndex,sensorType ,k ,b);
                }
                else
                {
                   _ports[index].PlugIn(portIndex,sensorType,k ,b);
                }
            }
        }
        public void PlugOut(int index)
        {
            if (index >= 0 && index < _ports.Length)
            {
                if (_ports[index] == null)
                {
                    _ports[index] = new PortInfo();
                    _ports[index].PortAvailable = true;
                }
                else
                {
                    _ports[index].PlugOut();
                }
            }

        }
        public bool Contains(int index)
        {
            return AvailablePortIndexes.Contains(index);
        }
        public bool PortChanged(int index, int portIndex)
        {
            bool changed = false;
            if (index >= 0 && index < PortCount)
            {
                if (_ports[index].PortIndex != portIndex)
                {
                    changed = true;
                }
            }
            return changed;
        }
        public void SetValue(int index, float value)
        {
            if (index >= 0 && index < _ports.Length)
            {
                if (_ports[index] == null)
                {
                    _ports[index] = new PortInfo();
                }
                _ports[index].LastValue = value;
            }
        }
        #endregion

    }
}
