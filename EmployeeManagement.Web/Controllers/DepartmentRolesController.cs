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
    public class DepartmentRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DepartmentRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.DepartmentRoles.ToListAsync());
        }

        // GET: DepartmentRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentRole = await _context.DepartmentRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentRole == null)
            {
                return NotFound();
            }

            return View(departmentRole);
        }

        // GET: DepartmentRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DepartmentRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] DepartmentRole departmentRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmentRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentRole);
        }

        // GET: DepartmentRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentRole = await _context.DepartmentRoles.FindAsync(id);
            if (departmentRole == null)
            {
                return NotFound();
            }
            return View(departmentRole);
        }

        // POST: DepartmentRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] DepartmentRole departmentRole)
        {
            if (id != departmentRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmentRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentRoleExists(departmentRole.Id))
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
            return View(departmentRole);
        }

        // GET: DepartmentRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentRole = await _context.DepartmentRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentRole == null)
            {
                return NotFound();
            }

            return View(departmentRole);
        }

        // POST: DepartmentRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departmentRole = await _context.DepartmentRoles.FindAsync(id);
            if (departmentRole != null)
            {
                _context.DepartmentRoles.Remove(departmentRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentRoleExists(int id)
        {
            return _context.DepartmentRoles.Any(e => e.Id == id);
        }
    }
}
