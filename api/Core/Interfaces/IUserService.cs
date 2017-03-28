using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserService : IDataService<User>
    {
        User GetByCredentials(string email, string password);
    }
}