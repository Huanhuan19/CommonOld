using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VirtualInstrument.Classes
{
    /// <summary>
    /// 在Graphics上操作时的不同动作类型
    /// </summary>
    public enum GraphicsSelectType
    {
        None,SelectData,SelectLabel,SelectArea,SelectCurve,SelectPoint,HighLighter
    }
    public class GraphicsSelectColorDefine
    {
        public static Color SelectedBorderColor(GraphicsSelectType type)
        {
            Color color = Color.Empty;
            switch (type)
            {
                default:
                case GraphicsSelectType.SelectArea:
                    color = Color.DarkCyan;
                    break;
                case GraphicsSelectType.SelectData:
                    color = Color.DarkGreen;
                    break;
                case GraphicsSelectType.SelectPoint:
                    color = Color.DarkBlue;
                    break;
            }
            return color;
        }
        public static Color SelectedAreaColor(GraphicsSelectType type)
        {
            Color color = Color.Empty;
            switch (type)
            {
                default:
                case GraphicsSelectType.SelectArea:
                    color = Color.LightCyan;
                    break;
                case GraphicsSelectType.SelectData:
                    color = Color.LightGreen;
                    break;
                case GraphicsSelectType.SelectPoint:
                    color = Color.LightBlue;
                    break;
            }
            return color;
        }
    }        
}
