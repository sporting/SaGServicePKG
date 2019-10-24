using SaGKernel.Specimen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaGModel;

namespace SaGBridge
{
    public class SysCassetteMagazineBridge : BaseBridge<SysCassetteMagazineM>
    {
        protected override string Api
        {
            get
            {
                return "SysCassetteMagazine";
            }
        }
        public SysCassetteMagazineBridge(string token) : base(token)
        {
        }
    }
}
