using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thakshilawa.Models;
using Thakshilawa.ViewModels;
using XYZLaundry.Data;
using XYZLaundry.Models;

namespace Thakshilawa.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StaffController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var staff = await _context.Staff
                .Select(s => new staffViewModel
                {
                    Name = s.Name,
                    NICNo = s.NICNo,
                    ContactNumber = s.ContactNumber,
                    Address = s.Address,
                    Email = s.Email,
                    DateofBirth = s.DateofBirth
                })
                .ToListAsync();

            return View(staff);
        }

        public IActionResult Create()
        {
            return View();
        }

        // GET: Staff/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Select(s => new staffViewModel
                {
                    StaffId = s.StaffId,
                    Name = s.Name,
                    ContactNumber = s.ContactNumber,
                    Email = s.Email,
                    Address = s.Address,
                    NICNo = s.NICNo,
                    DateofBirth = s.DateofBirth,
                    Designation = s.Designation,
                    ReportingPoint = s.ReportingPoint,
                    Department = s.Department,
                    AccountNumber = s.AccountNumber,
                    BankName = s.BankName,
                    Branch = s.Branch
                })
                .FirstOrDefaultAsync(m => m.StaffId == id);

            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: /Staff/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(staffViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();
            var staff = await _context.Staff
                .FirstOrDefaultAsync(c => c.StaffId == viewModel.StaffId);

            if (staff != null)
            {
                staff.StaffId = viewModel.StaffId;
                staff.Name = viewModel.Name;
                staff.ContactNumber = viewModel.ContactNumber;
                staff.Email = viewModel.Email;
                staff.Address = viewModel.Address;
                staff.NICNo = viewModel.NICNo;
                staff.DateofBirth = viewModel.DateofBirth;
                staff.Designation = viewModel.Designation;
                staff.ReportingPoint = viewModel.ReportingPoint;
                staff.Department = viewModel.Department;
                staff.AccountNumber = viewModel.AccountNumber;
                staff.BankName = viewModel.BankName;
                staff.Branch = viewModel.Branch;

                _context.Staff.Update(staff);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(staffViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();

            var staff = new Staff
            {
                StaffId = viewModel.StaffId,
                Name = viewModel.Name,
                ContactNumber = viewModel.ContactNumber,
                Email = viewModel.Email,
                Address = viewModel.Address,
                NICNo = viewModel.NICNo,
                DateofBirth = viewModel.DateofBirth,
                Designation = viewModel.Designation,
                ReportingPoint = viewModel.ReportingPoint,
                Department = viewModel.Department,
                AccountNumber = viewModel.AccountNumber,
                BankName = viewModel.BankName,
                Branch = viewModel.Branch            
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
