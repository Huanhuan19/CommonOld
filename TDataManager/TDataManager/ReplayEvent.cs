using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class ReplayEventArgs : EventArgs//重放的事件；含有三个参数的构造函数；
    {
        public ReplayEventArgs(string sectionName,int timeIndex,ReplayStatus replayStatus)
        {
            _sectionName = sectionName;
            _timeIndex = timeIndex;
            _replayStatus = replayStatus;
        }
        #region Props
        ReplayStatus _replayStatus;
        string _sectionName;
        int _timeIndex;
        public int TimeIndex
        {
            get { return _timeIndex; }
        }
        public string SectionName
        {
            get { return _sectionName; }
        }
        public ReplayStatus ReplayStatus
        {
            get { return _replayStatus; }
        }
        #endregion
    }

    public enum ReplayStatus : int
    {
        Start = 0 , Playing = 1,Pause = 2,SwitchSection = 3,End = 4,ReStart = 5
    }

    public delegate void ReplayHandler( object sender,ReplayEventArgs e );
}
