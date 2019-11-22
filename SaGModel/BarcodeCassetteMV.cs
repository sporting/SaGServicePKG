using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{    
    public class BarcodeCassetteMV : ITableModel<BarcodeCassetteMV>
    {
        public string OrdNo { get; set; }
        public string BarCodeDate { get; set; }
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
        public string GrossUser { get; set; }
        public string GrossDate { get; set; }
        public string EmbedUser { get; set; }
        public string EmbedDate { get; set; }
        public int SlideTotalAmount { get; set; }

        public BarcodeCassetteMV()
        {
            Initialize();
        }
        public void Initialize()
        {
            OrdNo = string.Empty;
            BarCodeDate = string.Empty;
            CassetteSequence = 0;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            CassetteF1 = string.Empty;
            GrossUser = string.Empty;
            GrossDate = string.Empty;
            EmbedUser = string.Empty;
            EmbedDate = string.Empty;
            SlideTotalAmount = 0;
        }


        public DataTable GenerateDataTable(BarcodeCassetteMV[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ord_no");
            dt.Columns.Add("barcode_date");
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
            dt.Columns.Add("embed_user");
            dt.Columns.Add("embed_date");
            dt.Columns.Add("slide_total_amount");

            Array.ForEach(models, wl => {
                DataRow row = dt.NewRow();
                row["ord_no"] = wl.OrdNo;
                row["barcode_date"] = wl.BarCodeDate;
                row["cassette_sequence"] = wl.CassetteSequence;
                row["cassette_F1"] = wl.CassetteF1;
                row["cassette_F2"] = wl.CassetteF2;
                row["cassette_F3"] = wl.CassetteF3;
                row["cassette_F4"] = wl.CassetteF4;
                row["cassette_F5"] = wl.CassetteF5;
                row["cassette_F6"] = wl.CassetteF6;
                row["cassette_F7"] = wl.CassetteF7;
                row["cassette_F8"] = wl.CassetteF8;
                row["cassette_F9"] = wl.CassetteF9;
                row["cassette_F10"] = wl.CassetteF10;
                row["cassette_F11"] = wl.CassetteF11;
                row["cassette_F12"] = wl.CassetteF12;
                row["cassette_F13"] = wl.CassetteF13;
                row["cassette_F14"] = wl.CassetteF14;
                row["cassette_F15"] = wl.CassetteF15;
                row["cassette_F16"] = wl.CassetteF16;
                row["cassette_F17"] = wl.CassetteF17;
                row["cassette_F18"] = wl.CassetteF18;
                row["cassette_F19"] = wl.CassetteF19;
                row["cassette_F20"] = wl.CassetteF20;

                row["gross_user"] = wl.GrossUser;
                row["gross_date"] = wl.GrossDate;
                row["embed_user"] = wl.EmbedUser;
                row["embed_date"] = wl.EmbedDate;
                row["slide_total_amount"] = wl.SlideTotalAmount;
                dt.Rows.Add(row);
            });

            return dt;
        }

        public BarcodeCassetteMV[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new BarcodeCassetteMV()
                    {
                        OrdNo = row["ord_no"].ToString(),
                        BarCodeDate = row["barcode_date"].ToString(),
                        CassetteSequence = SaConverter.ToInt(row["cassette_sequence"].ToString(),0),
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
                        EmbedUser = row["embed_user"].ToString(),
                        EmbedDate = row["embed_date"].ToString(),
                        SlideTotalAmount = SaConverter.ToInt(row["slide_total_amount"].ToString(), 0)
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
    }
    
}
