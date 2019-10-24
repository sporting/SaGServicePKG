using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class SysLogM : ITableModel<SysLogM>
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Params { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }
        public SysLogM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SysLogM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        EventName = row["event_name"].ToString(),
                        Params = row["params"].ToString(),
                        OpDate = row["op_date"].ToString(),
                        OpTime = row["op_time"].ToString()
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

        public DataTable GenerateDataTable(SysLogM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("event_name");
            dt.Columns.Add("params");
            dt.Columns.Add("op_date");
            dt.Columns.Add("op_time");

            Array.ForEach(models, slm => {
                DataRow row = dt.NewRow();
                row["id"] = slm.Id;
                row["event_name"] = slm.EventName;
                row["params"] = slm.Params;
                row["op_date"] = slm.OpDate;
                row["op_time"] = slm.OpTime;
                dt.Rows.Add(row);
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
