using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class OrderEmbedLogM : ITableModel<OrderEmbedLogM>
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int CassetteSequence { get; set; }
        public string EmbedUser { get; set; }
        public string EmbedDate { get; set; }
        public string EmbedTime { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }
        public DeleteFlagEnum IsDeleteFlag { get; set; }
        public OrderEmbedLogM()
        {
            Initialize();
        }
        public void Initialize()
        {
            Id = 0;
            OrdNo = string.Empty;
            CassetteSequence = 0;
            EmbedUser = string.Empty;
            EmbedDate = string.Empty;
            EmbedTime = string.Empty;
            OpDate = string.Empty;
            OpTime = string.Empty;
            IsDeleteFlag = DeleteFlagEnum.Normal;
        }

        public OrderEmbedLogM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderEmbedLogM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = SaConverter.ToInt(row["cassette_sequence"].ToString(), 0),
                        EmbedUser = row["embed_user"].ToString(),
                        EmbedDate = row["embed_date"].ToString(),
                        EmbedTime = row["embed_time"].ToString(),
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

        public DataTable GenerateDataTable(OrderEmbedLogM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ord_no");
            dt.Columns.Add("cassette_sequence");
            dt.Columns.Add("embed_user");
            dt.Columns.Add("embed_date");
            dt.Columns.Add("embed_time");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");
            dt.Columns.Add("is_delete_flag");

            Array.ForEach(models, oelm => {
                DataRow row = dt.NewRow();
                row["id"] = oelm.Id;
                row["ord_no"] = oelm.OrdNo;
                row["cassette_sequence"] = oelm.CassetteSequence;
                row["embed_user"] = oelm.EmbedUser;
                row["embed_date"] = oelm.EmbedDate;
                row["embed_time"] = oelm.EmbedTime;
                row["op_date"] = oelm.OpDate;
                row["op_time"] = oelm.OpTime;
                row["is_delete_flag"] = oelm.IsDeleteFlag == DeleteFlagEnum.Normal ? "N" : "D";
                dt.Rows.Add(row);
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
