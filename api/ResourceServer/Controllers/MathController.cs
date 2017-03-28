using System.Web.Http;
using Core.Entities;
using Core.Interfaces;

namespace ResourceServer.Controllers
{
    [RoutePrefix("Math")]
    [Authorize]
    public class MathController : DataApiController<Event, IEventService>
    {
        public MathController(IEventService service) : base(service)
        {
        }

        [HttpGet]
        [Route("Add")]
        public IHttpActionResult Add(int a, int b) => Ok(a + b);
    }
}