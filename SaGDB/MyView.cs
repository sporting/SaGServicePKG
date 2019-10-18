using SaGDB.Utils;
using System;
using System.Data;
using System.Data.Common;

namespace SaGDB
{
    /// <summary>
    /// 適用於複合 Table Join 
    /// </summary>
    public abstract class MyView
    {
        public string SchemaName {
            get
            {
                return Utils.AppSettings.SCHEMA_NAME;
            }
        }
        public MyView()
        {
        }

        /*
         * public DataTable GetGrossView(string sql)
         * {
         *   string sql = $"select * from {Utils.AppSettings.SCHEMA_NAME}.xxxx where yyyy ";
         *   return GetTable(sql);
         * }
        */


        protected DataTable GetView(MyDB db,string whereSql)
        {
            db.OpenDB();
            DbDataAdapter adapter = db.CreateDataAdapter(whereSql);
            //view 不需要更新資料表
            //db.CreateCommandBuilder(adapter);

            DataTable table = new DataTable();
            adapter.FillSchema(table, SchemaType.Source);
           
            try
            {
                table.BeginLoadData();
                table.Clear();
                adapter.Fill(table);
                table.EndLoadData();
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
            }

            return table;         
        }
        
    }
}
