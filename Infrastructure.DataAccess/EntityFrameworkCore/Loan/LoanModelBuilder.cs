using Core.Domain.Loan;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EntityFrameworkCore.Loan
{
    /// <summary>
    /// Loan Model Configuration
    /// </summary>
    public class LoanModelBuilder
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //Person
            modelBuilder.Entity<Person>(b =>
            {
                b.ToTable("LoanPerson");

                b.HasKey("Id");
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                
                b.Property(p => p.CI).HasMaxLength(11);
                b.Property(p => p.Name).HasMaxLength(150);
                b.Property(p => p.FatherLastName).HasMaxLength(50);
                b.Property(p => p.MotherLastName).HasMaxLength(50);
                b.Property(p => p.Phone).HasMaxLength(32);
                b.Property(p => p.Email).HasMaxLength(200);

                b.HasMany(p => p.Payments).WithOne(py => py.Person).HasForeignKey(py => py.PersonId).IsRequired();
            });

            //Payment
            modelBuilder.Entity<Payment>(b =>
            {
                b.ToTable("LoanPayment");
                
                b.HasKey("Id");
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
            });
        }
    }
}
