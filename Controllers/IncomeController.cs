using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XYZLaundry.Data;
using XYZLaundry.Models;

namespace Thakshilawa.Controllers
{
    public class IncomeController : Controller
    {
        private readonly XYZLaundry.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IncomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
