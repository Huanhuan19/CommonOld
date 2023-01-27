using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TDataManager
{
    public class Replay
    {
        public Replay()
        {
            LoadDefault();
        }
        #region Props
        List<ReplayProps> _replayPropsList = new List<ReplayProps>();
        Thread _thread;
        bool _looping;
        bool _pause;
        int _lastTimeIndex;
        int _currentSectionIndex;
        double _rate;
        DateTime _lastRefreshTime;
        ReplayStatus _status;
        public ReplayStatus Status
        {
            get { return _status; }
        }
        public string CurrentSectionName
        {
            get
            {
                string sectionName = "";
                if (_currentSectionIndex >= 0 && _currentSectionIndex < _replayPropsList.Count)
                {
                    sectionName = _replayPropsList[_currentSectionIndex].SectionName;
                }
                return sectionName;
            }
        }
        bool CanPopup
        {
            get
            {
                bool canPopup = false;
                if (!_pause)
                {
                    if (_currentSectionIndex >= 0 && _currentSectionIndex < _replayPropsList.Count)
                    {
                        ReplayProps replayProps = _replayPropsList[_currentSectionIndex];
                        TimeSpan span = DateTime.Now - _lastRefreshTime;
                        if (span.TotalSeconds >= replayProps.Interval / _rate)
                        {
                            canPopup = true;

                        }
                    }
                }
                return canPopup;

            }
        }
        bool ShouldSwitchToNext
        {
            get
            {
                bool shouldSwitch = false;
                if (_currentSectionIndex >= 0 && _currentSectionIndex < _replayPropsList.Count)
                {
                    ReplayProps replayProps = _replayPropsList[_currentSectionIndex];
                    if (_lastTimeIndex >= replayProps.Count)
                    {
                        shouldSwitch = true;
                    }
                }
                return shouldSwitch;
            }
        }
        public float Process
        {
            get
            {
                int count = 0 ,overCount = 0;
                for (int i = 0; i < _replayPropsList.Count; i++)
                {
                    count += _replayPropsList[i].Count;
                }
                if (_currentSectionIndex >= 0 && _currentSectionIndex < _replayPropsList.Count)
                {
                    for (int i = 0; i < _currentSectionIndex;i++ )
                    {
                        overCount += _replayPropsList[i].Count;
                    }
                }
                overCount += _lastTimeIndex;
                return count > 0 ? ((float)overCount / (float)count) : 0;
            }
        }
        public List<string> ReplaySectionNames
        {
            get
            {
                List<string> sectionNames = new List<string>();
                for (int i = 0; i < _replayPropsList.Count; i++)
                {
                    sectionNames.Add(_replayPropsList[i].SectionName);
                }
                return sectionNames;
            }
        }
        #endregion

        #region Event
        public event ReplayHandler ReplayEvent = null;
        protected void OnReplay(ReplayEventArgs e)
        {
            if (ReplayEvent != null)
            {
                ReplayEvent(this, e);
            }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _replayPropsList.Clear();
            
            _pause = false;
            _currentSectionIndex = -1;
            _looping = false;
            _lastTimeIndex = -1;
            _rate = 1;
            _lastRefreshTime = DateTime.Now;
            _status = ReplayStatus.End;
        }
        public void Initialize(List<ReplayProps> replayPropsList,double rate)
        {
            _replayPropsList.Clear();
            _replayPropsList.AddRange(replayPropsList);
            _pause = false;
            _lastTimeIndex = -1;
            _currentSectionIndex = -1;
            _rate = rate;
            _lastRefreshTime = DateTime.Now;
            _status = ReplayStatus.End;
        }
        void Looping()
        {
            while (_looping)
            {
                if (ShouldSwitchToNext)
                {
                    if (!NextSection())
                    {
                        Stop();
                    }
                    else
                    {
                        
                        _status = ReplayStatus.SwitchSection;
                        ReplayEventArgs e = new ReplayEventArgs(CurrentSectionName, _lastTimeIndex, ReplayStatus.SwitchSection);
                        OnReplay(e);
                    }
                }
                else
                {
                    if (CanPopup)
                    {
                        _lastTimeIndex++;
                        if (_looping)
                        {
                            Popup();
                        }
                    }

                }
                Thread.Sleep(0);
            }
        }
        bool NextSection()
        {
            bool haveNext = false;
            _currentSectionIndex++;
            if (_currentSectionIndex >= 0 && _currentSectionIndex < _replayPropsList.Count)
            {
                _lastTimeIndex = -1;
                _replayPropsList[_currentSectionIndex].StartTime = DateTime.Now;
                
                haveNext = true;
            }
            return haveNext;
        }
        void Popup()
        {
            _status = ReplayStatus.Playing;
            _lastRefreshTime = DateTime.Now;
            ReplayEventArgs e = new ReplayEventArgs(CurrentSectionName, _lastTimeIndex, ReplayStatus.Playing);
            OnReplay(e);
        }
        public void Start()
        {
            _pause = false;
            _currentSectionIndex = 0;
            if (_currentSectionIndex >= 0 && _currentSectionIndex < _replayPropsList.Count)
            {
                _lastTimeIndex = -1;
                _looping = true;
                _thread = new Thread(Looping);
                _thread.Start();
                _status = ReplayStatus.Start;
                ReplayEventArgs e = new ReplayEventArgs(CurrentSectionName, _lastTimeIndex, ReplayStatus.Start);
                OnReplay(e);
            }
        }
        public void Pause()
        {
            _pause = !_pause;
            _status = ReplayStatus.Pause;
            if (_pause)
            {
                ReplayEventArgs e = new ReplayEventArgs(CurrentSectionName, _lastTimeIndex, ReplayStatus.Pause);
                OnReplay(e);
            }
            else
            {
                ReplayEventArgs e = new ReplayEventArgs(CurrentSectionName, _lastTimeIndex, ReplayStatus.ReStart);
                OnReplay(e);

            }
        }
        public void Stop()
        {
            _looping = false;
            _status = ReplayStatus.End;
            ReplayEventArgs e = new ReplayEventArgs("", -1, ReplayStatus.End);
            OnReplay(e);
        }
        #endregion
    }
}
