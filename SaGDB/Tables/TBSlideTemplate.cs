using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// slide_template : slide printer template 樣版對照檔
    /// </summary>
    public sealed class TBSlideTemplate : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "slide_template_tb";
            }
        }
        public TBSlideTemplate(MyDB db, string whereSql) : base(db, whereSql)
        {

        }

    }
}
