
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// cassette_workstation
    /// </summary>
    public sealed class TBCassetteWorkStation : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "cassette_workstation_tb";
            }
        }
        public TBCassetteWorkStation(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
