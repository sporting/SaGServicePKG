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
    /// 對應 SaGDB.order_embed_log_tb Table
    /// 包埋處理 log
    /// </summary>

    public class OrderEmbedLog
    {
        public bool AddLog(OrderEmbedLogM log)
        {
            if (!string.IsNullOrEmpty(log.OrdNo))
            {
                log.OpDate = SaDate.TodayYMD();
                log.OpTime = SaDate.TodayHMS();

                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    IDbTransaction transaction = db.StartTransaction();
                    TBOrderEmbedLog tb = new TBOrderEmbedLog(db, "1=0");

                    DataRow row = tb.Table.NewRow();
                    row["ord_no"] = log.OrdNo;
                    row["cassette_sequence"] = log.CassetteSequence;
                    row["embed_user"] = log.EmbedUser;
                    row["embed_date"] = log.EmbedDate;
                    row["embed_time"] = log.EmbedTime;
                    row["op_date"] = log.OpDate;
                    row["op_time"] = log.OpTime;
                    row["is_delete_flag"] = log.IsDeleteFlag == DeleteFlagEnum.Normal ? "N" : "D";
                    tb.Table.Rows.Add(row);

                    OrderCassette order = new OrderCassette();
                    TBOrderCassette tbOrder = order.Update(db, log);

                    if (tb.Update() && tbOrder.Update())
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
            return true;
        }


    }
}
