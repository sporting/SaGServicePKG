using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Config
{
    public class CustomModelElementCollection : ConfigurationElementCollection
    {
        public CustomModelElement this[int index]
        {
            get { return (CustomModelElement)this.BaseGet(index); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CustomModelElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CustomModelElement)element).Key;
        }
    }
}
