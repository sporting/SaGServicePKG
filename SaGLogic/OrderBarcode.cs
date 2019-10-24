
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

    /// <summary>
    /// 作為前端應用與資料庫物件的中介層 
    /// 對應 SaGDB.order_barcode_tb Table
    /// 列印 Barcode list
    /// </summary>

    public class OrderBarcode
    {
        //列印 barcode 時寫入
        public TBOrderBarcode Update(MyDB db, OrderBarcodeLogM ordLog)
        {
            TBOrderBarcode tb = new TBOrderBarcode(db, $"ord_no='{ordLog.OrdNo}' for update");

            if (tb.RowsCount <= 0)
            {
                DataRow row = tb.Table.NewRow();
                row["ord_no"] = ordLog.OrdNo;
                row["barcode_total_amount"] = ordLog.Amount;
                row["cassette_total_amount"] = 0;
                row["create_date"] = SaDate.TodayYMD();
                row["create_time"] = SaDate.TodayHMS();
                row["op_date"] = row["create_date"];
                row["op_time"] = row["create_time"];
                tb.Table.Rows.Add(row);

                return tb;
            }
            else
            {
                DataRow row = tb.Table.Rows[0];
                row["barcode_total_amount"] = ordLog.Amount + SaConverter.ToInt(row["barcode_total_amount"], 0);
                row["op_date"] = SaDate.TodayYMD();
                row["op_time"] = SaDate.TodayHMS();

                return tb;
            }
        }

        //列印 cassette 時更新
        public TBOrderBarcode Update(MyDB db, OrderCassetteM ordLog, out int newCassetteSeq)
        {
            TBOrderBarcode tb = new TBOrderBarcode(db, $"ord_no='{ordLog.OrdNo}' for update");

            if (tb.RowsCount <= 0)
            {
                DataRow row = tb.Table.NewRow();
                row["ord_no"] = ordLog.OrdNo;
                row["barcode_total_amount"] = 0;
                row["cassette_total_amount"] = 1;
                row["create_date"] = SaDate.TodayYMD();
                row["create_time"] = SaDate.TodayHMS();
                row["op_date"] = row["create_date"];
                row["op_time"] = row["create_time"];
                tb.Table.Rows.Add(row);

                newCassetteSeq = SaConverter.ToInt(row["cassette_total_amount"], 1);

                return tb;
            }
            else
            {
                DataRow row = tb.Table.Rows[0];
                row["cassette_total_amount"] = 1 + SaConverter.ToInt(row["cassette_total_amount"], 0);

                newCassetteSeq = SaConverter.ToInt(row["cassette_total_amount"], 1);

                return tb;
            }
        }


        //有沒有該檢體編號
        public bool OrdNoExist(string ordNo)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderBarcode tb = new TBOrderBarcode(db, $"ord_no='{ordNo}'");
                    return tb.RowsCount > 0;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return false;
        }


  
    }
}
