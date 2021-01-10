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
    public class CafeteriaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CafeteriaController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var cafeteria = await _context.Cafeteria
                .Select(s => new cafeteriaViewModel
                {
                    Id = s.Id,
                    EmployeeID = s.EmployeeID,
                    Date = s.Date,
                    Income = s.Income,
                    Expenditure = s.Expenditure,
                }).ToListAsync();

            return View(cafeteria);
        }

        public IActionResult Create()
        {
            return View();
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var cafeteria = await _context.Cafeteria
                .Select(s => new cafeteriaViewModel
                {
                    Id = s.Id,
                    EmployeeID = s.EmployeeID,
                    Date = s.Date,
                    Income = s.Income,
                    Expenditure = s.Expenditure,
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cafeteria == null)
            {
                return NotFound();
            }

            return View(cafeteria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(cafeteriaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();
            var cafeteria = await _context.Cafeteria
                .FirstOrDefaultAsync(s => s.Id == viewModel.Id);

            if (cafeteria != null)
            {
                cafeteria.Id = viewModel.Id;
                cafeteria.EmployeeID = viewModel.EmployeeID;
                    cafeteria.Date = viewModel.Date;
                cafeteria.Income = viewModel.Income;
                cafeteria.Expenditure = viewModel.Expenditure;

                _context.Cafeteria.Update(cafeteria);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(cafeteriaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();

            var cafeteria = new Cafeteria
            {
                Id = viewModel.Id,
            EmployeeID = viewModel.EmployeeID,
            Date = viewModel.Date,
            Income = viewModel.Income,
            Expenditure = viewModel.Expenditure

        };

            _context.Cafeteria.Add(cafeteria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
