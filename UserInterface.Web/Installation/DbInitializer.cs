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
        private readonly InstallResources _installResources;
        private readonly IResourceRepository _resourceRepository;

        public DbInitializer(InstallResources installResources, IResourceRepository resourceRepository)
        {
            _installResources = installResources;
            _resourceRepository = resourceRepository;
        }

        public static void Initialize(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Database.EnsureCreated();
            
            //_installResources.InstallAsync();
            
            //User
            var adminRole = applicationDbContext.Roles.Find("Admin");
            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@test.cu",
                PhoneNumber = "1231244234",
                Active = true
            };

            applicationDbContext.UserRoles.Add(new UserRole { User = adminUser, Role = adminRole});

            applicationDbContext.Users.Add(adminUser);
            applicationDbContext.SaveChanges();
        }
    }
}
