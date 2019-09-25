using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil;
using System;
using System.Data;
using System.Linq;

namespace SaGLogic
{

    //列印 Barcode log
    public class OrderBarcodeLog : ITableModel<OrderBarcodeLogM>
    {
        public bool Add(OrderBarcodeLogM log)
        {
            if (!string.IsNullOrEmpty(log.OrdNo))
            {
                log.OpDate = Utility.Today().ToString("yyyyMMdd");
                log.OpTime = Utility.Today().ToString("HHmmss");

                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    IDbTransaction transaction = db.StartTransaction();
                    TBOrderBarcodeLog tb = new TBOrderBarcodeLog(db, "1=0");

                    DataRow row = tb.Table.NewRow();
                    row["ord_no"] = log.OrdNo;
                    row["amount"] = log.Amount;
                    row["op_date"] = log.OpDate;
                    row["op_time"] = log.OpTime;
                    row["reprint"] = log.RePrint ? "Y" : "N";
                    tb.Table.Rows.Add(row);

                    OrderBarcode order = new OrderBarcode();
                    TBOrderBarcode tbOrder = order.Update(db, log);

                    if (tb.Update() && tbOrder.Update())
                    {
                        if (db.Commit(transaction))
                            return true; ;
                    }

                    return false;
                }
                finally
                {
                    db.CloseDB();
                }
            }
            return true;
        }


        public OrderBarcodeLogM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderBarcodeLogM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        OrdNo = row["ord_no"].ToString(),
                        Amount = Convert.ToInt32(row["amount"].ToString()),
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

            foreach (OrderBarcodeLogM oblm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = oblm.Id;
                row["ord_no"] = oblm.OrdNo;
                row["amount"] = oblm.Amount;
                row["op_date"] = oblm.OpDate;
                row["op_time"] = oblm.OpTime;
                row["reprint"] = oblm.RePrint ? "Y" : "N";
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
