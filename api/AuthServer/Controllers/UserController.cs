using Core.Entities;
using Core.Interfaces;
using Services;

namespace AuthServer.Controllers
{
    public class UserController : DataApiController<User, SqlDataService<User, IRepository<User>>>
    {
        public UserController(SqlDataService<User, IRepository<User>> service) : base(service)
        {
        }
    }
}
