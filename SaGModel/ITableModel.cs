using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    /// <summary>
    /// interface 
    /// to Generate Model Class array
    /// or
    /// to Generate DataTable from Model Class Array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface ITableModel<T>
    {
        void Initialize();
        T[] GenerateModel(DataTable dt);

        DataTable GenerateDataTable(T[] models);
    }

    public enum SaDataState
    {
        Unchanged = 2,
        //
        // 摘要:
        //     資料列已經加入至 System.Data.DataRowCollection，並且尚未呼叫 System.Data.DataRow.AcceptChanges。
        Added = 4,
        //
        // 摘要:
        //     使用 System.Data.DataRow 的 System.Data.DataRow.Delete 方法來刪除資料列。
        Deleted = 8,
        //
        // 摘要:
        //     已經修改資料列，並且尚未呼叫 System.Data.DataRow.AcceptChanges。
        Modified = 16
    }
}
