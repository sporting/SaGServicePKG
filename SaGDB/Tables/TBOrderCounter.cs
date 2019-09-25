using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// 檢體編號取號
    /// </summary>
    public sealed class TBOrderCounter:MyTable
    {

        protected override string TableName
        {
            get
            {
                return "order_counter_tb";
            }
        }
        public TBOrderCounter(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
