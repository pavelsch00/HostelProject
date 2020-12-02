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
    public class SpecialtyController : Controller, ITableController<SpecialtyViewModel>
    {
        private readonly IRepository<Specialty> _specialtyRepository;

        private readonly IRepository<Faculty> _facultyRepository;
        public SpecialtyController(IRepository<Specialty> specialtyRepository, IRepository<Faculty>  facultyRepository)
        {
            _specialtyRepository = specialtyRepository;

            _facultyRepository = facultyRepository;
        }

        public IActionResult Index()
        {
            var viewModels = new List<SpecialtyViewModel>();

            foreach (var item in _specialtyRepository.GetAll())
            {
                viewModels.Add(new SpecialtyViewModel { Id = item.Id, Name = item.Name, FacultyId = item.FacultyId.GetValueOrDefault() });
            }

            return View(viewModels);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var specialty = await _specialtyRepository.GetById(id);

            var listFacultyId = _facultyRepository.GetAll().Select(item => item.Id).ToList();

            if (specialty == null)
            {
                return NotFound();
            }

            var viewModel = new SpecialtyViewModel { Id = specialty.Id, Name = specialty.Name,
                FacultyId = specialty.FacultyId.GetValueOrDefault(), ListFacultyId = listFacultyId };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SpecialtyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _facultyRepository.GetById(viewModel.FacultyId) == null)
                {
                    var listFacultyId = _facultyRepository.GetAll().Select(item => item.Id).ToList();
                    viewModel.ListFacultyId = listFacultyId;

                    ModelState.AddModelError("", "FacultyId does not exist");
                    return View(viewModel);
                }

                var specialty = new Specialty { Id = viewModel.Id, Name = viewModel.Name, FacultyId = viewModel.FacultyId };

                await _specialtyRepository.Edit(specialty);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var specialty = await _specialtyRepository.GetById(id);

            if (specialty == null)
            {
                return NotFound();
            }

            var viewModel = new SpecialtyViewModel { Id = specialty.Id, Name = specialty.Name, FacultyId = specialty.FacultyId.GetValueOrDefault() };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SpecialtyViewModel viewModel)
        {
            var specialty = new Specialty { Id = viewModel.Id, Name = viewModel.Name, FacultyId = viewModel.FacultyId };

            await _specialtyRepository.Delete(specialty);

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var listFacultyId = _facultyRepository.GetAll().Select(item => item.Id).ToList();

            return View(new SpecialtyViewModel { ListFacultyId = listFacultyId});
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpecialtyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _facultyRepository.GetById(viewModel.FacultyId) == null)
                {
                    ModelState.AddModelError("", "FacultyId does not exist");

                    var listFacultyId = _facultyRepository.GetAll().Select(item => item.Id).ToList();
                    viewModel.ListFacultyId = listFacultyId;

                    return View(viewModel);
                }

                var specialty = new Specialty { Name = viewModel.Name, FacultyId = viewModel.FacultyId };

                await _specialtyRepository.Add(specialty);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
