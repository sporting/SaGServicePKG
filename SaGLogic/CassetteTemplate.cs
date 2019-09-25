﻿using SaGDB;
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
    public class CassetteTemplate : ITableModel<CassetteTemplateM>
    {
        public CassetteTemplateM[] GetValues(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new CassetteTemplateM[] { };
            }

            MyDB db = new MyDB();
            try
            {
                db.OpenDB();
                TBCassetteTemplate tb = new TBCassetteTemplate(db, $"name='{name}'");

                var query = from row in tb.Table.AsEnumerable()
                            select new CassetteTemplateM()
                            {
                                Id = Converter.ToInt(row["id"].ToString(), 0),
                                Name = row["name"].ToString(),
                                Template = row["template"].ToString()
                            };

                if (query.Count() > 0)
                {
                    return query.ToArray();
                }
                else
                {
                    return new CassetteTemplateM[] { };
                }
            }
            finally
            {
                db.CloseDB();
            }
        }

        public CassetteTemplateM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new CassetteTemplateM()
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

        public DataTable GenerateDataTable(CassetteTemplateM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("template");

            foreach (CassetteTemplateM ctm in models)
            {
                DataRow row = dt.NewRow();
                row["id"] = ctm.Id;
                row["name"] = ctm.Name;
                row["template"] = ctm.Template;
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
