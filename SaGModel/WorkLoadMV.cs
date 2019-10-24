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
        public string FieldA { get; set; }
        public string FieldB { get; set; }
        public int Total { get; set; }

        public DataTable GenerateDataTable(SlideWorkLoadMV[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("date");
            dt.Columns.Add("user");
            dt.Columns.Add("slide_fieldA");
            dt.Columns.Add("slide_fieldB");
            dt.Columns.Add("total");

            Array.ForEach(models, wl => {
                DataRow row = dt.NewRow();
                row["date"] = wl.Date;
                row["user"] = wl.User;
                row["slide_fieldA"] = wl.FieldA;
                row["slide_fieldB"] = wl.FieldB;
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
                        FieldA = row["slide_fieldA"].ToString(),
                        FieldB = row["slide_fieldB"].ToString(),
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
