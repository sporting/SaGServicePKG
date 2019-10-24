using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Tables
{

    /// <summary>
    /// SysSpecimenStain
    /// </summary>
    public sealed class TBSysSpecimenStain : MyTable
    {

        protected override string TableName
        {
            get
            {
                return "sys_specimen_stain_tb";
            }
        }
        public TBSysSpecimenStain(MyDB db, string whereSql) : base(db, whereSql)
        {

        }

    }
}
