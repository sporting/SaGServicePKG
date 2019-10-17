using LCPMS15Lib;
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
    public class PrintCassetteController : ApiController
    {
        //Server 列印 cassette 

        //POST api/PrintCassette Entity Json
        public IHttpActionResult Post([FromBody]PrintCassetteM pcm)
        {
            if (!ModelState.IsValid)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintSlideController: ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            //取得 template 名稱
            CassetteTemplate cassetteTemplate = new CassetteTemplate();
            CassetteTemplateM[] templateAry = cassetteTemplate.GetValues(pcm.Template);
            if (templateAry.Count() <= 0)
            {
                templateAry = cassetteTemplate.GetValues(AppSettings.DefaultKey);
            }

            if (templateAry.Count() <= 0)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintCassetteController: Template {pcm.Template} is not Exist");

                return BadRequest(ModelState);
            }

            CassetteTemplateM template = templateAry[0];

            //取得 卡匣名稱
            CassetteMagazine cassetteMagazine = new CassetteMagazine();
            CassetteMagazineM[] magazineAry = cassetteMagazine.GetValues(pcm.Magazine);
            if (magazineAry.Count() <= 0)
            {
                magazineAry = cassetteMagazine.GetValues(AppSettings.DefaultKey);
            }
                        
            if (magazineAry.Count() <= 0)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintCassetteController: Magazine {pcm.Magazine} is not Exist");

                return BadRequest(ModelState);
            }

            CassetteMagazineM magazine = magazineAry[0];

            //取得 輸出目錄
            CassetteWorkStation cassetteWorkStation = new CassetteWorkStation();
            CassetteWorkStationM[] workstationAry = cassetteWorkStation.GetValues(pcm.WorkStation);
            if (workstationAry.Count() <= 0)
            {
                workstationAry = cassetteWorkStation.GetValues(AppSettings.DefaultKey);
            }

            if (workstationAry.Count() <= 0)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintCassetteController: WorkStation {pcm.WorkStation} is not Exist");

                return BadRequest(ModelState);
            }

            CassetteWorkStationM workstation = workstationAry[0];

            //寫入 OrderCassette
            OrderCassette cassette = new OrderCassette();
            int newCassetteSeq = 1;
            if (cassette.Add(pcm.Data, out newCassetteSeq))
            {
                pcm.Data.CassetteSequence = newCassetteSeq;

                //輸出 cassette 文字檔，列印 cassette
                LCPMS15Data lcpms15 = new LCPMS15Data();
                CassetteSampleFormat _cassette = new CassetteSampleFormat(template.Template, magazine.Magazine, pcm.Data.OrdNo, pcm.Data.CassetteSequence, pcm.Data.CassetteRemark, pcm.Data.CassetteFieldA, pcm.Data.CassetteFieldB);
                lcpms15.AddCassette(_cassette);

                if (lcpms15.SaveFile(workstation.Path))
                {
                    return Ok(pcm);
                }
                else
                {
                    LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintCassetteController: LCPMS15Data save failed");
                    
                    return BadRequest();
                }
            }
            else
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"PrintCassetteController: OrderCassette add data failed");

                return BadRequest();
            }
        }

    }
}
