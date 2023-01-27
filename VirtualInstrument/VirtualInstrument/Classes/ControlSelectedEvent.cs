using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class ControlSelectedEventArgs : EventArgs
    {
        public ControlSelectedEventArgs(ControlType controlType, object obj, bool focused)
        {
            _controlType = controlType;
            _obj = obj;
            _focused = focused;
        }

        #region Props
        ControlType _controlType;
        object _obj;
        bool _focused;
        public ControlType ControlType
        {
            get { return _controlType; }
        }
        public object SelectedControl
        {
            get { return _obj; }
        }
        public bool IsFocused
        {
            get { return _focused; }
        }

        #endregion
    }

    public delegate void ControlSelectedHandler(object sender,ControlSelectedEventArgs e );
}
