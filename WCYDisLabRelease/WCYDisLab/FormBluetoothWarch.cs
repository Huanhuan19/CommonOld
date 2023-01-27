using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCYDataSource;
using System.Resources;
using System.Reflection;


namespace WCYDisLab
{
    public partial class FormBluetoothWarch : Form
    {
        ResourceManager _resourceManager = new ResourceManager("WCYDisLab.FormBluetoothWarch", Assembly.GetExecutingAssembly());
        Classes.DataEngine _dataEngine;
        public FormBluetoothWarch(Classes.DataEngine pdataEngine)
        {
            InitializeComponent();
            _dataEngine = pdataEngine;

            ColumnHeader ch = new ColumnHeader();
            ch.Text = _resourceManager.GetString("availableDevices");   //设置列标题 
            ch.Width = 180;    //设置列宽度 
            ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式 
            WarchListView.Columns.Add(ch);   //将列头添加到ListView控件。

            ColumnHeader ch2 = new ColumnHeader();
            ch2.Text = _resourceManager.GetString("selectedList");   //设置列标题 
            ch2.Width = 180;    //设置列宽度 
            ch2.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式 
            SelectListView.Columns.Add(ch2);

            ButtonState(false);
            timerInit.Start();
        }
        int count = 140;
        private void btn_Search_Click(object sender, EventArgs e)
        {
            allBluetoothList.Clear();
            warchList.Clear();
            selectList.Clear();
            _dataEngine.DataSource.StartBluetoothSearch();
            timer1.Start();
            count = 140;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            try
            {
                selectList.Add(warchList[WarchListView.FocusedItem.Index]);
                warchList.RemoveAt(WarchListView.FocusedItem.Index);
                RefreshTwoView();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                warchList.Add(selectList[SelectListView.FocusedItem.Index]);
                selectList.RemoveAt(SelectListView.FocusedItem.Index);
                RefreshTwoView();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            _dataEngine.DataSource.BluetoothConnect(selectList);
        }

        public List<BluetoothInfo> allBluetoothList = new List<BluetoothInfo>();
        public List<BluetoothInfo> warchList = new List<BluetoothInfo>();
        public List<BluetoothInfo> selectList = new List<BluetoothInfo>();
        bool needResfrshTwoView = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            count--;
            if (count % 5 == 0 && needResfrshTwoView)
            {
                RefreshTwoView();
                needResfrshTwoView = false;
            }
            lock (_dataEngine.DataSource.bluetoothList)
            {
                foreach (var bt in _dataEngine.DataSource.bluetoothList)
                {
                    BluetoothInfo btInfo = (from o in allBluetoothList where o.Adresse == bt.Adresse && o.MAC == bt.MAC select o).ToList().FirstOrDefault();
                    if (btInfo == null)//new search item. add to warchList
                    {
                        btInfo = new BluetoothInfo();
                        btInfo.Adresse = bt.Adresse;
                        btInfo.MAC = bt.MAC;
                        allBluetoothList.Add(btInfo);

                        warchList.Add(btInfo);
                        needResfrshTwoView = true;
                    }
                }
            }
            if (count <= 0)
            {
                timer1.Stop();
            }
        }
        void RefreshTwoView()
        {
            WarchListView.Items.Clear();
            SelectListView.Items.Clear();
            for (int i = 0; i < warchList.Count; i++)
            {
                WarchListView.Items.Add(warchList[i].Adresse);
            }
            for (int i = 0; i < selectList.Count; i++)
            {
                SelectListView.Items.Add(selectList[i].Adresse);
            }
        }

        private void timerInit_Tick(object sender, EventArgs e)
        {
            if (_dataEngine.DataSource.StillWaitTime() > 0)
            {
                label1.Text = string.Format(_resourceManager.GetString("stillWaitTime"), _dataEngine.DataSource.StillWaitTime());
            }
            else
            {
                label1.Text = "";
                ButtonState(true);
            }
        }
        void ButtonState(bool state)
        {
            btn_Search.Enabled = state;
            btn_Select.Enabled = state;
            btn_Cancel.Enabled = state;
            button_confirm.Enabled = state;
        }
    }
}
