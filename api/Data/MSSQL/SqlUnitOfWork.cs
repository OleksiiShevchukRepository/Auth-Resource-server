using System;
using Core.Entities;
using Core.Interfaces;

namespace Data.MSSQL
{
    public class SqlUnitOfWork : IDisposable, ISqlUnitOfWork
    {
        private readonly SqlDbContext _sqlDbContext;
        private IRepository<User> _users;
        private IRepository<RefreshToken> _refreshTokens;

        private readonly IWebApplicationConfig _config;
        public SqlUnitOfWork(IWebApplicationConfig config)
        {
            _config = config;
            _sqlDbContext = new SqlDbContext(_config);
        }

        public void SaveChanges()
        {
            _sqlDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _sqlDbContext?.Dispose();
        }

        public IRepository<User> Users => _users ?? new SqlRepository<User>(_sqlDbContext);

        public IRepository<RefreshToken> RefreshTokens
            => _refreshTokens ?? new SqlRepository<RefreshToken>(_sqlDbContext);
    }
}
