
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
    public class CassetteMagazine : ITableModel<CassetteMagazineM>
    {
        public CassetteMagazineM[] GetValues(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new CassetteMagazineM[] { };
            }

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBCassetteMagazine tb = new TBCassetteMagazine(db, $"name='{name}'");

                var query = from row in tb.Table.AsEnumerable()
                            select new CassetteMagazineM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                Name = row["name"].ToString(),
                                Magazine = row["magazine"].ToString()
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new CassetteMagazineM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        public CassetteMagazineM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new CassetteMagazineM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Name = row["name"].ToString(),
                        Magazine = row["magazine"].ToString()
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

        public DataTable GenerateDataTable(CassetteMagazineM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("magazine");

            foreach (CassetteMagazineM ctm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = ctm.Id;
                row["name"] = ctm.Name;
                row["magazine"] = ctm.Magazine;
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
