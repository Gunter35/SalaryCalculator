using System.ComponentModel.DataAnnotations;

namespace SalaryCalculator.Infrastructure
{
    public class TaxYear
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

        public decimal IncomeТaxPercentage { get; set; }

        public decimal HealthInsurancePercentage { get; set; }

        public decimal MinimumThreshold { get; set; }

        public decimal MaximumThreshold { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
