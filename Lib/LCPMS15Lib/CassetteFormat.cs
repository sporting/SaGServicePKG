
namespace LCPMS15Lib
{
    public abstract class CassetteFormat
    {
        public abstract string Template(); //範本名稱
        public abstract string Magazine(); //cassette 匣
        //正面的文字
        public virtual string[] FrontTexts()
        {
            return new string[] { };
        }
        //左側的文字
        public virtual string[] LeftTexts()
        {
            return new string[] { };
        }
        //右側的文字
        public virtual string[] RightTexts()
        {
            return new string[] { };
        }

        public DataSection ToDataSection()
        {
            DataSection ds = new DataSection();
            ds.SetTemplate(Template());
            ds.SetMagazine(Magazine());

            string[] frontTexts = FrontTexts();
            if (frontTexts.Length > 0)
            {
                ds.SetFront(frontTexts);
            }
            string[] leftTexts = LeftTexts();
            if (leftTexts.Length > 0)
            {
                ds.SetLeft(leftTexts);
            }
            string[] rightTexts = RightTexts();
            if (rightTexts.Length > 0)
            {
                ds.SetRight(rightTexts);
            }

            return ds;
        }
    }
}