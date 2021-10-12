using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PDIS.Data;
using PDIS.Models;

namespace PDIS.Controllers
{
    [Authorize(Roles = "hr")]
    public class SalariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Salaries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Salaries.Include(s => s.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
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

        public List<string> GetMonthList()
        {
            List<string> monthnames = new List<string>();
            monthnames.Add("January");
            monthnames.Add("February");
            monthnames.Add("March");
            monthnames.Add("April");
            monthnames.Add("May");
            monthnames.Add("June");
            monthnames.Add("July");
            monthnames.Add("August");
            monthnames.Add("September");
            monthnames.Add("October");
            monthnames.Add("November");
            monthnames.Add("December");
            return monthnames;
        }

        public List<int> GetYearList()
        {
            List<int> years = new List<int>();
            int year = DateTime.Now.Year;
            while (year >= 2010)
            {
                years.Add(year);
                year--;
            }
            return years;
        }

        // GET: Salaries/Create
        public IActionResult Create()
        {
            ViewData["MonthNames"] = new SelectList(GetMonthList());
            ViewData["Years"] = new SelectList(GetYearList());
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeName");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaryID,SalaryMonth,SalaryYear,EmployeeID")] Salary salary)
        {
            ModelState.Remove("TotalSalary");
            if (ModelState.IsValid)
            {
                var records = _context.Salaries.
                    Where(j => j.EmployeeID == salary.EmployeeID && j.SalaryMonth == salary.SalaryMonth && j.SalaryYear == salary.SalaryYear);
                if (records.Count() > 0)
                {
                    ModelState.AddModelError("EmployeeID", "You Already Generate This Employee Salary for this Period");
                }
                else
                {
                    var employee = _context.Employees.Find(salary.EmployeeID);
                    salary.TotalSalary = 0;
                    _context.Add(salary);
                    _context.SaveChanges();
                    salary.TotalSalary += employee.BasicSalary;
                    SalaryDetail detail = new SalaryDetail();
                    detail.Amount = employee.BasicSalary;
                    detail.AllowanceName = "Basic Salary";
                    detail.SalaryID = salary.SalaryID;
                    _context.Add(detail);
                    //_context.SaveChanges();
                    var allowances = _context.Allowances;
                    foreach (var allowance in allowances)
                    {
                        float amount = employee.BasicSalary / 100 * allowance.Percentage;
                        salary.TotalSalary += amount;
                        detail = new SalaryDetail();
                        detail.Amount = amount;
                        detail.AllowanceName = allowance.AllowanceName;
                        detail.SalaryID = salary.SalaryID;
                        _context.Add(detail);
                        //_context.SaveChanges();
                    }
                    _context.Update(salary);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeName", salary.EmployeeID);
            ViewData["MonthNames"] = new SelectList(GetMonthList(), salary.SalaryMonth);
            ViewData["Years"] = new SelectList(GetYearList(), salary.SalaryYear);
            return View(salary);
        }

        // GET: Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeName", salary.EmployeeID);
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalaryID,SalaryMonth,SalaryYear,TotalSalary,EmployeeID")] Salary salary)
        {
            if (id != salary.SalaryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryExists(salary.SalaryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeName", salary.EmployeeID);
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.SalaryID == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(int id)
        {
            return _context.Salaries.Any(e => e.SalaryID == id);
        }
    }
}
