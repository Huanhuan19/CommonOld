using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WCYDisLab.Classes
{
    public class PublicMethods
    {
        #region Props
        #endregion

        #region Methods
        public static Point GetLocation(LocationType locationType, Rectangle mainRect, Point parentLoction, Size parentSize, Size childSize, int distance)
        {
            Point location = parentLoction;
            int pxm = parentLoction.X + parentSize.Width / 2;//父控件水平中点
            int pym = parentLoction.Y + parentSize.Height / 2;//父控件垂直中点
            int cxl = childSize.Width / 2;//子控件水平半长
            int cyl = childSize.Height / 2;//子控件垂直半长
            switch (locationType)
            {
                case LocationType.Top:
                    //location.X = pxm - cxl;
                    location.X = parentLoction.X;
                    location.Y = parentLoction.Y - childSize.Height - distance;
                    break;
                case LocationType.Bottom:
                    //location.X = pxm - cxl;
                    location.X = parentLoction.X;
                    location.Y = parentLoction.Y + parentSize.Height + distance;
                    break;
                case LocationType.Left:
                    location.X = parentLoction.X - childSize.Width - distance;
                    //location.Y = pym - cyl;
                    location.Y = parentLoction.Y;
                    break;
                case LocationType.Right:
                    location.X = parentLoction.X + parentSize.Width + distance;
                    //location.Y = pym - cyl;
                    location.Y = parentLoction.Y;
                    break;
                case LocationType.Center:
                    location.X = pxm - cxl;
                    location.Y = pym - cyl;
                    break;
            }
            //判断是否越过边界
            if (location.X < mainRect.Left)
            {
                location.X = mainRect.Left;
            }
            if (location.Y < mainRect.Top)
            {
                location.Y = mainRect.Top;
            }
            if (location.X + childSize.Width > mainRect.Right)
            {
                location.X = mainRect.Right - childSize.Width;
            }
            if (location.Y + childSize.Height > mainRect.Bottom)
            {
                location.Y = mainRect.Bottom - childSize.Height;
            }
            return location;
        }

        public static string Bounds2Str(Rectangle rect)
        {
            return rect.Left.ToString() + "_" + rect.Top.ToString() + "_" + rect.Right.ToString() + "_" + rect.Bottom.ToString();
        }
        public static Rectangle RectangleParse(string value)
        {
            Rectangle rect = Rectangle.Empty;
            string[] strLit = value.Split('_');
            if (strLit.Length == 4)
            {
                int left, top, right, bottom;
                int.TryParse(strLit[0], out left);
                int.TryParse(strLit[1], out top);
                int.TryParse(strLit[2], out right);
                int.TryParse(strLit[3], out bottom);
                rect = Rectangle.FromLTRB(left, top, right, bottom);
            }
            return rect;
        }
        public static float CalcFontSize(int rate)
        {
            return Properties.Settings.Default.DefaultFontSize * (float)Math.Pow(Properties.Settings.Default.FontSizeSeed, rate);
        }

        #endregion
    }

    public enum LocationType : int
    {
        Top = 0, Bottom = 1, Left = 2, Right = 3, Center = 4
    }
}
