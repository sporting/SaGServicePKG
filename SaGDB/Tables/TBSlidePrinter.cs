using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// slide_printer
    /// </summary>
    public sealed class TBSlidePrinter : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "slide_printer_tb";
            }
        }
        public TBSlidePrinter(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
