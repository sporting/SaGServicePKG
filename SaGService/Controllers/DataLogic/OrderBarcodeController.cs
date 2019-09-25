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
    public class OrderBarcodeController : ApiController
    {
        //有沒有這個 檢體編號

        //GET api/OrderBarcode/?ordNo=xxxx Entity Json
        public IHttpActionResult GetExist(string ordNo)
        {
            OrderBarcode order = new OrderBarcode();           
            
            return Ok(order.OrdNoExist(ordNo));
        }
    }
}
