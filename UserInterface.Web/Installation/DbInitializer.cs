using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace UserInterface.Web.Installation
{
    /// <summary>
    /// Initialize Db and Populate it with data
    /// </summary>
    public class DbInitializer
    {
        public async static Task Initialize(ApplicationDbContext applicationDbContext)
        {
            if( await applicationDbContext.Database.EnsureCreatedAsync())
            {
                //User
                var adminRole = applicationDbContext.Roles.FirstOrDefault(r => r.Name == Role.Admin);
                var adminUser = applicationDbContext.Users.FirstOrDefault(u => u.Email == "admin@example.com");

                if (adminRole == null && adminUser == null)
                    return;

                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(adminUser, "Admin123.");
                adminUser.PasswordHash = hashed;

                applicationDbContext.Users.Update(adminUser);
                await applicationDbContext.UserRoles.AddAsync(new UserRole { User = adminUser, Role = adminRole });

                await applicationDbContext.SaveChangesAsync();
            };
        }

    }
}
