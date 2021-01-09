//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using XYZLaundry.Data;
//using XYZLaundry.Models;
//using XYZLaundry.Models.Enum;
//using XYZLaundry.Services;
//using XYZLaundry.ViewModels;

//namespace XYZLaundry.Controllers
//{
//    [Authorize]
//    public class OrdersController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly IOrderItemService _orderItemService;

//        public OrdersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IOrderItemService orderItemService)
//        {
//            _userManager = userManager;
//            _context = context;
//            _orderItemService = orderItemService;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var customers = await _context.Orders
//                .Include(o => o.Customer)
//                .Include(o => o.OrderItems)
//                .Select(s => new OrdersViewModel
//                {
//                    OrderId = s.OrderId,
//                    OrderDate = s.CreatedOn,
//                    OrderStatus = s.OrderStatus,
//                    CollectionType = s.CollectionType,
//                    CustomerId = s.CustomerId,
//                    CustomerName = s.Customer.Name,
//                    ContactNumber = s.Customer.ContactNumber,
//                    Items = s.Items,
//                    Notes = s.Notes,
//                    ServiceType = s.ServiceType,
//                    OrderNumber = s.OrderNumber
//                })
//                .ToListAsync();

//            _orderItemService.SetOrderItems(new List<OrderItemViewModel>());

//            return View(customers);
//        }


//        public async Task<IActionResult> Create()
//        {
//            var model = new OrdersViewModel();
//            model.OrderItems.Add(new OrderItemViewModel());

//            await SetItems();
//            await SetCustomers();
//            return View(model);
//        }

//        // GET: Orders/Edit/1
//        public async Task<IActionResult> Edit(int id)
//        {
//            if (id < 0)
//            {
//                return NotFound();
//            }

//            var order = await _context.Orders
//                .Include(o => o.Customer)
//                .Select(s => new OrdersViewModel
//                {
//                    OrderId = s.OrderId,
//                    OrderNumber = s.OrderNumber,
//                    OrderDate = s.CreatedOn,
//                    OrderStatus = s.OrderStatus,
//                    CollectionType = s.CollectionType,
//                    CustomerId = s.CustomerId,
//                    CustomerName = s.Customer.Name,
//                    ContactNumber = s.Customer.ContactNumber,
//                    Items = s.Items,
//                    Notes = s.Notes,
//                    ServiceType = s.ServiceType
//                })
//                .FirstOrDefaultAsync(m => m.CustomerId == id);

//            if (order == null)
//            {
//                return NotFound();
//            }

//            await SetCustomers();
//            return View(order);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(OrdersViewModel viewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                await SetCustomers();
//                return View(viewModel);
//            }

//            var user = await GetCurrentUserAsync();
//            if (!IsSuperAdmin())
//            {
//                viewModel.CustomerId = (await _context.Customers.FirstOrDefaultAsync(s => s.Email == user.Email)).CustomerId;
//            }

//            var currentOrderItems = _orderItemService.GetOrderItems();

//            var order = new Order
//            {
//                OrderNumber = $"#{viewModel.OrderId:0000}",
//                CustomerId = viewModel.CustomerId,
//                Items = viewModel.Items,
//                ServiceType = viewModel.ServiceType.Value,
//                CollectionType = viewModel.CollectionType.Value,
//                Notes = viewModel.Notes,
//                OrderStatus = OrderStatus.Pending,
//                CreatedBy = user.Email,
//                CreatedOn = DateTime.Now
//            };

//            if (currentOrderItems.Any())
//            {
//                order.OrderItems = currentOrderItems.Select(s => new OrderItem
//                {
//                    ItemId = s.ItemId,
//                    Qty = s.Quantity,
//                    SubTotal = s.Price
//                }).ToList();
//            }

//            _context.Orders.Add(order);
//            await _context.SaveChangesAsync();

//            // Empty the list
//            _orderItemService.SetOrderItems(new List<OrderItemViewModel>());

//            order.OrderNumber = $"#{order.OrderId:0000}";
//            _context.Orders.Update(order);
//            await _context.SaveChangesAsync();

//            return RedirectToAction(nameof(Index));
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(OrdersViewModel viewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                await SetCustomers();
//                return View(viewModel);
//            }

//            var user = await GetCurrentUserAsync();
//            var order = await _context.Orders.FirstOrDefaultAsync(c => c.OrderId == viewModel.OrderId);

//            order.Items = viewModel.Items;
//            order.ServiceType = viewModel.ServiceType.Value;
//            order.CollectionType = viewModel.CollectionType.Value;
//            order.Notes = viewModel.Notes;
//            order.OrderStatus = viewModel.OrderStatus;
//            order.ModifiedBy = user.Email;
//            order.ModifiedOn = DateTime.Now;

//            _context.Orders.Update(order);
//            await _context.SaveChangesAsync();

//            return RedirectToAction(nameof(Index));
//        }

//        [HttpPost]
//        public async Task<ActionResult> BlankItem(int itemId, int quantity)
//        {
//            var currentItems = _orderItemService.GetOrderItems();

//            var item = _context.Items.Where(i => i.ItemId == itemId).FirstOrDefault();


//            var tempOrderItem = new OrderItemViewModel
//            {
//                ItemId = itemId,
//                Quantity = quantity,
//                Price = (decimal)(item?.ItemPrice * quantity)
//            };
//            currentItems.Add(tempOrderItem);
//            _orderItemService.SetOrderItems(currentItems);

//            await SetItems();
//            return PartialView("_OrderItem", new OrderItemViewModel());
//        }

//        private Task<ApplicationUser> GetCurrentUserAsync()
//        {
//            return _userManager.GetUserAsync(HttpContext.User);
//        }

//        private async Task SetCustomers()
//        {
//            var user = await GetCurrentUserAsync();
//            ViewBag.Customers = await _context.Customers
//                .Where(c => c.Email == (IsSuperAdmin() ? c.Email : user.Email))
//                .Select(s => new SelectListItem { Value = s.CustomerId.ToString(), Text = s.Name })
//                .ToListAsync();
//        }

//        private async Task SetItems()
//        {
//            var user = await GetCurrentUserAsync();
//            ViewBag.Items = await _context.Items
//                .Select(s => new SelectListItem { Value = s.ItemId.ToString(), Text = s.ItemName })
//                .ToListAsync();
//        }

//        private bool IsSuperAdmin()
//        {
//            var user = GetCurrentUserAsync().Result;
//            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
//            return userRole.ToLower().Equals("super admin");
//        }
//    }
//}
