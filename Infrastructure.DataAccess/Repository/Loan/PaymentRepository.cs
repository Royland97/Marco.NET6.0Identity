using Core.DataAccess.IRepository.Loan;
using Core.Domain.Loan;
using Infrastructure.DataAccess.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Loan
{
    /// <summary>
    /// Payment Repository
    /// </summary>
    public class PaymentRepository: GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) :
            base(context)
        {
        }
    }
}
