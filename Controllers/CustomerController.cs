using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using XYZLaundry.Data;
using XYZLaundry.Models;
using XYZLaundry.ViewModels;

namespace XYZLaundry.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers
                .Select(s => new CustomerViewModel
                {
                    Name = s.Name,
                    CustomerId = s.CustomerId,
                    ContactNumber = s.ContactNumber,
                    Address = s.Address,
                    Email = s.Email
                })
                .ToListAsync();

            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Select(s => new CustomerViewModel
                {
                    Name = s.Name,
                    CustomerId = s.CustomerId,
                    ContactNumber = s.ContactNumber,
                    Address = s.Address,
                    Email = s.Email
                })
                .FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: /Customer/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == viewModel.CustomerId);

            if (customer != null)
            {
                customer.Name = viewModel.Name;
                customer.ContactNumber = viewModel.ContactNumber;
                customer.Email = viewModel.Email;
                customer.Address = viewModel.Address;
                customer.ModifiedBy = user.Email;
                customer.ModifiedOn = DateTime.Now;

                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();

            var customer = new Customer
            {
                Name = viewModel.Name,
                ContactNumber = viewModel.ContactNumber,
                Email = viewModel.Email,
                Address = viewModel.Address,
                CreatedBy = user.Email,
                CreatedOn = DateTime.Now
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
