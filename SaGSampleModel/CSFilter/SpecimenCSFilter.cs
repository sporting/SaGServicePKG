using System.Collections.Generic;
using PPMSXLib.Format;
using LCPMS15Lib.Format;
using SaGKernel.Specimen;
using System;

namespace SaGKernel.CSFilter
{
    /// <summary>
    /// CSFilter 1個包埋盒要產生多少玻片
    /// Specimen: 特殊檢體的1個包埋盒依設定產生多個玻片及 FieldB 設定
    /// </summary>
    public class SpecimenCSFilter : ICassetteToSlideFilter
    {
        public SpecimenCollection SPCollection { get; set; }

        public string CSFilterName
        {
            get
            {
                return "SpecimenCSFilter";
            }
        }

        public int Priority
        {
            get
            {
                return 9;
            }
        }

        public SlideFormat[] GenerateSlides(CassetteFormat cssIn, SlideEnvironment slenv)
        {
            CassetteSampleFormat css = (CassetteSampleFormat)cssIn;
            if (MatchRule(css))
            {
                string[] stainings = SPCollection.GetStainingMethods(css.Data.SpecialRemark);
                List<SlideSampleFormat> list = new List<SlideSampleFormat>();

                Array.ForEach(stainings, staining => {
                    SlideSampleFormat smmh = new SlideSampleFormat();
                    smmh.SetVal(slenv, css.Data.PathoNo, css.Data.CassetteSequence, css.Data.SlideSequence, css.Data.SpecialRemark, css.Data.FieldA, staining);
                    list.Add(smmh);
                });
                
                return list.ToArray();
            }

            SlideSampleFormat defaultSmm = new SlideSampleFormat();
            defaultSmm.SetVal(slenv, css.Data.PathoNo, css.Data.CassetteSequence, css.Data.SlideSequence, css.Data.SpecialRemark, css.Data.FieldA, string.Empty);
            return new SlideSampleFormat[] { defaultSmm };
        }

        public bool MatchRule(CassetteFormat css)
        {
            //特殊染色且不是急作
            return ((CassetteSampleFormat)css).HasSpecimen;
        }

        public SpecimenCSFilter()
        {
        }
    }
}
