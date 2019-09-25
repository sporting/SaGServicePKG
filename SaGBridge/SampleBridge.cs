using SaGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGBridge
{
    public class SampleBridge: BaseBridge<Sample>
    {
        protected override string Api
        {
            get
            {
                return "Sample";
            }
        }

        public SampleBridge(string token):base(token)
        {
            
        }
    }
}
