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
        public string CassetteRemark { get; set; }
        public string CassetteFieldA { get; set; }
        public string CassetteFieldB { get; set; }

        public string CassetteSmallPiece { get; set; }
        public string CassetteDocNo { get; set; }

        public string GrossUser { get; set; }
        public string GrossDate { get; set; }
        public string GrossTime { get; set; }
        public string EmbedUser { get; set; }
        public string EmbedDate { get; set; }
        public string EmbedTime { get; set; }
        public int SlideTotalAmount { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }

        public OrderCassetteM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderCassetteM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = SaConverter.ToInt(row["cassette_sequence"].ToString(), 0),
                        CassetteRemark = row["cassette_remark"].ToString(),
                        CassetteFieldA = row["cassette_fieldA"].ToString(),
                        CassetteFieldB = row["cassette_fieldB"].ToString(),

                        CassetteDocNo = row["cassette_doc_no"].ToString(),
                        CassetteSmallPiece = row["cassette_small_pieces"].ToString(),

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
            dt.Columns.Add("cassette_remark");
            dt.Columns.Add("cassette_fieldA");
            dt.Columns.Add("cassette_fieldB");
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
                row["cassette_remark"] = ocm.CassetteRemark;
                row["cassette_fieldA"] = ocm.CassetteFieldA;
                row["cassette_fieldB"] = ocm.CassetteFieldB;
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
