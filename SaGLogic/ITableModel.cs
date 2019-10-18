using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGLogic
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
        T[] GenerateModel(DataTable dt);

        DataTable GenerateDataTable(T[] models);
    }
}
