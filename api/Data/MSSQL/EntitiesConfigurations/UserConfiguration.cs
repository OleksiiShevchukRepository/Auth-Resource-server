using System.Data.Entity.ModelConfiguration;
using Core.Entities;

namespace Data.MSSQL.EntitiesConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(a => a.Id);
            this.Property(a => a.FirstName).IsRequired();
            this.Property(a => a.LastName).IsRequired();
            this.Property(a => a.Email).IsRequired();
            this.Property(a => a.Password).IsRequired();

            HasMany(a => a.RefreshTokens).WithRequired(a => a.User).HasForeignKey(a => a.UserId);
        }
    }
}
