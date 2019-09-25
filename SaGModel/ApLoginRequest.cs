using SaGUtil.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class ApLoginRequest
    {
        public string App { get; set; }
        public string LoginUser { get; set; }
        public MachineInfo ApMachine { get; set; }
        public string Token { get; set; }
        public DateTime LoginDate { get; set;  }
    }
}
