using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class ColumnDefine
    {

        public ColumnDefine()
        {
            LoadDefault();
        }
        public ColumnDefine(string columnName, string columnCaption, int decimalCount)
        {
            _columnCaption = columnCaption;
            _columnName = columnName;
            _decimal = decimalCount;
        }
        public ColumnDefine(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        string _columnName;
        string _columnCaption;
        int _decimal;
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }
        public string ColumnCaption
        {
            get { return _columnCaption; }
            set { _columnCaption = value; }
        }
        public int Decimal
        {
            get { return _decimal; }
            set { _decimal = value; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _columnName = "";
            _columnCaption = "";
            _decimal = 2;
        }

        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("ColumnName", _columnName);
            keyValue.Add("ColumnCaption", _columnCaption);
            keyValue.Add("Decimal", _decimal.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _columnName = keyValue.GetValueByKey("ColumnName");
            _columnCaption = keyValue.GetValueByKey("ColumnCaption");
            int.TryParse(keyValue.GetValueByKey("Decimal"), out _decimal);

        }
        #endregion
    }
}
