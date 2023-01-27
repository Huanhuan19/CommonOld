using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class ConstantArgs : EventArgs//常数事件；
    {
        DataEventType _eventType;
        ConstantElement _constantElement;
        public ConstantArgs(DataEventType eventType, ConstantElement constantElement)
        {
            _eventType = eventType;
            _constantElement = constantElement;
        }
        public DataEventType EventType
        {
            get { return _eventType; }
        }
        public ConstantElement ConstantElement
        {
            get { return _constantElement; }
        }
    }
    public delegate void ConstantHandler(object sender, ConstantArgs e);
}
