using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorManager
{
    public class SensorCollection
    {
        public SensorCollection()
        {
            LoadDefault();
        }
        /// <summary>
        /// 传入XMlSerialize字符串
        /// </summary>
        /// <param name="value"></param>
        public SensorCollection(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        List<SensorDefine> _sensorDefines = new List<SensorDefine>();
        /// <summary>
        /// 所有的定义
        /// </summary>
        public List<SensorDefine> SensorDefines
        {
            get { return _sensorDefines; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _sensorDefines.Clear();
        }
        /// <summary>
        /// 新增一个传感器定义
        /// </summary>
        /// <param name="manufacturerID">厂商识别码</param>
        /// <param name="name">内部名称</param>
        /// <param name="caption">外部名称</param>
        /// <param name="sensorID">识别码</param>
        /// <param name="typeID">类型码</param>
        /// <param name="decimalcount">有效小数位数</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="unit">单位名称</param>
        /// <param name="calibration">校正值</param>
        /// <param name="modeID">工作方式码</param>
        /// <param name="sensorIndex">内部序号</param>
        public void Add(int manufacturerID, string name, string caption, byte sensorID, int typeID, int decimalcount, double maxValue, double minValue, string unit, double calibration, int modeID, int sensorIndex)
        {
            if (!Contains(sensorID, sensorIndex))
            {
                _sensorDefines.Add(new SensorDefine(manufacturerID, name, caption, sensorID, typeID, decimalcount, maxValue, minValue, unit, calibration, modeID, sensorIndex));
            }
        }
        /// <summary>
        /// 新增传感器
        /// </summary>
        /// <param name="value">XML序列字符串</param>
        public void Add(string value)
        {
            SensorDefine sensorDefine = new SensorDefine(value);
            if (!Contains(sensorDefine.SensorID, sensorDefine.SensorIndex))
            {
                _sensorDefines.Add(sensorDefine);
            }
        }
        /// <summary>
        /// 判断是否已有该传感器
        /// </summary>
        /// <param name="sensorID">识别码</param>
        /// <param name="sensorIndex">内部序号</param>
        /// <returns>是否包含</returns>
        public bool Contains(int sensorID, int sensorIndex)
        {
            bool contains = false;
            foreach (SensorDefine sensorDefine in _sensorDefines)
            {
                if (sensorDefine.SensorID == sensorID && sensorDefine.SensorIndex == sensorIndex)
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        /// <summary>
        /// 根据识别码和内部序号获取传感器索引
        /// </summary>
        /// <param name="sensorID">识别码</param>
        /// <param name="sensorIndex">内部编号</param>
        /// <returns></returns>
        public int GetIndexBySensorID_SensorIndex(byte sensorID, int sensorIndex)
        {
            int index = -1;
            for (int i = 0; i < _sensorDefines.Count; i ++ )
            {
                if (_sensorDefines[i].SensorID == sensorID && _sensorDefines[i].SensorIndex == sensorIndex)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        /// <summary>
        /// 根据识别码和内部序号获取传感器定义
        /// </summary>
        /// <param name="sensorID">识别码</param>
        /// <param name="sensorIndex">内部编号</param>
        /// <returns>传感器定义，如果找不到返回默认值（非null）</returns>
        public SensorDefine GetSensorDefineBySensorID_SensorIndex(byte sensorID, int sensorIndex)
        {
            SensorDefine sensorDefine = new SensorDefine();
            int index = GetIndexBySensorID_SensorIndex(sensorID, sensorIndex);
            if (index >= 0 && index < _sensorDefines.Count)
            {
                sensorDefine = _sensorDefines[index];
            }
            return sensorDefine;
        }
        /// <summary>
        /// 根据识别码获取传感器定义
        /// </summary>
        /// <param name="sensorID">识别码</param>
        /// <returns>传感器定义，如果找不到返回默认值（非null）</returns>
        public SensorDefine GetSensorDefineBySensorID(int sensorID)
        {
            SensorDefine sensorDefine = new SensorDefine();
            sensorDefine.Caption = "ID:"+sensorID.ToString();
            sensorDefine.SensorID = sensorID;
            for (int i = 0; i < _sensorDefines.Count; i++)
            {
                if (_sensorDefines[i].SensorID == sensorID)
                {
                    sensorDefine = _sensorDefines[i];
                    break;
                }
            }
            return sensorDefine;
        }
        /// <summary>
        /// 保存传感器定义至文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否保存成功</returns>
        public bool SaveToFile(string filename)
        {
            return KeyValue.FileStream.Save(filename, this.ToString());

        }
        /// <summary>
        /// 从文件读取传感器定义
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否读取成功</returns>
        public bool LoadFromFile(string filename)
        {
            string obj = string.Empty;
            bool success = KeyValue.FileStream.Load(filename, ref obj);
            if (!string.IsNullOrEmpty(obj))
            {
                Parse(obj);
            }
            return success;
                
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns>序列化字符串</returns>
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            for (int i = 0; i < _sensorDefines.Count; i++)
            {
                keyValue.Add(i.ToString(), _sensorDefines[i].ToString());
            }
            return keyValue.ToString();
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _sensorDefines.Clear();
            for (int i = 0; i < keyValue.Count; i++)
            {
                _sensorDefines.Add(new SensorDefine(keyValue.GetValueByKey(i.ToString())));
            }
        }
        #endregion
    }
}
