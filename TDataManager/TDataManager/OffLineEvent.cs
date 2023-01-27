using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager//离线事件；
{
    public class OffLineEventArgs: EventArgs
    {
        public OffLineEventArgs(bool offLine)
        {
            _offLine = offLine;
        }

        bool _offLine;
        public bool OffLine
        {
            get
            {
                return _offLine;
            }
        }
    }
    public delegate void OffLineHandler( object sender,OffLineEventArgs e );
}
