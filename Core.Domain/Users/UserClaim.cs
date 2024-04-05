using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represent an User Claim
    /// </summary>
    public class UserClaim: IdentityUserClaim<string>
    {
        public virtual User User { get; set; }
    }
}
