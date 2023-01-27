using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
namespace KeyValue
{
    public class KeyValue2
    {
        public KeyValue2()
        {
            Initialize();
        }
        public KeyValue2(string tableName)
        {
            Initialize(tableName);
        }
        void Initialize()
        {
            Initialize("KeyValue2");
        }
        void Initialize(string tableName)
        {
            _t = new DataTable(tableName);
            _t.Columns.AddRange(new DataColumn[] { 
                new DataColumn( "K",Type.GetType("System.String"),"",MappingType.Element ),
                new DataColumn( "V",Type.GetType("System.String"),"",MappingType.Element )
            });
        }
        DataTable _t;
        public DataTable DataTable
        {
            get
            {
                return _t;
            }
        }
        public void Restore(DataTable source)
        {
            Clear();
            foreach (DataRow row in source.Rows)
            {
                DataRow r = _t.NewRow();
                r["K"] = row["K"].ToString();
                r["V"] = row["V"].ToString();
                _t.Rows.Add(r);
            }
        }
        public void Clear()
        {
            _t.Rows.Clear();
        }
        public void Add(string key, string value)
        {
            DataRow r = _t.NewRow();
            r["K"] = key;
            r["V"] = value;
            _t.Rows.Add(r);
        }
        public void Replace(string key, string value)
        {
            int index = GetIndexByKey(key);
            if (index >= 0)
            {
                DataRow r = _t.Rows[index];
                r["V"] = value;
            }
            else
            {
                Add(key, value);
            }
        }
        public void Remove(string key)
        {
            int index = GetIndexByKey(key);
            while (index >= 0)
            {
                _t.Rows.RemoveAt(index);
                index = GetIndexByKey(key);
            }
        }
        public int Count
        {
            get
            {
                return _t.Rows.Count;
            }
        }
        public string GetValueByKey(string key)
        {
            int index = GetIndexByKey(key);
            string value = string.Empty;
            if (index >= 0)
            {
                value = _t.Rows[index]["V"].ToString();
            }
            return value;
        }
        public int GetIndexByKey(string key)
        {
            int index = -1;
            for (int i = 0; i < _t.Rows.Count; i++)
            {
                if (_t.Rows[i]["K"].ToString() == key)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        public bool ContainsKey(string key)
        {
            return GetIndexByKey(key) >= 0;
        }
        /// <summary>
        /// 得到DataTable Xml 序列化后的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SeiralizeDataTable(_t);
        }
        /// <summary>
        /// 从Xml 序列化的字符串得到DataTable
        /// </summary>
        /// <param name="value"></param>
        public void Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                _t.Clear();
            }
            else
            {
                _t = DeserializeDataTable(value);
            }
        }
        public static string SeiralizeDataTable(DataTable t)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, t);
            writer.Close();
            return sb.ToString();
        }

        public static DataTable DeserializeDataTable(string pXml)
        {
            DataTable t;
            try
            {
                System.IO.StringReader sr = new System.IO.StringReader(pXml);
                XmlReader reader = XmlReader.Create(sr);
                XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                t = serializer.Deserialize(reader) as DataTable;
            }
            catch//不很明白。
            {
                t = new DataTable("KeyValue2");
                t.Columns.AddRange(new DataColumn[] { 
                    new DataColumn( "K",Type.GetType("System.String"),"",MappingType.Element ),
                    new DataColumn( "V",Type.GetType("System.String"),"",MappingType.Element )
                });

            }
            return t;
        }
        public static Color ParseColor(string value)
        {
            Color color = Color.Empty;
            int colorValue;
            if (int.TryParse(value, out colorValue))
            {
                color = Color.FromArgb(colorValue);
            }
            return color;

        }
        public static string ColorToString(Color color)
        {
            return color.ToArgb().ToString();
        }
    }
}
