using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// 包埋處理人員 Log
    /// </summary>
    public sealed class TBOrderEmbedLog : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "order_embed_log_tb";
            }
        }
        public TBOrderEmbedLog(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
