using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGLogic
{
    /// <summary>
    /// 作為前端應用與資料庫物件的中介層 
    /// 對應 SaGDB.slide_printer_tb Table
    /// </summary>
    public class SlidePrinter : ITableModel<SlidePrinterM>
    {
        public SlidePrinterM[] GetValues(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new SlidePrinterM[] { };
            }

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBSlidePrinter tb = new TBSlidePrinter(db, $"name='{name}'");

                var query = from row in tb.Table.AsEnumerable()
                            select new SlidePrinterM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                Name = row["name"].ToString(),
                                Printer = row["printer"].ToString()
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new SlidePrinterM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        public SlidePrinterM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SlidePrinterM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Name = row["name"].ToString(),
                        Printer = row["printer"].ToString()
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

        public DataTable GenerateDataTable(SlidePrinterM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("printer");

            Array.ForEach(models, ctm => {
                DataRow row = dt.NewRow();
                row["id"] = ctm.Id;
                row["name"] = ctm.Name;
                row["printer"] = ctm.Printer;
                dt.Rows.Add(row);
            });

            return dt;
        }
    }
}
