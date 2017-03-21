using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ResourceServer.Controllers
{
    [Authorize]
    [RoutePrefix("Math")]
    public class MathController : ApiController
    {
        [HttpGet]
        [Route("Add")]
        public IHttpActionResult Add(int a, int b) => Ok(a + b);
    }
}
