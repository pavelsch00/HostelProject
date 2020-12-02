using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HostelProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }


            if (User.IsInRole("Manager"))
            {
                return RedirectToAction("Index", "Manager");
            }

            if (User.IsInRole("Mentor"))
            {
                return RedirectToAction("Index", "Mentor");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
