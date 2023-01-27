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
    public partial class Camera : UserControl
    {
        public Camera()
        {
            InitializeComponent();
            Init();
        }

        #region Props
        Classes.Camera _camera;
        bool _isStarted = false, _isRecording = false;
        public Classes.Camera CameraClass
        {
            get { return _camera; }
        }
        public bool IsStarted
        {
            get { return _isStarted; }
        }
        public bool IsRecording
        {
            get { return _isRecording; }
        }
        #endregion

        #region Methods
        void Init()
        {
            _camera =new VirtualInstrument.Classes.Camera(this.Handle, 0, 0, this.Width, this.Height);

        }
        public void Start()
        {
            _camera.Start();
            _isStarted = true;
        }
        public void Stop()
        {
            _camera.Stop();
            _isStarted = false;
        }
        public void GrabImage(string filename)
        {
            _camera.GrabImage(filename);
        }
        public void StartRecord(string filename)
        {
            _camera.Kinescope(filename);
            _isRecording = true;
        }
        public void StartRecord()
        {
            _camera.Kinescope();
            _isRecording = true;
        }
        public void StopRecord()
        {
            _camera.StopKinescope();
            _isRecording = false;
        }
        #endregion
    }
}
