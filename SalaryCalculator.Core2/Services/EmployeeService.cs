using Microsoft.EntityFrameworkCore;
using SalaryCalculator.Core.Contracts;
using SalaryCalculator.Core.Models;
using SalaryCalculator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext context;

        public EmployeeService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<IEnumerable<EmployeeViewModel>> GetAllAsync()
        {
            var entities = await context.Employees
                .Include(y => y.TaxYear).ToListAsync();

            return entities
                .Select(e => new EmployeeViewModel()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    GrossSalary = e.GrossSalary,
                    NetSalary = e.NetSalary,
                    TaxYear = e.TaxYear.Year
                });
        }

        public async Task AddEmployeeAsync(AddEmployeeViewModel employee)
        {
            var entity = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                GrossSalary = employee.GrossSalary,
                TaxYearId = employee.TaxYearId
            };

            var year = await context.TaxYears.FirstOrDefaultAsync(y => y.Id == employee.TaxYearId);

            if (entity.GrossSalary <= year.MinimumThreshold)
            {
                entity.NetSalary = entity.GrossSalary;
            }
            if(entity.GrossSalary > year.MinimumThreshold && entity.GrossSalary <= year.MaximumThreshold)
            {
                decimal currSalary = entity.GrossSalary - year.MinimumThreshold;
                decimal incomeTax = currSalary * year.IncomeТaxPercentage;
                decimal healthInsurance = currSalary * year.HealthInsurancePercentage;
                entity.NetSalary = entity.GrossSalary - (incomeTax + healthInsurance);
            }
            if (entity.GrossSalary > year.MaximumThreshold)
            {
                decimal currSalary = entity.GrossSalary - year.MinimumThreshold;
                decimal incomeTax = currSalary * year.IncomeТaxPercentage;
                decimal healthInsurance = (year.MaximumThreshold - year.MinimumThreshold) * year.HealthInsurancePercentage;
                entity.NetSalary = entity.GrossSalary - (incomeTax + healthInsurance);
            }
            await context.Employees.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaxYear>> GetYearsAsync()
        {
            return await context.TaxYears.ToListAsync();
        }

        public async Task AddYearAsync(AddYearViewModel year)
        {
            var entity = new TaxYear()
            {
                Year = year.Year,
                MinimumThreshold = year.MinimumThreshold,
                MaximumThreshold = year.MaximumThreshold,
                HealthInsurancePercentage = year.HealthInsurancePercentage,
                IncomeТaxPercentage = year.IncomeТaxPercentage
            };

            await context.TaxYears.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {

            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                throw new ArgumentException("Invalid employee Id");
            }

            context.Employees.Remove(employee);
            context.SaveChanges();
        }
    }
}
