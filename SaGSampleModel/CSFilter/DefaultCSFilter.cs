using PPMSXLib.Format;
using LCPMS15Lib.Format;

namespace SaGKernel.CSFilter
{
    /// <summary>
    /// CSFilter 1個包埋盒要產生多少玻片
    /// Default: 1個包埋盒產生1個玻片
    /// </summary>
    public class DefaultCSFilter : ICassetteToSlideFilter
    {
        public string CSFilterName
        {
            get
            {
                return "DefaultCSFilter";
            }
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }

        public SlideFormat[] GenerateSlides(CassetteFormat cssIn, SlideEnvironment slenv)
        {
            SlideSampleFormat smmh = new SlideSampleFormat();
            CassetteSampleFormat css = (CassetteSampleFormat)cssIn;
            smmh.SetVal(slenv, css.Data.PathoNo, css.Data.CassetteSequence, css.Data.SlideSequence, css.Data.SpecialRemark, css.Data.FieldA, string.Empty);

            return new SlideSampleFormat[] { smmh };
        }

        public bool MatchRule(CassetteFormat css)
        {
            return true;
        }

    }
}
