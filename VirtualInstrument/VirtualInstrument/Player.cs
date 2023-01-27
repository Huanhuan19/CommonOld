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
    public partial class Player : UserControl
    {
        public Player()
        {
            InitializeComponent();
            LoadDefault();
        }

        #region Props
        Microsoft.DirectX.AudioVideoPlayback.Video _video;

        string _filename;
        bool _available;
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        public bool Available
        {
            get { return _available; }
        }
        #endregion

        #region Methods
        void LoadDefault()
        {
            _video = null;
            _filename = "";
            _available = false;
        }
        public bool Initialize(string filename)
        {
            try
            {
                _video = new Microsoft.DirectX.AudioVideoPlayback.Video(filename, false);
                _video.Owner = this;
                _video.Size = _video.Owner.Size;
                _filename = filename;
                _available = true;
            }
            catch
            {
                LoadDefault();
            }
            return _available;
        }

        public void Open(string filename)
        {
            if (_video != null)
            {
                _video.Open(filename, false);
            }
            else
            {
                Initialize(filename);
            }
        }
        public void Play()
        {
            if (_video != null)
            {
                _video.Play();

            }
        }
        public void Pause()
        {
            if (_video != null)
            {
                if (_video.Playing)
                {
                    _video.Pause();
                }
            }
        }
        public void Resume()
        {
            if (_video != null)
            {

                if (_video.Paused)
                {
                    _video.Play();
                }
            }
        }
        public void Rewind()
        {
            if (_video != null)
            {

                _video.CurrentPosition = 0;
            }
        }
        public void Stop()
        {
            if (_video != null)
            {
                if (_video.Playing)
                {
                    _video.Stop();
                }
            }
        }


        #endregion

        #region Serialize
        public override string ToString()
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Add("Filename", _filename);
            return keyValue.ToString();
        }
        public void Parse(string value)
        {
            KeyValue.KeyValue keyValue = new KeyValue.KeyValue();
            keyValue.Parse(value);
            Initialize(keyValue.GetValueByKey("Filename"));

        }
        #endregion
    }
}
