using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Core.Models
{
    public class AddYearViewModel
    {
        public int Year { get; set; }

        public decimal IncomeТaxPercentage { get; set; }

        public decimal HealthInsurancePercentage { get; set; }

        public decimal MinimumThreshold { get; set; }

        public decimal MaximumThreshold { get; set; }
    }
}
