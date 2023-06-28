using Microsoft.AspNetCore.Mvc;
using SalaryCalculator.Core.Contracts;
using SalaryCalculator.Core.Models;

namespace SalaryCalculator.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
                var model = await employeeService.GetAllAsync();

                return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
                var model = new AddEmployeeViewModel()
                {
                    TaxYears = await employeeService.GetYearsAsync()
                };

                return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await employeeService.AddEmployeeAsync(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddYear()
        {
            var model = new AddYearViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddYear(AddYearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await employeeService.AddYearAsync(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }
        public async Task<IActionResult> Delete(int Id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "User");
            }

            await employeeService.DeleteAsync(Id);
            return RedirectToAction(nameof(All));



        }


    }
}
