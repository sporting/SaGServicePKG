using SaGDB.Utils;
using SaGUtil.System;
using System;
using System.Data;
using System.Data.Common;


namespace SaGDB
{
    public sealed class MyDB
    {
        //const string _logName = "MyDB";

        private DbConnection _conn;
        private DbProviderFactory _factory;

        public DbConnection dbConn { get { return _conn; } }


        public MyDB()
        {
            _factory = DbProviderFactories.GetFactory(AppSettings.MyDBProviderName);
            _conn = _factory.CreateConnection();
            _conn.ConnectionString = AppSettings.MyDBConnectionString;
        }

        public bool IsOpen
        {
            get { return (_conn.State == ConnectionState.Open); }
        }

        public void OpenDB()
        {
            if (!IsOpen)
            {
                try
                {
                    _conn.Close();
                    _conn.Open();
                }
                catch (Exception ex)
                {
                    MyLog.Fatal(this, ex.Message);
                }
            }
        }

        public void CloseDB()
        {
            _conn.Close();
        }

        public DbCommand CreateCommand()
        {
            return _conn.CreateCommand();
        }

        public DbDataAdapter CreateDataAdapter(string sql)
        {
            DbDataAdapter adapter = _factory.CreateDataAdapter();
            adapter.SelectCommand = CreateCommand();
            adapter.SelectCommand.CommandText = sql;
            
            return adapter;
        }

        public DbCommandBuilder CreateCommandBuilder(DbDataAdapter adapter)
        {
            DbCommandBuilder builder = _factory.CreateCommandBuilder();
            builder.DataAdapter = adapter;

            try
            {
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this, ex.Message);
            }

            return builder;
        }

        public IDbTransaction StartTransaction()
        {
            OpenDB();
            return _conn.BeginTransaction();
        }

        public bool Commit(IDbTransaction transaction)
        {
            if (IsOpen)
            {
                try
                {
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MyLog.Fatal(this, ex.Message);
                    return false;
                }
            }

            return false;
        }

    }
}
