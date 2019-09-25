
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// 檢體編號列印Log
    /// </summary>
    public sealed class TBOrderBarcodeLog: MyTable
    {

        protected override string TableName
        {
            get
            {
                return "order_barcode_log_tb";
            }
        }
        public TBOrderBarcodeLog(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
