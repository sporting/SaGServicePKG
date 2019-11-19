using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil.Data;
using SaGUtil.System;
using SaGUtil.Utils;
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

    public class OrderSlide 
    {
        public bool Add(OrderSlideM log, out int newSlideSeq)
        {
            newSlideSeq = 1;

            if (!string.IsNullOrEmpty(log.OrdNo))
            {
                log.OpDate = SaDate.TodayYMD();
                log.OpTime = SaDate.TodayHMS();

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
            TBOrderSlide tb = new TBOrderSlide(db, $"ord_no='{log.OrdNo}' and cassette_sequence='{log.CassetteSequence}' and slide_sequence='{log.SlideSequence}' for update");

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
                    OrderSlideM[] osms= new OrderSlideM().GenerateModel(tb.Table);

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

        public OrderSlideM[] GetSlidesByOrdNo(string ordNo)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderSlide tb = new TBOrderSlide(db, $"ord_no='{ordNo}'");
                    OrderSlideM[] osms = new OrderSlideM().GenerateModel(tb.Table);

                    if (osms.Count() >0)
                    {
                        return osms;
                    }
                    else
                    {
                        return new OrderSlideM[] { };
                    }
                }
                catch (Exception ex)
                {
                    MyLog.Fatal(this, ex.Message);
                    return new OrderSlideM[] { };
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return new OrderSlideM[] { };
        }

        public OrderSlideM[] GetSlidesByOrdNo(string ordNo,int cassetteSequence)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderSlide tb = new TBOrderSlide(db, $"ord_no='{ordNo}' and cassette_sequence={cassetteSequence}");
                    OrderSlideM[] osms = new OrderSlideM().GenerateModel(tb.Table);

                    if (osms.Count() > 0)
                    {
                        return osms;
                    }
                    else
                    {
                        return new OrderSlideM[] { };
                    }
                }
                catch (Exception ex)
                {
                    MyLog.Fatal(this, ex.Message);
                    return new OrderSlideM[] { };
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return new OrderSlideM[] { };
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
                    return new OrderSlideM().GenerateModel(tb.Table);
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
                    return new OrderSlideM().GenerateModel(tb.Table);
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

                    return new OrderSlideM().GenerateModel(tb.Table);
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

                    return new OrderSlideM().GenerateModel(tb.Table);
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
