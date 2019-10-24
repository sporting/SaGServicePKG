using SaGKernel.Specimen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaGModel;

namespace SaGBridge
{
    public class SysSpecimenStainValidBridge : BaseBridge<SysSpecimenStainM>
    {
        protected override string Api
        {
            get
            {
                return "SysSpecimenStainValid";
            }
        }
        public SysSpecimenStainValidBridge(string token) : base(token)
        {
        }
    }
}
