using Core.Entities;
using Core.Interfaces;

namespace Services
{
    public interface IUserService : IDataService<User>
    {
        User GetByCredentials(string email, string password);
    }
}