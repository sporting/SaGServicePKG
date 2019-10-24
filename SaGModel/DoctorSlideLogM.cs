using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class DoctorSlideLogM : ITableModel<DoctorSlideLogM>
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int CassetteSequence { get; set; }
        public int SlideSequence { get; set; }
        public string DoctorUser { get; set; }
        public string DoctorDate { get; set; }
        public string DoctorTime { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }       
        public DeleteFlagEnum IsDeleteFlag { get; set; }



        public DoctorSlideLogM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new DoctorSlideLogM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = SaConverter.ToInt(row["cassette_sequence"], 0),
                        SlideSequence = SaConverter.ToInt(row["slide_sequence"], 0),
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

            Array.ForEach(models, oslm => {
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
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
