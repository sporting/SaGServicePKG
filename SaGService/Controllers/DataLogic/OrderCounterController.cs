using SaGLogic;
using SaGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaGService.Controllers
{
    public class OrderCounterController : ApiController
    {
        //Get api/OrderCounter/?head=xxx&yyyy=xxxx
        public IHttpActionResult GetNext(string head,string yyyy)
        {
            OrderCounter counter = new OrderCounter();
            string nextCount = counter.GetNextOrderCount(head, yyyy);

            if (string.IsNullOrEmpty(nextCount))
            {
                return NotFound();
            }
            else
            {
                return Ok(nextCount);
            }
        }

        //Get api/OrderCounter/?ordNo=xxx
        public IHttpActionResult GetExist(string headTag, string headYear, string tailSeq)
        {
            OrderCounter counter = new OrderCounter();
            
            if (!counter.GetExist( headTag,  headYear,  tailSeq))
            {
                return NotFound();
            }
            else
            {
                return Ok(true);
            }
        }
    }
}
