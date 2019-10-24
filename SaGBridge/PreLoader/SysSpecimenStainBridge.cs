using SaGKernel.Specimen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaGModel;

namespace SaGBridge
{
    public class SysSpecimenStainBridge : BaseBridge<SysSpecimenStainM>
    {
        protected override string Api
        {
            get
            {
                return "SysSpecimenStain";
            }
        }
        public SysSpecimenStainBridge(string token) : base(token)
        {
        }
    }
}
