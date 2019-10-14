using SaGKernel.Settings;
using SaGKernel.MajorClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaGKernel.UI;

namespace SaGKernel.Settings
{
    public class CassetteSettings
    {
        public string TemplateName;
        //public string CassetteName;
        //public string CassetteName2;
        //public string CassetteName3;
        //public string CassetteName4;
        //public string CassetteName5;
        //public string CassetteName6;
        //public string CassetteName7;
        //public string CassetteName8;
        public string outputFolder;
        public MajorCassetteSettingsCollection MajorCassettes;

        public void InitialIfNull()
        {
            if (MajorCassettes == null)
            {
                MajorCassettes = new MajorCassetteSettingsCollection();
            }
            
            if (MajorCassettes.Count <= 0)
            {
                IMajorClass mcIns = MajorClassify.GetInstance().GetDefaultMajorClass();
                MajorCassetteSettings mcs = new MajorCassetteSettings();
                mcs.MajorClassName = mcIns.ClassName;
                mcs.CassetteMagazineName = CassetteColors.GetInstance().GetDefaultName(); //CassetteSettingsApi.GetDefaultCassetteMagazineName();
                MajorCassettes.Add( mcs);
            }

            if (string.IsNullOrEmpty(TemplateName))
            {
                TemplateName = "Normal";
            }

            //if (string.IsNullOrEmpty(CassetteName))
            //{
            //    CassetteName = "Normal(White)";
            //}

            //if (string.IsNullOrEmpty(CassetteName2))
            //{
            //    CassetteName2 = "Normal(White)";
            //}
            //if (string.IsNullOrEmpty(CassetteName3))
            //{
            //    CassetteName3 = "Normal(White)";
            //}
            //if (string.IsNullOrEmpty(CassetteName4))
            //{
            //    CassetteName4 = "Normal(White)";
            //}
            //if (string.IsNullOrEmpty(CassetteName5))
            //{
            //    CassetteName5 = "Normal(White)";
            //}
            //if (string.IsNullOrEmpty(CassetteName6))
            //{
            //    CassetteName6 = "Normal(White)";
            //}
            //if (string.IsNullOrEmpty(CassetteName7))
            //{
            //    CassetteName7 = "Normal(White)";
            //}
            //if (string.IsNullOrEmpty(CassetteName8))
            //{
            //    CassetteName8 = "Normal(White)";
            //}
            if (string.IsNullOrEmpty(outputFolder))
            {
                outputFolder = "C:\\";
            }
        }
    }
}
