using SalaryCalculator.Core.Models;
using SalaryCalculator.Infrastructure;

namespace SalaryCalculator.Core.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> GetAllAsync();
        Task<IEnumerable<TaxYear>> GetYearsAsync();
        Task AddEmployeeAsync(AddEmployeeViewModel employee);
        Task AddYearAsync(AddYearViewModel year);
        Task DeleteAsync(int id);
    }
}
