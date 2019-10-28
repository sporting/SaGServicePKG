using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Views
{
    public class ViewSlideWorkLoad : MyView
    {
        public ViewSlideWorkLoad()
        {

        }

        public DataTable Get(string begDate, string endDate, string slideUser)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();

                string sql = string.Empty;
                if (string.IsNullOrEmpty(slideUser))
                {
                    sql = $"SELECT op_date date,slide_user as user,slide_fieldA,slide_fieldB,count(*) total FROM {SchemaName}.order_slide_tb WHERE op_date>='{begDate}' and op_date<='{endDate}' group by op_date,slide_user,slide_fieldA,slide_fieldB";
                }
                else
                {
                    sql = $"SELECT op_date date,slide_user as user,slide_fieldA,slide_fieldB,count(*) total FROM {SchemaName}.order_slide_tb WHERE op_date>='{begDate}' and op_date<='{endDate}' and slide_user='{slideUser}' group by op_date,slide_user,slide_fieldA,slide_fieldB";
                }

                return GetView(db,sql);
            }
            finally
            {
                db.CloseDB();
            }
        }
    }
}
