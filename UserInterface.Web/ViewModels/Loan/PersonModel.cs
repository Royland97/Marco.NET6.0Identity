using System.ComponentModel.DataAnnotations;

namespace UserInterface.Web.ViewModels.Loan
{
    public class PersonModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(11)]
        public string CI { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string FatherLastName { get; set; }

        [Required]
        [StringLength(50)]
        public string MotherLastName { get; set; }

        [Required]
        public float AmountBorrowed { get; set; }

        [StringLength(32)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LoanDate { get; set; }
        public int ChargeDate { get; set; }
        public int LoanMonths { get; set; }
        public int Interest { get; set; }
    }
}
