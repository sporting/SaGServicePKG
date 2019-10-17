using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.IO
{
    public class SaFile
    {
        public static string GetTicksFileName(string outputFolder,string fileNameExt)
        {
            if (string.IsNullOrEmpty(outputFolder))
            {
                throw new Exception("Please assign outputFolder value");
            }

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            if (fileNameExt.Length > 0)
            {
                //fileNameExt = "txt" then ".txt"
                fileNameExt = fileNameExt[0].Equals(".") ? fileNameExt :$".{fileNameExt}";
            }

            if (Directory.Exists(outputFolder))
            {
                string uniqueFileName = DateTime.Now.Ticks.ToString();
                uniqueFileName = string.Concat(uniqueFileName,string.IsNullOrEmpty(fileNameExt) ? "" : $".{fileNameExt}");

                string outputFileName = Path.Combine(outputFolder, uniqueFileName);

                return outputFileName;
            }

            return string.Empty;
        }
    }
}
