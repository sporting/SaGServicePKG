using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Config
{
    public class CustomModelSection : ConfigurationSection
    {
        private CustomModelSection()
        {
        }

        //[ConfigurationProperty("MajorModel")]
        //public MajorModelElement MajorModel
        //{
        //    get { return (MajorModelElement)this["MajorModel"]; }
        //    set { this["MajorModel"] = value; }
        //}

        [ConfigurationProperty("CustomModels"), ConfigurationCollection(typeof(CustomModelElement))]
        public CustomModelElementCollection CustomModels
        {
            get { return this["CustomModels"] as CustomModelElementCollection; }
            set { this["CustomModels"] = value; }
        }
    }
}
