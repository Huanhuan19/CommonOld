using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualInstrument
{
    public partial class Guid : UserControl
    {
        public Guid()
        {
            InitializeComponent();
            timer1.Start();
            LoadDefault();
        }

        #region Props
        string _url;
        bool _allowNavigation;
        public string Url
        {
            get { return _url; }
            set { _url = value; _needRefreshAll = true; }
        }
        public bool AllowNavigation
        {
            get { return _allowNavigation; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _url = "";
            _allowNavigation = false;
            _needRefreshAll = true;
        }
        public void Initialize(string url,bool allowNavigation)
        {
            _url = url;
            _allowNavigation = allowNavigation;
            _needRefreshAll = true;
        }

        void RefreshAll()
        {
            webBrowser1.Url = new Uri(_url); ;
            webBrowser1.AllowNavigation = _allowNavigation;
            webBrowser1.Refresh(WebBrowserRefreshOption.IfExpired);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_needRefreshAll)
            {
                _needRefreshAll = false;
                RefreshAll();
            }
        }

        #endregion

        #region Varialbles
        bool _needRefreshAll = false;
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Url", _url);
            keyValue.Add("AllowNavigation", _allowNavigation.ToString());
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            _url = keyValue.GetValueByKey("Url");
            bool.TryParse(keyValue.GetValueByKey("AllowNavigation"), out _allowNavigation);
            _needRefreshAll = true;
        }
        #endregion
    }
}
