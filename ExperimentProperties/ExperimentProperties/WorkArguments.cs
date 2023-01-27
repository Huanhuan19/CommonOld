using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExperimentProperties
{
    public class WorkArguments
    {
        public WorkArguments()
        {
            LoadDefault();
        }
        public WorkArguments(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        StartProps _startProps;
        EndProps _endProps;
        IntervalCollection _intervalCollection;
        PhotoGateProps _photoGateProps;
        public IntervalCollection IntervalCollection
        {
            get { return _intervalCollection; }
        }
        public StartProps StartProperties
        {
            get { return _startProps; }
        }
        public EndProps EndProperties
        {
            get { return _endProps; }
        }
        public PhotoGateProps PhotoGateProperties
        {
            get { return _photoGateProps; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _intervalCollection = new IntervalCollection();
            _endProps = new EndProps();
            _startProps = new StartProps();
            _photoGateProps = new PhotoGateProps();
        }
        public void New()
        {
            _endProps = new EndProps();
            _startProps = new StartProps();
            _photoGateProps = new PhotoGateProps();
            
        }
        public int CalcCapcity(double interval)
        {
            int capcity = 0;
            switch (_endProps.EndMode)
            {
                case EndMode.IndexCount:
                    capcity = _endProps.ReservedPoints;
                    break;
                case EndMode.Manual:
                case EndMode.Remote:
                case EndMode.TimeStamp:
                case EndMode.ValueDecrease:
                case EndMode.ValueIncrease:
                    capcity = (int)Math.Round(_endProps.ReservedSeconds / interval,0) +1;
                    break;
            }
            return capcity;
        }
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("StartProps", _startProps.ToString());
            keyValue.Add("EndProps", _endProps.ToString());
            //keyValue.Add("IntervalCollection", _intervalCollection.ToString());
            keyValue.Add("PhotoGateProps", _photoGateProps.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _startProps.Parse(keyValue.GetValueByKey("StartProps"));
            _endProps.Parse(keyValue.GetValueByKey("EndProps"));
            //_intervalCollection.Parse(keyValue.GetValueByKey("IntervalCollection"));
            _photoGateProps.Parse(keyValue.GetValueByKey("PhotoGateProps"));
        }
        #endregion
    }
}
