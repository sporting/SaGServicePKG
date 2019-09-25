using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class OrderBarcodeLogM
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int Amount { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }

        public bool RePrint { get; set; }
    }
}
