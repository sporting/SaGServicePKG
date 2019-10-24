using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class OrderSlideM : ITableModel<OrderSlideM>
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int CassetteSequence { get; set; }
        public int SlideSequence { get; set; }
        public string SlideUser { get; set; }
        public string SlideRemark { get; set; }
        public string SlideFieldA { get; set; }
        public string SlideFieldB { get; set; }
        public string DoctorUser { get; set; }
        public string DoctorDate { get; set; }
        public string DoctorTime { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }


        public OrderSlideM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderSlideM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = SaConverter.ToInt(row["cassette_sequence"], 0),
                        SlideFieldA = row["slide_fieldA"].ToString(),
                        SlideFieldB = row["slide_fieldB"].ToString(),
                        SlideRemark = row["slide_remark"].ToString(),
                        SlideSequence = SaConverter.ToInt(row["slide_sequence"], 0),
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

            Array.ForEach(models, osm => {
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
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
