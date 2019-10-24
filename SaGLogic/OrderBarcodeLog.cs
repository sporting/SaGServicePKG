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
    /// 對應 SaGDB.order_barcode_log_tb Table
    /// 列印 Barcode log
    /// </summary>
    public class OrderBarcodeLog 
    {
        public bool Add(OrderBarcodeLogM log)
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
                    TBOrderBarcodeLog tb = new TBOrderBarcodeLog(db, "1=0");

                    DataRow row = tb.Table.NewRow();
                    row["ord_no"] = log.OrdNo;
                    row["amount"] = log.Amount;
                    row["op_date"] = log.OpDate;
                    row["op_time"] = log.OpTime;
                    row["reprint"] = log.RePrint ? "Y" : "N";
                    tb.Table.Rows.Add(row);

                    OrderBarcode order = new OrderBarcode();
                    TBOrderBarcode tbOrder = order.Update(db, log);

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
