using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;

namespace UserInterface.Web.Installation
{
    /// <summary>
    /// Initialize Db and Populate it with data
    /// </summary>
    public class DbInitializer
    {
        private readonly IInstallResources _installResources;

        public DbInitializer(IInstallResources installResources)
        {
            _installResources = installResources;
        }

        public async static Task Initialize(ApplicationDbContext applicationDbContext)
        {
            if (await applicationDbContext.Database.EnsureCreatedAsync()) 
            {
                //await _installResources.InstallAsync();

                //User
                var adminRole = await applicationDbContext.Roles.FindAsync("Admin");
                var adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@test.cu",
                    PhoneNumber = "1231244234",
                    Active = true
                };

                await applicationDbContext.UserRoles.AddAsync(new UserRole { User = adminUser, Role = adminRole});

                await applicationDbContext.Users.AddAsync(adminUser);
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
