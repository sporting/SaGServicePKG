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
        public string SlideF1 { get; set; }
        public string SlideF2 { get; set; }
        public string SlideF3 { get; set; }
        public string SlideF4 { get; set; }
        public string SlideF5 { get; set; }
        public string SlideF6 { get; set; }
        public string SlideF7 { get; set; }
        public string SlideF8 { get; set; }
        public string SlideF9 { get; set; }
        public string SlideF10 { get; set; }
        public string SlideF11 { get; set; }
        public string SlideF12 { get; set; }
        public string SlideF13 { get; set; }
        public string SlideF14 { get; set; }
        public string SlideF15 { get; set; }
        public string SlideF16 { get; set; }
        public string SlideF17 { get; set; }
        public string SlideF18 { get; set; }
        public string SlideF19 { get; set; }
        public string SlideF20 { get; set; }
        //public string SlideRemark { get; set; }
        //public string SlideFieldA { get; set; }
        //public string SlideFieldB { get; set; }
        public string DoctorUser { get; set; }
        public string DoctorDate { get; set; }
        public string DoctorTime { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }
        public OrderSlideM()
        {
            Initialize();
        }
        public void Initialize()
        {
            Id = 0;
            OrdNo = string.Empty;
            CassetteSequence = 0;
            SlideSequence = 0;
            SlideUser = string.Empty;
            SlideF1 = string.Empty;
            SlideF2 = string.Empty;
            SlideF3 = string.Empty;
            SlideF4 = string.Empty;
            SlideF5 = string.Empty;
            SlideF6 = string.Empty;
            SlideF7 = string.Empty;
            SlideF8 = string.Empty;
            SlideF9 = string.Empty;
            SlideF10 = string.Empty;
            SlideF11 = string.Empty;
            SlideF12 = string.Empty;
            SlideF13 = string.Empty;
            SlideF14 = string.Empty;
            SlideF15 = string.Empty;
            SlideF16 = string.Empty;
            SlideF17 = string.Empty;
            SlideF18 = string.Empty;
            SlideF19 = string.Empty;
            SlideF20 = string.Empty;
            DoctorUser = string.Empty;
            DoctorDate = string.Empty;
            DoctorTime = string.Empty;
            OpDate = string.Empty;
            OpTime = string.Empty;
        }

        public OrderSlideM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderSlideM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = SaConverter.ToInt(row["cassette_sequence"], 0),
                        SlideF1 = row["slide_F1"].ToString(),
                        SlideF2 = row["slide_F2"].ToString(),
                        SlideF3 = row["slide_F3"].ToString(),
                        SlideF4 = row["slide_F4"].ToString(),
                        SlideF5 = row["slide_F5"].ToString(),
                        SlideF6 = row["slide_F6"].ToString(),
                        SlideF7 = row["slide_F7"].ToString(),
                        SlideF8 = row["slide_F8"].ToString(),
                        SlideF9 = row["slide_F9"].ToString(),
                        SlideF10 = row["slide_F10"].ToString(),
                        SlideF11 = row["slide_F11"].ToString(),
                        SlideF12 = row["slide_F12"].ToString(),
                        SlideF13 = row["slide_F13"].ToString(),
                        SlideF14 = row["slide_F14"].ToString(),
                        SlideF15 = row["slide_F15"].ToString(),
                        SlideF16 = row["slide_F16"].ToString(),
                        SlideF17 = row["slide_F17"].ToString(),
                        SlideF18 = row["slide_F18"].ToString(),
                        SlideF19 = row["slide_F19"].ToString(),
                        SlideF20 = row["slide_F20"].ToString(),
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
            dt.Columns.Add("slide_F1");
            dt.Columns.Add("slide_F2");
            dt.Columns.Add("slide_F3");
            dt.Columns.Add("slide_F4");
            dt.Columns.Add("slide_F5");
            dt.Columns.Add("slide_F6");
            dt.Columns.Add("slide_F7");
            dt.Columns.Add("slide_F8");
            dt.Columns.Add("slide_F9");
            dt.Columns.Add("slide_F10");
            dt.Columns.Add("slide_F11");
            dt.Columns.Add("slide_F12");
            dt.Columns.Add("slide_F13");
            dt.Columns.Add("slide_F14");
            dt.Columns.Add("slide_F15");
            dt.Columns.Add("slide_F16");
            dt.Columns.Add("slide_F17");
            dt.Columns.Add("slide_F18");
            dt.Columns.Add("slide_F19");
            dt.Columns.Add("slide_F20");
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
                row["slide_F1"] = osm.SlideF1;
                row["slide_F2"] = osm.SlideF2;
                row["slide_F3"] = osm.SlideF3;
                row["slide_F4"] = osm.SlideF4;
                row["slide_F5"] = osm.SlideF5;
                row["slide_F6"] = osm.SlideF6;
                row["slide_F7"] = osm.SlideF7;
                row["slide_F8"] = osm.SlideF8;
                row["slide_F9"] = osm.SlideF9;
                row["slide_F10"] = osm.SlideF10;
                row["slide_F11"] = osm.SlideF11;
                row["slide_F12"] = osm.SlideF12;
                row["slide_F13"] = osm.SlideF13;
                row["slide_F14"] = osm.SlideF14;
                row["slide_F15"] = osm.SlideF15;
                row["slide_F16"] = osm.SlideF16;
                row["slide_F17"] = osm.SlideF17;
                row["slide_F18"] = osm.SlideF18;
                row["slide_F19"] = osm.SlideF19;
                row["slide_F20"] = osm.SlideF20;
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
