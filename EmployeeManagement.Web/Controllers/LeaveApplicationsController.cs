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
    public class LeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.ApplicationStatus)
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.ApplicationStatus)
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public async Task<IActionResult> Create()
        {
            // Make the async call to the database for dropdowns
            var durationList = await _context.SystemCodeDetails
                                              .Include(x => x.SystemCode)
                                              .Where(y => y.SystemCode.Code == "LeaveDuration")
                                              .ToListAsync();
            var employeeList = await _context.Employees.ToListAsync();
            var leaveTypeList = await _context.LeaveTypes.ToListAsync();

            // Pass the results to ViewData for the dropdown lists
            ViewData["DurationId"] = new SelectList(durationList, "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(employeeList, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(leaveTypeList, "Id", "Name");
            return View();
        }

        // POST: LeaveApplications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveApplication leaveApplication)
        {
            // Fetch the "Pending" status
            var pendingStatus = await _context.SystemCodeDetails
                                              .Include(x => x.SystemCode)
                                              .Where(y => y.SystemCode.Code == "Pending" && y.SystemCode.Code == "LeaveApprovalCode")
                                              .FirstOrDefaultAsync();

            if (pendingStatus == null)
            {
                ModelState.AddModelError(string.Empty, "The Pending status could not be found.");
                return View(leaveApplication);
            }

            if (ModelState.IsValid)
            {
                leaveApplication.CreatedOn = DateTime.Now;
                leaveApplication.CreatedById = "Shane Dean";
                leaveApplication.StatusId = pendingStatus.Id;

                _context.Add(leaveApplication);
                await _context.SaveChangesAsync();  // Ensure async saving
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdown lists if ModelState is invalid
            ViewData["DurationId"] = new SelectList(await _context.SystemCodeDetails
                                                                  .Include(x => x.SystemCode)
                                                                  .Where(y => y.SystemCode.Code == "LeaveDuration")
                                                                  .ToListAsync(), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(await _context.Employees.ToListAsync(), "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(await _context.LeaveTypes.ToListAsync(), "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var pendingStatus = await _context.SystemCodeDetails
                                                  .Include(x => x.SystemCode)
                                                  .Where(y => y.SystemCode.Code == "Pending" && y.SystemCode.Code == "LeaveApprovalCode")
                                                  .FirstOrDefaultAsync();

                if (pendingStatus == null)
                {
                    ModelState.AddModelError(string.Empty, "The Pending status could not be found.");
                    return View(leaveApplication);
                }

                try
                {
                    leaveApplication.ModifiedById = "Shane Dean";
                    leaveApplication.ModifiedOn = DateTime.Now;
                    leaveApplication.StatusId = pendingStatus.Id;
                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();  // Ensure async saving
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id))
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

            // Repopulate dropdown lists if ModelState is invalid
            ViewData["DurationId"] = new SelectList(await _context.SystemCodeDetails.ToListAsync(), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(await _context.Employees.ToListAsync(), "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(await _context.LeaveTypes.ToListAsync(), "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.ApplicationStatus)
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication != null)
            {
                _context.LeaveApplications.Remove(leaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(int id)
        {
            return _context.LeaveApplications.Any(e => e.Id == id);
        }
    }
}
