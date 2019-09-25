using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// 玻片 list (主檔三)
    /// </summary>
    public sealed class TBOrderSlide : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "order_slide_tb";
            }
        }
        public TBOrderSlide(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
