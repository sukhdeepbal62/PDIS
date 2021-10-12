using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PDIS.Data;
using PDIS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PDIS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MyDetails()
        {
            string userid = _userManager.GetUserName(this.User);
            var employee = await _context.Employees
                .Include(j => j.Department)
                .Include(j => j.Post)
                .FirstOrDefaultAsync(m => m.UserName == userid);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> ViewSalaryDetails()
        {
            string userid = _userManager.GetUserName(this.User);
            var employee = await _context.Employees
                .Include(j => j.Department)
                .Include(j => j.Post)
                .FirstOrDefaultAsync(m => m.UserName == userid);
            if (employee == null)
            {
                return NotFound();
            }
            var salaries = _context.Salaries.Include(s => s.Employee)
                .Where(s => s.EmployeeID == employee.EmployeeID);
            return View(salaries);
        }

        public async Task<IActionResult> ViewSalarySlip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salaries
                .Include(s => s.Employee)
                .Include(s => s.SalaryDetails)
                .FirstOrDefaultAsync(m => m.SalaryID == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
