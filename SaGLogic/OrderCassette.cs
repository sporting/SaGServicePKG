using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil.System;
using SaGUtil.Data;
using System;
using System.Data;
using System.Linq;

namespace SaGLogic
{

    //Cassette 主檔
    public class OrderCassette : ITableModel<OrderCassetteM>
    {

        public bool Add(OrderCassetteM log, out int newCassetteSeq)
        {
            newCassetteSeq = 1;

            if (!string.IsNullOrEmpty(log.OrdNo))
            {
                log.OpDate = SaDate.Today().ToString("yyyyMMdd");
                log.OpTime = SaDate.Today().ToString("HHmmss");

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
                    
                    return GenerateModel(tb.Table);
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

                    return GenerateModel(tb.Table);
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


        public OrderCassetteM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderCassetteM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = Convert.ToInt32(row["cassette_sequence"].ToString()),
                        CassetteRemark = row["cassette_remark"].ToString(),
                        CassetteFieldA = row["cassette_fieldA"].ToString(),
                        CassetteFieldB = row["cassette_fieldB"].ToString(),
                        GrossUser = row["gross_user"].ToString(),
                        GrossDate = row["gross_date"].ToString(),
                        GrossTime = row["gross_time"].ToString(),
                        EmbedUser = row["embed_user"].ToString(),
                        EmbedDate = row["embed_date"].ToString(),
                        EmbedTime = row["embed_time"].ToString(),
                        SlideTotalAmount = Convert.ToInt32(row["slide_total_amount"].ToString()),
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

        public DataTable GenerateDataTable(OrderCassetteM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ord_no");
            dt.Columns.Add("cassette_sequence");
            dt.Columns.Add("cassette_remark");
            dt.Columns.Add("cassette_fieldA");
            dt.Columns.Add("cassette_fieldB");
            dt.Columns.Add("gross_user");
            dt.Columns.Add("gross_date");
            dt.Columns.Add("gross_time");
            dt.Columns.Add("embed_user");
            dt.Columns.Add("embed_date");
            dt.Columns.Add("embed_time");
            dt.Columns.Add("slide_total_amount");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");

            foreach (OrderCassetteM ocm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = ocm.Id;
                row["ord_no"] = ocm.OrdNo;
                row["cassette_sequence"] = ocm.CassetteSequence;
                row["cassette_remark"] = ocm.CassetteRemark;
                row["cassette_fieldA"] = ocm.CassetteFieldA;
                row["cassette_fieldB"] = ocm.CassetteFieldB;
                row["gross_user"] = ocm.GrossUser;
                row["gross_date"] = ocm.GrossDate;
                row["gross_time"] = ocm.GrossTime;
                row["embed_user"] = ocm.EmbedUser;
                row["embed_date"] = ocm.EmbedDate;
                row["embed_time"] = ocm.EmbedTime;
                row["slide_total_amount"] = ocm.SlideTotalAmount;
                row["op_date"] = ocm.OpDate;
                row["op_time"] = ocm.OpTime;
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
