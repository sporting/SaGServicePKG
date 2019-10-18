using SaGDB.Utils;
using SaGUtil.System;
using System;
using System.Data;
using System.Data.Common;

namespace SaGDB
{
    public class DynamicTable
    {
        private MyDB _db;
        DbDataAdapter _adapter;

        public DynamicTable(MyDB db)
        {
            _db = db;
        }


        public DataTable GetView(string whereSql)
        {
            _db.OpenDB();
            _adapter = _db.CreateDataAdapter(whereSql);

            DataTable table = new DataTable();
            _adapter.FillSchema(table, SchemaType.Source);

            try
            {
                table.BeginLoadData();
                table.Clear();
                _adapter.Fill(table);
                table.EndLoadData();
            }
            catch
            {

            }

            return table;
        }

        public int ExecuteNonQuery(string sql)
        {
            try
            {
                _db.OpenDB();
                DbCommand cmd = _db.CreateCommand();
                cmd.CommandText = sql;
                
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{ex.Message}: {sql}");
                return 0;
            }
        }

        public object ExecuteScalar(string sql)
        {
            try
            {
                _db.OpenDB();
                DbCommand cmd = _db.CreateCommand();
                cmd.CommandText = sql;

                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, $"{ex.Message}: {sql}");
                return null;
            }
        }
    }
}
