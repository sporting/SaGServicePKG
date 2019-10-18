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
    /// 對應 SaGDB.order_slide_tb Table
    /// 玻片 list
    /// </summary>

    public class OrderSlide : ITableModel<OrderSlideM>
    {
        public bool Add(OrderSlideM log, out int newSlideSeq)
        {
            newSlideSeq = 1;

            if (!string.IsNullOrEmpty(log.OrdNo))
            {
                log.OpDate = SaDate.Today().ToString("yyyyMMdd");
                log.OpTime = SaDate.Today().ToString("HHmmss");

                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    IDbTransaction transaction = db.StartTransaction();

                    OrderCassette order = new OrderCassette();
                    TBOrderCassette tbOrder = order.Update(db, log, out newSlideSeq);

                    log.SlideSequence = newSlideSeq;

                    TBOrderSlide tb = new TBOrderSlide(db, "1=0");

                    DataRow row = tb.Table.NewRow();
                    row["ord_no"] = log.OrdNo;
                    row["cassette_sequence"] = log.CassetteSequence;
                    row["slide_sequence"] = log.SlideSequence;
                    row["slide_user"] = log.SlideUser;

                    row["slide_remark"] = log.SlideRemark;
                    row["slide_fieldA"] = log.SlideFieldA;
                    row["slide_fieldB"] = log.SlideFieldB;

                    row["doctor_user"] = log.DoctorUser;
                    row["doctor_date"] = log.DoctorDate;
                    row["doctor_time"] = log.DoctorTime;
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

        public TBOrderSlide Update(MyDB db, DoctorSlideLogM log)
        {
            TBOrderSlide tb = new TBOrderSlide(db, $"ord_no='{log.OrdNo}' and cassette_sequence={log.CassetteSequence} and slide_sequence={log.SlideSequence} for update");

            if (tb.RowsCount > 0)
            {
                DataRow row = tb.Table.Rows[0];
                row["doctor_user"] = log.DoctorUser;
                row["doctor_date"] = log.DoctorDate;
                row["doctor_time"] = log.DoctorTime;
            }

            return tb;
        }

        //有沒有該玻片
        public bool SlideExist(string ordNo, int cassetteSeq, int slideSeq)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderSlide tb = new TBOrderSlide(db, $"ord_no='{ordNo}' and cassette_sequence={cassetteSeq} and slide_sequence={slideSeq}");
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

        public OrderSlideM GetSlidesByOrdNoSeq(string ordNo,int cassetteSequence,int slideSequence)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderSlide tb = new TBOrderSlide(db, $"ord_no='{ordNo}' and cassette_sequence={cassetteSequence} and slide_sequence={slideSequence}");
                    OrderSlideM[] osms= GenerateModel(tb.Table);

                    if (osms.Count() == 1)
                    {
                        return osms[0];
                    }
                    else
                    {
                        return null;
                    }
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

        public OrderSlideM[] GetSlidesByDoctorDate(string doctorDate)
        {
            if (!string.IsNullOrEmpty(doctorDate))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderSlide tb = new TBOrderSlide(db, $"doctor_date='{doctorDate}'");
                    return GenerateModel(tb.Table);
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
        public OrderSlideM[] GetSlidesByOpDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderSlide tb = new TBOrderSlide(db, $"op_date='{date}'");
                    return GenerateModel(tb.Table);
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

        public OrderSlideM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderSlideM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = Convert.ToInt32(row["cassette_sequence"]),
                        SlideFieldA = row["slide_fieldA"].ToString(),
                        SlideFieldB = row["slide_fieldB"].ToString(),
                        SlideRemark = row["slide_remark"].ToString(),
                        SlideSequence = Convert.ToInt32(row["slide_sequence"]),
                        SlideUser = row["slide_user"].ToString(),
                        DoctorDate = row["doctor_date"].ToString(),
                        DoctorTime = row["doctor_time"].ToString(),
                        DoctorUser = row["doctor_user"].ToString(),
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

        public DataTable GenerateDataTable(OrderSlideM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ord_no");
            dt.Columns.Add("cassette_sequence");
            dt.Columns.Add("slide_sequence");
            dt.Columns.Add("slide_user");
            dt.Columns.Add("slide_remark");
            dt.Columns.Add("slide_fieldA");
            dt.Columns.Add("slide_fieldB");
            dt.Columns.Add("doctor_user");
            dt.Columns.Add("doctor_date");
            dt.Columns.Add("doctor_time");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");

            foreach (OrderSlideM osm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = osm.Id;
                row["ord_no"] = osm.OrdNo;
                row["cassette_sequence"] = osm.CassetteSequence;
                row["slide_sequence"] = osm.SlideSequence;
                row["slide_user"] = osm.SlideUser;
                row["slide_remark"] = osm.SlideRemark;
                row["slide_fieldA"] = osm.SlideFieldA;
                row["slide_fieldB"] = osm.SlideFieldB;
                row["doctor_user"] = osm.DoctorUser;
                row["doctor_date"] = osm.DoctorDate;
                row["doctor_time"] = osm.DoctorTime;
                row["op_date"] = osm.OpDate;
                row["op_time"] = osm.OpTime;
                dt.Rows.Add(row);
            }

            return dt;
        }

        public OrderSlideM[] GetSlideDetail(string begDate, string endDate, string slideUser)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                MyDB db = new MyDB();

                try
                {
                    db.OpenDB();

                    TBOrderSlide tb;
                    if (string.IsNullOrEmpty(slideUser))
                    {
                        tb = new TBOrderSlide(db, $"op_date>='{begDate}' and op_date<='{endDate}'");
                    }
                    else
                    {
                        tb = new TBOrderSlide(db, $"op_date>='{begDate}' and op_date<='{endDate}' and slide_user='{slideUser}'");
                    }

                    return GenerateModel(tb.Table);
                }
                catch
                {
                    return new OrderSlideM[] { };
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return new OrderSlideM[] { };
        }

        public OrderSlideM[] GetDoctorSlideDetail(string begDate, string endDate, string doctorUser)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                MyDB db = new MyDB();

                try
                {
                    db.OpenDB();

                    TBOrderSlide tb;
                    if (string.IsNullOrEmpty(doctorUser))
                    {
                        tb = new TBOrderSlide(db, $"doctor_date>='{begDate}' and doctor_date<='{endDate}'");
                    }
                    else
                    {
                        tb = new TBOrderSlide(db, $"doctor_date>='{begDate}' and doctor_date<='{endDate}' and doctor_user='{doctorUser}'");
                    }

                    return GenerateModel(tb.Table);
                }
                catch
                {
                    return new OrderSlideM[] { };
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return new OrderSlideM[] { };
        }
    }
}
