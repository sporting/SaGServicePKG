using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Config
{
    public class SpecimenSection : ConfigurationSection
    {
        private SpecimenSection()
        {
        }

        [ConfigurationProperty("SpecimenFile")]
        public SpecimenElement SpecimenFile
        {
            get { return (SpecimenElement)this["SpecimenFile"]; }
            set { this["SpecimenFile"] = value; }
        }

    }
}
