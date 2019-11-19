using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil.Data;
using SaGUtil.System;
using SaGUtil.Utils;
using System;
using System.Data;
using System.Linq;

namespace SaGLogic
{
    /// <summary>
    /// 作為前端應用與資料庫物件的中介層 
    /// 對應 SaGDB.sysuser_tb Table
    /// sysuser
    /// </summary>

    public class SysUser 
    {
        public bool Add(SysUserM user)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();

                IDbTransaction transaction = db.StartTransaction();
                TBSysUser tb = new TBSysUser(db, "1=0");

                DataRow row = tb.Table.NewRow();
                row["user_id"] = user.UserId;
                row["name"] = user.Name;
                tb.Table.Rows.Add(row);

                if (tb.Update())
                {
                    if (db.Commit(transaction))
                        return true; ;
                }

                return false;
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
                return false;
            }
            finally
            {
                db.CloseDB();
            }
        }

        public SysUserM[] GetAll()
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBSysUser tb = new TBSysUser(db, "1=1");

                var query = from row in tb.Table.AsEnumerable()
                            select new SysUserM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                UserId = row["user_id"].ToString(),
                                Name = row["name"].ToString()
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new SysUserM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }


        public SysUserM GetUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBSysUser tb = new TBSysUser(db, $"user_id='{userId}'");

                var query = from row in tb.Table.AsEnumerable()
                            select new SysUserM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                UserId = row["user_id"].ToString(),
                                Name = row["name"].ToString()
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray()[0];
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        private TBSysUser Update(MyDB db, SysUserM sum)
        {
            TBSysUser tb = new TBSysUser(db, $"Id='{sum.Id}' for update");

            if (tb.RowsCount <= 0)
            {
                DataRow row = tb.Table.NewRow();
                row["user_id"] = sum.UserId;
                row["name"] = sum.Name;
                tb.Table.Rows.Add(row);
            }
            else
            {
                DataRow row = tb.Table.Rows[0];
                row["user_id"] = sum.UserId;
                row["name"] = sum.Name;
            }

            return tb;
        }
        public bool Updates(SysUserM[] sums)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                IDbTransaction transaction = db.StartTransaction();
                bool res = true;
                Array.ForEach(sums, spm =>
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

        private TBSysUser Delete(MyDB db, int id)
        {
            TBSysUser tb = new TBSysUser(db, $"Id={id} for update");

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
