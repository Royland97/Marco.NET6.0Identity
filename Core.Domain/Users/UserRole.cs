using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents the association between User and Role
    /// </summary>
    public class UserRole: IdentityUserRole<string>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
