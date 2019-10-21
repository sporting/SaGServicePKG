
using SaGDB;
using SaGDB.Tables;
using SaGModel;
using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGLogic
{
    /// <summary>
    /// 作為前端應用與資料庫物件的中介層 
    /// 對應 SaGDB.slide_template_tb Table
    /// </summary>
    public class SlideTemplate : ITableModel<SlideTemplateM>
    {
        public SlideTemplateM[] GetValues(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new SlideTemplateM[] { };
            }

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBSlideTemplate tb = new TBSlideTemplate(db, $"name='{name}'");

                var query = from row in tb.Table.AsEnumerable()
                            select new SlideTemplateM()
                            {
                                Id = SaConverter.ToInt(row["id"].ToString(), 0),
                                Name = row["name"].ToString(),
                                Template = row["template"].ToString()
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new SlideTemplateM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        public SlideTemplateM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SlideTemplateM()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Name = row["name"].ToString(),
                        Template = row["template"].ToString()
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

        public DataTable GenerateDataTable(SlideTemplateM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("template");

            Array.ForEach(models, ctm => {
                DataRow row = dt.NewRow();
                row["id"] = ctm.Id;
                row["name"] = ctm.Name;
                row["template"] = ctm.Template;
                dt.Rows.Add(row);
            });

            return dt;
        }
    }
}
