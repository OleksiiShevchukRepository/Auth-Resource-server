using System;
using System.Web.Http;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNet.Identity;

namespace AuthServer.Controllers
{
    [RoutePrefix("User")]
    [Authorize]
    public class UserController : DataApiController<User, IUserService>
    {
        public UserController(IUserService service) : base(service)
        {
        }

        [Route("GetAllUsers")]
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            var result = Service.GetAll();
            return Ok(result);
        }

        [Route("GetCurrentUser")]
        [HttpGet]
        public IHttpActionResult GetCurrentUser()
        {
            var userId = User.Identity.GetUserId();
            var currentUser = Service.GetById(new Guid(userId));
            return currentUser != null ? Ok(currentUser) : (IHttpActionResult)BadRequest();
        }
    }
}