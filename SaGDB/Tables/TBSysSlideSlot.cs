using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{

    /// <summary>
    /// 玻片輸出卡匣
    /// </summary>
    public sealed class TBSysSlideSlot : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "sys_slide_slot_tb";
            }
        }
        public TBSysSlideSlot(MyDB db, string whereSql) : base(db, whereSql)
        {

        }

    }
}
