using SaGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGBridge
{
    public class LoginBridge: BaseBridge<ApLoginRequest>
    {
        protected override string Api
        {
            get
            {
                return "Login";
            }
        }

        public LoginBridge(string token):base(token)
        {
            
        }
    }
}
