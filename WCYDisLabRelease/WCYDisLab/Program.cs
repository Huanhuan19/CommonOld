using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WCYDisLab
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 1)
            {
                Application.Run(new Form1(args[0], args[1]));
            }
            else
            {
                FormSelectLanguage f = new FormSelectLanguage();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Form1());
                }
            }
        }
    }
}
