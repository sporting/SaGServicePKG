using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class SysSpecimenStainM : ITableModel<SysSpecimenStainM>
    {
        public int Id { get; set; }
        //檢體別
        public string Specimen { get; set; }
        public int Seq { get; set; }
        //染色方法
        public string Stain { get; set; }
        //停用註記 Y/N
        public string StopFlag { get; set; }
        public SysSpecimenStainM[] GenerateModel(DataTable dt)
        {
            var v = from DataRow row in dt.AsEnumerable()
                    select new SysSpecimenStainM()
                    {
                        Id = SaConverter.ToInt(row["id"], 0),
                        Specimen = row["specimen"].ToString(),
                        Seq = SaConverter.ToInt(row["seq"], 0),
                        Stain = row["stain"].ToString(),
                        StopFlag = row["stop_flag"].ToString()
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

        public DataTable GenerateDataTable(SysSpecimenStainM[] models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("specimen");
            dt.Columns.Add("seq");
            dt.Columns.Add("stain");
            dt.Columns.Add("stop_flag");

            Array.ForEach(models, ssm =>
            {
                DataRow row = dt.NewRow();
                row["id"] = ssm.Id;
                row["specimen"] = ssm.Specimen;
                row["seq"] = ssm.Seq;
                row["stain"] = ssm.Stain;
                row["stop_flag"] = ssm.StopFlag;
                dt.Rows.Add(row);
            });
            dt.AcceptChanges();
            return dt;
        }
    }
}
