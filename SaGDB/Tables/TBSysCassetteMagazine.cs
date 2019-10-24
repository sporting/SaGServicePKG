using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{

    /// <summary>
    /// 包埋盒輸出卡匣
    /// </summary>
    public sealed class TBSysCassetteMagazine : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "sys_cassette_magazine_tb";
            }
        }
        public TBSysCassetteMagazine(MyDB db, string whereSql) : base(db, whereSql)
        {

        }

    }
}
