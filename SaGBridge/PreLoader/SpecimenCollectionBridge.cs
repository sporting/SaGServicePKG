using SaGKernel.Specimen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGBridge
{
    public class SpecimenCollectionBridge : BaseBridge<SpecimenItem>
    {
        protected override string Api
        {
            get
            {
                return "SpecimenCollection";
            }
        }
        public SpecimenCollectionBridge(string token) : base(token)
        {
        }
    }
}
