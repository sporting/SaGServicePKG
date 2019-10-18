using SaGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGLogic
{
    /// <summary>
    /// 檢查登入人員是否符合 Rule
    /// 暫時都是 True
    /// </summary>
    public class UserValidate
    {
        public static bool CheckUser(ApLoginRequest loginRequest)
        {
            return true;
        }
    }
}
