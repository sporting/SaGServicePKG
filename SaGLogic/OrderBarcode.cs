﻿
using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil;
using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGLogic
{

    //列印 Barcode list
    public class OrderBarcode : ITableModel<OrderBarcodeM>
    {
        //列印 barcode 時寫入
        public TBOrderBarcode Update(MyDB db, OrderBarcodeLogM ordLog)
        {
            TBOrderBarcode tb = new TBOrderBarcode(db, $"ord_no='{ordLog.OrdNo}' for update");

            if (tb.RowsCount <= 0)
            {
                DataRow row = tb.Table.NewRow();
                row["ord_no"] = ordLog.OrdNo;
                row["barcode_total_amount"] = ordLog.Amount;
                row["cassette_total_amount"] = 0;
                row["create_date"] = Utility.Today().ToString("yyyyMMdd");
                row["create_time"] = Utility.Today().ToString("HHmmss");
                row["op_date"] = row["create_date"];
                row["op_time"] = row["create_time"];
                tb.Table.Rows.Add(row);

                return tb;
            }
            else
            {
                DataRow row = tb.Table.Rows[0];
                row["barcode_total_amount"] = ordLog.Amount + Converter.ToInt(row["barcode_total_amount"], 0);
                row["op_date"] = Utility.Today().ToString("yyyyMMdd");
                row["op_time"] = Utility.Today().ToString("HHmmss");

                return tb;
            }
        }

        //列印 cassette 時更新
        public TBOrderBarcode Update(MyDB db, OrderCassetteM ordLog, out int newCassetteSeq)
        {
            TBOrderBarcode tb = new TBOrderBarcode(db, $"ord_no='{ordLog.OrdNo}' for update");

            if (tb.RowsCount <= 0)
            {
                DataRow row = tb.Table.NewRow();
                row["ord_no"] = ordLog.OrdNo;
                row["barcode_total_amount"] = 0;
                row["cassette_total_amount"] = 1;
                row["create_date"] = Utility.Today().ToString("yyyyMMdd");
                row["create_time"] = Utility.Today().ToString("HHmmss");
                row["op_date"] = row["create_date"];
                row["op_time"] = row["create_time"];
                tb.Table.Rows.Add(row);

                newCassetteSeq = Converter.ToInt(row["cassette_total_amount"], 1);

                return tb;
            }
            else
            {
                DataRow row = tb.Table.Rows[0];
                row["cassette_total_amount"] = 1 + Converter.ToInt(row["cassette_total_amount"], 0);

                newCassetteSeq = Converter.ToInt(row["cassette_total_amount"], 1);

                return tb;
            }
        }


        //有沒有該檢體編號
        public bool OrdNoExist(string ordNo)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                MyDB db = new MyDB();
                try
                {
                    db.OpenDB();

                    TBOrderBarcode tb = new TBOrderBarcode(db, $"ord_no='{ordNo}'");
                    return tb.RowsCount > 0;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    db.CloseDB();
                }
            }

            return false;
        }


        public OrderBarcodeM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new OrderBarcodeM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        OrdNo = row["ord_no"].ToString(),
                        BarcodeTotalAmount = Convert.ToInt32(row["barcode_total_amount"].ToString()),
                        CassetteTotalAmount = Convert.ToInt32(row["cassette_total_amount"].ToString()),
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

            foreach (OrderBarcodeM obm in models)
            {
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
            }

            return dt;
        }
    }
}
