using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class OrderEmbedLogM
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int CassetteSequence { get; set; }
        public string EmbedUser { get; set; }
        public string EmbedDate { get; set; }
        public string EmbedTime { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }
        public DeleteFlagEnum IsDeleteFlag { get; set; }
    }
}
