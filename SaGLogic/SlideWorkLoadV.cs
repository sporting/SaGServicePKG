using SaGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SaGDB;
using SaGDB.Views;

namespace SaGLogic
{
    /// <summary>
    /// 作為前端應用與資料庫物件的中介層 
    /// 玻片工作量 View
    /// </summary>
    public class SlideWorkLoadV : ITableModel<SlideWorkLoadMV>
    {
        public SlideWorkLoadMV[] Get(string begDate, string endDate, string slideUser)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                try
                {
                    ViewSlideWorkLoad view = new ViewSlideWorkLoad();
                    DataTable dt = view.Get(begDate,endDate, slideUser);

                    return GenerateModel(dt);
                }
                catch
                {
                    return new SlideWorkLoadMV[] { };
                }
            }

            return new SlideWorkLoadMV[] { };
        }

        public DataTable GenerateDataTable(SlideWorkLoadMV[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("date");
            dt.Columns.Add("user");
            dt.Columns.Add("slide_fieldA");
            dt.Columns.Add("slide_fieldB");
            dt.Columns.Add("total");

            foreach (SlideWorkLoadMV wl in models)
            {
                DataRow row = dt.NewRow();
                row["date"] = wl.Date;
                row["user"] = wl.User;
                row["slide_fieldA"] = wl.FieldA;
                row["slide_fieldB"] = wl.FieldB;
                row["total"] = wl.Total;
                dt.Rows.Add(row);
            }

            return dt;
        }

        public SlideWorkLoadMV[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SlideWorkLoadMV()
                    {
                        Date=row["date"].ToString(),
                        User=row["user"].ToString(),
                        FieldA = row["slide_fieldA"].ToString(),
                        FieldB = row["slide_fieldB"].ToString(),
                        Total = Convert.ToInt32(row["total"].ToString())
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
