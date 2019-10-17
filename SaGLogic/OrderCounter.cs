using SaGDB;
using SaGDB.Tables;
using SaGUtil.Data;
using System;
using System.Data;

namespace SaGLogic
{
    public class OrderCounter
    {
        public string GetNextOrderCount(string head, string yyyy)
        {
            if (yyyy.Length != 4)
            {
                return string.Empty;
            }

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();

                IDbTransaction transaction = db.StartTransaction();
                TBOrderCounter tb = new TBOrderCounter(db,$"head='{head}' and break_key='{yyyy}' for update");

                int counter = 1;

                if (tb.Table.Rows.Count <= 0)
                {
                    DataRow row = tb.Table.NewRow();

                    row["head"] = head;
                    row["break_key"] = yyyy;
                    row["counter"] = counter.ToString();

                    tb.Table.Rows.Add(row);
                }
                else
                {
                    DataRow row = tb.Table.Rows[0];
                    counter = Convert.ToInt32(row["counter"].ToString());
                    counter += 1;

                    row["counter"] = counter;
                }

                string nextCounter = $"{head}{yyyy.Substring(2)}-{counter:D5}";

                if (tb.Update())
                {
                    if (db.Commit(transaction))
                        return nextCounter;                                    
                }

                return string.Empty;
            }
            finally
            {
                db.CloseDB();
            }           
        }

        public bool GetExist(string ordNo)
        {
            if (ordNo.Length < 9 || ordNo.Length > 10)
            {
                return false;
            }

            if (ordNo.IndexOf("-") < 0)
            {
                return false;
            }

            string head = ordNo.Substring(0, ordNo.IndexOf("-"));
            string headYear = head.Substring(head.Length - 2, 2);
            string headTag = head.Substring(0, head.Length - 2);
            int tailSeq = SaConverter.ToInt(ordNo.Substring(ordNo.IndexOf("-") + 1, 5), 0);

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();

                IDbTransaction transaction = db.StartTransaction();
                TBOrderCounter tb = new TBOrderCounter(db, $"head='{headTag}' and break_key like '%{headYear}' and counter>={tailSeq}");

                return tb.RowsCount > 0;
            }
            finally
            {
                db.CloseDB();
            }
        }
    }
}
