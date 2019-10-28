using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaGUtil.WinForm
{
    public class SaAssembly
    {
        public static string EntryName
        {
            get
            {
                //root application
                return $"{Assembly.GetEntryAssembly().GetName().Name}";
            }
        }

        public static string ProductVersion
        {
            get
            {
                return $"{Application.ProductVersion}";
                //return $"{AppDomain.CurrentDomain.FriendlyName} {Application.ProductVersion}";
            }
        }
    }
}
