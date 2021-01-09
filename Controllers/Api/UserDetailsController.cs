using XYZLaundry.Data;
using XYZLaundry.Models;
using XYZLaundry.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XYZLaundry.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/UserDetails")]
    public class UserDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserDetailsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager = null)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Get user details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserDetails()
        {
            var users = await _context.ApplicationUsers
                .Select(s => new ApplicationUserViewModel
                {
                    ApplicationUserId = s.Id,
                    Email = s.Email,
                    ContactNumber = s.PhoneNumber,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Role = _userManager.GetRolesAsync(s).Result.FirstOrDefault()
                })
                .ToListAsync();

            return Ok(new { Items = users, Count = users.Count() });
        }

        /// <summary>
        /// Get list of user roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("UserRoles")]
        public IActionResult GetPaymentTypes()
        {
            var userRoles = new List<SelectOptionViewModel> {
                new SelectOptionViewModel { Id = 1, Value = "Super Admin" },
                new SelectOptionViewModel { Id = 2, Value = "Manager" },
                new SelectOptionViewModel { Id = 3, Value = "User" }
            };
            return Ok(new { Items = userRoles, Count = userRoles.Count() });
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CrudViewModel<ApplicationUserViewModel> payload)
        {
            var userViewModel = payload.Value;
            var user = await _userManager.FindByEmailAsync(userViewModel.Email);
            user.Email = userViewModel.Email;
            user.PhoneNumber = userViewModel.ContactNumber;
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;

            await _userManager.UpdateAsync(user);

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (currentRole != userViewModel.Role)
            {
                if (!string.IsNullOrEmpty(currentRole))
                {
                    await _userManager.RemoveFromRoleAsync(user, currentRole);
                }
                await _userManager.AddToRoleAsync(user, userViewModel.Role);
            }

            if (userViewModel.Password.Equals(userViewModel.ConfirmPassword))
            {
                await _userManager.ChangePasswordAsync(user, userViewModel.OldPassword, userViewModel.Password);
            }

            return Ok();
        }

        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CrudViewModel<ApplicationUserViewModel> payload)
        {
            var userViewModel = payload.Value;
            var existingUserWithSameEmail = await _userManager.FindByEmailAsync(userViewModel.Email);
            if (existingUserWithSameEmail == null)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    PhoneNumber = userViewModel.ContactNumber,
                    DateCreated = DateTime.Now.ToString()
                };

                await _userManager.CreateAsync(user, userViewModel.Password);
                await _userManager.AddToRoleAsync(user, userViewModel.Role);
            }

            return Ok();
        }

        [Route("Remove")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] CrudViewModel<OrdersViewModel> payload)
        {
            return Ok();
        }
    }
}
