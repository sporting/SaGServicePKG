using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil;
using System;
using System.Data;
using System.Linq;

namespace SaGLogic
{

    //包埋處理 log
    public class OrderEmbedLog : ITableModel<OrderEmbedLogM>
    {
        public bool AddLog(OrderEmbedLogM log)
        {
            if (!string.IsNullOrEmpty(log.OrdNo))
            {
                log.OpDate = Utility.Today().ToString("yyyyMMdd");
                log.OpTime = Utility.Today().ToString("HHmmss");

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


        public OrderEmbedLogM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderEmbedLogM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = Convert.ToInt32(row["cassette_sequence"].ToString()),
                        EmbedUser = row["embed_user"].ToString(),
                        EmbedDate = row["embed_date"].ToString(),
                        EmbedTime = row["embed_time"].ToString(),
                        OpDate = row["op_date"].ToString(),
                        OpTime = row["op_time"].ToString(),
                        IsDeleteFlag = row["is_delete_flag"].ToString() == "N" ? DeleteFlagEnum.Normal : DeleteFlagEnum.Delete
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

        public DataTable GenerateDataTable(OrderEmbedLogM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ord_no");
            dt.Columns.Add("cassette_sequence");
            dt.Columns.Add("embed_user");
            dt.Columns.Add("embed_date");
            dt.Columns.Add("embed_time");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");
            dt.Columns.Add("is_delete_flag");

            foreach (OrderEmbedLogM oelm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = oelm.Id;
                row["ord_no"] = oelm.OrdNo;
                row["cassette_sequence"] = oelm.CassetteSequence;
                row["embed_user"] = oelm.EmbedUser;
                row["embed_date"] = oelm.EmbedDate;
                row["embed_time"] = oelm.EmbedTime;
                row["op_date"] = oelm.OpDate;
                row["op_time"] = oelm.OpTime;
                row["is_delete_flag"] = oelm.IsDeleteFlag == DeleteFlagEnum.Normal ? "N" : "D";
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
