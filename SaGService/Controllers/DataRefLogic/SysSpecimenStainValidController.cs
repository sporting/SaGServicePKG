﻿using SaGLogic;
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
    public class SysSpecimenStainValidController : ApiController
    {
  
        public IHttpActionResult GetValidAll()
        {
            if (!ModelState.IsValid)
            {
                MyLog.Info(this, "ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            SysSpecimenStain ssStain = new SysSpecimenStain();

            return Ok(ssStain.GetValidAll());
        }

    }
}
