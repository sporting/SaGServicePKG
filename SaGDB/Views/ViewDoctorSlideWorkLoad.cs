using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGDB.Views
{
    public class ViewDoctorSlideWorkLoad : MyView
    {
        public ViewDoctorSlideWorkLoad()
        {

        }

        public DataTable Get(string begDate, string endDate, string doctorUser)
        {
            MyDB db = new MyDB();
            try
            {
                db.OpenDB();

                string sql = string.Empty;
                if (string.IsNullOrEmpty(doctorUser))
                {
                    sql = $"SELECT doctor_date date,doctor_user user,slide_fieldA,slide_fieldB,count(*) total FROM {SchemaName}.order_slide_tb WHERE doctor_date>='{begDate}' and doctor_date<='{endDate}' group by doctor_date,doctor_user,slide_fieldA,slide_fieldB";
                }
                else
                {
                    sql = $"SELECT doctor_date date,doctor_user user,slide_fieldA,slide_fieldB,count(*) total FROM {SchemaName}.order_slide_tb WHERE doctor_date>='{begDate}' and doctor_date<='{endDate}' and doctor_user='{doctorUser}' group by doctor_date,doctor_user,slide_fieldA,slide_fieldB";
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
