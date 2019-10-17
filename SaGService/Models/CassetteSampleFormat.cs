using LCPMS15Lib;
using SaGKernel;
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
    public class CassetteSampleFormat : CassetteFormat
    {
        public override string Template { get; set; }
        public override string Magazine { get; set; }

        public QRDataStruct Data;

        public CassetteSampleFormat(string QRCode)
        {
            Clear();

            Template = string.Empty;
            Magazine = string.Empty;

            Data = new QRDataStruct(QRCode);
        }
        public CassetteSampleFormat(string templateName, string cassetteName, string pathoNo, int cassetteSeq, string specialRemark, string fieldA, string fieldB)
        {
            Clear();

            Template = templateName;
            Magazine = cassetteName;

            Data = new QRDataStruct(pathoNo, cassetteSeq, 0, specialRemark, fieldA, fieldB);
        }

        public CassetteSampleFormat(string templateName, string cassetteName, string pathoMajor, string pathoMajorTail, string pathoSequence, int cassetteSeq, string specialRemark, string fieldA, string fieldB)
        {
            Clear();

            Template = templateName;
            Magazine = cassetteName;

            Data = new QRDataStruct($"{pathoMajorTail}-{pathoSequence}", cassetteSeq, 0, specialRemark, fieldA, fieldB);
        }

        private void Clear()
        {
            Template = string.Empty;
            Magazine = string.Empty;
            if (Data != null)
            {
                Data.Clear();
            }
        }


        public override string[] LeftSide { get { return new string[] { }; } set { } }
        public override string[] RightSide { get { return new string[] { }; } set { } }
        public override string[] FrontSide
        {
            get
            {
                return new string[] {
                Data.PathoQRText,
                Data.PathoNo,
                Data.SpecialRemark,
                Data.FieldA,
                Data.FieldB
            };
            }
            set { }
        }

    }
}
