using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class OrderCassetteM
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int CassetteSequence { get; set; }
        public string CassetteRemark { get; set; }
        public string CassetteFieldA { get; set; }
        public string CassetteFieldB { get; set; }
        public string GrossUser { get; set; }
        public string GrossDate { get; set; }
        public string GrossTime { get; set; }
        public string EmbedUser { get; set; }
        public string EmbedDate { get; set; }
        public string EmbedTime { get; set; }
        public int SlideTotalAmount { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }        
    }
}
