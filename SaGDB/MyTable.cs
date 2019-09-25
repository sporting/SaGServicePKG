using SaGUtil.System;
using System;
using System.Data;
using System.Data.Common;

namespace SaGDB
{
    /// <summary>
    /// 適合單一 Table 更新資料用
    /// </summary>
    public abstract class MyTable
    {
        protected abstract string TableName { get; }

        MyDB _db;
        public DbConnection dbConn { get { return _db.dbConn; } }
        DbDataAdapter _adapter;
        DataTable _table;

        public DataTable Table { get { return _table; } }
        public int RowsCount { get { return _table.Rows.Count; } }
        
        public string SchemaName
        {
            get
            {
                return Utils.AppSettings.SCHEMA_NAME;
            }
        }


        public MyTable(MyDB db,string whereSql)
        {
            _db = db;

            string sql = $"select * from {SchemaName}.{TableName}";

            if (!string.IsNullOrEmpty(whereSql))
            {
                sql = $"{sql} where {whereSql}";
            }
            _table = GetTable(sql);
        }

        private DataTable GetTable(string whereSql)
        {
            _db.OpenDB();
            _adapter = _db.CreateDataAdapter(whereSql);
            _db.CreateCommandBuilder(_adapter);

            DataTable table = new DataTable();
            _adapter.FillSchema(table, SchemaType.Source);
           
            try
            {
                table.BeginLoadData();
                table.Clear();
                _adapter.Fill(table);
                table.EndLoadData();
            }
            catch (Exception ex)
            {

            }

            return table;         
        }

        public bool Update()
        {
            try
            {
                _adapter.Update(_table);
                _table.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogMan.Instance.Error(this.GetType().Name, ex.Message);
                return false;
            }
        }
    }
}
