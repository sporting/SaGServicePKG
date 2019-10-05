using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Format
{
    
    public class CassetteEnvironment
    {
        public string TemplateName;
        public string CassetteName;
        public CassetteEnvironment()
        {

        }

        public CassetteEnvironment(string templateName, string cassetteName):this()
        {
            TemplateName = templateName;
            CassetteName = cassetteName;
        }
    }
}
