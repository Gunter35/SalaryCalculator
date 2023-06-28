using SalaryCalculator.Infrastructure;

namespace SalaryCalculator.Core.Models
{
    public class AddEmployeeViewModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public decimal GrossSalary { get; set; }

        public int TaxYearId { get; set; }

        public IEnumerable<TaxYear> TaxYears { get; set; } = new List<TaxYear>();
    }
}
