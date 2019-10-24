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
    public class SysSlideSlotController : ApiController
    {        
        public IHttpActionResult Post([FromBody]SysSlideSlotM[] ssms)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");
                return BadRequest(ModelState);
            }

            SysSlideSlot ssSlot = new SysSlideSlot();
            
            if (ssSlot.Updates(ssms))
            {
                return Ok();
            }
            else
            {
                MyLog.Error(this, "SysSlideSlot Add failed");
                return BadRequest();
            }
        }

        //Delete api/SysSlideSlot/
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");
                return BadRequest(ModelState);
            }

            SysSlideSlot ssSlot = new SysSlideSlot();

            if (ssSlot.Delete(new int[] { id }))
            {
                return Ok();
            }
            else
            {
                MyLog.Error(this, "SysParamsM Delete failed");
                return BadRequest();
            }
        }
        //GET api/SysSlideSlot/?key=xxxx Entity Json
        public IHttpActionResult GetAll()
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            SysSlideSlot ssSlot = new SysSlideSlot();

            return Ok(ssSlot.GetValues());
        }
    }
}
