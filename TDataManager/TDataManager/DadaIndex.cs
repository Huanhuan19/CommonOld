using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDataManager
{
    public class DadaIndex//数据索引类；
    {
        public DadaIndex()
        {
            LoadDefault();
        }
        /// <summary>
        /// 数据索引
        /// </summary>
        /// <param name="index">索引值</param>
        public DadaIndex( int index)
        {
            Initialize( index);
        }
        /// <summary>
        /// 数据索引
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public DadaIndex(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        
        
        int _index;
        /// <summary>
        /// 索引值
        /// </summary>
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Available
        {
            get { return _index != -1; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _index = -1;
        }
        void Initialize(int index)
        {
            _index = index;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns>序列化字符串</returns>
        public override string ToString()
        {
            return _index.ToString();
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="value">序列化字符串</param>
        public void Parse(string value)
        {
            int.TryParse(value, out _index);
        }
        #endregion
    }
}
