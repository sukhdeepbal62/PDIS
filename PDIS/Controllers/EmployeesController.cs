using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PDIS.Data;
using PDIS.Models;

namespace PDIS.Controllers
{
    [Authorize(Roles = "hr")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<IdentityUser> _userManager;
        public EmployeesController(ApplicationDbContext context, IWebHostEnvironment env, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _environment = env;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Employees.Include(e => e.Department).Include(e => e.Post);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Post)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            ViewData["PostID"] = new SelectList(_context.Posts, "PostID", "PostName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,EmployeeName,FileUpload,JoiningDate,DepartmentID,PostID")] Employee employee)
        {
            ModelState.Remove("BasicSalary");
            ModelState.Remove("UserName");
            using (var memoryStream = new MemoryStream())
            {
                await employee.FileUpload.FormFile.CopyToAsync(memoryStream);
                string photoname = employee.FileUpload.FormFile.FileName;
                employee.ExtName = Path.GetExtension(photoname);
                if (!".jpg.jpeg.png.gif.bmp".Contains(employee.ExtName.ToLower()))
                {
                    ModelState.AddModelError("FileUpload.FormFile", "Invalid Format of Image Given.");
                }
                else
                {
                    ModelState.Remove("ExtName");
                }
            }

            if (ModelState.IsValid)
            {
                var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostID == employee.PostID);
                employee.BasicSalary = post.Salary;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                var rootFolder = Path.Combine(_environment.WebRootPath, "employees_photo");
                if (!Directory.Exists(rootFolder))
                {
                    Directory.CreateDirectory(rootFolder);
                }
                string filename = employee.EmployeeID + employee.ExtName;
                var filePath = Path.Combine(rootFolder, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await employee.FileUpload.FormFile.CopyToAsync(fileStream).ConfigureAwait(false);
                }
                string username = "P00" + employee.EmployeeID + "@pdis.com";
                string password = "Employee#5421";
                var user = new IdentityUser { UserName = username, Email = username, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    employee.UserName = username;
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", employee.DepartmentID);
            ViewData["PostID"] = new SelectList(_context.Posts, "PostID", "PostName", employee.PostID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", employee.DepartmentID);
            ViewData["PostID"] = new SelectList(_context.Posts, "PostID", "PostName", employee.PostID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,EmployeeName,ExtName,UserName,BasicSalary,JoiningDate,DepartmentID,PostID")] Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeID))
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
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", employee.DepartmentID);
            ViewData["PostID"] = new SelectList(_context.Posts, "PostID", "PostName", employee.PostID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Post)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
    }
}
