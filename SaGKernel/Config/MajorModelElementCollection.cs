using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.Config
{
    public class MajorModelElementCollection : ConfigurationElementCollection
    {
        public MajorModelElement this[int index]
        {
            get { return (MajorModelElement)this.BaseGet(index); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new MajorModelElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MajorModelElement)element).Key;
        }
    }
}
