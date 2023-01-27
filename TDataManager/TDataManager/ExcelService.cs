using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace TDataManager
{
    public class ExcelService
    {
        public ExcelService(string templetFilePath, string outputFilePath)
        {
            //if (templetFilePath == null)
            //    throw new Exception("Excel模板文件路径不能为空！");

            //if (outputFilePath == null)
            //    throw new Exception("输出Excel文件路径不能为空！");

            //if (!File.Exists(templetFilePath))
            //    throw new Exception("指定路径的Excel模板文件不存在！");

            this.templetFile = templetFilePath;
            this.outputFile = outputFilePath;
            beforeTime = DateTime.Now;


        }
        #region Variables
        protected string templetFile = null;
        protected string outputFile = null;
        protected object missing = Missing.Value;
        private 

        DateTime beforeTime;
        DateTime afterTime;
        IWorkbook workBook;
        ISheet workSheet;
        ISheet sheet;
        #endregion

        public void InitExcel(bool showExcel)
        {
            //app = new ApplicationClass();
            //app.Visible = false;
            afterTime = DateTime.Now;
        }

        public void InitWorkBook()
        {

            string FileExt = ".xlsx";
            if (FileExt == ".xlsx")
            {
                workBook = new XSSFWorkbook();
            }
            else if (FileExt == ".xls")
            {
                workBook = new HSSFWorkbook();
            }
            else
            {
                workBook = null;
            }
            if (workBook == null)
            {
                return;
            }

            //workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing,
            //missing, missing, missing, missing, missing, missing, missing);

        }

        public void InitWorkSheet(object sheetNameOrIndex)
        {
            //if (sheetNameOrIndex != null)
            //{
            //    workSheet = (ISheet)workBook.Sheets.get_Item(sheetNameOrIndex);
            //}
            //else
            //{
            //    workSheet = (ISheet)workBook.Sheets.get_Item(1);
            //}
        }

        public void InitSheet(object sheetNameOrIndex)
        {
            //if (sheetNameOrIndex != null)
            //{
            //    sheet = (Excel.Worksheet)workBook.Worksheets.get_Item(sheetNameOrIndex);
            //}
            //else
            //{
            //    sheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
            //}

            sheet = string.IsNullOrEmpty(sheetNameOrIndex.ToString ()) ? workBook.CreateSheet("Sheet1") : workBook.CreateSheet(sheetNameOrIndex.ToString());
        }
        public string SheetName
        {
            get
            {
                return sheet.SheetName;
            }
            set
            {

               // sheet.SheetName = value;
            }
        }
        #region --根据模板查找替换
        /// <summary>
        /// 根据模板中的键值对,查找对应的键并替换成相应的值 
        /// </summary>
        /// <param name="templetString">查找替换的键值对</param>
        /// <param name="startRange">开始的单元格区域名称"例如:A1":格式:"列+行"</param>
        /// <param name="endRange">结束区域的单元格名称"例如:Z100" 格式:"列+行"</param>
        public void WriteTemplateString(Dictionary<string, string> templetString, string startRange, string endRange)
        {
            #region --初始化相关内容
            if (templetString == null || templetString.Count < 1)
                return;

            //if (app == null)
            //{
            //    InitExcel(false);
            //}

            if (workBook == null)
            {
                InitWorkBook();
            }

            if (workSheet == null)
            {
                InitWorkSheet(null);
            }

            if (sheet == null)
            {
                InitSheet(null);
            }

            #endregion
            #region --查找替换--
            foreach (KeyValuePair<string, string> pair in templetString)
            {
                try
                {
                    //Excel.Range selectRange = sheet.get_Range("A1", "S100").Find(pair.Key, missing, missing, Excel.XlLookAt.xlWhole, missing, Excel.XlSearchDirection.xlNext, missing, missing);


                    //sheet.Cells[selectRange.Row, selectRange.Column] = pair.Value;

                    //  sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[100, 100]).Replace(pair.Key, pair.Value, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, missing, missing);
                }
                catch
                {

                }
            }
            #endregion

        }
        public void Merge(string startRange, string endRange, string text)
        {
            //Excel.Range selectRange = sheet.get_Range(startRange, endRange);
            //selectRange.Merge(true);
            //selectRange.Borders.LineStyle = (object)1;
            //selectRange.Borders.Weight = (object)3;
            //selectRange.Borders.ColorIndex = (object)6;
            //selectRange.Borders.Color = (object)20;
            //selectRange.WrapText = (object)1;
            //selectRange.FillDown();
        }
        /// <summary>
        /// 在模板的字符串列(右侧)行(下侧)写入相应的数据字符串
        /// </summary>
        /// <param name="templetString"></param>
        /// <param name="dt"></param>
        /// <param name="RowMax"></param>
        /// <param name="ColumnMax"></param>
        /// <param name="IsPager"></param>
        public void WriteTemplateTable(Dictionary<string, string> templetString, DataTable dt, int RowMax, int ColumnMax, bool IsPager)
        {
            if (templetString == null || dt == null)
                return;
            if (templetString.Count == 0 || dt.Rows.Count == 0)
                return;

            foreach (KeyValuePair<string, string> pair in templetString)
            {
                try
                {
                    //Excel.Range selectRange = sheet.get_Range("A1", "S100").Find(pair.Key, missing, missing, Excel.XlLookAt.xlWhole, missing, Excel.XlSearchDirection.xlNext, missing, missing);

                    //     sheet.Cells[selectRange.Row, selectRange.Column] = pair.Value;

                    //if (selectRange == null)
                    //    break;

                    //int startX = selectRange.Row;
                    //int startY = selectRange.Column;

                    //int columnNum = dt.Columns[pair.Value].Ordinal;

                    //int i = 0;
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    if (i > RowMax)
                    //        break;
                    //    sheet.Cells[startX + i, startY] = dr[columnNum];
                    //    i++;
                    //}

                    //  sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[100, 100]).Replace(pair.Key, pair.Value, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, missing, missing);
                }
                catch
                {

                }
            }

        }
        #endregion

        #region --直接在指定的单元格处写入数据--
        /// <summary>
        /// 在指定的单元格写入指定的值
        /// </summary>
        /// <param name="rowIndex">单元格的行标识</param>
        /// <param name="columnIndex">单元格的列标识</param>
        /// <param name="Value">要插入的值</param>
        public void WriteDesignationValue(object columnIndex, object rowIndex, object Value)
        {

            //sheet.ActiveCell[rowIndex, columnIndex] = Value;
        }
        IRow rowHeader=null;
        public void CreateRow(object rowIndex)
        {
            rowHeader = sheet.CreateRow((int)rowIndex);
        }
        public void WriterCell(object columnIndex, object Value)
        {
            if (rowHeader==null )
            {
                return;
            }
            ICell cell = rowHeader.CreateCell((int)columnIndex);
            cell.SetCellValue(Value.ToString ());
        }
        /// <summary>
        /// 在指定单元格写入指定的图片
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="picturePath"></param>
        public void WritePictureValue(object columnIndex, object rowIndex, string picturePath)
        {
            //object m_objOpt = System.Reflection.Missing.Value;
            //string rangeName = columnIndex.ToString() + rowIndex.ToString();
            //Excel.Range range = sheet.get_Range(rangeName, rangeName);
            ////range.Select();
            //Excel.Pictures pics = (Excel.Pictures)sheet.Pictures(m_objOpt);
            //pics.Insert(picturePath, m_objOpt);
            //range.Value = m_objOpt;
        }
        /// <summary>
        /// 指定起始位置插入DataTable
        /// </summary>
        /// <param name="startRange">起始位置</param>
        /// <param name="dt">数据表</param>
        /// <param name="RowMax">最大行</param>
        /// <param name="ColumnMax">最大列</param>
        /// <param name="IsPager">是否分页(分页暂未实现)</param>
        /// <param name="hasCaption">数据表是否要显示表头</param>
        public void InsertDesigntionTable(string startRange, DataTable dt, int RowMax, int ColumnMax, bool IsPager, bool hasCaption)
        {
            //int startRowIndex = sheet.get_Range(startRange, startRange).Row;
            //int startColumnIndex = sheet.get_Range(startRange, startRange).Column;

            //Excel.Range rangeSelect = sheet.get_Range(startRange, startRange);

            //if (dt == null)
            //    return;
            //int i = 0;
            //int j = 0;
            //rangeSelect.Select();

            //if (hasCaption)
            //{
            //    rangeSelect.EntireRow.Insert(missing);
            //    i = i + 1;
            //    j = 0;
            //    foreach (DataColumn c in dt.Columns)
            //    {
            //        sheet.Cells[startRowIndex + i, startColumnIndex + j] = c.ColumnName;
            //    }
            //}

            //foreach (DataRow dr in dt.Rows)
            //{
            //    j = 0;
            //    if (i > RowMax)
            //        break;
            //    //foreach (object o in dr.ItemArray)
            //    //{
            //    //    if (j > ColumnMax)
            //    //        break;
            //    //    sheet.Cells[startRowIndex + i, startColumnIndex + j] = o;
            //    //    j++;
            //    //}
            //    rangeSelect.EntireRow.Insert(missing);
            //    foreach (object o in dr.ItemArray)
            //    {
            //        if (j > ColumnMax)
            //            break;
            //        sheet.Cells[startRowIndex + i, startColumnIndex + j] = o;
            //        j++;
            //    }
            //    i++;

            //}
        }

        /// <summary>
        /// 在指定位置写入一个表格
        /// </summary>
        public void WriteDesignationTable(string startRange, DataTable dt, int RowMax, int ColumnMax, bool IsPager)
        {

            //int startRowIndex = sheet.get_Range(startRange, startRange).Row;
            //int startColumnIndex = sheet.get_Range(startRange, startRange).Column;
            //if (dt == null)
            //    return;
            ////for (int index = 0; index < dt.Columns.Count; index++)
            ////{
            ////    sheet.Cells[startRowIndex, startColumnIndex+index] = dt.Columns[index].Caption;
            ////}
            //int i = 0;
            //int j = 0;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    j = 0;
            //    if (i > RowMax)
            //        break;
            //    foreach (object o in dr.ItemArray)
            //    {
            //        if (j > ColumnMax)
            //            break;
            //        sheet.Cells[startRowIndex + i, startColumnIndex + j] = o;
            //        j++;
            //    }

            //    i++;

            //}
        }

        #endregion
        public void SaveToTemplateAndExit()
        {
            try
            {
                //workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing);
                //workBook.Close(null, null, null);
                //app.Workbooks.Close();
                //app.Application.Quit();
                //app.Quit();

                //System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                //workSheet = null;
                //workBook = null;
                //app = null;

                Application.DoEvents();

                //转为字节数组  
                MemoryStream stream = new MemoryStream();
                workBook.Write(stream);
                var buf = stream.ToArray();

                //保存为Excel文件  
                using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                    fs.Close();
                }

                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Process[] myProcesses;
                DateTime startTime;
                myProcesses = Process.GetProcessesByName("Excel");

                //得不到Excel进程ID，暂时只能判断进程启动时间
                foreach (Process myProcess in myProcesses)
                {
                    startTime = myProcess.StartTime;

                    if (startTime > beforeTime && startTime < afterTime)
                    {
                        myProcess.Kill();
                    }
                }
            }
        }

    }
}
