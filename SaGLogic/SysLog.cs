using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil.Data;
using SaGUtil.System;
using System;
using System.Data;
using System.Linq;

namespace SaGLogic
{
    /// <summary>
    /// 作為前端應用與資料庫物件的中介層 
    /// 對應 SaGDB.sys_log_tb Table
    /// system log
    /// </summary>

    public class SysLog 
    {
        public bool Add(SysLogM log)
        {
            log.OpDate = SaDate.TodayYMD();
            log.OpTime = SaDate.TodayHMS();

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

    
    }
}
