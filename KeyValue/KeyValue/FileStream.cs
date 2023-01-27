using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyValue
{
    public class FileStream
    {
        public static bool Load(string filename, ref string obj)//文件读取 
        {
            bool success = false;
            try
            {
                System.IO.StreamReader reader = System.IO.File.OpenText(filename);
                obj = reader.ReadToEnd();
                reader.Close();
                //RefreshDataSourceSensorTypeClassList();
                success = true;
            }
            catch
            {
                obj = string.Empty;
                success = false;
            }
            return success;

        }
        public static bool Save(string filename, string obj)//文件的创建和存储
        {
            bool success = false;
            try
            {
                System.IO.StreamWriter writer = System.IO.File.CreateText(filename);
                if (writer != null)
                {
                    writer.Write(obj );
                    writer.Close();
                    //RefreshDataSourceSensorTypeClassList();
                    success = true;

                }
            }
            catch
            {
                success = false;

            }
            return success;

        }
    }
}
