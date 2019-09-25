using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// SYSTEM LOG
    /// </summary>
    public sealed class TBSysLog : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "sys_log_tb";
            }
        }
        public TBSysLog(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
