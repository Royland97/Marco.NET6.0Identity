using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Loan
{
    /// <summary>
    /// Represent a Person Loan
    /// </summary>
    public class Person: Entity
    {
        #region Fields

        [Required]
        public string CI { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FatherLastName { get; set; }

        [Required]
        public string MotherLastName { get; set; }

        [Required]
        public float AmountBorrowed { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime LoanDate { get; set; }
        public int ChargeDate { get; set; }
        public int LoanMonths { get; set; }
        public int Interest { get; set; }

        #endregion

        #region Relations

        /// <summary>
        /// Gets the Payments of the Loan
        /// </summary>
        public virtual ICollection<Payment> Payments { get; set; }

        #endregion
    }
}
