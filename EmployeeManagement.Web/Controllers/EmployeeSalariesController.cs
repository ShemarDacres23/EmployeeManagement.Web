using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Web.Data;
using EmployeeManagement.Web.Models;

namespace EmployeeManagement.Web.Controllers
{
    public class EmployeeSalariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeSalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeSalaries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeeSalaries.Include(e => e.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmployeeSalaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSalary = await _context.EmployeeSalaries
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeSalary == null)
            {
                return NotFound();
            }

            return View(employeeSalary);
        }

        // GET: EmployeeSalaries/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            return View();
        }

        // POST: EmployeeSalaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,BasicSalary,Bonus,PaymentDate,Description")] EmployeeSalary employeeSalary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeSalary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Log model errors if save failed
            foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(modelError.ErrorMessage);
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", employeeSalary.EmployeeId);
            return View(employeeSalary);
        }


        // GET: EmployeeSalaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSalary = await _context.EmployeeSalaries.FindAsync(id);
            if (employeeSalary == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", employeeSalary.EmployeeId);
            return View(employeeSalary);
        }

        // POST: EmployeeSalaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,BasicSalary,Bonus,PaymentDate,Description")] EmployeeSalary employeeSalary)
        {
            if (id != employeeSalary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeSalary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeSalaryExists(employeeSalary.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", employeeSalary.EmployeeId);
            return View(employeeSalary);
        }

        // GET: EmployeeSalaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSalary = await _context.EmployeeSalaries
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeSalary == null)
            {
                return NotFound();
            }

            return View(employeeSalary);
        }

        // POST: EmployeeSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeSalary = await _context.EmployeeSalaries.FindAsync(id);
            if (employeeSalary != null)
            {
                _context.EmployeeSalaries.Remove(employeeSalary);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeSalaryExists(int id)
        {
            return _context.EmployeeSalaries.Any(e => e.Id == id);
        }
    }
}
