using System;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRefreshTokenService
    {
        void Add(RefreshToken item);
        RefreshToken GetByToken(string token);
        RefreshToken GetByUserId(Guid userId);
        void Delete(RefreshToken item);
    }
}
