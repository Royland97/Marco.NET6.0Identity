using Core.Domain.Loan;

namespace UserInterface.Web.ViewModels.Loan
{
    public class LoanProfile: BaseProfile
    {
        public LoanProfile()
        {
            CreateMap<PersonModel, Person>();
            CreateMap<Person, PersonModel>();

            CreateMap<Payment, PaymentModel>();
            CreateMap<PaymentModel, Payment>();
        }
    }
}
