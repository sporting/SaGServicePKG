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
    public class SysCassetteMagazineController : ApiController
    {        
        public IHttpActionResult Post([FromBody]SysCassetteMagazineM[] scms)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");
                return BadRequest(ModelState);
            }

            SysCassetteMagazine scm = new SysCassetteMagazine();
            
            if (scm.Update(scms))
            {
                return Ok();
            }
            else
            {
                MyLog.Error(this, "SysCassetteMagazine Add failed");
                return BadRequest();
            }
        }

        //Delete api/SysCassetteMagazine/
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");
                return BadRequest(ModelState);
            }

            SysCassetteMagazine scm = new SysCassetteMagazine();

            if (scm.Delete(new int[] { id }))
            {
                return Ok();
            }
            else
            {
                MyLog.Error(this, "SysParamsM Delete failed");
                return BadRequest();
            }
        }

        //GET api/SysCassetteMagazine/?key=xxxx Entity Json
        public IHttpActionResult GetAll()
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            SysCassetteMagazine scmm = new SysCassetteMagazine();

            return Ok(scmm.GetValues());
        }
    }
}
