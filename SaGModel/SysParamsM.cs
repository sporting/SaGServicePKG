using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class SysParamsM : ITableModel<SysParamsM>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seq { get; set; }
        public string Value { get; set; }

        public SysParamsM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SysParamsM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        Seq = SaConverter.ToInt(row["seq"], 0),
                        Name = row["name"].ToString(),
                        Value = row["value"].ToString()
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

        public DataTable GenerateDataTable(SysParamsM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("seq");
            dt.Columns.Add("name");
            dt.Columns.Add("value");

            Array.ForEach(models, spm => {
                DataRow row = dt.NewRow();
                row["id"] = spm.Id;
                row["seq"] = spm.Seq;
                row["name"] = spm.Name;
                row["value"] = spm.Value;
                dt.Rows.Add(row);
            });

            dt.AcceptChanges();

            return dt;
        }
    }
}
