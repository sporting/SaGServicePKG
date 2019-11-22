using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class OrderGrossLogM : ITableModel<OrderGrossLogM>
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int CassetteSequence { get; set; }
        public string GrossUser { get; set; }
        public string GrossDate { get; set; }
        public string GrossTime { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }
        public DeleteFlagEnum IsDeleteFlag { get; set; }
        public OrderGrossLogM()
        {
            Initialize();
        }
        public void Initialize()
        {
            Id = 0;
            OrdNo = string.Empty;
            CassetteSequence = 0;
            GrossUser = string.Empty;
            GrossDate = string.Empty;
            GrossTime = string.Empty;
            OpDate = string.Empty;
            OpTime = string.Empty;
            IsDeleteFlag = DeleteFlagEnum.Normal;
        }
        public OrderGrossLogM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderGrossLogM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        OrdNo = row["ord_no"].ToString(),
                        CassetteSequence = SaConverter.ToInt(row["cassette_sequence"].ToString(), 0),
                        GrossUser = row["gross_user"].ToString(),
                        GrossDate = row["gross_date"].ToString(),
                        GrossTime = row["gross_time"].ToString(),
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

        public DataTable GenerateDataTable(OrderGrossLogM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ord_no");
            dt.Columns.Add("cassette_sequence");
            dt.Columns.Add("gross_user");
            dt.Columns.Add("gross_date");
            dt.Columns.Add("gross_time");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");
            dt.Columns.Add("is_delete_flag");

            Array.ForEach(models, oglm => {
                DataRow row = dt.NewRow();
                row["id"] = oglm.Id;
                row["ord_no"] = oglm.OrdNo;
                row["cassette_sequence"] = oglm.CassetteSequence;
                row["gross_user"] = oglm.GrossUser;
                row["gross_date"] = oglm.GrossDate;
                row["gross_time"] = oglm.GrossTime;
                row["op_date"] = oglm.OpDate;
                row["op_time"] = oglm.OpTime;
                row["is_delete_flag"] = oglm.IsDeleteFlag == DeleteFlagEnum.Normal ? "N" : "D";
                dt.Rows.Add(row);
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
