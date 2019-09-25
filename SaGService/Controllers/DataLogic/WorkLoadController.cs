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
    public class WorkLoadController : ApiController
    {
        //GET api/WorkLoad/?type=ggg&begDate=xxx&endDate=yyy&user=zzz Entity Json
        public IHttpActionResult Get(string type,string begDate,string endDate,string user)
        {

            if (type == "gross")
            {
                GrossWorkLoadV work = new GrossWorkLoadV();
                WorkLoadMV[] wv = work.Get(begDate, endDate, user);
                return Ok(wv);
            }
            else if (type == "embed")
            {
                EmbedWorkLoadV work = new EmbedWorkLoadV();
                WorkLoadMV[] wv = work.Get(begDate, endDate, user);
                return Ok(wv);
            }
            else if (type == "slide")
            {
                SlideWorkLoadV work = new SlideWorkLoadV();
                SlideWorkLoadMV[] wv = work.Get(begDate, endDate, user);
                return Ok(wv);
            }
            else if (type == "doctor")
            {
                DoctorSlideWorkLoadV work = new DoctorSlideWorkLoadV();
                SlideWorkLoadMV[] wv = work.Get(begDate, endDate, user);
                return Ok(wv);
            }
            else
            {
                return BadRequest();
            }

           
        }
    }
}
