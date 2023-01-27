using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace TDataManager
{
    public class ObjConvert
    {
        public static string Image2Str(Image image  )//定义了两个静态变量；
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.MemoryBmp);
            return Convert.ToBase64String(ms.GetBuffer());
        }
        public static Image Str2Image(string value)
        {
            MemoryStream memoryStream = new MemoryStream();
            byte[] array = Convert.FromBase64String(value);
            memoryStream.Write(array, 0, array.Length);
            return Image.FromStream(memoryStream, true, true);

        }
    }
}
