using System.Web.Http;

namespace ResourceServer.Controllers
{
    public class MeController : ApiController
    {
        [Authorize]
        public string Get()
        {
            return this.User.Identity.Name;
        }
    }
}
