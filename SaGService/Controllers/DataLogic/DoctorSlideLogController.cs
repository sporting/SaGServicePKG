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
    public class DoctorSlideLogController : ApiController
    {
        //POST api/DoctorSlideLog Entity Json
        public IHttpActionResult Post([FromBody]DoctorSlideLogM log)
        {
            if (!ModelState.IsValid)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"DoctorSlideLogController: ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            DoctorSlideLog doctorSlideLog = new DoctorSlideLog();
            if (doctorSlideLog.AddLog(log))
            {
                return Ok(log);
            }
            else
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"DoctorSlideLogController: DoctorSlideLog AddLog failed");

                return BadRequest();
            }
        }


        //GET api/DoctorSlideLog/?date=xxxx Entity Json
        public IHttpActionResult GetByOpDate(string date)
        {
            DoctorSlideLog dsl = new DoctorSlideLog();
            DoctorSlideLogM[] dslm = dsl.GetDoctorSlidesByOpDate(date);

            return Ok(dslm);
        }
    }
}
