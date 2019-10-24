using SaGKernel.Specimen;
using SaGLogic;
using SaGModel;
using SaGService.PreLoader;
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
    public class SpecimenCollectionController : ApiController
    {
        //POST api/SpecimenCollection Entity Json
        public SpecimenCollection GetAll()
        {
            return SpecimenLoader.GetInstance().Specimens;
        }     
    }
}
