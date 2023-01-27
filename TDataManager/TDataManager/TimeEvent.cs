using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager//SectionEventHandler事件；
{
    public class SectionEventArgs:EventArgs
    {
        public SectionEventArgs(DataEventType eventType, string sectionName, int sectionIndex)
        {
            _timeEventType = eventType;
            _sectionName = sectionName;
            _sectionIndex = sectionIndex;
        }

        #region Props
        DataEventType _timeEventType;
        string  _sectionName;
        int _sectionIndex;
        public DataEventType EventType
        {
            get { return _timeEventType; }
        }
        public string  SectionName
        {
            get { return _sectionName; }
        }
        public int SectionIndex
        {
            get { return _sectionIndex; }
        }
        #endregion
    }

    public enum DataEventType : int//数据触发类型；
    {
        Add = 0 ,Remove = 1,Modify = 2,Clear = 3,Move=4,Shink = 5,MoveUp = 6,MoveDown = 7
    }

    public delegate void SectionEventHandler(object sender,SectionEventArgs e );
}
