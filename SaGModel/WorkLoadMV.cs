using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    //工作量 (日/人/量)
    public class WorkLoadMV : ITableModel<WorkLoadMV>
    {
        public string Date { get; set; }
        public string User { get; set; }
        public int Total { get; set; }
        public WorkLoadMV()
        {
            Initialize();
        }
        public void Initialize()
        {
            Date = string.Empty;
            User = string.Empty;
            Total = 0;
        }

        public DataTable GenerateDataTable(WorkLoadMV[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("date");
            dt.Columns.Add("user");
            dt.Columns.Add("total");

            Array.ForEach(models, wl => {
                DataRow row = dt.NewRow();
                row["date"] = wl.Date;
                row["user"] = wl.User;
                row["total"] = wl.Total;
                dt.Rows.Add(row);
            });

            return dt;
        }

        public WorkLoadMV[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new WorkLoadMV()
                    {
                        Date = row["date"].ToString(),
                        User = row["user"].ToString(),
                        Total = SaConverter.ToInt(row["total"].ToString(), 0)
                    };

            if (v != null)
            {
                return v.ToArray();
            }
            else
            {
                return null;
            }
        }
    }

    //玻片工作量 (日/人/fieldA/fieldB/量)
    public class SlideWorkLoadMV : ITableModel<SlideWorkLoadMV>
    {
        public string Date { get; set; }
        public string User { get; set; }
        public string F1 { get; set; }
        public string F2 { get; set; }
        public int Total { get; set; }
        public SlideWorkLoadMV()
        {
            Initialize();
        }
        public void Initialize()
        {
            Date = string.Empty;
            User = string.Empty;

        }
        public DataTable GenerateDataTable(SlideWorkLoadMV[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("date");
            dt.Columns.Add("user");
            dt.Columns.Add("slide_F1");
            dt.Columns.Add("slide_F2");
            dt.Columns.Add("total");

            Array.ForEach(models, wl => {
                DataRow row = dt.NewRow();
                row["date"] = wl.Date;
                row["user"] = wl.User;
                row["slide_F1"] = wl.F1;
                row["slide_F2"] = wl.F2;
                row["total"] = wl.Total;
                dt.Rows.Add(row);
            });
            dt.AcceptChanges();
            return dt;
        }

        public SlideWorkLoadMV[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SlideWorkLoadMV()
                    {
                        Date = row["date"].ToString(),
                        User = row["user"].ToString(),
                        F1 = row["slide_F1"].ToString(),
                        F2 = row["slide_F2"].ToString(),
                        Total = SaConverter.ToInt(row["total"].ToString(), 0)
                    };

            if (v != null)
            {
                return v.ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
