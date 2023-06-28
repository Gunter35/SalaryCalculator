using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserEmployee> UsersEmployees { get; set; }
        public DbSet<TaxYear> TaxYears { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEmployee>()
                .HasKey(x => new { x.UserId, x.EmployeeId });

            builder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(60)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Entity<TaxYear>()
                .HasData(new TaxYear()
                {
                    Id = 1,
                    Year = 2023,
                    IncomeТaxPercentage = (decimal)0.1,
                    HealthInsurancePercentage = (decimal)0.15,
                    MinimumThreshold = 1000,
                    MaximumThreshold = 3000,
                });
            base.OnModelCreating(builder);
        }
    }
}
