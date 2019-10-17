using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaGUtil.System
{
    public class SaEnumVal : Attribute
    {
        public string Description { get; set; }

        public SaEnumVal(string Description)
        {
            this.Description = Description;
        }

        public override string ToString()
        {
            return this.Description.ToString();
        }

        public static string Value(Enum e)
        {            
            var members = e.GetType().GetMember(e.ToString());
            var attributes = members[0].GetCustomAttributes(typeof(SaEnumVal), false);
            var description = ((SaEnumVal)attributes[0]).Description;
            return description;
        }
    }

}
