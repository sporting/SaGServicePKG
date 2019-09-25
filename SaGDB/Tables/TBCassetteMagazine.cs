using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// cassette_magazine
    /// </summary>
    public sealed class TBCassetteMagazine : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "cassette_magazine_tb";
            }
        }
        public TBCassetteMagazine(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
