using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Config
{
    public class MajorModelSection : ConfigurationSection
    {
        private MajorModelSection()
        {
        }

        //[ConfigurationProperty("MajorModel")]
        //public MajorModelElement MajorModel
        //{
        //    get { return (MajorModelElement)this["MajorModel"]; }
        //    set { this["MajorModel"] = value; }
        //}

        [ConfigurationProperty("MajorModels"), ConfigurationCollection(typeof(MajorModelElement))]
        public MajorModelElementCollection MajorModels
        {
            get { return this["MajorModels"] as MajorModelElementCollection; }
            set { this["MajorModels"] = value; }
        }
    }
}
