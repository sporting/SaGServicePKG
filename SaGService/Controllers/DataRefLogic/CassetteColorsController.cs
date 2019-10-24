using SaGKernel.UI;
using System.Web.Http;

namespace SaGService.Controllers
{
    public class CassetteColorsController : ApiController
    {
        //POST api/CassetteColors Entity Json
        public CassetteColors GetAll()
        {
            return CassetteColors.GetInstance();
        }     
    }
}
