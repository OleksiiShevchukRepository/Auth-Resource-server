using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ResourceServer.Controllers
{
    [RoutePrefix("Math")]
    public class MathController : ApiController
    {
        private static List<string> _strings;

        public MathController()
        {
            _strings = _strings ?? new List<string>() {"1", "2", "3"};
        }
        [HttpGet]
        [Route("Add")]
        public IHttpActionResult Add(int a, int b) => Ok(a + b);
    }
}
