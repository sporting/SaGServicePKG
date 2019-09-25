using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Views
{
    public class ViewEmbedWorkLoad : MyView
    {
        public ViewEmbedWorkLoad()
        {

        }

        public DataTable Get(string begDate, string endDate, string embedUser)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();

                string sql = string.Empty;
                if (string.IsNullOrEmpty(embedUser))
                {
                    sql = $"SELECT embed_date date,embed_user user,count(*) total FROM {SchemaName}.order_cassette_tb WHERE embed_date>='{begDate}' and embed_date<='{endDate}' group by embed_date,embed_user";
                }
                else
                {
                    sql = $"SELECT embed_date date,embed_user user,count(*) total FROM {SchemaName}.order_cassette_tb WHERE embed_date>='{begDate}' and embed_date<='{endDate}' and embed_user='{embedUser}' group by embed_date,embed_user";
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
