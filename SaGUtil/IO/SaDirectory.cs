using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.IO
{
    public class SaDirectory
    {
        public static string Desktop()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        public static string SpecialFolder(Environment.SpecialFolder sf)
        {
            return Environment.GetFolderPath(sf);
        }
    }
}
