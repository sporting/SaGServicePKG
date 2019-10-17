using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.Extensions
{
    public static class SaObjectExt
    {
        public static string ToStringEx(this object s)
        {
            return s == null ? string.Empty : s.ToString();
        }
    }
}
