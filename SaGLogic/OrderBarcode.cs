
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
using SaGUtil.Extensions;

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

        
        public OrderBarcodeM[] GetByOrdNo(string ordNo)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderBarcode tb = new TBOrderBarcode(db, $"ord_no='{ordNo}'");
                    return new OrderBarcodeM().GenerateModel(tb.Table);
                }
                catch
                {
                    return new OrderBarcodeM[] { };
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return new OrderBarcodeM[] { };
        }

        public DataTable GetBarcodeGroup(string begDate, string endDate)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                MyDB db = new MyDB();

                try
                {
                    db.OpenDB();

                    TBOrderBarcode tb;
                    tb = new TBOrderBarcode(db, $"create_date>='{begDate}' and create_date<='{endDate}'");

                    DataTable result = tb.Table.AsEnumerable()
                         .Select(p => new
                         {
                             ord_no = p["ord_no"],
                             create_date = p["create_date"],
                             cassette_amount = p["cassette_total_amount"]
                         }
                         ).ToDataTable();

                    return result;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return null;
        }
        public DataTable GetCassettesGroup(string ordNo)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();

                try
                {
                    db.OpenDB();

                    TBOrderCassette tb;
                    tb = new TBOrderCassette(db, $"ord_no='{ordNo}'");

                    DataTable result = tb.Table.AsEnumerable()
                         .GroupBy(p => new
                         {
                             ord_no = p["ord_no"],
                             op_date = p["op_date"]
                         })
                          .Select(p => new
                          {
                              ord_no = p.Key.ord_no,
                              op_date = p.Key.op_date,
                              count = p.Count(),
                              slide_amount = p.Sum(r => SaConverter.ToInt(r["slide_total_amount"].ToString(), 0))
                          }
                          ).ToDataTable();

                    return result;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return null;
        }

    }
}
