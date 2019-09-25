using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaGUtil.Extensions;
using System.IO;

namespace SaGComs
{
    public partial class ReportForm : Form
    {
        protected APClientForm MdiParentForm
        {
            get
            {
                return (APClientForm)this.ParentForm;
            }
        }

        protected string ServiceToken
        {
            get
            {
                return MdiParentForm.ServiceToken;
            }
        }

        public ReportForm()
        {
            InitializeComponent();

            SaveDialog = new SaveFileDialog();
            SaveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            SaveDialog.DefaultExt = "xls";
            SaveDialog.RestoreDirectory = true;
            SaveDialog.Filter = "Excel|*.xls|逗號(，)分隔|*.csv";
            SaveDialog.Title = "Export to Excel";
        }

        //查詢資料的處理程序
        public virtual bool QueryProcess()
        {
            if (!MdiParentForm.Connected)
            {
                MessageBox.Show("系統服務尚未建立，請稍候");
                return false;
            }

            return true;
        }

        protected SaveFileDialog SaveDialog;
        //匯出的處理程序
        public virtual void ExportDataProcess()
        {
            SaveDialog.FileName = $"{this.Text}_{MdiParentForm.Now.ToString("yyyyMMddHHmmss")}";
            if (SaveDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(SaveDialog.FileName))
            {
                if (SaveDialog.FilterIndex == 1)
                {
                    dgvData.ExportToExcel(SaveDialog.FileName, "Sheet1",true);
                }
                else
                {
                    dgvData.ExportToCSV(SaveDialog.FileName, true,true, ",");
                }
            }
        }

        //查到資料的處理程序
        public virtual void DataExistProcess()
        {

        }

        //查無資料的處理程序
        public virtual void DataNotFoundProcess()
        {

        }
        //清除查詢條件及DataGridView
        public virtual void ClearProcess()
        {

        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbQuery_Click(object sender, EventArgs e)
        {
            if (QueryProcess())
            {
                DataExistProcess();
            }
            else
            {
                DataNotFoundProcess();
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            ExportDataProcess();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            ClearProcess();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
