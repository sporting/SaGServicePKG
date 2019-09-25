using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    //工作量 (日/人/量)
    public class WorkLoadMV
    {
        public string Date { get; set; }
        public string User { get; set; }
        public int Total { get; set; } 
    }

    //玻片工作量 (日/人/fieldA/fieldB/量)
    public class SlideWorkLoadMV
    {
        public string Date { get; set; }
        public string User { get; set; }
        public string FieldA { get; set; }
        public string FieldB { get; set; }
        public int Total { get; set; }
    }
}
