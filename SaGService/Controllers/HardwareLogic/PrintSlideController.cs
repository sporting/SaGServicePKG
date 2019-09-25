using PPMSXLib;
using SaGLogic;
using SaGModel;
using SaGService.Models;
using SaGService.Security;
using SaGService.Utils;
using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaGService.Controllers
{
    public class PrintSlideController : ApiController
    {
        //Server 列印 slide 

        //POST api/PrintCassette Entity Json
        public IHttpActionResult Post([FromBody]PrintSlideM psm)
        {
            if (!ModelState.IsValid)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintSlideController: ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            //取得 template 名稱
            SlideTemplate slideTemplate = new SlideTemplate();
            SlideTemplateM[] templateAry = slideTemplate.GetValues(psm.Template);
            if (templateAry.Count() <= 0)
            {
                templateAry = slideTemplate.GetValues(AppSettings.DefaultKey);
            }

            if (templateAry.Count() <= 0)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintSlideController: Template {psm.Template} is not Exist");

                return BadRequest(ModelState);
            }

            SlideTemplateM template = templateAry[0];

            //取得 印表機名稱
            SlidePrinter slidePrinter = new SlidePrinter();
            SlidePrinterM[] slideAry = slidePrinter.GetValues(psm.Printer);
            if (slideAry.Count() <= 0)
            {
                slideAry = slidePrinter.GetValues(AppSettings.DefaultKey);
            }
                        
            if (slideAry.Count() <= 0)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintSlideController: Printer {psm.Printer} is not Exist");

                return BadRequest(ModelState);
            }

            SlidePrinterM slideM = slideAry[0];

            //取得 輸出目錄
            SlideWorkStation slideWorkStation = new SlideWorkStation();
            SlideWorkStationM[] workstationAry = slideWorkStation.GetValues(psm.WorkStation);
            if (workstationAry.Count() <= 0)
            {
                workstationAry = slideWorkStation.GetValues(AppSettings.DefaultKey);
            }

            if (workstationAry.Count() <= 0)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintSlideController: WorkStation {psm.WorkStation} is not Exist");

                return BadRequest(ModelState);
            }
            SlideWorkStationM workstation = workstationAry[0];                        
            //檢查 cassette
            OrderCassette order = new OrderCassette();
            if (!order.CassetteExist(psm.Data.OrdNo, psm.Data.CassetteSequence))
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintSlideController: 沒有此包埋盒 {psm.Data.OrdNo} {psm.Data.CassetteSequence}");

                return BadRequest(ModelState);
            }

            //寫入 OrderSlide
            OrderSlide slide = new OrderSlide();
            int newSlideSeq = 1;
            if (slide.Add(psm.Data, out newSlideSeq))
            {
                psm.Data.SlideSequence = newSlideSeq;

                //輸出 slide csv file，列印 slide
                PPMSXData ppmsx = new PPMSXData();

                SlideSampleFormat ssf = new SlideSampleFormat(psm.Data.OrdNo);
                ssf.Data.SpecialRemark = psm.Data.SlideRemark;
                ssf.Data.FieldA = psm.Data.SlideFieldA;
                ssf.Data.FieldB = psm.Data.SlideFieldB;
                ssf.Data.CassetteSequence = psm.Data.CassetteSequence;
                ssf.SetSlideSequence(psm.Data.SlideSequence);

                ppmsx.AddData(template.Template, slideM.Printer,0, new DataSection() { Val0 = ssf.Data.PathoQRText, Val1 = ssf.Data.PathoMajorText, Val2 = ssf.Data.PathoSequence, Val3 = ssf.Data.SpecialRemark, Val4 = ssf.Data.FieldA, Val5 = ssf.Data.FieldB });

                if (ppmsx.SaveFile(workstation.Path))
                {
                    return Ok(psm);
                }
                else
                {
                    LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintSlideController: OrderSlide add data failed");

                    return BadRequest();
                }                
            }
            else
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintSlideController: OrderSlide add data failed");

                return BadRequest();
            }
        }

    }
}
