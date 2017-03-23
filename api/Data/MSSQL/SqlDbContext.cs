using System.Data.Entity;
using Core.Entities;
using Core.Interfaces;
using Data.MSSQL.EntitiesConfigurations;

namespace Data.MSSQL
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() : base("name=SqlUsersResourceServer")
        {}

        public SqlDbContext(string connectionString) : base($"name={connectionString}")
        {}

        public SqlDbContext(IWebApplicationConfig config) : base($"name={config.SqlDbName}")
        {}

        static SqlDbContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SqlDbContext>());
        }

        public virtual DbSet<User> Users{ get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RefreshTokenConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}