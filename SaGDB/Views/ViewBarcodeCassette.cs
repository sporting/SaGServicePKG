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

                string sql = $@"select a.ord_no,a.create_date as barcode_date,b.cassette_sequence,b.cassette_remark, b.cassette_fieldA, b.cassette_fieldB,b.cassette_doc_no, b.cassette_small_pieces,
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

                string sql = $@"select a.ord_no,a.create_date as barcode_date,b.cassette_sequence,b.cassette_remark, b.cassette_fieldA, b.cassette_fieldB,b.cassette_doc_no, b.cassette_small_pieces,
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
