using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCYDataSource
{
    public class DataCatch
    {
        public DataCatch()
        {
            ClearTempData();
            _writeIndex = -1;
            _readIndex = -1;
            _tempIndex = -1;
        }
        #region Props
        byte[] _catch= new byte[20480];
        int _writeIndex = -1, _readIndex = -1 ,_tempIndex = -1;
        byte[] _tempData = new byte[2048];
        bool _locked = false;
        public bool Locked 
        {
            get { return _locked; }
        }
        public bool HaveNewData
        {
            get
            {
                return _readIndex != _writeIndex;
            }
        }
        public int Count
        {
            get
            {
                int count = 0;
                if (_readIndex < _writeIndex)
                {
                    count = _writeIndex - _readIndex;
                }
                else if (_readIndex > _writeIndex)
                {
                    count = _catch.Length - _readIndex + _writeIndex;
                }
                return count;
            }
        }
        #endregion

        #region Methods
        public void Add(byte[] buffer,int length)
        {
            lock (_catch)
            {
                _locked = true;
                for (int i = 0; i < length; i++)
                {
                    _writeIndex++;
                    if (_writeIndex < 0 || _writeIndex >= _catch.Length)
                    {
                        _writeIndex = 0;
                    }
                    _catch[_writeIndex] = buffer[i];
                }
                //---------
                //_writeIndex++;
                //if (_writeIndex < 0 || _writeIndex >= _catch.Length)
                //{
                //    _writeIndex = 0;
                //}
                //_catch[_writeIndex] = 0xff;
                //-----------------
                _locked = false;
            }
        }
        public void Read()
        {
            while (Locked)
            {
            }
            int count = Count;
            for (int i = 0; i < Count; i++)
            {
                _readIndex++;
                if (_readIndex < 0 || _readIndex >= _catch.Length)
                {
                    _readIndex = 0;
                }
                _tempIndex++;
                if (_tempIndex < 0 || _tempIndex >= _tempData.Length)
                {
                    _tempIndex = 0;
                }
                _tempData[_tempIndex] = _catch[_readIndex];
                if (_tempData[_tempIndex] == 0xff)
                {
                    byte[] buffer = new byte[_tempIndex + 1];
                    Array.Copy(_tempData, buffer, _tempIndex + 1);
                    SourceDataEventArgs e = new SourceDataEventArgs(buffer, _tempIndex+1);
                    OnSourceDataRecieved(e);
                    _tempIndex = -1;
                }

            }
        }
        public void ClearTempData()
        {
            _tempIndex = -1;
            for (int i = 0; i < _tempData.Length; i++)
            {
                if (_tempData[i] == 0xff)
                {
                    _tempData[i] = 0x00;
                    break;
                }
                _tempData[i] = 0x00;
               
            }
        }

        #endregion

        #region Events
        public event SourceDataHandler SourceDataRecieved = null;
        protected void OnSourceDataRecieved(SourceDataEventArgs e)
        {
            if (SourceDataRecieved != null)
            {
                SourceDataRecieved(this, e);
            }
        }
        #endregion
    }
}
