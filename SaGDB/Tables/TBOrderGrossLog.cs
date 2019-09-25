using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// Gross 處理人員 Log
    /// </summary>
    public sealed class TBOrderGrossLog : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "order_gross_log_tb";
            }
        }
        public TBOrderGrossLog(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
