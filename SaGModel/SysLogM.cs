using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class SysLogM
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Params { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }
    }
}
