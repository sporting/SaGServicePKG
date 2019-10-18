using System.Collections.Generic;
using SaGKernel.MajorClass;
using PPMSXLib.Format;
using LCPMS15Lib.Format;

namespace SaGKernel.CSFilter
{
    /// <summary>
    /// CSFilter 1個包埋盒要產生多少玻片
    /// Skin: 皮膚類的1個包埋盒產生2個玻片
    /// </summary>
    public class SkinCSFilter : ICassetteToSlideFilter
    {
        public string CSFilterName
        {
            get
            {
                return "SkinCSFilter";
            }
        }

        public int Priority
        {
            get
            {
                return 7;
            }
        }

        public SlideFormat[] GenerateSlides(CassetteFormat cssIn,SlideEnvironment slenv)
        {
            CassetteSampleFormat css = (CassetteSampleFormat)cssIn;
            if (MatchRule(css))
            {   
                //Skin 產生兩片玻片
                return GenerateSameSlide(css, slenv, 2);
            }

            SlideSampleFormat defaultSmm = new SlideSampleFormat();
            defaultSmm.SetVal(slenv, css.Data.PathoNo, css.Data.CassetteSequence, css.Data.SlideSequence, css.Data.SpecialRemark, css.Data.FieldA, string.Empty);
            return new SlideSampleFormat[] { defaultSmm };
        }

        private SlideSampleFormat[] GenerateSameSlide(CassetteSampleFormat css, SlideEnvironment slenv, int v)
        {
            List<SlideSampleFormat> list = new List<SlideSampleFormat>();
            for (int i = 1; i <= v; i++)
            {
                SlideSampleFormat smmh = new SlideSampleFormat();
                smmh.SetVal(slenv, css.Data.PathoNo, css.Data.CassetteSequence, css.Data.SlideSequence, css.Data.SpecialRemark, css.Data.FieldA, string.Empty);
                list.Add(smmh);
            }

            return list.ToArray();
        }

        public bool MatchRule(CassetteFormat css)
        {
            ////序號第一碼是8開頭的，表示是 Skin (皮膚)
            //return css.Data.PathoSequence.Substring(0, 1) == "8";
            return ((CassetteSampleFormat)css).Data.SpecimenMajorClass.MajorClass==MajorClassEnum.SkinMC;
        }

    }
}
