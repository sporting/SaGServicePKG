
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
    /// 對應 SaGDB.sysparams_tb Table
    /// </summary>
    public class SysParams:ITableModel<SysParamsM>
    {
        public SysParamsM[] GetValues(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new SysParamsM[] { };
            }

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBSysParams tb = new TBSysParams(db, $"name='{name}'");

                var query = from row in tb.Table.AsEnumerable()
                            select new SysParamsM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                Name = row["name"].ToString(),
                                Seq = SaConverter.ToInt(row["seq"].ToString(), 0),
                                Value = row["value"].ToString()
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new SysParamsM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        public SysParamsM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SysParamsM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Seq = Convert.ToInt32(row["seq"]),
                        Name = row["name"].ToString(),
                        Value = row["value"].ToString()
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

        public DataTable GenerateDataTable(SysParamsM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("seq");
            dt.Columns.Add("name");
            dt.Columns.Add("value");

            foreach (SysParamsM spm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = spm.Id;
                row["seq"] = spm.Seq;
                row["name"] = spm.Name;
                row["value"] = spm.Value;
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
