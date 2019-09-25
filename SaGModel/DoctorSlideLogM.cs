using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    public class DoctorSlideLogM
    {
        public int Id { get; set; }
        public string OrdNo { get; set; }
        public int CassetteSequence { get; set; }
        public int SlideSequence { get; set; }
        public string DoctorUser { get; set; }
        public string DoctorDate { get; set; }
        public string DoctorTime { get; set; }
        public string OpDate { get; set; }
        public string OpTime { get; set; }       
        public DeleteFlagEnum IsDeleteFlag { get; set; }
    }
}
