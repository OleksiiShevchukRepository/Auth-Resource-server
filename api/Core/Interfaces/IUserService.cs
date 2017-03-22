using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserService
    {
        User GetByCredentials(string email, string password);
    }
}