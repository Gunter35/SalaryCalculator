using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalaryCalculator.Infrastructure
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        

        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; } = null!;
        
        [Required]
        public decimal GrossSalary { get; set; }

        public decimal? NetSalary { get; set; }

        [Required]
        [ForeignKey(nameof(TaxYear))]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; } = null!;

        public ICollection<UserEmployee> UsersEmployees { get; set; } = new List<UserEmployee>();
    }
}
