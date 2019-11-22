using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class OrderCassetteM : ITableModel<OrderCassetteM>
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int CassetteSequence { get; set; }
        public string CassetteF1 { get; set; }
        public string CassetteF2 { get; set; }
        public string CassetteF3 { get; set; }
        public string CassetteF4 { get; set; }
        public string CassetteF5 { get; set; }
        public string CassetteF6 { get; set; }
        public string CassetteF7 { get; set; }
        public string CassetteF8 { get; set; }
        public string CassetteF9 { get; set; }
        public string CassetteF10 { get; set; }
        public string CassetteF11 { get; set; }
        public string CassetteF12 { get; set; }
        public string CassetteF13 { get; set; }
        public string CassetteF14 { get; set; }
        public string CassetteF15 { get; set; }
        public string CassetteF16 { get; set; }
        public string CassetteF17 { get; set; }
        public string CassetteF18 { get; set; }
        public string CassetteF19 { get; set; }
        public string CassetteF20 { get; set; }
        //public string CassetteRemark { get; set; }
        //public string CassetteFieldA { get; set; }
        //public string CassetteFieldB { get; set; }

        //public string CassetteSmallPiece { get; set; }
        //public string CassetteDocNo { get; set; }

        public string GrossUser { get; set; }
        public string GrossDate { get; set; }
        public string GrossTime { get; set; }
        public string EmbedUser { get; set; }
        public string EmbedDate { get; set; }
        public string EmbedTime { get; set; }
        public int SlideTotalAmount { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }
        public OrderCassetteM()
        {
            Initialize();
        }
        public void Initialize()
        {
            Id = 0;
            OrdNo = string.Empty;
            CassetteSequence = 0;
            CassetteF1 = string.Empty;
            CassetteF2 = string.Empty;
            CassetteF3 = string.Empty;
            CassetteF4 = string.Empty;
            CassetteF5 = string.Empty;
            CassetteF6 = string.Empty;
            CassetteF7 = string.Empty;
            CassetteF8 = string.Empty;
            CassetteF9 = string.Empty;
            CassetteF10 = string.Empty;
            CassetteF11 = string.Empty;
            CassetteF12 = string.Empty;
            CassetteF13 = string.Empty;
            CassetteF14 = string.Empty;
            CassetteF15 = string.Empty;
            CassetteF16 = string.Empty;
            CassetteF17 = string.Empty;
            CassetteF18 = string.Empty;
            CassetteF19 = string.Empty;
            CassetteF20 = string.Empty;
            GrossUser = string.Empty;
            GrossDate = string.Empty;
            GrossTime = string.Empty;
            EmbedUser = string.Empty;
            EmbedDate = string.Empty;
            EmbedTime = string.Empty;
            SlideTotalAmount = 0;
            OpDate = string.Empty;
            OpTime = string.Empty;
        }

        public OrderCassetteM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderCassetteM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = SaConverter.ToInt(row["cassette_sequence"].ToString(), 0),
                        CassetteF1 = row["cassette_F1"].ToString(),
                        CassetteF2 = row["cassette_F2"].ToString(),
                        CassetteF3 = row["cassette_F3"].ToString(),

                        CassetteF4 = row["cassette_F4"].ToString(),
                        CassetteF5 = row["cassette_F5"].ToString(),
                        CassetteF6 = row["cassette_F6"].ToString(),
                        CassetteF7 = row["cassette_F7"].ToString(),
                        CassetteF8 = row["cassette_F8"].ToString(),
                        CassetteF9 = row["cassette_F9"].ToString(),
                        CassetteF10 = row["cassette_F10"].ToString(),
                        CassetteF11 = row["cassette_F11"].ToString(),
                        CassetteF12 = row["cassette_F12"].ToString(),
                        CassetteF13 = row["cassette_F13"].ToString(),
                        CassetteF14 = row["cassette_F14"].ToString(),
                        CassetteF15 = row["cassette_F15"].ToString(),
                        CassetteF16 = row["cassette_F16"].ToString(),
                        CassetteF17 = row["cassette_F17"].ToString(),
                        CassetteF18 = row["cassette_F18"].ToString(),
                        CassetteF19 = row["cassette_F19"].ToString(),
                        CassetteF20 = row["cassette_F20"].ToString(),

                        GrossUser = row["gross_user"].ToString(),
                        GrossDate = row["gross_date"].ToString(),
                        GrossTime = row["gross_time"].ToString(),
                        EmbedUser = row["embed_user"].ToString(),
                        EmbedDate = row["embed_date"].ToString(),
                        EmbedTime = row["embed_time"].ToString(),
                        SlideTotalAmount = SaConverter.ToInt(row["slide_total_amount"].ToString(), 0),
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
            dt.Columns.Add("cassette_F1");
            dt.Columns.Add("cassette_F2");
            dt.Columns.Add("cassette_F3");
            dt.Columns.Add("cassette_F4");
            dt.Columns.Add("cassette_F5");
            dt.Columns.Add("cassette_F6");
            dt.Columns.Add("cassette_F7");
            dt.Columns.Add("cassette_F8");
            dt.Columns.Add("cassette_F9");
            dt.Columns.Add("cassette_F10");
            dt.Columns.Add("cassette_F11");
            dt.Columns.Add("cassette_F12");
            dt.Columns.Add("cassette_F13");
            dt.Columns.Add("cassette_F14");
            dt.Columns.Add("cassette_F15");
            dt.Columns.Add("cassette_F16");
            dt.Columns.Add("cassette_F17");
            dt.Columns.Add("cassette_F18");
            dt.Columns.Add("cassette_F19");
            dt.Columns.Add("cassette_F20");
            dt.Columns.Add("gross_user");
            dt.Columns.Add("gross_date");
            dt.Columns.Add("gross_time");
            dt.Columns.Add("embed_user");
            dt.Columns.Add("embed_date");
            dt.Columns.Add("embed_time");
            dt.Columns.Add("slide_total_amount");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");

            Array.ForEach(models, ocm => {
                DataRow row = dt.NewRow();
                row["id"] = ocm.Id;
                row["ord_no"] = ocm.OrdNo;
                row["cassette_sequence"] = ocm.CassetteSequence;
                row["cassette_F1"] = ocm.CassetteF1;
                row["cassette_F2"] = ocm.CassetteF2;
                row["cassette_F3"] = ocm.CassetteF3;
                row["cassette_F4"] = ocm.CassetteF4;
                row["cassette_F5"] = ocm.CassetteF5;
                row["cassette_F6"] = ocm.CassetteF6;
                row["cassette_F7"] = ocm.CassetteF7;
                row["cassette_F8"] = ocm.CassetteF8;
                row["cassette_F9"] = ocm.CassetteF9;
                row["cassette_F10"] = ocm.CassetteF10;
                row["cassette_F11"] = ocm.CassetteF11;
                row["cassette_F12"] = ocm.CassetteF12;
                row["cassette_F13"] = ocm.CassetteF13;
                row["cassette_F14"] = ocm.CassetteF14;
                row["cassette_F15"] = ocm.CassetteF15;
                row["cassette_F16"] = ocm.CassetteF16;
                row["cassette_F17"] = ocm.CassetteF17;
                row["cassette_F18"] = ocm.CassetteF18;
                row["cassette_F19"] = ocm.CassetteF19;
                row["cassette_F20"] = ocm.CassetteF20;

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
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
