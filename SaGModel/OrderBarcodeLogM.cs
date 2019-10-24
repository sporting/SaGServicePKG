using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class OrderBarcodeLogM : ITableModel<OrderBarcodeLogM>
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int Amount { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }

        public bool RePrint { get; set; }


        public OrderBarcodeLogM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderBarcodeLogM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        OrdNo = row["ord_no"].ToString(),
                        Amount = SaConverter.ToInt(row["amount"].ToString(), 0),
                        RePrint = row["reprint"].ToString() == "Y",
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

        public DataTable GenerateDataTable(OrderBarcodeLogM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ord_no");
            dt.Columns.Add("amount");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");
            dt.Columns.Add("reprint");

            Array.ForEach(models, oblm => {
                DataRow row = dt.NewRow();
                row["id"] = oblm.Id;
                row["ord_no"] = oblm.OrdNo;
                row["amount"] = oblm.Amount;
                row["op_date"] = oblm.OpDate;
                row["op_time"] = oblm.OpTime;
                row["reprint"] = oblm.RePrint ? "Y" : "N";
                dt.Rows.Add(row);
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
