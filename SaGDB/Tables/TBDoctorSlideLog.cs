
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{
    /// <summary>
    /// 病理醫師分玻片 Log
    /// </summary>
    public sealed class TBDoctorSlideLog : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "doctor_slide_log_tb";
            }
        }
        public TBDoctorSlideLog(MyDB db,string whereSql):base(db,whereSql)
        {
         
        }

    }
}
