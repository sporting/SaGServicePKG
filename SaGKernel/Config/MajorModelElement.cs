using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Config
{
    public class MajorModelElement : ConfigurationElement
    {
        [ConfigurationProperty("Key")]
        public string Key
        {
            get { return (string)this["Key"]; }
            set { this["Key"] = value; }
        }

        [ConfigurationProperty("Value")]
        public string Value
        {
            get { return (string)this["Value"]; }
            set { this["Value"] = value; }
        }
    }
}
