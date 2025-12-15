using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents a Claim granted to all users within a role
    /// </summary>
    public class RoleClaim: IdentityRoleClaim<string>
    {
        public virtual Role Role { get; set; }
    }
}
