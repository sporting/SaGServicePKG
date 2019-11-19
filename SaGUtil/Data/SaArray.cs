using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.Data
{
    public class SaArray<T>
    {
        public static T[] CopyArray(T[] src)
        {
            T[] ary1 = new T[src.Length];
            Array.Copy(src, 0, ary1, 0, src.Length);
            return ary1;
        }
    }
}
