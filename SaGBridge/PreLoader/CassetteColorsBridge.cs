using SaGKernel.Specimen;
using SaGKernel.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGBridge
{
    public class CassetteColorsBridge : BaseBridge<CassetteColor>
    {
        protected override string Api
        {
            get
            {
                return "CassetteColors";
            }
        }
        public CassetteColorsBridge(string token) : base(token)
        {
        }
    }
}
