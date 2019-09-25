
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// Barcode list (主檔一)
    /// </summary>
    public sealed class TBOrderBarcode: MyTable
    {

        protected override string TableName
        {
            get
            {
                return "order_barcode_tb";
            }
        }
        public TBOrderBarcode(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
