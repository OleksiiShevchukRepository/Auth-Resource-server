using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
