using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// slide : slide 工作站對照檔
    /// </summary>
    public sealed class TBSlideWorkStation : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "slide_workstation_tb";
            }
        }
        public TBSlideWorkStation(MyDB db, string whereSql) : base(db, whereSql)
        {

        }
    }
}
