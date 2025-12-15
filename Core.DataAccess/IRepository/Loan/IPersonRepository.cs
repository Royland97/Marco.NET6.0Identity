using Core.Domain.Loan;

namespace Core.DataAccess.IRepository.Loan
{
    public interface IPersonRepository: IGenericRepository<Person>
    {
        /// <summary>
        /// Find a Person by it's Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Person FindPersonByName(string name);
    }
}
