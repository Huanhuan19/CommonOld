using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualInstrument.Classes
{
    public class CameraProps
    {
        public CameraProps()
        {
            LoadDefault();
        }
        public CameraProps(bool useExCamera, string exCameraFilename, string exVideoPath)
        {
            Initialize(useExCamera, exCameraFilename, exVideoPath);
        }
        public CameraProps(string value)
        {
            LoadDefault();
            Parse(value);
        }
        #region Props
        bool _useExCamera;
        string _exCameraFilename;
        string _exVideoPath;
        public bool UseExCamera
        {
            get { return _useExCamera; }
            set { _useExCamera = value; }
        }
        public string ExCameraFilename
        {
            get { return _exCameraFilename; }
            set { _exCameraFilename = value; }
        }
        public string ExVideoPath
        {
            get { return _exVideoPath; }
            set { _exVideoPath = value; }
        }

        #endregion

        #region Methods
        void LoadDefault()
        {
            _useExCamera = false;
            _exCameraFilename = "";
            _exVideoPath = "";
        }
        void Initialize(bool useExCamera, string exCameraFilename, string exVideoPath)
        {
            _useExCamera = useExCamera;
            _exCameraFilename = exCameraFilename;
            _exVideoPath = exVideoPath;
        }
        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("UseExCamera", _useExCamera.ToString());
            keyValue.Add("ExCameraFilename", _exCameraFilename);
            keyValue.Add("ExVideoPath", _exVideoPath);
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            bool.TryParse(keyValue.GetValueByKey("UseExCamera"), out _useExCamera);
            _exVideoPath = keyValue.GetValueByKey("ExVideoPath");
            _exCameraFilename = keyValue.GetValueByKey("ExCameraFilename");
        }

        #endregion

    }
}
