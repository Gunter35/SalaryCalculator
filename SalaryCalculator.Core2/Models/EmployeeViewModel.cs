using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Core.Models
{
    public class EmployeeViewModel
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

        [Required]
        public decimal? NetSalary { get; set; }

        public int TaxYear { get; set; }
    }
}
