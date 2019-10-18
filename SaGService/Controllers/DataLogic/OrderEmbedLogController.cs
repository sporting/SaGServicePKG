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
    public class OrderEmbedLogController : ApiController
    {
        //POST api/OrderEmbedLog Entity Json
        public IHttpActionResult Post([FromBody]OrderEmbedLogM log)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");
                return BadRequest(ModelState);
            }

            OrderEmbedLog embedLog = new OrderEmbedLog();
            if (embedLog.AddLog(log))
            {
                return Ok(log);
            }
            else
            {
                MyLog.Error(this, "OrderEmbedLog AddLog failed");
                return BadRequest();
            }
        }
    }
}
