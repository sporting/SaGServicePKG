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
    public class DoctorSlideLogsController : ApiController
    {
        //POST api/DoctorSlideLogs Entity Json
     
        public IHttpActionResult Post([FromBody]DoctorSlideLogM[] logs)
        {
            if (!ModelState.IsValid)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"DoctorSlideLogsController: ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            DoctorSlideLog doctorSlideLog = new DoctorSlideLog();
            if (doctorSlideLog.AddLog(logs))
            {
                return Ok(logs);
            }
            else
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"DoctorSlideLogsController: DoctorSlideLog AddLog failed");

                return BadRequest();
            }
        }

    }
}
