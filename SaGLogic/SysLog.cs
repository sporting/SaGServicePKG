using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil.System;
using System;
using System.Data;
using System.Linq;

namespace SaGLogic
{

    //system log
    public class SysLog : ITableModel<SysLogM>
    {
        public bool Add(SysLogM log)
        {
            log.OpDate = SaDate.Today().ToString("yyyyMMdd");
            log.OpTime = SaDate.Today().ToString("HHmmss");

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();

                IDbTransaction transaction = db.StartTransaction();
                TBSysLog tb = new TBSysLog(db, "1=0");

                DataRow row = tb.Table.NewRow();
                row["event_name"] = log.EventName;
                row["params"] = log.Params;
                row["op_date"] = log.OpDate;
                row["op_time"] = log.OpTime;
                tb.Table.Rows.Add(row);

                if (tb.Update())
                {
                    if (db.Commit(transaction))
                        return true; ;
                }

                return false;
            }
            finally
            {
                db.CloseDB();
            }
        }

        public SysLogM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SysLogM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        EventName = row["event_name"].ToString(),
                        Params = row["params"].ToString(),
                        OpDate = row["op_date"].ToString(),
                        OpTime = row["op_time"].ToString()
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

        public DataTable GenerateDataTable(SysLogM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("event_name");
            dt.Columns.Add("params");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");

            foreach (SysLogM slm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = slm.Id;
                row["event_name"] = slm.EventName;
                row["params"] = slm.Params;
                row["op_date"] = slm.OpDate;
                row["op_time"] = slm.OpTime;
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
