using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil.System;
using SaGUtil.Data;
using System.Data;
using System.Linq;
using SaGUtil.Extensions;

namespace SaGLogic
{
    /// <summary>
    /// 作為前端應用與資料庫物件的中介層 
    /// 對應 SaGDB.order_cassette_tb Table
    /// Cassette 主檔
    /// </summary>

    public class OrderCassette
    {

        public bool Add(OrderCassetteM log, out int newCassetteSeq)
        {
            newCassetteSeq = 1;

            if (!string.IsNullOrEmpty(log.OrdNo))
            {
                log.OpDate = SaDate.TodayYMD();
                log.OpTime = SaDate.TodayHMS();

                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    IDbTransaction transaction = db.StartTransaction();

                    OrderBarcode order = new OrderBarcode();
                    TBOrderBarcode tbOrder = order.Update(db, log, out newCassetteSeq);

                    log.CassetteSequence = newCassetteSeq;

                    TBOrderCassette tb = new TBOrderCassette(db, "1=0");

                    DataRow row = tb.Table.NewRow();
                    row["ord_no"] = log.OrdNo;
                    row["cassette_sequence"] = log.CassetteSequence;
                    row["cassette_remark"] = log.CassetteRemark;
                    row["cassette_fieldA"] = log.CassetteFieldA;
                    row["cassette_fieldB"] = log.CassetteFieldB;
                    row["cassette_doc_no"] = log.CassetteDocNo;
                    row["cassette_small_pieces"] = log.CassetteSmallPiece;
                    row["gross_user"] = log.GrossUser;
                    row["gross_date"] = log.GrossDate;
                    row["gross_time"] = log.GrossTime;
                    row["embed_user"] = log.EmbedUser;
                    row["embed_date"] = log.EmbedDate;
                    row["embed_time"] = log.EmbedTime;
                    row["slide_total_amount"] = log.SlideTotalAmount;
                    row["op_date"] = log.OpDate;
                    row["op_time"] = log.OpTime;

                    tb.Table.Rows.Add(row);

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

        public TBOrderCassette Update(MyDB db, OrderGrossLogM log)
        {
            TBOrderCassette tb = new TBOrderCassette(db, $"ord_no='{log.OrdNo}' and cassette_sequence={log.CassetteSequence} for update");

            if (tb.RowsCount > 0)
            {
                DataRow row = tb.Table.Rows[0];
                row["gross_user"] = log.GrossUser;
                row["gross_date"] = log.GrossDate;
                row["gross_time"] = log.GrossTime;
            }

            return tb;
        }

        public TBOrderCassette Update(MyDB db, OrderEmbedLogM log)
        {
            TBOrderCassette tb = new TBOrderCassette(db, $"ord_no='{log.OrdNo}' and cassette_sequence={log.CassetteSequence} for update");

            if (tb.RowsCount > 0)
            {
                DataRow row = tb.Table.Rows[0];
                row["embed_user"] = log.EmbedUser;
                row["embed_date"] = log.EmbedDate;
                row["embed_time"] = log.EmbedTime;
            }

            return tb;
        }

        public TBOrderCassette Update(MyDB db, OrderSlideM log, out int newSlideSeq)
        {
            TBOrderCassette tb = new TBOrderCassette(db, $"ord_no='{log.OrdNo}' and cassette_sequence={log.CassetteSequence} for update");
            newSlideSeq = 1;

            if (tb.RowsCount > 0)
            {
                DataRow row = tb.Table.Rows[0];
                row["slide_total_amount"] = 1 + SaConverter.ToInt(row["slide_total_amount"], 0);

                newSlideSeq = SaConverter.ToInt(row["slide_total_amount"], 1);
            }


            return tb;
        }

        public OrderCassetteM[] GetGrossDetail(string begDate, string endDate, string grossUser)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                MyDB db = new MyDB();

                try
                {
                    db.OpenDB();

                    TBOrderCassette tb;
                    if (string.IsNullOrEmpty(grossUser))
                    {
                        tb = new TBOrderCassette(db, $"gross_date>='{begDate}' and gross_date<='{endDate}'");
                    }
                    else
                    {
                        tb = new TBOrderCassette(db, $"gross_date>='{begDate}' and gross_date<='{endDate}' and gross_user='{grossUser}'");
                    }

                    return new OrderCassetteM().GenerateModel(tb.Table);
                }
                catch
                {
                    return new OrderCassetteM[] { };
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return new OrderCassetteM[] { };
        }

        public OrderCassetteM[] GetEmbedDetail(string begDate, string endDate, string embedUser)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                MyDB db = new MyDB();

                try
                {
                    db.OpenDB();

                    TBOrderCassette tb;
                    if (string.IsNullOrEmpty(embedUser))
                    {
                        tb = new TBOrderCassette(db, $"embed_date>='{begDate}' and embed_date<='{endDate}'");
                    }
                    else
                    {
                        tb = new TBOrderCassette(db, $"embed_date>='{begDate}' and embed_date<='{endDate}' and embed_user='{embedUser}'");
                    }

                    return new OrderCassetteM().GenerateModel(tb.Table);
                }
                catch
                {
                    return new OrderCassetteM[] { };
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return new OrderCassetteM[] { };
        }

        public DataTable GetCassettesGroup(string begDate, string endDate)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                MyDB db = new MyDB();

                try
                {
                    db.OpenDB();

                    TBOrderCassette tb;
                    tb = new TBOrderCassette(db, $"op_date>='{begDate}' and op_date<='{endDate}'");

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
                             slide_amount = p.Sum(r=>SaConverter.ToInt(r["slide_total_amount"].ToString(),0))
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

        public OrderCassetteM[] GetCassettes(string ordNo)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();

                try
                {
                    db.OpenDB();

                    TBOrderCassette tb;
                    tb = new TBOrderCassette(db, $"ord_no='{ordNo}'");

                    return new OrderCassetteM().GenerateModel(tb.Table);
                }
                catch
                {
                    return new OrderCassetteM[] { };
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return new OrderCassetteM[] { };
        }

        public OrderCassetteM[] GetCassettes(string ordNo,int cassetteSequence)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();

                try
                {
                    db.OpenDB();

                    TBOrderCassette tb;
                    tb = new TBOrderCassette(db, $"ord_no='{ordNo}' and cassette_sequence={cassetteSequence}");

                    return new OrderCassetteM().GenerateModel(tb.Table);
                }
                catch
                {
                    return new OrderCassetteM[] { };
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return new OrderCassetteM[] { };
        }

        //取得最新的 slide_sequence
        public int GetSlideSequence(string ordNo, int cassetteSeq)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderCassette tb = new TBOrderCassette(db, $"ord_no='{ordNo}' and cassette_sequence={cassetteSeq}");
                    if (tb.RowsCount > 0)
                    {
                        return SaConverter.ToInt(tb.Table.Rows[0]["cassette_total_amount"].ToString(), 0);
                    }
                }
                catch
                {
                    return 0;
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return 0;
        }

        //有沒有該包埋盒
        public bool CassetteExist(string ordNo, int cassetteSeq)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderCassette tb = new TBOrderCassette(db, $"ord_no='{ordNo}' and cassette_sequence={cassetteSeq}");
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
