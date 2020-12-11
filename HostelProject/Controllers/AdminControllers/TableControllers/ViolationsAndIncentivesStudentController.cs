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
    public class ViolationsAndIncentivesStudentController : Controller, ITableController<ViolationsAndIncentivesStudentViewModel>
    {
        private readonly IRepository<ViolationsAndIncentivesStudent> _violationsAndIncentivesStudentRepository;

        private readonly IRepository<ViolationsAndIncentive> _violationsAndIncentivesRepository;

        private readonly IRepository<Student> _studentRepository;

        public ViolationsAndIncentivesStudentController(IRepository<ViolationsAndIncentivesStudent> violationsAndIncentivesStudentRepository,
            IRepository<Student> studentRepository, IRepository<ViolationsAndIncentive> violationsAndIncentivesRepository)
        {
            _violationsAndIncentivesStudentRepository = violationsAndIncentivesStudentRepository;
            _studentRepository = studentRepository;
            _violationsAndIncentivesRepository = violationsAndIncentivesRepository;
        }

        public IActionResult Index()
        {
            var viewModels = new List<ViolationsAndIncentivesStudentViewModel>();
            foreach (var item in _violationsAndIncentivesStudentRepository.GetAll())
            {
                viewModels.Add(new ViolationsAndIncentivesStudentViewModel { Id = item.Id, StudentId = item.StudentId.GetValueOrDefault(),
                    ViolationsAndIncentivesId = item.ViolationsAndIncentivesId.GetValueOrDefault(), Date =  item.Date});
            }

            return View(viewModels);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var violationsAndIncentivesStudent = await _violationsAndIncentivesStudentRepository.GetById(id);

            var listStudentId = _studentRepository.GetAll().Select(item => item.Id).ToList();

            var listViolationsAndIncentivesId = _violationsAndIncentivesRepository.GetAll().Select(item => item.Id).ToList();

            if (violationsAndIncentivesStudent == null)
            {
                return NotFound();
            }

            var viewModel = new ViolationsAndIncentivesStudentViewModel { Id = violationsAndIncentivesStudent.Id,
                StudentId = violationsAndIncentivesStudent.StudentId.GetValueOrDefault(),
                ViolationsAndIncentivesId = violationsAndIncentivesStudent.ViolationsAndIncentivesId.GetValueOrDefault(),
                ListStudentId = listStudentId, ListViolationsAndIncentivesId = listViolationsAndIncentivesId,
                Date = violationsAndIncentivesStudent.Date
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ViolationsAndIncentivesStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var listStudentId = _studentRepository.GetAll().Select(item => item.Id).ToList();

                var listViolationsAndIncentivesId = _violationsAndIncentivesRepository.GetAll().Select(item => item.Id).ToList();

                viewModel.ListViolationsAndIncentivesId = listViolationsAndIncentivesId;

                viewModel.ListStudentId = listStudentId;

                if (await _studentRepository.GetById(viewModel.StudentId) == null)
                {
                    ModelState.AddModelError("", "StudentId does not exist");
                    return View(viewModel);
                }

                if (await _violationsAndIncentivesRepository.GetById(viewModel.ViolationsAndIncentivesId) == null)
                {
                    ModelState.AddModelError("", "ViolationsAndIncentivesId does not exist");
                    return View(viewModel);
                }

                var violationsAndIncentivesStudent = new ViolationsAndIncentivesStudent { Id = viewModel.Id, StudentId = viewModel.StudentId,
                    ViolationsAndIncentivesId = viewModel.ViolationsAndIncentivesId, Date = viewModel.Date };

                await _violationsAndIncentivesStudentRepository.Edit(violationsAndIncentivesStudent.Id, violationsAndIncentivesStudent);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var violationsAndIncentivesStudent = await _violationsAndIncentivesStudentRepository.GetById(id);

            if (violationsAndIncentivesStudent == null)
            {
                return NotFound();
            }

            var viewModel = new ViolationsAndIncentivesStudentViewModel { Id = violationsAndIncentivesStudent.Id,
                StudentId = violationsAndIncentivesStudent.StudentId.GetValueOrDefault(),
                ViolationsAndIncentivesId = violationsAndIncentivesStudent.ViolationsAndIncentivesId.GetValueOrDefault()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ViolationsAndIncentivesStudentViewModel viewModel)
        {
            var violationsAndIncentivesStudent = new ViolationsAndIncentivesStudent
            {
                Id = viewModel.Id,
                StudentId = viewModel.StudentId,
                ViolationsAndIncentivesId = viewModel.ViolationsAndIncentivesId
            };

            await _violationsAndIncentivesStudentRepository.Delete(violationsAndIncentivesStudent);

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var listStudentId = _studentRepository.GetAll().Select(item => item.Id).ToList();

            var listViolationsAndIncentivesId = _violationsAndIncentivesRepository.GetAll().Select(item => item.Id).ToList();

            var viewModel = new ViolationsAndIncentivesStudentViewModel
            {
                ListViolationsAndIncentivesId = listViolationsAndIncentivesId,
                ListStudentId = listStudentId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ViolationsAndIncentivesStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var listStudentId = _studentRepository.GetAll().Select(item => item.Id).ToList();

                var listViolationsAndIncentivesId = _violationsAndIncentivesRepository.GetAll().Select(item => item.Id).ToList();

                viewModel.ListViolationsAndIncentivesId = listViolationsAndIncentivesId;

                viewModel.ListStudentId = listStudentId;

                if (await _studentRepository.GetById(viewModel.StudentId) == null)
                {
                    ModelState.AddModelError("", "StudentId does not exist");
                    return View(viewModel);
                }

                if (await _violationsAndIncentivesRepository.GetById(viewModel.ViolationsAndIncentivesId) == null)
                {
                    ModelState.AddModelError("", "ViolationsAndIncentivesId does not exist");
                    return View(viewModel);
                }

               var violationsAndIncentivesStudent = new ViolationsAndIncentivesStudent { StudentId = viewModel.StudentId,
                    ViolationsAndIncentivesId = viewModel.ViolationsAndIncentivesId, Date = viewModel.Date };

                await _violationsAndIncentivesStudentRepository.Add(violationsAndIncentivesStudent);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
