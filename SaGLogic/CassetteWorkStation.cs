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
    /// 對應 SaGDB.cassette_workstation_tb Table
    /// </summary>
    public class CassetteWorkStation : ITableModel<CassetteWorkStationM>
    {
        public CassetteWorkStationM[] GetValues(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new CassetteWorkStationM[] { };
            }

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBCassetteWorkStation tb = new TBCassetteWorkStation(db, $"name='{name}'");

                var query = from row in tb.Table.AsEnumerable()
                            select new CassetteWorkStationM()
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
                    return new CassetteWorkStationM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        public CassetteWorkStationM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new CassetteWorkStationM()
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

        public DataTable GenerateDataTable(CassetteWorkStationM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("path");

            foreach (CassetteWorkStationM ctm in models)
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
