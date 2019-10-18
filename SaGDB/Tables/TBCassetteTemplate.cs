using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// cassette_template : cassette printer template 樣版對照檔
    /// </summary>
    public sealed class TBCassetteTemplate : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "cassette_template_tb";
            }
        }
        public TBCassetteTemplate(MyDB db, string whereSql) : base(db, whereSql)
        {

        }

    }
}
