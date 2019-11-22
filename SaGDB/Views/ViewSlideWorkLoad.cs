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
                    sql = $@"SELECT op_date date,slide_user as user,slide_F1,slide_F2,slide_F3,slide_F4,slide_F5,slide_F6,slide_F7,slide_F8,
                    slide_F9,slide_F10,slide_F11,slide_F12,slide_F13,slide_F14,slide_F15,slide_F16,slide_F17,
                    slide_F18,slide_F19,slide_F20,count(*) total FROM {SchemaName}.order_slide_tb WHERE op_date>='{begDate}' and op_date<='{endDate}' 
                    group by op_date,slide_user,slide_F1,slide_F2,slide_F3,slide_F4,slide_F5,slide_F6,slide_F7,slide_F8,
                    slide_F9,slide_F10,slide_F11,slide_F12,slide_F13,slide_F14,slide_F15,slide_F16,slide_F17,
                    slide_F18,slide_F19,slide_F20";
                }
                else
                {
                    sql = $@"SELECT op_date date,slide_user as user,slide_F1,slide_F2,slide_F3,slide_F4,slide_F5,slide_F6,slide_F7,slide_F8,
                    slide_F9,slide_F10,slide_F11,slide_F12,slide_F13,slide_F14,slide_F15,slide_F16,slide_F17,
                    slide_F18,slide_F19,slide_F20,count(*) total FROM {SchemaName}.order_slide_tb WHERE op_date>='{begDate}' and op_date<='{endDate}' and slide_user='{slideUser}' 
                    group by op_date,slide_user,slide_F1,slide_F2,slide_F3,slide_F4,slide_F5,slide_F6,slide_F7,slide_F8,
                    slide_F9,slide_F10,slide_F11,slide_F12,slide_F13,slide_F14,slide_F15,slide_F16,slide_F17,
                    slide_F18,slide_F19,slide_F20";
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
