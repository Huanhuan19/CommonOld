 using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;


// ------------------------------------------------------------------
// 窗口消息 WM_CAP... 哪个是送到 AVICAP 窗口
// ------------------------------------------------------------------


//WM_CAP_START= WM_USER=1024;

//// start of unicode messages
//WM_CAP_UNICODE_START=            WM_USER+100; //开始

//WM_CAP_GET_CAPSTREAMPTR=         (WM_CAP_START+ 1); //获得 CAPSTR EAMPTR

//WM_CAP_SET_CALLBACK_ERROR=       (WM_CAP_START+ 2); //设置回调错误
//WM_CAP_SET_CALLBACK_STATUS=      (WM_CAP_START+ 3); //设置回调状态

//WM_CAP_SET_CALLBACK_YIELD=       (WM_CAP_START+ 4); //设置回调出产
//WM_CAP_SET_CALLBACK_FRAME=       (WM_CAP_START+ 5); //设置回调结构
//WM_CAP_SET_CALLBACK_VIDEOSTREAM= (WM_CAP_START+ 6); //设置回调视频流
//WM_CAP_SET_CALLBACK_WAVESTREAM= (WM_CAP_START+ 7); //设置回调视频波流
//WM_CAP_GET_USER_DATA            =(WM_CAP_START+ 8); //获得使用者数据
//WM_CAP_SET_USER_DATA           =(WM_CAP_START+ 9) ; //设置使用者数据

//WM_CAP_DRIVER_CONNECT          =(WM_CAP_START+ 10); //驱动程序连接
//WM_CAP_DRIVER_DISCONNECT       =(WM_CAP_START+ 11); //断开启动程序连接

//WM_CAP_DRIVER_GET_NAME         =(WM_CAP_START+ 12); //获得驱动程序名字
//WM_CAP_DRIVER_GET_VERSION      =(WM_CAP_START+ 13); //获得驱动程序版本

//WM_CAP_DRIVER_GET_CAPS         =(WM_CAP_START+ 14); //获得驱动程序性能

//WM_CAP_FILE_SET_CAPTURE_FILE    =(WM_CAP_START+ 20); //设置捕获文件的文件名
//WM_CAP_FILE_GET_CAPTURE_FILE    =(WM_CAP_START+ 21); //获得捕获文件的文件名
//WM_CAP_FILE_SAVEAS              =(WM_CAP_START+ 23); //另存文件为
//WM_CAP_FILE_SAVEDIB             =(WM_CAP_START+ 25); //保存文件

//// out of order to save on ifdefs
//WM_CAP_FILE_ALLOCATE           =(WM_CAP_START+ 22); //分派文件, 为捕获文件建一个指定大小的文件
//WM_CAP_FILE_SET_INFOCHUNK      =(WM_CAP_START+ 24); //设置开始文件

//WM_CAP_EDIT_COPY               =(WM_CAP_START+ 30); //编辑复制,把图象考入剪贴板

//WM_CAP_SET_AUDIOFORMAT         =(WM_CAP_START+ 35); //设置音频格式
//WM_CAP_GET_AUDIOFORMAT         =(WM_CAP_START+ 36); //捕获音频格式

//WM_CAP_DLG_VIDEOFORMAT         =(WM_CAP_START+ 41); //1065 打开视频格式设置对话框, 选择数字视频的框架大小和视频图像的色深，以及捕获视频图像的压缩格式。
//WM_CAP_DLG_VIDEOSOURCE         =(WM_CAP_START+ 42); //1066 打开属性设置对话框，设置对比度、亮度等。(视频源对话框) 选择视频输入通道和视频图像的动态参数。
//WM_CAP_DLG_VIDEODISPLAY        =(WM_CAP_START+ 43); //1067 打开视频显示对话框
//WM_CAP_GET_VIDEOFORMAT         =(WM_CAP_START+ 44); //1068 获得视频格式
//WM_CAP_SET_VIDEOFORMAT         =(WM_CAP_START+ 45); //1069 设置视频格式
//WM_CAP_DLG_VIDEOCOMPRESSION    =(WM_CAP_START+ 46); //1070 打开压缩设置对话框

//WM_CAP_SET_PREVIEW             =(WM_CAP_START+ 50); //设置预览模式
//WM_CAP_SET_OVERLAY             =(WM_CAP_START+ 51); //设置覆盖
//WM_CAP_SET_PREVIEWRATE         =(WM_CAP_START+ 52); //设置预览模式下图像的帧速度
//WM_CAP_SET_SCALE               =(WM_CAP_START+ 53); //设置预览视频的缩放比例
//WM_CAP_GET_STATUS              =(WM_CAP_START+ 54); //获得状态
//WM_CAP_SET_SCROLL              =(WM_CAP_START+ 55); //设置卷

//WM_CAP_GRAB_FRame              =(WM_CAP_START+ 60); //逮捕结构
//WM_CAP_GRAB_FRame_NOSTOP       =(WM_CAP_START+ 61); //停止逮捕结构,截取当前图象

//WM_CAP_SEQUENCE                =(WM_CAP_START+ 62); //次序，捕捉到文件
//WM_CAP_SEQUENCE_NOFILE         =(WM_CAP_START+ 63); //没有文件
//WM_CAP_SET_SEQUENCE_SETUP      =(WM_CAP_START+ 64); //设置当前捕获的帧频率
//WM_CAP_GET_SEQUENCE_SETUP      =(WM_CAP_START+ 65); //获得当前捕获的帧频率

//WM_CAP_SET_MCI_DEVICE          =(WM_CAP_START+ 66); //设置媒体控制接口
//WM_CAP_GET_MCI_DEVICE          =(WM_CAP_START+ 67); //获得媒体控制接口

//WM_CAP_STOP                    =(WM_CAP_START+ 68); //停止捕捉
//WM_CAP_ABORT                   =(WM_CAP_START+ 69); //异常中断

//WM_CAP_SINGLE_FRame_OPEN       =(WM_CAP_START+ 70); //打开单一的结构
//WM_CAP_SINGLE_FRame_CLOSE      =(WM_CAP_START+ 71); //关闭单一的结构
//WM_CAP_SINGLE_FRame            =(WM_CAP_START+ 72); //单一的结构

//WM_CAP_PAL_OPEN                =(WM_CAP_START+ 80); //打开视频
//WM_CAP_PAL_SAVE                =(WM_CAP_START+ 81); //保存视频

//WM_CAP_PAL_PASTE               =(WM_CAP_START+ 82); //粘贴视频
//WM_CAP_PAL_AUTOCREATE          =(WM_CAP_START+ 83); //自动创造
//WM_CAP_PAL_MANUALCREATE        =(WM_CAP_START+ 84); //手动创造

//// Following added post VFW 1.1
//WM_CAP_SET_CALLBACK_CAPCONTROL =(WM_CAP_START+ 85); // 设置收回的错误

//WM_CAP_END                      =WM_CAP_SET_CALLBACK_CAPCONTROL;



namespace VirtualInstrument.Classes
{

    /// <summary>
    /// 一个控制摄像头的类
    /// </summary>
    public class Camera
    {
        // 开始定义消息参数 整数型
        const string avicap32 = "avicap32.dll";

        private const int WM_USER = 0x400;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CAP_START = WM_USER;
        private const int WM_CAP_STOP = WM_CAP_START + 68;
        private const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;
        private const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;
        private const int WM_CAP_SAVEDIB = WM_CAP_START + 25;
        private const int WM_CAP_GRAB_FRAME = WM_CAP_START + 60;
        private const int WM_CAP_SEQUENCE = WM_CAP_START + 62;
        private const int WM_CAP_FILE_SET_CAPTURE_FILEA = WM_CAP_START + 20;
        private const int WM_CAP_SEQUENCE_NOFILE = WM_CAP_START + 63;
        private const int WM_CAP_SET_OVERLAY = WM_CAP_START + 51;
        private const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
        private const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START + 6;
        private const int WM_CAP_SET_CALLBACK_ERROR = WM_CAP_START + 2;
        private const int WM_CAP_SET_CALLBACK_STATUSA = WM_CAP_START + 3;
        private const int WM_CAP_SET_CALLBACK_FRAME = WM_CAP_START + 5;
        private const int WM_CAP_SET_SCALE = WM_CAP_START + 53;
        private const int WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52;
        private const int WM_COPYDATA = 0x004A;
        private const int HWND_BROADCAST = 0xffff;
        private IntPtr hWndC;
        private bool bStat = false;

        private IntPtr mControlPtr;
        private int mWidth;
        private int mHeight;
        private int mLeft;
        private int mTop;

        /// <summary>
        /// 初始化摄像头
        /// </summary>
        /// <param name="handle">控件的句柄</param>
        /// <param name="left">开始显示的左边距</param>
        /// <param name="top">开始显示的上边距</param>
        /// <param name="width">要显示的宽度</param>
        /// <param name="height">要显示的长度</param>
        public Camera(IntPtr handle, int left, int top, int width, int height)
        {
            ReadProps();
            if (!_cameraProps.UseExCamera)
            {
                mControlPtr = handle;
                mWidth = width;
                mHeight = height;
                mLeft = left;
                mTop = top;
            }
            else
            {
                exhWndC = FindWindow(lpszParentClass, lpszParentWindow);
                exMessageID = RegisterWindowMessage("Exp_Record_Msg");
            }
        }

        [DllImport("avicap32.dll")]
        private static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);

        [DllImport("avicap32.dll")]
        private static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);
        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, long lParam);
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent,IntPtr hwndChildAfter,string lpszClass,string lpszWindow);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);

        #region Camera Props
        Classes.CameraProps _cameraProps = new CameraProps();
        public Classes.CameraProps CameraProps
        {
            get { return _cameraProps; }
        }
        string lpszParentClass = "#32770";
        string lpszParentWindow = "ExpRecorder";
        //string lpszParentWindow = "RegMsgRecvDemo";
        [StructLayout(LayoutKind.Sequential)]

        public struct COPYDATASTRUCT
        {

            public IntPtr dwData;

            public int cbData;

            public IntPtr lpData;

        }


        IntPtr exhWndC;
        uint exMessageID;
        void ReadProps()
        {
            string filename = Application.StartupPath + "\\videoCapture.avi";
            if (System.IO.File.Exists(filename))
            {
                System.IO.StreamReader reader = System.IO.File.OpenText(filename);
                string value = reader.ReadLine();
                _cameraProps.Parse(value);
                reader.Close();
            }

        }
        #endregion
        /// <summary>
        /// 开始显示图像
        /// </summary>
        public void Start()
        {
            if (!_cameraProps.UseExCamera)//内置摄像程序
            {
                if (bStat)
                    return;

                bStat = true;
                byte[] lpszName = new byte[100];

                hWndC = capCreateCaptureWindowA(lpszName, WS_CHILD | WS_VISIBLE, mLeft, mTop, mWidth, mHeight, mControlPtr, 0);

                if (hWndC.ToInt32() != 0)
                {
                    SendMessage(hWndC, WM_CAP_SET_CALLBACK_VIDEOSTREAM, 0, 0);
                    SendMessage(hWndC, WM_CAP_SET_CALLBACK_ERROR, 0, 0);
                    SendMessage(hWndC, WM_CAP_SET_CALLBACK_STATUSA, 0, 0);
                    SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, 0, 0);
                    SendMessage(hWndC, WM_CAP_SET_SCALE, 1, 0);
                    SendMessage(hWndC, WM_CAP_SET_PREVIEWRATE, 66, 0);
                    SendMessage(hWndC, WM_CAP_SET_OVERLAY, 1, 0);
                    SendMessage(hWndC, WM_CAP_SET_PREVIEW, 1, 0);
                }

                //return;
            }
            else//外置摄像程序
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(_cameraProps.ExCameraFilename,_cameraProps.ExVideoPath);
                startInfo.WindowStyle = ProcessWindowStyle.Normal;

                Process.Start(startInfo);

            }
        }

        /// <summary>
        /// 停止显示

        /// </summary>
        public void Stop()
        {
            if (!_cameraProps.UseExCamera)
            {
                SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, 0, 0);
                bStat = false;
            }
            else
            {
                //SendMessage(exhWndC, (int)exMessageID, 400, 0);
                SendMessage((IntPtr)HWND_BROADCAST, (int)exMessageID, 400, 0);
            }
        }


        /// <summary>
        /// 抓图
        /// </summary>
        /// <param name="path">要保存bmp文件的路径</param>
        public void GrabImage(string path)
        {
            if (!_cameraProps.UseExCamera)
            {
                IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
                SendMessage(hWndC, WM_CAP_SAVEDIB, 0, hBmp.ToInt64());
            }
            else
            {
            }
        }

        /// <summary>
        /// 录像
        /// </summary>
        /// <param name="path">要保存avi文件的路径</param>
        public void Kinescope(string path)
        {
            if (!_cameraProps.UseExCamera)
            {
                IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
                SendMessage(hWndC, WM_CAP_FILE_SET_CAPTURE_FILEA, 0, hBmp.ToInt64());
                SendMessage(hWndC, WM_CAP_SEQUENCE, 0, 0);
            }
            else
            {

                IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
                SendMessage((IntPtr)HWND_BROADCAST, (int)exMessageID, 0, hBmp.ToInt64());
                SendMessage((IntPtr)HWND_BROADCAST, (int)exMessageID, 100, 0);

            }
        }
        public void Kinescope()
        {
            if (!_cameraProps.UseExCamera)
            {
                IntPtr hBmp = Marshal.StringToHGlobalAnsi(System.IO.Path.GetRandomFileName() + ".avi");
                SendMessage(hWndC, WM_CAP_FILE_SET_CAPTURE_FILEA, 0, hBmp.ToInt64());
                SendMessage(hWndC, WM_CAP_SEQUENCE, 0, 0);
            }
            else
            {

                SendMessage((IntPtr)HWND_BROADCAST, (int)exMessageID, 100, 0);

            }
        }

        /// <summary>
        /// 停止录像
        /// </summary>
        public void StopKinescope()
        {
            if (!_cameraProps.UseExCamera)
            {
                SendMessage(hWndC, WM_CAP_STOP, 0, 0);
            }
            else
            {
                SendMessage((IntPtr)HWND_BROADCAST, (int)exMessageID, 200, 0);

            }
        }

    }
}



