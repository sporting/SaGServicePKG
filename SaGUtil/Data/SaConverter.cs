using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.Data
{
    /// <summary>
    /// Converter Function Collection
    /// </summary>
    public class SaConverter
    {
        public static int ToInt(object obj, int defVal)
        {
            int val = defVal;
            if (int.TryParse(obj.ToString(), out val))
            {
                return val;
            }

            return defVal;
        }
    }
}
