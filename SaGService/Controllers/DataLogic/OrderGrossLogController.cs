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
    public class OrderGrossLogController : ApiController
    {
        //POST api/OrderGrossLog Entity Json
        public IHttpActionResult Post([FromBody]OrderGrossLogM log)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            OrderGrossLog grossLog = new OrderGrossLog();
            if (grossLog.AddLog(log))
            {
                return Ok(log);
            }
            else
            {
                MyLog.Error(this, "OrderGrossLog AddLog failed");

                return BadRequest();
            }
        }
    }
}
