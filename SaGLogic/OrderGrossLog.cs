using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil.System;
using System;
using System.Data;
using System.Linq;

namespace SaGLogic
{
    /// <summary>
    /// 作為前端應用與資料庫物件的中介層 
    /// 對應 SaGDB.order_gross_log_tb Table
    /// Gross 處理 log
    /// </summary>
    public class OrderGrossLog:ITableModel<OrderGrossLogM>
    {
        public bool AddLog(OrderGrossLogM log)
        {
            if (!string.IsNullOrEmpty(log.OrdNo))
            {
                log.OpDate = SaDate.Today().ToString("yyyyMMdd");
                log.OpTime = SaDate.Today().ToString("HHmmss");

                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    IDbTransaction transaction = db.StartTransaction();
                    TBOrderGrossLog tb = new TBOrderGrossLog(db, "1=0");

                    DataRow row = tb.Table.NewRow();
                    row["ord_no"] = log.OrdNo;
                    row["cassette_sequence"] = log.CassetteSequence;
                    row["gross_user"] = log.GrossUser;
                    row["gross_date"] = log.GrossDate;
                    row["gross_time"] = log.GrossTime;
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


        public OrderGrossLogM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderGrossLogM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = Convert.ToInt32(row["cassette_sequence"].ToString()),
                        GrossUser = row["gross_user"].ToString(),
                        GrossDate = row["gross_date"].ToString(),
                        GrossTime = row["gross_time"].ToString(),
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

        public DataTable GenerateDataTable(OrderGrossLogM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ord_no");
            dt.Columns.Add("cassette_sequence");
            dt.Columns.Add("gross_user");
            dt.Columns.Add("gross_date");
            dt.Columns.Add("gross_time");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");
            dt.Columns.Add("is_delete_flag");

            foreach (OrderGrossLogM oglm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = oglm.Id;
                row["ord_no"] = oglm.OrdNo;
                row["cassette_sequence"] = oglm.CassetteSequence;
                row["gross_user"] = oglm.GrossUser;
                row["gross_date"] = oglm.GrossDate;
                row["gross_time"] = oglm.GrossTime;
                row["op_date"] = oglm.OpDate;
                row["op_time"] = oglm.OpTime;
                row["is_delete_flag"] = oglm.IsDeleteFlag == DeleteFlagEnum.Normal ? "N" : "D";
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
