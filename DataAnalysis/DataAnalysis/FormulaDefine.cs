using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAnalysis
{
    public class FormulaDefine
    {
        public FormulaDefine()
        {
            LoadDefault();
        }
        public FormulaDefine(string name, string expression, string description, int parts)
        {
            _name = name;
            _expression = expression;
            _description = description;
            _parts = parts;
        }
        #region Props
        string _name;
        string _description;
        string _expression ;
        int _parts ;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
            }
        }
        public int Parts
        {
            get
            {
                return _parts;
            }
            set
            {
                _parts = value;
            }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _name = "";
            _description = "";
            _expression = "";
            _parts = 1;

        }
        #endregion
    }
}
