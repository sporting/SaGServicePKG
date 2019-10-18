using PPMSXLib.Format;
using LCPMS15Lib.Format;

namespace SaGKernel.CSFilter
{
    /// <summary>
    /// 1個包埋盒產生0~n個玻片規則介面
    /// </summary>
    public interface ICassetteToSlideFilter
    {
        string CSFilterName { get; }
        int Priority { get;} //數字越大越優先
        //輸出多個玻片的資訊
        SlideFormat[] GenerateSlides(CassetteFormat css, SlideEnvironment slenv);
        //檢核是否符合該Filter
        bool MatchRule(CassetteFormat css);
    }
}
