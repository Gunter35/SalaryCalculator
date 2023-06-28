using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Infrastructure
{
    public class User : IdentityUser
    {
        public ICollection<UserEmployee> UsersEmployees { get; set; } = new List<UserEmployee>();
    }
}
