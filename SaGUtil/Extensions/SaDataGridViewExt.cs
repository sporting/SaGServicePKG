using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SaGUtil.Extensions
{
    /// <summary>
    /// DataGridView Extension Methods
    /// </summary>
    public static class SaDataGridViewExt
    {
        private static string GetNextColumnName(DataGridView dgView, int counter, string fieldName)
        {
            string columnName = counter == 0 ? "Column" + fieldName : "Column" + counter.ToString() + fieldName;

            if (dgView.Columns.Contains(columnName))
            {
                int nextCnt = counter + 1;
                columnName = GetNextColumnName(dgView, nextCnt, fieldName);
            }

            return columnName;
        }

        public static DataGridViewColumn AddColumn(this DataGridView dgView, string fieldName, string displayName, Type columnType)
        {
            DataGridViewColumn column;
            if (columnType.Equals(typeof(DataGridViewCheckBoxColumn)))
            {
                column = new DataGridViewCheckBoxColumn(false);
                ((DataGridViewCheckBoxColumn)column).TrueValue = true;
                ((DataGridViewCheckBoxColumn)column).FalseValue = false;
            }
            else if (columnType.Equals(typeof(DataGridViewComboBoxColumn)))
                column = new DataGridViewComboBoxColumn();
            else if (columnType.Equals(typeof(DataGridViewButtonColumn)))
            {
                column = new DataGridViewButtonColumn();
                ((DataGridViewButtonColumn)column).Text = displayName;
            }
            else
                column = new DataGridViewTextBoxColumn();
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            column.HeaderText = displayName;

            column.Name = GetNextColumnName(dgView, 0, fieldName);
            column.DataPropertyName = fieldName;

            dgView.Columns.Add(column);

            return column;
        }

        public static DataGridViewColumn AddColumn(this DataGridView dgView, string fieldName, string displayName, Type columnType,int displayWidth)
        {
            DataGridViewColumn column;
            if (columnType.Equals(typeof(DataGridViewCheckBoxColumn)))
            {
                column = new DataGridViewCheckBoxColumn(false);
                ((DataGridViewCheckBoxColumn)column).TrueValue = true;
                ((DataGridViewCheckBoxColumn)column).FalseValue = false;
            }
            else if (columnType.Equals(typeof(DataGridViewComboBoxColumn)))
                column = new DataGridViewComboBoxColumn();
            else if (columnType.Equals(typeof(DataGridViewButtonColumn)))
            {
                column = new DataGridViewButtonColumn();
                ((DataGridViewButtonColumn)column).Text = displayName;
            }
            else
                column = new DataGridViewTextBoxColumn();
            //column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            column.HeaderText = displayName;
            column.Width = displayWidth;
            column.Name = GetNextColumnName(dgView, 0, fieldName);
            column.DataPropertyName = fieldName;

            dgView.Columns.Add(column);

            return column;
        }

        /// <summary>
        ///         ''' for combobox
        ///         ''' </summary>
        ///         ''' <param name="dgView"></param>
        ///         ''' <param name="fieldName"></param>
        ///         ''' <param name="displayName"></param>
        ///         ''' <param name="comboDataSource"></param>
        ///         ''' <param name="comboDisplayMember"></param>
        ///         ''' <param name="comboValueMember"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public static DataGridViewColumn AddColumn(this DataGridView dgView, string fieldName, string displayName, object comboDataSource, string comboDisplayMember, string comboValueMember)
        {
            DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn)dgView.AddColumn(fieldName, displayName, typeof(DataGridViewComboBoxColumn));
            column.DataSource = comboDataSource;
            column.DisplayMember = comboDisplayMember;
            column.ValueMember = comboValueMember;
            return column;
        }


        /// <summary>
        ///         ''' for checkbox
        ///         ''' </summary>
        ///         ''' <param name="dgView"></param>
        ///         ''' <param name="fieldName"></param>
        ///         ''' <param name="displayName"></param>
        ///         ''' <param name="TrueValue"></param>
        ///         ''' <param name="FalseValue"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public static DataGridViewColumn AddColumn(this DataGridView dgView, string fieldName, string displayName, object TrueValue, object FalseValue)
        {
            DataGridViewCheckBoxColumn column = (DataGridViewCheckBoxColumn)dgView.AddColumn(fieldName, displayName, typeof(DataGridViewCheckBoxColumn));
            column.TrueValue = TrueValue;
            column.FalseValue = FalseValue;
            return column;
        }

        public static void DrawRequiredColor(this DataGridView dgView, Color color)
        {
            if (dgView.DataSource != null)
            {
                if (dgView.DataSource is DataTable)
                {
                    DataTable table = (DataTable)dgView.DataSource;
                    foreach (DataColumn col in table.Columns)
                    {
                        foreach (DataGridViewColumn dCol in dgView.Columns)
                        {
                            if (col.ColumnName == dCol.DataPropertyName)
                            {
                                if (!col.AllowDBNull)
                                    dCol.DefaultCellStyle.ForeColor = color;
                                break;
                            }
                        }
                    }
                }
            }
        }
        public static bool ExportToCSV(this DataGridView dgv, string strExportFileName, bool blnWriteColumnHeaderNames,bool blnWriteColumnHeaderText, string strDelimiterType)
        {
            try
            {
                StreamWriter sr = File.CreateText(strExportFileName);
                string strDelimiter = strDelimiterType;
                int intColumnCount = 0;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible)
                        intColumnCount += 1;
                }
                string strRowData = "";
                if (blnWriteColumnHeaderNames)
                {
                    for (int intX = 0; intX <= intColumnCount - 1; intX++)
                    {
                        if (dgv.Columns[intX].Visible)
                            if (blnWriteColumnHeaderText)
                            {
                                strRowData += dgv.Columns[intX].HeaderText.Replace(strDelimiter, "") + (intX < intColumnCount ? strDelimiter : "");
                            }
                            else
                            {
                                strRowData += dgv.Columns[intX].Name.Replace(strDelimiter, "") + (intX < intColumnCount ? strDelimiter : "");
                            }
                    }
                    sr.WriteLine(strRowData);
                }
                for (int intX = 0; intX <= dgv.Rows.Count - 1; intX++)
                {
                    strRowData = "";
                    for (int intRowData = 0; intRowData <= intColumnCount - 1; intRowData++)
                    {
                        if (dgv.Columns[intRowData].Visible)
                            strRowData += dgv.Rows[intX].Cells[intRowData].Value.ToStringEx().Replace(strDelimiter, "") +(intRowData < intColumnCount? strDelimiter: "");
                    }
                    sr.WriteLine(strRowData);
                }
                sr.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ExportToJson(this DataGridView dgv, string strExportFileName)
        {
            try
            {
                StreamWriter sr = File.CreateText(strExportFileName);
                Hashtable ht;
                HashSet<Hashtable> hs = new HashSet<Hashtable>();
                string js;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    ht = new Hashtable();
                    for (int colidx = 0; colidx <= dgv.Columns.Count - 1; colidx++)
                        ht.Add(dgv.Columns[colidx].HeaderText, row.Cells[colidx].Value.ToStringEx());
                    hs.Add(ht);
                }

                js = JsonConvert.SerializeObject(hs);

                sr.Write(js);
                sr.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ExportToExcel(this DataGridView dgv, string strExportFileName, string sheetName,bool blnWriteHeaderText)
        {
            try
            {
                // 建立Excel 2003檔案
                IWorkbook wb = new HSSFWorkbook();
                ISheet ws;

                // 建立Excel 2007檔案
                ws = wb.CreateSheet(sheetName);

                ws.CreateRow(0); // 第一行為欄位名稱
                int celli = 0;
                for (int i = 0; i <= dgv.Columns.Count - 1; i++)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        if (blnWriteHeaderText)
                        {
                            ws.GetRow(0).CreateCell(celli).SetCellValue(dgv.Columns[i].HeaderText);
                        }
                        else
                        {
                            ws.GetRow(0).CreateCell(celli).SetCellValue(dgv.Columns[i].Name);
                        }
                        celli += 1;
                    }
                }

                for (int i = 0; i <= dgv.Rows.Count - 1; i++)
                {
                    ws.CreateRow(i + 1);
                    celli = 0;
                    for (int j = 0; j <= dgv.Columns.Count - 1; j++)
                    {
                        if (dgv.Columns[j].Visible)
                        {
                            ws.GetRow(i + 1).CreateCell(j).SetCellValue(dgv.Rows[i].Cells[j].Value.ToStringEx());
                            celli += 1;
                        }
                    }
                }
                using (FileStream f = new FileStream(strExportFileName, FileMode.Create))
                {
                    wb.Write(f);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}


