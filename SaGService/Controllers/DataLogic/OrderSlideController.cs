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
    public class OrderSlideController : ApiController
    {
        //列印 Slide 時寫入

        //POST api/OrderSlide Entity Json
        public IHttpActionResult Post([FromBody]OrderSlideM log)
        {
            if (!ModelState.IsValid)
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"OrderSlideController: ModelState.IsValid = false");

                return BadRequest(ModelState);
            }

            OrderSlide slide = new OrderSlide();
            int newSlideSeq = 1;
            if (slide.Add(log,out newSlideSeq))
            {
                log.SlideSequence = newSlideSeq;

                return Ok(log);
            }
            else
            {
                LogMan.Instance.Info(GlobalVars.LOGGER_NAME, $"OrderSlideController: OrderSlide Add failed");

                return BadRequest();
            }
        }

        //有沒有這個 Slide

        //GET api/OrderSlide/?ordNo=xxxx&cassetteSeq=yyyy&slideSeq=zzzz Entity Json
        public IHttpActionResult GetExist(string ordNo,int cassetteSeq,int slideSeq)
        {
            OrderSlide order = new OrderSlide();

            return Ok(order.SlideExist(ordNo, cassetteSeq, slideSeq));
        }

        //GET api/OrderSlide/?ordNo=xxxx&cassetteSequence=yyy&slideSequence=zzz Entity Json
        public IHttpActionResult GetByOrdNoSeq(string ordNo, int cassetteSequence, int slideSequence)
        {
            OrderSlide order = new OrderSlide();
            OrderSlideM osm = order.GetSlidesByOrdNoSeq(ordNo,cassetteSequence,slideSequence);
            return Json<OrderSlideM>(osm);
        }

        //GET api/OrderSlide/?date=xxxx Entity Json
        public IHttpActionResult GetByOpDate(string date)
        {
            OrderSlide order = new OrderSlide();
            OrderSlideM[] osm =   order.GetSlidesByOpDate(date);
            return Json<OrderSlideM[]>(osm);
            //return Ok("ok123");
            //return Ok(osm);
        }

        //GET api/OrderSlide/?date=xxxx Entity Json
        public IHttpActionResult GetByDoctorDate(string doctorDate)
        {
            OrderSlide order = new OrderSlide();
            OrderSlideM[] osm = order.GetSlidesByDoctorDate(doctorDate);
            return Json<OrderSlideM[]>(osm);
        }

        //GET api/OrderCassette/?type=ggg&begDate=xxx&endDate=yyy&user=zzz Entity Json
        public IHttpActionResult GetDetail(string type, string begDate, string endDate, string user)
        {
            OrderSlide order = new OrderSlide();

            if (type == "slide")
            {
                return Json<OrderSlideM[]>(order.GetSlideDetail(begDate, endDate, user));
            }
            else if (type == "doctor")
            {
                return Json<OrderSlideM[]>(order.GetDoctorSlideDetail(begDate, endDate, user));
            }

            return NotFound();
        }
    }
}
