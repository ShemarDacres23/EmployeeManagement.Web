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
    public class EmployeeLeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeLeaveApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeLeaveApplications
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeLeaveApplications.ToListAsync());
        }

        // GET: EmployeeLeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeLeaveApplication = await _context.EmployeeLeaveApplications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeLeaveApplication == null)
            {
                return NotFound();
            }

            return View(employeeLeaveApplication);
        }

        // GET: EmployeeLeaveApplications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeLeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,LeaveType,StartDate,EndDate,Description,Status")] EmployeeLeaveApplication employeeLeaveApplication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeLeaveApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeLeaveApplication);
        }

        // GET: EmployeeLeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeLeaveApplication = await _context.EmployeeLeaveApplications.FindAsync(id);
            if (employeeLeaveApplication == null)
            {
                return NotFound();
            }
            return View(employeeLeaveApplication);
        }

        // POST: EmployeeLeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,LeaveType,StartDate,EndDate,Description,Status")] EmployeeLeaveApplication employeeLeaveApplication)
        {
            if (id != employeeLeaveApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeLeaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeLeaveApplicationExists(employeeLeaveApplication.Id))
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
            return View(employeeLeaveApplication);
        }

        // GET: EmployeeLeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeLeaveApplication = await _context.EmployeeLeaveApplications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeLeaveApplication == null)
            {
                return NotFound();
            }

            return View(employeeLeaveApplication);
        }

        // POST: EmployeeLeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeLeaveApplication = await _context.EmployeeLeaveApplications.FindAsync(id);
            if (employeeLeaveApplication != null)
            {
                _context.EmployeeLeaveApplications.Remove(employeeLeaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeLeaveApplicationExists(int id)
        {
            return _context.EmployeeLeaveApplications.Any(e => e.Id == id);
        }
    }
}
