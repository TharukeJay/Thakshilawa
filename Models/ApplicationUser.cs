﻿using Microsoft.AspNetCore.Identity;

namespace XYZLaundry.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string DateCreated { get; set; }        
    }
}
