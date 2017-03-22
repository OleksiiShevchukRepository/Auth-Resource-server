using Core.Entities;

namespace Core.Interfaces
{
    public interface ISqlUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<RefreshToken> RefreshTokens { get; }
    }
}