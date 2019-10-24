
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


    public class SysSlideSlot 
    {
        public SysSlideSlotM[] GetValues()
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBSysSlideSlot tb = new TBSysSlideSlot(db, "1=1");

                var query = from row in tb.Table.AsEnumerable()
                            select new SysSlideSlotM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                MajorClass = row["major_class"].ToString(),
                                Slot = SaConverter.ToInt(row["slot"].ToString(),0)
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new SysSlideSlotM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        private TBSysSlideSlot Update(MyDB db, SysSlideSlotM ssm)
        {
            TBSysSlideSlot tb = new TBSysSlideSlot(db, $"Id='{ssm.Id}' for update");

            if (tb.RowsCount <= 0)
            {
                DataRow row = tb.Table.NewRow();
                row["major_class"] = ssm.MajorClass;
                row["slot"] = SaConverter.ToInt(ssm.Slot,0);
                tb.Table.Rows.Add(row);

                return tb;
            }
            else
            {
                DataRow row = tb.Table.Rows[0];
                row["major_class"] = ssm.MajorClass;
                row["slot"] = SaConverter.ToInt(ssm.Slot,0);

                return tb;
            }
        }


        public bool Updates(SysSlideSlotM[] ssms)
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

        private TBSysSlideSlot Delete(MyDB db, int id)
        {
            TBSysSlideSlot tb = new TBSysSlideSlot(db, $"Id={id} for update");

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
