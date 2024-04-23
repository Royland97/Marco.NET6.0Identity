using Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EntityFrameworkCore.Users
{
    /// <summary>
    /// Users Model Configuration
    /// </summary>
    public class UsersModelBuilder
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>(b => 
            {
                b.HasMany(u => u.Claims).WithOne(uc => uc.User).HasForeignKey(uc => uc.UserId).IsRequired();
                b.HasMany(u => u.Logins).WithOne(ul => ul.User).HasForeignKey(uc => uc.UserId).IsRequired();
                b.HasMany(u => u.Tokens).WithOne(ut => ut.User).HasForeignKey(uc => uc.UserId).IsRequired();
                b.HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId).IsRequired();
            });
            
            //Role
            modelBuilder.Entity<Role>(b =>
            {
                b.HasMany(r => r.RoleClaims).WithOne(rc => rc.Role).HasForeignKey(rc => rc.RoleId).IsRequired();
                b.HasMany(r => r.UserRoles).WithOne(ur => ur.Role).HasForeignKey(ur => ur.RoleId).IsRequired();
            });
            
            //Resource
            modelBuilder.Entity<Resource>(b =>
            {
                b.ToTable("AspNetResources");
                
                b.Property(r => r.Name).HasMaxLength(256);
                b.Property(r => r.Description).HasMaxLength(256);

                b.HasMany(r => r.Roles).WithMany(r => r.Resources).UsingEntity("AspNetRoleResource");
            });

        }
    }
}
