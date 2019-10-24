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
    /// 玻片工作量 View
    /// </summary>
    public class SlideWorkLoadV 
    {
        public SlideWorkLoadMV[] Get(string begDate, string endDate, string slideUser)
        {
            if (!string.IsNullOrEmpty(begDate) && !string.IsNullOrEmpty(endDate))
            {
                try
                {
                    ViewSlideWorkLoad view = new ViewSlideWorkLoad();
                    DataTable dt = view.Get(begDate,endDate, slideUser);

                    return new SlideWorkLoadMV().GenerateModel(dt);
                }
                catch
                {
                    return new SlideWorkLoadMV[] { };
                }
            }

            return new SlideWorkLoadMV[] { };
        }

    }
}
