using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Users
{
    /// <summary>
    /// User Repository
    /// </summary>
    public class UserRepository: GenericRepository<User> ,IUserRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ApplicationDbContext context):
            base(context)
        {
        }

    }
}
