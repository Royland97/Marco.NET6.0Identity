using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents the User Login
    /// </summary>
    public class UserLogin: IdentityUserLogin<string>
    {
        public virtual User User { get; set; }
    }
}
