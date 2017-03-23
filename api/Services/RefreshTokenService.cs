using System;
using Core.Entities;
using Core.Interfaces;

namespace Services
{
    public class RefreshTokenService : SqlDataService<RefreshToken, IRepository<RefreshToken>>, IRefreshTokenService
    {
        public RefreshTokenService(IWebApplicationConfig config) : base(config)
        {}

        protected override void Initialize(out IRepository<RefreshToken> repository)
        {
            repository = SqlUnitOfWork.RefreshTokens;
        }

        public RefreshToken GetByToken(string token)
        {
            return Repository.Single(a => a.Token == token);
        }

        public RefreshToken GetByUserId(Guid userId)
        {
            return Repository.Single(a => a.UserId == userId);
        }

        public void Delete(RefreshToken item)
        {
            Repository.Delete(a => a.Id == item.Id);
        }

        void IRefreshTokenService.Add(RefreshToken item)
        {
            var existingToken = Single(a => a.UserId == item.UserId);
            if (existingToken != null)
            {
                Delete(existingToken);
            }
            Add(item);
            SqlUnitOfWork.SaveChanges();
        }
    }
}
