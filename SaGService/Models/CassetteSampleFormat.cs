using LCPMS15Lib;
using SaGLogic;
using SaGUtil.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SaGService.Models
{
    /// <summary>
    /// Demo 使用的Cassette範例格式
    /// </summary>
    public class CassetteSampleFormat:CassetteFormat
    {
        public string TemplateName;
        public string CassetteName;

        public CassetteSlideSplit Data;
                

        public CassetteSampleFormat(string templateName, string cassetteName, string pathoNo,int cassetteSeq, string specialRemark, string fieldA, string fieldB)
        {
            Clear();

            TemplateName = templateName;
            CassetteName = cassetteName;

            Data = new CassetteSlideSplit(pathoNo, cassetteSeq, 0, specialRemark, fieldA, fieldB);          
        }

        public CassetteSampleFormat(string templateName, string cassetteName, string pathoMajor, string pathoMajorTail, string pathoSequence, int cassetteSeq, string specialRemark, string fieldA, string fieldB)
        {
            Clear();

            TemplateName = templateName;
            CassetteName = cassetteName;

            Data = new CassetteSlideSplit($"{pathoMajorTail}-{pathoSequence}", cassetteSeq, 0, specialRemark, fieldA, fieldB);
        }

        private void Clear()
        {
            TemplateName = string.Empty;
            CassetteName = string.Empty;
            if (Data != null)
            {
                Data.Clear();
            }
        }

        public override string Template()
        {
            return TemplateName;
        }

        public override string Magazine()
        {
            return CassetteName;
        }

        public override string[] FrontTexts()
        {
            List<string> ss = new List<string>();
            ss.Add(Data.PathoQRText);
            ss.Add(Data.PathoNo);
            ss.Add(Data.SpecialRemark);
            ss.Add(Data.FieldA);
            ss.Add(Data.FieldB);
            return ss.ToArray();
        }
    }
}
