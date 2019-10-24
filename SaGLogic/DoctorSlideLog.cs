using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil;
using SaGUtil.Data;
using SaGUtil.System;
using System;
using System.Data;
using System.Linq;

namespace SaGLogic
{

    /// <summary>
    /// 作為前端應用與資料庫物件的中介層 
    /// 對應 SaGDB.doctor_slide_log_tb Table
    /// 病理醫師分片 Log
    /// </summary>
    public class DoctorSlideLog 
    {
        public bool AddLog(DoctorSlideLogM log)
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
                    TBDoctorSlideLog tb = new TBDoctorSlideLog(db, "1=0");

                    DataRow row = tb.Table.NewRow();
                    row["ord_no"] = log.OrdNo;
                    row["cassette_sequence"] = log.CassetteSequence;
                    row["slide_sequence"] = log.SlideSequence;
                    row["doctor_user"] = log.DoctorUser;
                    row["doctor_date"] = log.DoctorDate;
                    row["doctor_time"] = log.DoctorTime;
                    row["op_date"] = log.OpDate;
                    row["op_time"] = log.OpTime;
                    row["is_delete_flag"] = log.IsDeleteFlag == DeleteFlagEnum.Normal ? "N" : "D";
                    tb.Table.Rows.Add(row);

                    OrderSlide order = new OrderSlide();
                    TBOrderSlide tbOrder = order.Update(db, log);

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

        public bool AddLog(DoctorSlideLogM[] logs)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                IDbTransaction transaction = db.StartTransaction();
                TBDoctorSlideLog tb = new TBDoctorSlideLog(db, "1=0");

                Array.ForEach(logs, log => {
                    log.OpDate = SaDate.TodayYMD();
                    log.OpTime = SaDate.TodayHMS();

                    DataRow row = tb.Table.NewRow();
                    row["ord_no"] = log.OrdNo;
                    row["cassette_sequence"] = log.CassetteSequence;
                    row["slide_sequence"] = log.SlideSequence;
                    row["doctor_user"] = log.DoctorUser;
                    row["doctor_date"] = log.DoctorDate;
                    row["doctor_time"] = log.DoctorTime;
                    row["op_date"] = log.OpDate;
                    row["op_time"] = log.OpTime;
                    row["is_delete_flag"] = log.IsDeleteFlag == DeleteFlagEnum.Normal ? "N" : "D";
                    tb.Table.Rows.Add(row);

                    OrderSlide order = new OrderSlide();
                    TBOrderSlide tbOrder = order.Update(db, log);

                    tbOrder.Update();
                });
                
                if (tb.Update())
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


        public DoctorSlideLogM[] GetDoctorSlidesByOpDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBDoctorSlideLog tb = new TBDoctorSlideLog(db, $"op_date='{date}'");
                    return new DoctorSlideLogM().GenerateModel(tb.Table);
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
