using System.Web.Http;

namespace patient_daily.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public string Index()
        {
            return "Silence is golden";
        }
    }
}
