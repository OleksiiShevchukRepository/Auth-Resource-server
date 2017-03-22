using Core.Entities;
using Core.Interfaces;

namespace Services
{
    internal class UserService : SqlDataService<User, IRepository<User>>, IUserService
    {
        public UserService(IWebApplicationConfig config) : base(config)
        {}

        protected override void Initialize(out IRepository<User> repository)
        {
            repository = Context.Users;
        }

        public User GetByCredentials(string email, string password)
        {
            return Repository.Single(a => a.Email == email && a.Password == password);
        }
    }
}