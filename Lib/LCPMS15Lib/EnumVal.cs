using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCPMS15Lib
{
    public class EnumVal : Attribute
    {
        public string Description { get; set; }

        public EnumVal(string Description)
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
            var attributes = members[0].GetCustomAttributes(typeof(EnumVal), false);
            var description = ((EnumVal)attributes[0]).Description;
            return description;
        }
    }

}
