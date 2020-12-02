using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostelProject.Controllers.MentorControllers
{
    [Authorize(Roles = "Mentor")]
    public class HostelMentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
