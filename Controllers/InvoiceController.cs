using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InvoiceController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var invoices = await _context.Invoices
                .Include(o => o.Order).ThenInclude(o => o.Customer)
                .Include(o => o.Order).ThenInclude(o => o.OrderItems)
                .Where(i => i.Order.Customer.Email == (IsSuperAdmin() ? i.Order.Customer.Email : user.Email))
                .Select(s => new InvoiceViewModel
                {
                    InvoiceNumber = s.InvoiceNumber,
                    InvoiceDate = s.CreatedOn,
                    DueDate = s.DueDate,
                    CustomerId = s.Order.CustomerId,
                    Discount = s.Discount,
                    GrandTotal = s.GrandTotal,
                    InvoiceId = s.InvoiceId,
                    IsPaid = s.IsPaid,
                    NoteToRecipient = s.NoteToRecipient,
                    OrderId = s.OrderId,
                    Shipping = s.Shipping,
                    SubTotal = s.SubTotal,
                    TaxAmount = s.TaxAmount,
                    TermsAndCondition = s.TermsAndCondition,
                    CustomerName = s.Order.Customer.Name,
                    OrderNumber = s.Order.OrderNumber,
                    OrderItems = s.Order.OrderItems.Select(i => new OrderItemViewModel
                    {
                        ItemId = i.ItemId,
                        Item = i.Item,
                        OrderId = i.OrderId,
                        Price = i.SubTotal,
                        Quantity = i.Qty
                    }).ToList()
                })
                .ToListAsync();

            await SetOrders();
            return View(invoices);
        }


        public async Task<IActionResult> Create()
        {
            await SetOrders();
            return View();
        }

        // GET: Invoice/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(o => o.Order).ThenInclude(o => o.Customer)
                .Where(i => i.InvoiceId == id)
                .Select(s => new InvoiceViewModel
                {
                    InvoiceNumber = s.InvoiceNumber,
                    InvoiceDate = s.CreatedOn,
                    DueDate = s.DueDate,
                    CustomerId = s.Order.CustomerId,
                    Discount = s.Discount,
                    GrandTotal = s.GrandTotal,
                    InvoiceId = s.InvoiceId,
                    IsPaid = s.IsPaid,
                    NoteToRecipient = s.NoteToRecipient,
                    OrderId = s.OrderId,
                    Shipping = s.Shipping,
                    SubTotal = s.SubTotal,
                    TaxAmount = s.TaxAmount,
                    TermsAndCondition = s.TermsAndCondition,
                    CustomerName = s.Order.Customer.Name,
                    OrderNumber = s.Order.OrderNumber
                })
                .FirstOrDefaultAsync();

            if (invoice == null)
            {
                return NotFound();
            }

            await SetOrders();
            return View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InvoiceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await SetOrders();
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();

            var invoice = new Invoice()
            {
                Discount = viewModel.Discount,
                GrandTotal = viewModel.GrandTotal,
                InvoiceId = viewModel.InvoiceId,
                IsPaid = viewModel.IsPaid,
                NoteToRecipient = viewModel.NoteToRecipient,
                OrderId = viewModel.OrderId,
                Shipping = viewModel.Shipping,
                SubTotal = viewModel.SubTotal,
                TaxAmount = viewModel.TaxAmount,
                TermsAndCondition = viewModel.TermsAndCondition,
                CreatedBy = user.Email
            };

            var orderItems = await _context.OrderItems.Where(o => o.OrderId == viewModel.OrderId).ToListAsync();
            if (orderItems != null)
            {
                invoice.SubTotal = orderItems.Sum(o => o.SubTotal);
            }

            invoice.GrandTotal = invoice.SubTotal + invoice.Shipping - invoice.Discount;

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InvoiceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await SetOrders();
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();
            var invoice = await _context.Invoices.FirstOrDefaultAsync(c => c.InvoiceId == viewModel.InvoiceId);

            invoice.Discount = viewModel.Discount;
            invoice.InvoiceId = viewModel.InvoiceId;
            invoice.IsPaid = viewModel.IsPaid;
            invoice.NoteToRecipient = viewModel.NoteToRecipient;
            invoice.Shipping = viewModel.Shipping;
            invoice.TaxAmount = viewModel.TaxAmount;
            invoice.TermsAndCondition = viewModel.TermsAndCondition;
            invoice.ModifiedBy = user.Email;
            invoice.ModifiedOn = DateTime.Now;

            var orderItems = await _context.OrderItems.Where(o => o.OrderId == viewModel.OrderId).ToListAsync();
            if (orderItems != null)
            {
                invoice.SubTotal = orderItems.Sum(o => o.SubTotal);
            }

            invoice.GrandTotal = invoice.SubTotal + invoice.Shipping - invoice.Discount;

            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Print(int id)
        {
            return await GetInvoice(id);
        }

        public async Task<IActionResult> PrintInvoice(int id)
        {
            return await GetInvoice(id);
        }

        private async Task<IActionResult> GetInvoice(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(o => o.Order).ThenInclude(o => o.Customer)
                .Include(o => o.Order).ThenInclude(o => o.OrderItems)
                .Where(i => i.InvoiceId == id)
                .Select(s => new InvoiceViewModel
                {
                    InvoiceNumber = s.InvoiceNumber,
                    InvoiceDate = s.CreatedOn,
                    DueDate = s.DueDate,
                    CustomerId = s.Order.CustomerId,
                    Discount = s.Discount,
                    GrandTotal = s.GrandTotal,
                    InvoiceId = s.InvoiceId,
                    IsPaid = s.IsPaid,
                    NoteToRecipient = s.NoteToRecipient,
                    OrderId = s.OrderId,
                    Shipping = s.Shipping,
                    SubTotal = s.SubTotal,
                    TaxAmount = s.TaxAmount,
                    TermsAndCondition = s.TermsAndCondition,
                    CustomerName = s.Order.Customer.Name,
                    OrderNumber = s.Order.OrderNumber,
                    Order = new OrdersViewModel
                    {
                        Items = s.Order.Items,
                        ServiceType = s.Order.ServiceType,
                        OrderNumber = s.Order.OrderNumber
                    },
                    Customer = new CustomerViewModel
                    {
                        Address = s.Order.Customer.Address,
                        ContactNumber = s.Order.Customer.ContactNumber,
                        Email = s.Order.Customer.Email,
                        Name = s.Order.Customer.Name
                    },
                    OrderItems = s.Order.OrderItems.Select(i => new OrderItemViewModel
                    {
                        ItemId = i.ItemId,
                        Item = i.Item,
                        OrderId = i.OrderId,
                        Price = i.SubTotal,
                        Quantity = i.Qty
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private bool IsSuperAdmin()
        {
            var user = GetCurrentUserAsync().Result;
            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            return userRole.ToLower().Equals("super admin");
        }

        private async Task SetOrders()
        {
            ViewBag.Orders = await _context.Orders
                .Select(s => new SelectListItem { Value = s.OrderId.ToString(), Text = s.OrderNumber })
                .ToListAsync();
        }
    }
}
