﻿using SaGModel;
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
    public class GrossWorkLoadV : ITableModel<WorkLoadMV>
    {
        public WorkLoadMV[] Get(string begDate, string endDate, string grossUser)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                try
                {
                    ViewGrossWorkLoad view = new ViewGrossWorkLoad();
                    DataTable dt = view.Get(begDate,endDate,grossUser);

                    return GenerateModel(dt);
                }
                catch
                {
                    return new WorkLoadMV[] { };
                }
            }

            return new WorkLoadMV[] { };
        }

        public DataTable GenerateDataTable(WorkLoadMV[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("date");
            dt.Columns.Add("user");
            dt.Columns.Add("total");

            foreach (WorkLoadMV wl in models)
            {
                DataRow row = dt.NewRow();
                row["date"] = wl.Date;
                row["user"] = wl.User;
                row["total"] = wl.Total;
                dt.Rows.Add(row);
            }

            return dt;
        }

        public WorkLoadMV[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new WorkLoadMV()
                    {
                        Date=row["date"].ToString(),
                        User=row["user"].ToString(),
                        Total=Convert.ToInt32(row["total"].ToString())
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
