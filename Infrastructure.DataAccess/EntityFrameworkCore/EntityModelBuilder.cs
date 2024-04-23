using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EntityFrameworkCore
{
    /// <summary>
    /// Entity Model Configuration
    /// </summary>
    public class EntityModelBuilder
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>(b =>
            {
                b.HasKey("Id");
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
            });
        }
    }
}
