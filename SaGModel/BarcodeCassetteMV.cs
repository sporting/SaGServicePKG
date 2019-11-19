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
        public string CassetteRemark { get; set; }
        public string CassetteFieldA { get; set; }
        public string CassetteFieldB { get; set; }
        public string CassetteDocNo { get; set; }
        public int CassetteSmallPieces { get; set; }
        public string GrossUser { get; set; }
        public string GrossDate { get; set; }
        public string EmbedUser { get; set; }
        public string EmbedDate { get; set; }
        public int SlideTotalAmount { get; set; }


        public DataTable GenerateDataTable(BarcodeCassetteMV[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ord_no");
            dt.Columns.Add("barcode_date");
            dt.Columns.Add("cassette_sequence");
            dt.Columns.Add("cassette_remark");
            dt.Columns.Add("cassette_fieldA");
            dt.Columns.Add("cassette_fieldB");
            dt.Columns.Add("cassette_doc_no");
            dt.Columns.Add("cassette_small_pieces");
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
                row["cassette_remark"] = wl.CassetteRemark;
                row["cassette_fieldA"] = wl.CassetteFieldA;
                row["cassette_fieldB"] = wl.CassetteFieldB;
                row["cassette_doc_no"] = wl.CassetteDocNo;
                row["cassette_small_pieces"] = wl.CassetteSmallPieces;
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
                        CassetteRemark = row["cassette_remark"].ToString(),
                        CassetteFieldA = row["cassette_fieldA"].ToString(),
                        CassetteFieldB = row["cassette_fieldB"].ToString(),
                        CassetteDocNo = row["cassette_doc_no"].ToString(),
                        CassetteSmallPieces = SaConverter.ToInt(row["cassette_small_pieces"].ToString(), 0),
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
