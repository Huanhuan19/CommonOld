using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExperimentProperties
{   
    /// <summary>
    /// Manual 手工控制，ValueIncrease值上升触发，ValueDecrease值下降触发，PhotoGate光电门方式触发，TimeStamp时间触发，IndexCount数据个数触发，Remote远程控制
    /// </summary>
    public enum EndMode:int
    {
        Manual = 0, ValueIncrease = 4, ValueDecrease = 5, TimeStamp = 3, IndexCount = 2, Remote = 6
       
    }
}
