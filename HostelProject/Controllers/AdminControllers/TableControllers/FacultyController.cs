using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostelProject.Interfaces;
using HostelProject.Models.Entities;
using HostelProject.ViewModels.AdminViewModels.DataBaseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostelProject.Controllers.AdminControllers.TableControllers
{
    [Authorize(Roles = "Admin")]
    public class FacultyController : Controller, ITableController<FacultyViewModel>
    {
        private readonly IRepository<Faculty> _facultyRepository;

        public FacultyController(IRepository<Faculty> facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new List<FacultyViewModel>();

            foreach (var item in _facultyRepository.GetAll().ToList())
            {
                viewModel.Add(new FacultyViewModel { Id = item.Id, Name = item.Name });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var faculty = await _facultyRepository.GetById(id);

            if (faculty == null)
            {
                return NotFound();
            }

            var viewModel = new FacultyViewModel { Id = faculty.Id, Name = faculty.Name};

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FacultyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var faculty = new Faculty { Id = viewModel.Id, Name = viewModel.Name};

                await _facultyRepository.Edit(faculty);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var faculty = await _facultyRepository.GetById(id);

            if (faculty == null)
            {
                return NotFound();
            }

            var viewModel = new FacultyViewModel { Id = faculty.Id, Name = faculty.Name };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FacultyViewModel viewModel)
        {
            var faculty = new Faculty { Id = viewModel.Id, Name = viewModel.Name };

            await _facultyRepository.Delete(faculty);

            return RedirectToAction("Index");
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(FacultyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var faculty = new Faculty { Name = viewModel.Name };

                await _facultyRepository.Add(faculty);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
