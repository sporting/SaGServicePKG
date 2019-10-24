
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


    public class SysCassetteMagazine
    {
        public SysCassetteMagazineM[] GetValues()
        {        
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBSysCassetteMagazine tb = new TBSysCassetteMagazine(db, "1=1");

                var query = from row in tb.Table.AsEnumerable()
                            select new SysCassetteMagazineM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                MajorClass = row["major_class"].ToString(),
                                Magazine = row["magazine"].ToString()
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new SysCassetteMagazineM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        private TBSysCassetteMagazine Update(MyDB db, SysCassetteMagazineM smm)
        {
            TBSysCassetteMagazine tb = new TBSysCassetteMagazine(db, $"Id='{smm.Id}' for update");

            if (tb.RowsCount <= 0)
            {
                DataRow row = tb.Table.NewRow();
                row["major_class"] = smm.MajorClass;
                row["magazine"] = smm.Magazine;
                tb.Table.Rows.Add(row);

                return tb;
            }
            else
            {
                DataRow row = tb.Table.Rows[0];
                row["major_class"] = smm.MajorClass;
                row["magazine"] = smm.Magazine;

                return tb;
            }
        }
        public bool Update(SysCassetteMagazineM[] ssms)
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


        private TBSysCassetteMagazine Delete(MyDB db, int id)
        {
            TBSysCassetteMagazine tb = new TBSysCassetteMagazine(db, $"Id={id} for update");

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
