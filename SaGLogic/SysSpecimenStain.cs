
using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil.System;
using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGLogic
{


    public class SysSpecimenStain
    {
        private TBSysSpecimenStain Update(MyDB db, SysSpecimenStainM ssm)
        {
            TBSysSpecimenStain tb = new TBSysSpecimenStain(db, $"Id='{ssm.Id}' for update");

            if (tb.RowsCount <= 0)
            {
                DataRow row = tb.Table.NewRow();
                row["specimen"] = ssm.Specimen;
                row["seq"] = ssm.Seq;
                row["stain"] = ssm.Stain;
                row["stop_flag"] = ssm.StopFlag;
                tb.Table.Rows.Add(row);
            }
            else
            {
                DataRow row = tb.Table.Rows[0];
                row["specimen"] = ssm.Specimen;
                row["seq"] = ssm.Seq;
                row["stain"] = ssm.Stain;
                row["stop_flag"] = ssm.StopFlag;
            }

            return tb;
        }


        public bool Update(SysSpecimenStainM[] ssms)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                IDbTransaction transaction = db.StartTransaction();
                bool res = true;
                Array.ForEach(ssms, ssm =>
                {
                    if (!res) { return; }

                    MyTable tb = Update(db, ssm);
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
        
        private TBSysSpecimenStain Delete(MyDB db, int id)
        {
            TBSysSpecimenStain tb = new TBSysSpecimenStain(db, $"Id={id} for update");

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

        public SysSpecimenStainM[] GetValidAll()
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBSysSpecimenStain tb = new TBSysSpecimenStain(db, $"stop_flag='N'");

                var query = from row in tb.Table.AsEnumerable()
                            select new SysSpecimenStainM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                Specimen = row["specimen"].ToString(),
                                Seq = SaConverter.ToInt(row["seq"].ToString(), 0),
                                Stain = row["stain"].ToString(),
                                StopFlag = row["stop_flag"].ToString(),
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new SysSpecimenStainM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        public SysSpecimenStainM[] GetAll()
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBSysSpecimenStain tb = new TBSysSpecimenStain(db, $"1=1");

                var query = from row in tb.Table.AsEnumerable()
                            select new SysSpecimenStainM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                Specimen = row["specimen"].ToString(),
                                Seq = SaConverter.ToInt(row["seq"].ToString(), 0),
                                Stain = row["stain"].ToString(),
                                StopFlag = row["stop_flag"].ToString(),
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new SysSpecimenStainM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }
    }
}
