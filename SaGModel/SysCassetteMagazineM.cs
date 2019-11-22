using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class SysCassetteMagazineM : ITableModel<SysCassetteMagazineM>
    {
        public int Id { get; set; }
        //MajorClass
        public string MajorClass { get; set; }
        //包埋盒卡匣名稱
        public string Magazine { get; set; }
        public SysCassetteMagazineM()
        {
            Initialize();
        }
        public void Initialize()
        {
            Id = 0;
            MajorClass = string.Empty;
            Magazine = string.Empty;
        }
        public SysCassetteMagazineM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SysCassetteMagazineM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        MajorClass = row["major_class"].ToString(),
                        Magazine = row["magazine"].ToString()
                    };

            if (v != null)
            {
                return v.ToArray();
            }
            else
            {
                return null;
            }
        }

        public DataTable GenerateDataTable(SysCassetteMagazineM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("major_class");
            dt.Columns.Add("magazine");

            Array.ForEach(models, smm =>
            {
                DataRow row = dt.NewRow();
                row["id"] = smm.Id;
                row["major_class"] = smm.MajorClass;
                row["magazine"] = smm.Magazine;
                dt.Rows.Add(row);
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
