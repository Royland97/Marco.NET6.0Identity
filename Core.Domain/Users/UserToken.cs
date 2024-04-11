using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents the User authentication token
    /// </summary>
    public class UserToken: IdentityUserToken<string>
    {
        public virtual User User { get; set; }
    }
}