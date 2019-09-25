using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class OrderBarcodeM
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int BarcodeTotalAmount { get; set; }
        public int CassetteTotalAmount { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }

        public string OpDate { get; set; }
        public string OpTime { get; set; }        
    }
}
