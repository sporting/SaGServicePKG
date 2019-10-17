using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil;
using SaGUtil.System;
using System;
using System.Data;
using System.Linq;

namespace SaGLogic
{

    //病理醫師分片 Log
    public class DoctorSlideLog : ITableModel<DoctorSlideLogM>
    {
        public bool AddLog(DoctorSlideLogM log)
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

                foreach (DoctorSlideLogM log in logs)
                {
                    log.OpDate = SaDate.Today().ToString("yyyyMMdd");
                    log.OpTime = SaDate.Today().ToString("HHmmss");                 

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
                }

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

        public DoctorSlideLogM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new DoctorSlideLogM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = Convert.ToInt32(row["cassette_sequence"]),
                        SlideSequence = Convert.ToInt32(row["slide_sequence"]),
                        DoctorUser = row["doctor_user"].ToString(),
                        DoctorDate = row["doctor_date"].ToString(),
                        DoctorTime = row["doctor_time"].ToString(),
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

        public DataTable GenerateDataTable(DoctorSlideLogM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ord_no");
            dt.Columns.Add("cassette_sequence");
            dt.Columns.Add("slide_sequence");
            dt.Columns.Add("doctor_user");
            dt.Columns.Add("doctor_date");
            dt.Columns.Add("doctor_time");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");
            dt.Columns.Add("is_delete_flag");

            foreach (DoctorSlideLogM oslm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = oslm.Id;
                row["ord_no"] = oslm.OrdNo;
                row["cassette_sequence"] = oslm.CassetteSequence;
                row["slide_sequence"] = oslm.SlideSequence;
                row["doctor_user"] = oslm.DoctorUser;
                row["doctor_date"] = oslm.DoctorDate;
                row["doctor_time"] = oslm.DoctorTime;
                row["op_date"] = oslm.OpDate;
                row["op_time"] = oslm.OpTime;
                row["is_delete_flag"] = oslm.IsDeleteFlag == DeleteFlagEnum.Normal ? "N" : "D";
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
