﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thakshilawa.Controllers
{
    public class CafeteriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}