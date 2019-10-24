using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class SysSlideSlotM : ITableModel<SysSlideSlotM>
    {
        public int Id { get; set; }
        //MajorClass
        public string MajorClass { get; set; }
        //玻片卡匣序 0,1
        public int Slot { get; set; }

        public SysSlideSlotM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SysSlideSlotM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        MajorClass = row["major_class"].ToString(),
                        Slot = SaConverter.ToInt(row["slot"], 0)
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

        public DataTable GenerateDataTable(SysSlideSlotM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("major_class");
            dt.Columns.Add("slot");

            Array.ForEach(models, ssm =>
            {
                DataRow row = dt.NewRow();
                row["id"] = ssm.Id;
                row["major_class"] = ssm.MajorClass;
                row["slot"] = SaConverter.ToInt(ssm.Slot, 0);
                dt.Rows.Add(row);
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
