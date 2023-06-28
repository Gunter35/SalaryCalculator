using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SalaryCalculator.Core.Models;
using SalaryCalculator.Core.Services;
using SalaryCalculator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.UnitTests
{
    public class EmployeeServiceTest
    {
        [Test]
        public async Task AddYear_Method_Should_Add_TaxYear_To_The_Db()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var employeeService = new EmployeeService(dbContext);

            var taxYear = new AddYearViewModel()
            {
                Year = 2023,
                MaximumThreshold = 3000,
                MinimumThreshold = 1000,
                IncomeТaxPercentage = (decimal)0.1,
                HealthInsurancePercentage = (decimal)0.15
            };
            await employeeService.AddYearAsync(taxYear);

            Assert.AreEqual(1, dbContext.TaxYears.Count());
        }
        [Test]
        public async Task Add_Method_Should_Add_Employee_To_The_Db()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var employeeService = new EmployeeService(dbContext);

            var taxYear = new AddYearViewModel()
            {
                Year = 2023,
                MaximumThreshold = 3000,
                MinimumThreshold = 1000,
                IncomeТaxPercentage = (decimal)0.1,
                HealthInsurancePercentage = (decimal)0.15
            };
            await employeeService.AddYearAsync(taxYear);

            var employee = new AddEmployeeViewModel()
            {
                FirstName = "Peter",
                LastName = "Parker",
                GrossSalary = 5000,
                TaxYearId = 1
            };
            await employeeService.AddEmployeeAsync(employee);

            Assert.AreEqual(1, dbContext.Employees.Count());
        }

        [Test]
        public async Task CheckingIfGetAllWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var employeeService = new EmployeeService(dbContext);

            var taxYear = new AddYearViewModel()
            {
                Year = 2023,
                MaximumThreshold = 3000,
                MinimumThreshold = 1000,
                IncomeТaxPercentage = (decimal)0.1,
                HealthInsurancePercentage = (decimal)0.15
            };
            await employeeService.AddYearAsync(taxYear);

            var employee = new Employee()
            {
                FirstName = "Peter",
                LastName = "Parker",
                GrossSalary = 5000,
                TaxYearId = 1
            };

            dbContext.Employees.Add(employee);

            var result = await employeeService.GetAllAsync();

            Assert.AreEqual(dbContext.Employees.Count(), result.Count());
        }

        [Test]
        public async Task CheckingIfGetGenresWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var employeeService = new EmployeeService(dbContext);

            var year = new TaxYear()
            {
                Year = 2023,
                IncomeТaxPercentage= (decimal)0.1,
                HealthInsurancePercentage= (decimal)0.15,
                MinimumThreshold= 1000,
                MaximumThreshold = 3000
            };
            var year2 = new TaxYear()
            {
                Year = 2022,
                IncomeТaxPercentage = (decimal)0.15,
                HealthInsurancePercentage = (decimal)0.2,
                MinimumThreshold = 900,
                MaximumThreshold = 2500
            };

            dbContext.TaxYears.Add(year);
            dbContext.TaxYears.Add(year2);


            var result = await employeeService.GetYearsAsync();

            Assert.AreEqual(dbContext.TaxYears.Count(), result.Count());
        }

        [Test]
        public async Task CheckingIfDeleteWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var employeeService = new EmployeeService(dbContext);


            var taxYear = new AddYearViewModel()
            {
                Year = 2023,
                MaximumThreshold = 3000,
                MinimumThreshold = 1000,
                IncomeТaxPercentage = (decimal)0.1,
                HealthInsurancePercentage = (decimal)0.15
            };
            await employeeService.AddYearAsync(taxYear);

            var employee = new AddEmployeeViewModel()
            {
                FirstName = "Peter",
                LastName = "Parker",
                GrossSalary = 5000,
                TaxYearId = 1
            };
            await employeeService.AddEmployeeAsync(employee);

            Assert.AreEqual(1, dbContext.Employees.Count());

            await employeeService.DeleteAsync(dbContext.Employees.First().Id);

            Assert.AreEqual(0, dbContext.Employees.Count());
        }
    }
}
