using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostelProject.Interfaces;
using HostelProject.Models.Entities;
using HostelProject.ViewModels;

using HostelProject.ViewModels.MentorViewModels;
using Microsoft.AspNetCore.Mvc;
using HostelProject.ViewModels.MentorViewModels.ViolationsAndIncentivesDataViewModels;
using HostelProject.ViewModels.ManagerViewModels.StudentDataViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HostelProject.Controllers.ViolationsAndIncentivesStudentListControllers
{
    [Authorize(Roles = "Mentor")]
    public class ViolationsAndIncentivesStudentListController : Controller
    {
        private readonly IRepository<Student> _studentRepository;

        private readonly IRepository<ViolationsAndIncentive> _violationsAndIncentiveRepository;

        private readonly IRepository<ViolationsAndIncentivesStudent> _violationsAndIncentivesStudentRepository;

        private readonly IRepository<Position> _positionRepository;

        public ViolationsAndIncentivesStudentListController(IRepository<Student> studentRepository,
            IRepository<ViolationsAndIncentive> violationsAndIncentiveRepository, IRepository<ViolationsAndIncentivesStudent> violationsAndIncentivesStudentRepository,
            IRepository<Position> positionRepository)
        {
            _studentRepository = studentRepository;
            _violationsAndIncentiveRepository = violationsAndIncentiveRepository;
            _violationsAndIncentivesStudentRepository = violationsAndIncentivesStudentRepository;
            _positionRepository = positionRepository;
        }

        public IActionResult ShowList(int id)
        {
            var viewModel = new ViolationsAndIncentivesStudentListViewModel();

            var student = _studentRepository.GetById(id).Result;
            var violationsAndIncentivesStudent = _violationsAndIncentivesStudentRepository.GetAll().Where(item => item.StudentId == student.Id).ToList();

            viewModel.Id = student.Id;
            viewModel.FullName = student.FullName;
            viewModel.TotalScore = 0;
            viewModel.ViolationsAndIncentiveViewModelList = new List<ViewModels.AdminViewModels.DataBaseViewModels.ViolationsAndIncentiveViewModel>();
            foreach (var item in violationsAndIncentivesStudent)
            {
                var itemId = _violationsAndIncentiveRepository.GetById(item.ViolationsAndIncentivesId).Result.Id;
                var itemName = _violationsAndIncentiveRepository.GetById(item.ViolationsAndIncentivesId).Result.Name;
                var itemScore = _violationsAndIncentiveRepository.GetById(item.ViolationsAndIncentivesId).Result.Score;
                var itemDate = item.Date;
                viewModel.ViolationsAndIncentiveViewModelList.Add(new ViewModels.AdminViewModels.DataBaseViewModels.ViolationsAndIncentiveViewModel(itemId, itemName, itemScore, itemDate));
                viewModel.TotalScore += _violationsAndIncentiveRepository.GetById(item.ViolationsAndIncentivesId).Result.Score;
            }

            return View(viewModel);
        }

        public IActionResult Create(int id)
        {
            var listStudent = _studentRepository.GetAll();

            var listViolationsAndIncentives = _violationsAndIncentiveRepository.GetAll();

            var viewModel = new AddViolationViewModel
            {
                Id = id,
                FullName = _studentRepository.GetById(id).Result.FullName,
                ListViolationsAndIncentives = (from item in listViolationsAndIncentives
                                               select new ViolationsAndIncentivesViewModel(item.Id, item.Name)).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddViolationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var listStudent = _studentRepository.GetAll();

                var listViolationsAndIncentives = _violationsAndIncentiveRepository.GetAll();

                viewModel.ListViolationsAndIncentives = (from item in listViolationsAndIncentives
                                                         select new ViolationsAndIncentivesViewModel(item.Id, item.Name)).ToList();

                if (await _violationsAndIncentiveRepository.GetById(viewModel.ViolationsAndIncentivesId) == null)
                {
                    ModelState.AddModelError("", "ViolationsAndIncentivesId does not exist");
                    return View(viewModel);
                }

                var violationsAndIncentivesStudent = new ViolationsAndIncentivesStudent
                {
                    StudentId = viewModel.Id,
                    ViolationsAndIncentivesId = viewModel.ViolationsAndIncentivesId,
                    Date = viewModel.Date
                };

                await _violationsAndIncentivesStudentRepository.Add(violationsAndIncentivesStudent);
                if (User.IsInRole("Manager"))
                {
                    return RedirectToAction("index", "StudentMenu");
                }
                if (User.IsInRole("Mentor"))
                {
                    return RedirectToAction("index", "HostelMentor");
                }
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var listPosition = _positionRepository.GetAll().ToList();
            var student = await _studentRepository.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new ChangePositionViewModel
            {
                Id = student.Id,

                PositionList = (from item in listPosition
                                select new PositionViewModel(item.Id, item.Name)).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChangePositionViewModel viewModel)
        {
            var student = await _studentRepository.GetById(viewModel.Id);
            var listPosition = _positionRepository.GetAll().ToList();

            viewModel.PositionList = (from item in listPosition
                                  select new PositionViewModel(item.Id, item.Name)).ToList();

            if (ModelState.IsValid)
            {

                student.PositionId = viewModel.PositionId;

                               await _studentRepository.Edit(student.Id, student);

                return RedirectToAction("index", "HostelMentor");
            }

            return View(viewModel);
        }
    }

}
