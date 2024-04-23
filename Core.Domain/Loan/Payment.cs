using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Loan
{
    /// <summary>
    /// Represent a Payment Loan
    /// </summary>
    public class Payment: Entity
    {
        #region Fields

        [Required]
        public int Amount { get; set; }

        [Required]
        public int Period { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }

        #endregion
    }
}
