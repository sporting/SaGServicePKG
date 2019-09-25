using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{

    /// <summary>
    /// SYSPARAMS
    /// </summary>
    public sealed class TBSysParams : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "sysparams_tb";
            }
        }
        public TBSysParams(MyDB db, string whereSql) : base(db, whereSql)
        {

        }

    }
}
