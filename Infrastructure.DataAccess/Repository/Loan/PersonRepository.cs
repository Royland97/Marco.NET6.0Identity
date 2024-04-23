using Core.DataAccess.IRepository.Loan;
using Core.Domain.Loan;
using Infrastructure.DataAccess.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Loan
{
    /// <summary>
    /// Person Repository
    /// </summary>
    public class PersonRepository: GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context):
            base(context)
        {
        }

        /// <summary>
        /// Find a Person by it's Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Person FindPersonByName(string name)
        {
            if(string.IsNullOrEmpty(name))
                return null;

            return context.Persons.FirstOrDefault(x => x.Name == name);
        }
    }
}
