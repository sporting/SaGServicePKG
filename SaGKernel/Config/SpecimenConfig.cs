using SaGUtil.Configuration;
using System;
using System.IO;
using SaGKernel.Utils;
using SaGKernel.Specimen;

namespace SaGKernel.Config
{
    public class SpeicmenConfig
    {
        public static string CsvFileName()
        {
            try
            {
                var config = SaAppSettings.GetAppSection<SpecimenSection>("SpecimenSection");
                return (string)config.SpecimenFile.Value;
            }
            catch (Exception ex)
            {
                MyLog.Fatal(typeof(SpeicmenConfig), ex.Message);
                return string.Empty;
            }
        }

        public static SpecimenCollection GetSpecimenData()
        {
            string csvFile = CsvFileName();
            if (!string.IsNullOrEmpty(csvFile))
            {
                if (File.Exists(csvFile))
                {
                    FileInfo f = new FileInfo(csvFile);
                    return SpecimenCollection.ParseCSVSpecimenData(f.FullName);
                }
            }
            return  new SpecimenCollection();
        }
    }
}
