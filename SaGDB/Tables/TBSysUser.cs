using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{

    /// <summary>
    /// SYSUSER
    /// </summary>
    public sealed class TBSysUser : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "sysuser_tb";
            }
        }
        public TBSysUser(MyDB db, string whereSql) : base(db, whereSql)
        {

        }

    }
}
