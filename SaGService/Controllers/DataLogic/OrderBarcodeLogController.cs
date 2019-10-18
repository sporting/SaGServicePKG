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
                MyLog.Info(this, "ModelState.IsValid = false");
                return BadRequest(ModelState);
            }

            OrderBarcodeLog orderCounterLog = new OrderBarcodeLog();
            if (orderCounterLog.Add(log))
            {
                return Ok(log);
            }
            else
            {
                MyLog.Error(this, "OrderBarcodeLog Add failed");
                return BadRequest();
            }
        }
    }
}
