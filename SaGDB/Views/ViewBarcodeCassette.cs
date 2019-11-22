using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Views
{
    public class ViewBarcodeCassette : MyView
    {
        public ViewBarcodeCassette()
        {

        }

        public DataTable Get(string begDate, string endDate)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();

                string sql = $@"select a.ord_no,a.create_date as barcode_date,b.cassette_sequence,b.cassette_F1, b.cassette_F2, 
b.cassette_F3,b.cassette_F4, b.cassette_F5, b.cassette_F6, b.cassette_F7, b.cassette_F8, b.cassette_F9, b.cassette_F10, b.cassette_F11,
b.cassette_F12, b.cassette_F12, b.cassette_F13, b.cassette_F14, b.cassette_F15, b.cassette_F16, b.cassette_F17, b.cassette_F18, 
b.cassette_F19, b.cassette_F20,
b.gross_user,b.gross_date,b.embed_user,b.embed_date,
(case when b.slide_total_amount is null then 0 else b.slide_total_amount end) slide_total_amount
from {SchemaName}.order_barcode_tb a
left outer join {SchemaName}.order_cassette_tb b
on a.ord_no = b.ord_no
where a.create_date >= '{begDate}' and a.create_date<='{endDate}'";

                return GetView(db, sql);
            }
            finally
            {
                db.CloseDB();
            }
        }
        public DataTable Get(string ordNo)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();

                string sql = $@"select a.ord_no,a.create_date as barcode_date,b.cassette_sequence,b.cassette_F1, b.cassette_F2, 
b.cassette_F3,b.cassette_F4, b.cassette_F5, b.cassette_F6, b.cassette_F7, b.cassette_F8, b.cassette_F9, b.cassette_F10, b.cassette_F11,
b.cassette_F12, b.cassette_F12, b.cassette_F13, b.cassette_F14, b.cassette_F15, b.cassette_F16, b.cassette_F17, b.cassette_F18, 
b.cassette_F19, b.cassette_F20,
b.gross_user,b.gross_date,b.embed_user,b.embed_date,
(case when b.slide_total_amount is null then 0 else b.slide_total_amount end) slide_total_amount
from {SchemaName}.order_barcode_tb a
left outer join {SchemaName}.order_cassette_tb b
on a.ord_no = b.ord_no
where a.ord_no='{ordNo}'";

                return GetView(db, sql);
            }
            finally
            {
                db.CloseDB();
            }
        }
    }
}
