
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// Cassette list (主檔二)
    /// </summary>
    public sealed class TBOrderCassette : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "order_cassette_tb";
            }
        }
        public TBOrderCassette(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
