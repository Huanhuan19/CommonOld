using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager//定义传感器和公式，时间，时刻的枚举类型；
{
    public enum DataType : int
    {
        Other = 0 , Constant = 10,Sensor = 20 , Expr = 30,TimeIndex = 40,TimeStamp = 50
    }
}
