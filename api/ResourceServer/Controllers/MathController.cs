using System.Collections.Generic;
using System.Web.Http;

namespace ResourceServer.Controllers
{
    [RoutePrefix("Math")]
    public class MathController : ApiController
    {
        private readonly 

        public MathController()
        {
            _strings = _strings ?? new List<string>() {"1", "2", "3"};
        }
        [HttpGet]
        [Route("Add")]
        public IHttpActionResult Add(int a, int b) => Ok(a + b);

        [HttpGet]
        [Route("Add")]
        public IHttpActionResult Count() => Ok(a + b);
    }
}
