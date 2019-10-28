using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Views
{
    public class ViewGrossWorkLoad : MyView
    {
        public ViewGrossWorkLoad()
        {

        }

        public DataTable Get(string begDate, string endDate, string grossUser)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();

                string sql = string.Empty;
                if (string.IsNullOrEmpty(grossUser))
                {
                    sql = $"SELECT gross_date date,gross_user as user,count(*) total FROM {SchemaName}.order_cassette_tb WHERE gross_date>='{begDate}' and gross_date<='{endDate}' group by gross_date,gross_user";
                }
                else
                {
                    sql = $"SELECT gross_date date,gross_user as user,count(*) total FROM {SchemaName}.order_cassette_tb WHERE gross_date>='{begDate}' and gross_date<='{endDate}' and gross_user='{grossUser}' group by gross_date,gross_user";
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
