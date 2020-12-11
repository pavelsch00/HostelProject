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
                return RedirectToAction("Index", "DataBase");
            }

            if (User.IsInRole("Manager"))
            {
                return RedirectToAction("Index", "StudentMenu");
            }

            if (User.IsInRole("Mentor"))
            {
                return RedirectToAction("Index", "HostelMentor");
            }

            return RedirectToAction("Index", "ShowRating");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
