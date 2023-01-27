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
    public partial class Camera2 : UserControl
    {
        public Camera2()
        {
            InitializeComponent();
            Initialize();
            this.Disposed += new EventHandler(Camera2_Disposed);
        }

        void Camera2_Disposed(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                _capture.Stop();
                _capture.PreviewWindow = null;
            }
        }

        #region Props
        DirectX.Capture.Capture _capture;
        DirectX.Capture.Filters _filters = new DirectX.Capture.Filters();
        public bool Cued
        {
            get { return _capture.Cued;}
        }
        #endregion

        #region Methods
        bool Initialize()
        {
            bool success = false;
            
            if (_filters.VideoInputDevices.Count > 0 && _filters.AudioInputDevices.Count > 0)
            {
                _capture = new DirectX.Capture.Capture(_filters.VideoInputDevices[0], _filters.AudioInputDevices[0]);
                success = true;
                _capture.FrameRate = 15;
                _capture.FrameSize = new Size(320, 240);
                
            }
            return success;
        }
        public void Preview()
        {
            try
            {
                if (_capture.PreviewWindow == null)
                {
                    _capture.PreviewWindow = this.panel1;
                }
                else
                {
                    _capture.PreviewWindow = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to enable/disable preview. Please submit a bug report.\n\n" + ex.Message + "\n\n" + ex.ToString());
            }

        }
        public void StartRecord(string filename)
        {
            try
            {
                if (_capture == null)
                    throw new ApplicationException("Please select a video and/or audio device.");
                if (!_capture.Cued)
                    _capture.Filename = filename;
                _capture.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.ToString());
            }

        }
        public void StopRecord()
        {
            try
            {
                if (_capture == null)
                    throw new ApplicationException("Please select a video and/or audio device.");
                _capture.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.ToString());
            }
        }
        public void Stop()
        {
            if (_capture != null)
            {
                _capture.Stop();
            }
        }
        #endregion
    }
}
