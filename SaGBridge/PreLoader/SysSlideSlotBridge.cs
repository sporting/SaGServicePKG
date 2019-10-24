using SaGKernel.Specimen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaGModel;

namespace SaGBridge
{
    public class SysSlideSlotBridge : BaseBridge<SysSlideSlotM>
    {
        protected override string Api
        {
            get
            {
                return "SysSlideSlot";
            }
        }
        public SysSlideSlotBridge(string token) : base(token)
        {
        }
    }
}
