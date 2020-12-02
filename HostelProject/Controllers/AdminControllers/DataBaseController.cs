using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostelProject.Interfaces;
using HostelProject.Models.Entities;
using HostelProject.Models.Repositories;
using HostelProject.ViewModels.AdminViewModels.DataBaseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostelProject.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class DataBaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
