using System.ComponentModel.DataAnnotations;

namespace UserInterface.Web.ViewModels.Loan
{
    public class PaymentModel
    {
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int Period { get; set; }

        [Required()]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Person Id")]
        public int PersonId { get; set; }
    }
}
