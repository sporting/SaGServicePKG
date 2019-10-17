
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
    public class SlideWorkStation : ITableModel<SlideWorkStationM>
    {
        public SlideWorkStationM[] GetValues(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new SlideWorkStationM[] { };
            }

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBSlideWorkStation tb = new TBSlideWorkStation(db, $"name='{name}'");

                var query = from row in tb.Table.AsEnumerable()
                            select new SlideWorkStationM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                Name = row["name"].ToString(),
                                Path = row["path"].ToString()
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new SlideWorkStationM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        public SlideWorkStationM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SlideWorkStationM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Name = row["name"].ToString(),
                        Path = row["path"].ToString()
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

        public DataTable GenerateDataTable(SlideWorkStationM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("path");

            foreach (SlideWorkStationM ctm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = ctm.Id;
                row["name"] = ctm.Name;
                row["path"] = ctm.Path;
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
