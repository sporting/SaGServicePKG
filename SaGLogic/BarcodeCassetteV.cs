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
    /// 包埋工作量 View
    /// </summary>
    public class BarcodeCassetteV
    {
        public BarcodeCassetteMV[] Get(string begDate, string endDate)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                try
                {
                    ViewBarcodeCassette view = new ViewBarcodeCassette();
                    DataTable dt = view.Get(begDate,endDate);

                    return new BarcodeCassetteMV().GenerateModel(dt);
                }
                catch
                {
                    return new BarcodeCassetteMV[] { };
                }
            }

            return new BarcodeCassetteMV[] { };
        }
        public BarcodeCassetteMV[] Get(string ordNo)
        {
            if (!string.IsNullOrEmpty(ordNo))
            {
                try
                {
                    ViewBarcodeCassette view = new ViewBarcodeCassette();
                    DataTable dt = view.Get(ordNo);

                    return new BarcodeCassetteMV().GenerateModel(dt);
                }
                catch
                {
                    return new BarcodeCassetteMV[] { };
                }
            }

            return new BarcodeCassetteMV[] { };
        }
    }
}
