using SaGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SaGDB;
using SaGDB.Views;
using SaGUtil.Data;

namespace SaGLogic
{
    /// <summary>
    /// 作為前端應用與資料庫物件的中介層 
    /// Gross 工作量 View
    /// </summary>
    public class GrossWorkLoadV 
    {
        public WorkLoadMV[] Get(string begDate, string endDate, string grossUser)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                try
                {
                    ViewGrossWorkLoad view = new ViewGrossWorkLoad();
                    DataTable dt = view.Get(begDate,endDate,grossUser);

                    return new WorkLoadMV().GenerateModel(dt);
                }
                catch
                {
                    return new WorkLoadMV[] { };
                }
            }

            return new WorkLoadMV[] { };
        }

        
    }
}
