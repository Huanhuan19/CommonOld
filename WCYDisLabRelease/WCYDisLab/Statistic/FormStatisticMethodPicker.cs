using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WCYDisLab.Statistic
{
    public partial class FormStatisticMethodPicker : Form
    {
        public FormStatisticMethodPicker(DataAnalysis.StatisticTypeDefine method)
        {
            InitializeComponent();
            SelectedMethod = method;
        }
        #region Props
        public DataAnalysis.StatisticTypeDefine SelectedMethod
        {
            get
            {
                DataAnalysis.StatisticTypeDefine method = DataAnalysis.StatisticTypeDefine.Average;
                if (radioButton_average.Checked)
                {
                    method = DataAnalysis.StatisticTypeDefine.Average;
                }
                else if (radioButton_maximum.Checked)
                {
                    method = DataAnalysis.StatisticTypeDefine.Maximum;
                }
                else if (radioButton_median.Checked)
                {
                    method = DataAnalysis.StatisticTypeDefine.Median;
                }
                else if (radioButton_minimum.Checked)
                {
                    method = DataAnalysis.StatisticTypeDefine.Minimum;
                }
                else if (radioButton_standardError.Checked)
                {
                    method = DataAnalysis.StatisticTypeDefine.StandardError;
                }
                else if (radioButton_sum.Checked)
                {
                    method = DataAnalysis.StatisticTypeDefine.Sum;
                }
                return method;
            }
            set
            {
                switch (value)
                {
                    case DataAnalysis.StatisticTypeDefine.Average:
                        radioButton_average.Checked = true;
                        break;
                    case DataAnalysis.StatisticTypeDefine.Maximum:
                        radioButton_maximum.Checked = true;
                        break;
                    case DataAnalysis.StatisticTypeDefine.Median:
                        radioButton_median.Checked = true;
                        break;
                    case DataAnalysis.StatisticTypeDefine.Minimum:
                        radioButton_minimum.Checked = true;
                        break;
                    case DataAnalysis.StatisticTypeDefine.StandardError:
                        radioButton_standardError.Checked = true;
                        break;
                    case DataAnalysis.StatisticTypeDefine.Sum:
                        radioButton_sum.Checked = true;
                        break;
                }
            }
        }
        #endregion

    }
}
