using System.Web.Http;

namespace ResourceServer.Controllers
{
    [RoutePrefix("Math")]
    [Authorize]
    public class MathController : ApiController
    {
        [HttpGet]
        [Route("Add")]
        public IHttpActionResult Add(int a, int b) => Ok(a + b);
    }
}
