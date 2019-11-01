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

        //Post api/SysParams/
        public IHttpActionResult Post([FromBody]SysParamsM[] spms)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");
                return BadRequest(ModelState);
            }

            SysParams sysparams = new SysParams();

            if (sysparams.Update(spms))
            {
                return Ok();
            }
            else
            {
                MyLog.Error(this, "SysParams Add failed");
                return BadRequest();
            }
        }

        //Delete api/SysParams/
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");
                return BadRequest(ModelState);
            }

            SysParams sysparams = new SysParams();

            if (sysparams.Delete(new int[] { id }))
            {
                return Ok();
            }
            else
            {
                MyLog.Error(this, "SysParams Delete failed");
                return BadRequest();
            }
        }
    }
}
