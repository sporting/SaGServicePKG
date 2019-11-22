using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class OrderBarcodeM : ITableModel<OrderBarcodeM>
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int BarcodeTotalAmount { get; set; }
        public int CassetteTotalAmount { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }

        public string OpDate { get; set; }
        public string OpTime { get; set; }
        public OrderBarcodeM()
        {
            Initialize();
        }
        public void Initialize()
        {
            Id = 0;
            OrdNo = string.Empty;
            BarcodeTotalAmount = 0;
            CassetteTotalAmount = 0;
            CreateDate = string.Empty;
            CreateTime = string.Empty;
            OpDate = string.Empty;
            OpTime = string.Empty;
        }

        public OrderBarcodeM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderBarcodeM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        OrdNo = row["ord_no"].ToString(),
                        BarcodeTotalAmount = SaConverter.ToInt(row["barcode_total_amount"].ToString(), 0),
                        CassetteTotalAmount = SaConverter.ToInt(row["cassette_total_amount"].ToString(), 0),
                        CreateDate = row["create_date"].ToString(),
                        CreateTime = row["create_time"].ToString(),
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

        public DataTable GenerateDataTable(OrderBarcodeM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ord_no");
            dt.Columns.Add("barcode_total_amount");
            dt.Columns.Add("cassette_total_amount");
            dt.Columns.Add("create_date");
            dt.Columns.Add("create_time");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");

            Array.ForEach(models, obm => {
                DataRow row = dt.NewRow();
                row["id"] = obm.Id;
                row["ord_no"] = obm.OrdNo;
                row["barcode_total_amount"] = obm.BarcodeTotalAmount;
                row["cassette_total_amount"] = obm.CassetteTotalAmount;
                row["create_date"] = obm.CreateDate;
                row["create_time"] = obm.CreateTime;
                row["op_date"] = obm.OpDate;
                row["op_time"] = obm.OpTime;
                dt.Rows.Add(row);
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
