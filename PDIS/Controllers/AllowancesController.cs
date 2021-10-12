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
    public class AllowancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllowancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Allowances
        public async Task<IActionResult> Index()
        {
            return View(await _context.Allowances.ToListAsync());
        }

        // GET: Allowances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowance = await _context.Allowances
                .FirstOrDefaultAsync(m => m.AllowanceID == id);
            if (allowance == null)
            {
                return NotFound();
            }

            return View(allowance);
        }

        // GET: Allowances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Allowances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AllowanceID,ShortName,AllowanceName,Percentage")] Allowance allowance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allowance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allowance);
        }

        // GET: Allowances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowance = await _context.Allowances.FindAsync(id);
            if (allowance == null)
            {
                return NotFound();
            }
            return View(allowance);
        }

        // POST: Allowances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AllowanceID,ShortName,AllowanceName,Percentage")] Allowance allowance)
        {
            if (id != allowance.AllowanceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allowance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllowanceExists(allowance.AllowanceID))
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
            return View(allowance);
        }

        // GET: Allowances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowance = await _context.Allowances
                .FirstOrDefaultAsync(m => m.AllowanceID == id);
            if (allowance == null)
            {
                return NotFound();
            }

            return View(allowance);
        }

        // POST: Allowances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allowance = await _context.Allowances.FindAsync(id);
            _context.Allowances.Remove(allowance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllowanceExists(int id)
        {
            return _context.Allowances.Any(e => e.AllowanceID == id);
        }
    }
}
