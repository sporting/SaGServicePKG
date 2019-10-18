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
    public class SysParamsController : ApiController
    {
        //GET api/SysParams/?key=xxxx Entity Json
        public IHttpActionResult GetValues(string name)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            SysParams sysparams = new SysParams();

            return Ok(sysparams.GetValues(name));
        }
    }
}
