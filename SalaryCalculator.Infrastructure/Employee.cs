using System.ComponentModel.DataAnnotations;


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

        public ICollection<UserEmployee> UsersEmployees { get; set; } = new List<UserEmployee>();
    }
}
