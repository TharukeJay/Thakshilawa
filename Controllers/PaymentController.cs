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
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var payments = await _context.Payments
                .Select(s => new paymentViewModel
                {
                    PaymentsId = s.PaymentsId,
                    StudentID = s.StudentID,
                    ClassID = s.ClassID,
                    Amount = s.Amount,
                    PaymentType = s.PaymentType,
                    Month = s.Month,
                    PaidOn = s.PaidOn
                }).ToListAsync();

            return View(payments);
        }

        public IActionResult Create()
        {
            return View();
        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Select(s => new paymentViewModel
                {
                    PaymentsId = s.PaymentsId,
                    StudentID = s.StudentID,
                    ClassID = s.ClassID,
                    Amount = s.Amount,
                    PaymentType = s.PaymentType,
                    Month = s.Month,
                    PaidOn = s.PaidOn
                })
                .FirstOrDefaultAsync(m => m.PaymentsId == id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(paymentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();
            var payment = await _context.Payments
                .FirstOrDefaultAsync(s => s.PaymentsId == viewModel.PaymentsId);

            if (payment != null)
            {
                payment.PaymentsId = viewModel.PaymentsId;
                payment.StudentID = viewModel.StudentID;
                payment.ClassID = viewModel.ClassID;
                payment.Amount = viewModel.Amount;
                payment.PaymentType = viewModel.PaymentType;
                payment.Month = viewModel.Month;
                payment.PaidOn = viewModel.PaidOn;

                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(paymentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();

            var payments = new Payments
            {
                PaymentsId = viewModel.PaymentsId,
            StudentID = viewModel.StudentID,
            ClassID = viewModel.ClassID,
            Amount = viewModel.Amount,
            PaymentType = viewModel.PaymentType,
            Month = viewModel.Month,
            PaidOn = viewModel.PaidOn

        };

            _context.Payments.Add(payments);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
