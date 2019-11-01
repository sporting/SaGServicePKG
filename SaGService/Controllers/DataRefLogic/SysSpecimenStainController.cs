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
    public class SysSpecimenStainController : ApiController
    {
        public IHttpActionResult Post([FromBody]SysSpecimenStainM[] ssms)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");
                return BadRequest(ModelState);
            }

            SysSpecimenStain ssStain = new SysSpecimenStain();
            
            if (ssStain.Update(ssms))
            {
                return Ok();
            }
            else
            {
                MyLog.Error(this, "SysSpecimenStain Add failed");
                return BadRequest();
            }
        }

        //Delete api/SysSpecimenStain/
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");
                return BadRequest(ModelState);
            }

            SysSpecimenStain ssStain = new SysSpecimenStain();

            if (ssStain.Delete(new int[] {id}))
            {
                return Ok();
            }
            else
            {
                MyLog.Error(this, "SysSpecimenStain Delete failed");
                return BadRequest();
            }
        }

        //GET api/SysSpecimenStain/ Entity Json
        public IHttpActionResult GetAll()
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            SysSpecimenStain ssStain = new SysSpecimenStain();

            return Ok(ssStain.GetAll());
        }

    }
}
