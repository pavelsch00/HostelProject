using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostelProject.Interfaces;
using HostelProject.Models.Entities;
using HostelProject.ViewModels.AdminViewModels.DataBaseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

namespace HostelProject.Controllers.AdminControllers.TableControllers
{
    [Authorize(Roles = "Admin")]
    public class MentorController : Controller, ITableController<MentorViewModel>
    {
        private readonly IRepository<Mentor> _mentorRepository;

        private readonly IRepository<Faculty> _facultyRepository;

        private readonly UserManager<User> _userManager;

        public MentorController(IRepository<Mentor> mentorRepository, IRepository<Faculty> facultyRepository, UserManager<User> userManager)
        {
            _mentorRepository = mentorRepository;

            _facultyRepository = facultyRepository;

            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModels = new List<MentorViewModel>();

            foreach (var item in _mentorRepository.GetAll())
            {
                viewModels.Add(new MentorViewModel { Id = item.Id, FacultyId = item.FacultyId.GetValueOrDefault(), UserId = item.UserId });
            }

            return View(viewModels);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var mentor = await _mentorRepository.GetById(id);

            var listFacultyId = _facultyRepository.GetAll().Select(item => item.Id).ToList();

            var listUserId = _userManager.Users.Select(item => item.Id).ToList();

            if (mentor == null)
            {
                return NotFound();
            }

            var viewModel = new MentorViewModel { Id = mentor.Id, FacultyId = mentor.FacultyId.GetValueOrDefault(),
                UserId = mentor.UserId, ListUserId = listUserId, ListFacultyId = listFacultyId };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MentorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var listFacultyId = _facultyRepository.GetAll().Select(item => item.Id).ToList();

                var listUserId = _userManager.Users.Select(item => item.Id).ToList();

                var tempViewModel = new MentorViewModel { Id = viewModel.Id, FacultyId = viewModel.FacultyId, UserId = viewModel.UserId,
                    ListUserId = listUserId, ListFacultyId = listFacultyId };

                if (await _facultyRepository.GetById(viewModel.FacultyId) == null)
                {
                    ModelState.AddModelError("", "FacultyId does not exist");
                    return View(tempViewModel);
                }

                var user = await _userManager.FindByIdAsync(viewModel.UserId);

                if (user == null)
                {
                    ModelState.AddModelError("", "UserId does not exist");
                    return View(tempViewModel);
                }

                IList<string> userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(viewModel.UserId));

                if (userRoles.Where(item => item == "Mentor").FirstOrDefault() == null)
                {
                    ModelState.AddModelError("", "User is not a mentor");
                    return View(tempViewModel);
                }

                var mentor = new Mentor { Id = viewModel.Id, FacultyId = viewModel.FacultyId, UserId = viewModel.UserId };

                await _mentorRepository.Edit(mentor);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var mentor = await _mentorRepository.GetById(id);

            if (mentor == null)
            {
                return NotFound();
            }

            var viewModel = new MentorViewModel { Id = mentor.Id, FacultyId = mentor.FacultyId.GetValueOrDefault(), UserId = mentor.UserId};

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MentorViewModel viewModel)
        {
            var mentor = new Mentor { Id = viewModel.Id, FacultyId = viewModel.FacultyId, UserId = viewModel.UserId };

            await _mentorRepository.Delete(mentor);

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var listFacultyId = _facultyRepository.GetAll().Select(item => item.Id).ToList();

            var listUserId = _userManager.Users.Select(item => item.Id).ToList();
            return View(new MentorViewModel { ListFacultyId = listFacultyId, ListUserId = listUserId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(MentorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var listFacultyId = _facultyRepository.GetAll().Select(item => item.Id).ToList();

                var listUserId = _userManager.Users.Select(item => item.Id).ToList();

                var tempViewModel = new MentorViewModel { Id = viewModel.Id, FacultyId = viewModel.FacultyId, UserId = viewModel.UserId,
                    ListUserId = listUserId, ListFacultyId = listFacultyId };

                if (await _facultyRepository.GetById(viewModel.FacultyId) == null)
                {
                    ModelState.AddModelError("", "FacultyId does not exist");
                    return View(viewModel);
                }

                var user = await _userManager.FindByIdAsync(viewModel.UserId);

                if (user == null)
                {
                    ModelState.AddModelError("", "UserId does not exist");
                    return View(viewModel);
                }

                IList<string> userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(viewModel.UserId));

                if (userRoles.Where(item => item == "Mentor").FirstOrDefault() == null)
                {
                    ModelState.AddModelError("", "User is not a mentor");
                    return View(viewModel);
                }

                var mentor = new Mentor { FacultyId = viewModel.FacultyId, UserId = viewModel.UserId };

                await _mentorRepository.Add(mentor);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
