using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Microsoft.AspNetCore.Authorization;

namespace UserInterface.Web.Authorization
{
    /// <summary>
    /// AuthorizationHandler
    /// </summary>
    public class ResourceAuthorizationHandler: AuthorizationHandler<ResourceAuthorizationRequirement, string>
    {
        private readonly IUserRepository _userRepository;

        public ResourceAuthorizationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAuthorizationRequirement requirement, string resource)
        {
            if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
            {
                User user = await _userRepository.GetUserByNameAsync(context.User.Identity.Name);

                if (user != null)
                    if (user.Roles.Any(role => role.Resources.Any(resource => resource.Name.Equals(resource))))
                        context.Succeed(requirement);
            }
        }
    }
}
