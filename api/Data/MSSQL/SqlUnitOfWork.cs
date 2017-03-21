using System;

namespace Data.MSSQL
{
    public class SqlUnitOfWork : IDisposable
    {
        private readonly SqlDbContext _sqlDbContext;

        public SqlUnitOfWork(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public void SaveChanges()
        {
            _sqlDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _sqlDbContext?.Dispose();
        }
    }
}
