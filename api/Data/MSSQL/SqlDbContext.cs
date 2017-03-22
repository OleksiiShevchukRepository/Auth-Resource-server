using System.Data.Entity;
using Core.Entities;
using Core.Interfaces;

namespace Data.MSSQL
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() : base("name=SqlUsers")
        {}

        public SqlDbContext(string connectionString) : base($"name={connectionString}")
        {}

        public SqlDbContext(IWebApplicationConfig config) : base($"name={config.SqlDbConnectionString}")
        {}

        static SqlDbContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SqlDbContext>());
        }

        public virtual DbSet<User> Users{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(a => a.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
