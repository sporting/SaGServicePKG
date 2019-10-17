using SaGLogic;
using SaGModel;
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
    public class OrderCassetteController : ApiController
    {
        //列印 cassette 時寫入

        //POST api/OrderCassette Entity Json
        public IHttpActionResult Post([FromBody]OrderCassetteM log)
        {
            if (!ModelState.IsValid)
            {
                SaLogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"OrderCassetteController: ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            OrderCassette cassette = new OrderCassette();
            int newCassetteSeq = 1;
            if (cassette.Add(log,out newCassetteSeq))
            {
                log.CassetteSequence = newCassetteSeq;

                return Ok(log);
            }
            else
            {
                SaLogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"OrderCassetteController: OrderCassette Add failed");
                return BadRequest();
            }
        }

        //有沒有這個 cassette

        //GET api/OrderCassette/?ordNo=xxxx&cassetteSeq=yyyy Entity Json
        public IHttpActionResult GetExist(string ordNo,int cassetteSeq)
        {
            OrderCassette order = new OrderCassette();

            return Ok(order.CassetteExist(ordNo, cassetteSeq));
        }

        //GET api/OrderCassette/?type=ggg&begDate=xxx&endDate=yyy&user=zzz Entity Json
        public IHttpActionResult GetDetail(string type, string begDate, string endDate, string user)
        {
            OrderCassette order = new OrderCassette();

            if (type == "gross")
            {
                return Json<OrderCassetteM[]>((order.GetGrossDetail(begDate, endDate, user)));
            }
            else if (type == "embed")
            {
                return Json<OrderCassetteM[]>((order.GetEmbedDetail(begDate, endDate, user)));
            }

            return NotFound();
        }
    }
}
