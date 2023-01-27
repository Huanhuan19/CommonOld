using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VirtualInstrument.Classes
{
    public class PublicMethods
    {
        #region Props

        public static int STANDARD_TEXT_LENGTH = 10;
        public static Color[] BasicColors = new Color[] { Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 128, 0), Color.FromArgb(255, 0, 0, 255) };
        static Color[] _color1 = new Color[] { Color.Red,Color.Maroon,Color.DeepPink,Color.DarkRed,Color.Magenta,Color.Brown,Color.Gold,Color.OrangeRed,Color.Sienna,Color.Chocolate,Color.Orange};
        static Color[] _color2 = new Color[] { Color.Green, Color.Olive, Color.DarkOliveGreen, Color.Lime, Color.Chartreuse,Color.ForestGreen,Color.DarkGreen };
        static Color[] _color3 = new Color[] { Color.Blue, Color.DarkSlateGray, Color.BlueViolet, Color.Teal, Color.DarkBlue, Color.DarkCyan, Color.RoyalBlue, Color.DarkTurquoise,Color.SteelBlue,Color.DeepSkyBlue };
        static Color[] _highLighterColors = new Color[] { Color.FromArgb(150, 255, 0, 0), Color.FromArgb(150, 0, 255, 0), Color.FromArgb(150, 0, 0, 255) };
        #endregion
        public static float GetFontSizef(int width, int height, string text, float rate)
        {
            float min = Math.Min(width, height);
            int length = !string.IsNullOrEmpty(text ) ? text.Length : STANDARD_TEXT_LENGTH;
            float sizef = (float)width / (float)length * rate;
            return sizef > min * rate ? min * rate : sizef;

        }
        public static Color GetNewColor()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int index = random.Next(0, BasicColors.Length - 1);

            return BasicColors[index];
        }
        public static Color GetNewHighLighterColor()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int index = random.Next(0, _highLighterColors.Length - 1);

            return _highLighterColors[index];
        }

        public static Color GetNewColor(Color basicColor,int sectionIndex)
        {
            Color color = Color.Black;

            if( basicColor.ToArgb() == BasicColors[0].ToArgb() )
            {

                Random random = new Random(DateTime.Now.Millisecond);
                int index = sectionIndex;
                while (index >= _color1.Length)
                {
                    index -= _color1.Length;
                }
                color = _color1[index];
            }
            else if( basicColor.ToArgb() == BasicColors[1].ToArgb() )
            {
                Random random = new Random(DateTime.Now.Millisecond);
                int index = sectionIndex;
                while (index >= _color2.Length)
                {
                    index -= _color2.Length;
                }
                color = _color2[index];
            }
            else
            {
                Random random = new Random(DateTime.Now.Millisecond);
                int index = sectionIndex;
                while (index >= _color3.Length)
                {
                    index -= _color3.Length;
                }
                color = _color3[index];
            }
            return color;
        }
        public static Color GetNewColor(int sectionIndex)
        {
            Color color = Color.Black;
            int r = (int)Math.Round(Math.IEEERemainder(sectionIndex + 1, 3), 0);
            if (r == 0)
            {
                if (sectionIndex >= 0 && sectionIndex < _color1.Length)
                {
                    color = _color1[sectionIndex];
                }
            }
            else if (r == 1)
            {
                if (sectionIndex >= 0 && sectionIndex < _color2.Length)
                {
                    color = _color2[sectionIndex];
                }
            }
            else
            {
                if (sectionIndex >= 0 && sectionIndex < _color3.Length)
                {
                    color = _color3[sectionIndex];
                }

            }
            return color;
        }
    }
}
