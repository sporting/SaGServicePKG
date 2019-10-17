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
    public class OrderBarcodeLogController : ApiController
    {
        //列印barcode時寫 log

        //POST api/OrderBarcodeLog Entity Json
        public IHttpActionResult Post([FromBody]OrderBarcodeLogM log)
        {
            if (!ModelState.IsValid)
            {
                SaLogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"OrderBarcodeLogController: ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            OrderBarcodeLog orderCounterLog = new OrderBarcodeLog();
            if (orderCounterLog.Add(log))
            {
                return Ok(log);
            }
            else
            {
                SaLogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"OrderBarcodeLogController: OrderBarcodeLog Add failed");

                return BadRequest();
            }
        }
    }
}
