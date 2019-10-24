
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
    public class SysParams
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

        private TBSysParams Update(MyDB db, SysParamsM spm)
        {
            TBSysParams tb = new TBSysParams(db, $"Id='{spm.Id}' for update");

            if (tb.RowsCount <= 0)
            {
                DataRow row = tb.Table.NewRow();
                row["name"] = spm.Name;
                row["seq"] = spm.Seq;
                row["value"] = spm.Value;
                tb.Table.Rows.Add(row);
            }
            else
            {
                DataRow row = tb.Table.Rows[0];
                row["name"] = spm.Name;
                row["seq"] = spm.Seq;
                row["value"] = spm.Value;
            }

            return tb;
        }
        public bool Update(SysParamsM[] spms)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                IDbTransaction transaction = db.StartTransaction();
                bool res = true;
                Array.ForEach(spms, spm =>
                {
                    if (!res) { return; }

                    MyTable tb = Update(db, spm);
                    res = tb.Update();
                });

                if (res)
                {
                    if (db.Commit(transaction))
                        return true;
                }
                return false;
            }
            finally
            {
                db.CloseDB();
            }
        }

        private TBSysParams Delete(MyDB db, int id)
        {
            TBSysParams tb = new TBSysParams(db, $"Id={id} for update");

            if (tb.RowsCount > 0)
            {
                tb.Table.Rows[0].Delete();
            }

            return tb;
        }
        public bool Delete(int[] ids)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                IDbTransaction transaction = db.StartTransaction();
                bool res = true;
                Array.ForEach(ids, id =>
                {
                    if (!res) { return; }

                    MyTable tb = Delete(db, id);
                    res = tb.Update();
                });

                if (res)
                {
                    if (db.Commit(transaction))
                        return true;
                }
                return false;
            }
            finally
            {
                db.CloseDB();
            }
        }
    }
}
