using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRSystem.Infrastructure.Configration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Entities.Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Primary Key
            builder.HasKey(e => e.EmployeeId);


            builder.Property(e => e.EmployeeCode)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.HasIndex(e => e.EmployeeCode).IsUnique();


            builder.Property(e => e.FullName)
                   .IsRequired()
                   .HasMaxLength(150);
            builder.HasIndex(e => e.FullName).IsUnique();


            builder.Property(e => e.Qualification)
                   .HasMaxLength(50);


            builder.HasMany(e => e.Vacations)
                   .WithOne(v => v.Employee)
                   .HasForeignKey(v => v.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
