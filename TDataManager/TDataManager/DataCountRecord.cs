using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class DataCountRecord
    {
        public DataCountRecord()//Index的读和Count的存取；
        {
            LoadDefault();
        }
        public DataCountRecord(int index, int count)
        {
            _index = index;
            _count = count;
            _lastTime = DateTime.Now;
        }
        int _index;
        int _count;
        DateTime _lastTime = DateTime.Now;
        public int Index
        {
            get
            {
                return _index;
            }
        }
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }

        }

        public DateTime LastTime
        {
            get { return _lastTime; }
            set { _lastTime = value; }
        }
        void LoadDefault()
        {
            _index = -1;
            _count = -1;
            _lastTime = DateTime.Now;
        }
    }
}
