using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class SysUserM : ITableModel<SysUserM>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public SysUserM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SysUserM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        UserId = row["user_id"].ToString(),
                        Name = row["name"].ToString()
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

        public DataTable GenerateDataTable(SysUserM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("user_id");
            dt.Columns.Add("name");

            Array.ForEach(models, sysum => {
                DataRow row = dt.NewRow();
                row["id"] = sysum.Id;
                row["user_id"] = sysum.UserId;
                row["name"] = sysum.Name;
                dt.Rows.Add(row);
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
