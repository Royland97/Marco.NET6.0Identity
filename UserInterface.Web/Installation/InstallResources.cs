using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using UserInterface.Web.Authorization;

namespace UserInterface.Web.Installation
{
    /// <summary>
    /// Install all Resources in the application
    /// </summary>
    public class InstallResources   
    {
        private readonly IResourceRepository _resourceRepository;

        public InstallResources(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task InstallAsync()
        {
            await InstallUsersResources();
        }

        /// <summary>
        /// Install the User's Resource in the application
        /// </summary>
        /// <returns></returns>
        public async Task InstallUsersResources()
        {
            List<Resource> resources = new()
            {
                new Resource
                {
                    Name = ResourcesNames.CreateUser,
                    Description = "Creates a new User"
                },
                new Resource
                {
                    Name = ResourcesNames.UpdateUser,
                    Description = "Updates an User"
                },
                new Resource
                {
                    Name = ResourcesNames.DeleteUser,
                    Description = "Deletes an User"
                },
                new Resource
                {
                    Name = ResourcesNames.GetUserById,
                    Description = "Gets an User by it's Id"
                },
                new Resource
                {
                    Name = ResourcesNames.GetAllUsers,
                    Description = "Gets all Users"
                }
            };

            await _resourceRepository.SaveAllResourcesAsync(resources);
        }
    }
}
