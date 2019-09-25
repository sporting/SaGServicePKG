using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGLogic
{
    interface ITableModel<T>
    {
        T[] GenerateModel(DataTable dt);

        DataTable GenerateDataTable(T[] models);
    }
}
