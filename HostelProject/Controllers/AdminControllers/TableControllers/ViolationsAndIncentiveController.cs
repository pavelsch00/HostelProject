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
    public class ViolationsAndIncentiveController : Controller, ITableController<ViolationsAndIncentiveViewModel>
    {
        private readonly IRepository<ViolationsAndIncentive> _violationsAndIncentiveRepository;

        public ViolationsAndIncentiveController(IRepository<ViolationsAndIncentive> violationsAndIncentiveRepository)
        {
            _violationsAndIncentiveRepository = violationsAndIncentiveRepository;
        }

        public IActionResult Index()
        {
            var violationsAndIncentive = new List<ViolationsAndIncentiveViewModel>();

            foreach (var item in _violationsAndIncentiveRepository.GetAll().ToList())
            {
                violationsAndIncentive.Add(new ViolationsAndIncentiveViewModel { Id = item.Id, Name = item.Name,
                    Score = item.Score });
            }

            return View(violationsAndIncentive);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var violationsAndIncentive = await _violationsAndIncentiveRepository.GetById(id);

            if (violationsAndIncentive == null)
            {
                return NotFound();
            }

            var viewModel = new ViolationsAndIncentiveViewModel { Id = violationsAndIncentive.Id,
                Name = violationsAndIncentive.Name, Score = violationsAndIncentive.Score };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ViolationsAndIncentiveViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var violationsAndIncentive = new ViolationsAndIncentive { Id = viewModel.Id, Name = viewModel.Name,
                    Score = viewModel.Score };

                await _violationsAndIncentiveRepository.Edit(violationsAndIncentive);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var violationsAndIncentive = await _violationsAndIncentiveRepository.GetById(id);

            if (violationsAndIncentive == null)
            {
                return NotFound();
            }

            var viewModel = new ViolationsAndIncentiveViewModel { Id = violationsAndIncentive.Id,
                Name = violationsAndIncentive.Name, Score = violationsAndIncentive.Score };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ViolationsAndIncentiveViewModel viewModel)
        {
            var violationsAndIncentive = new ViolationsAndIncentive
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Score = viewModel.Score
            };

            await _violationsAndIncentiveRepository.Delete(violationsAndIncentive);

            return RedirectToAction("Index");
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ViolationsAndIncentiveViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var violationsAndIncentive = new ViolationsAndIncentive { Name = viewModel.Name,
                    Score = viewModel.Score };

                await _violationsAndIncentiveRepository.Add(violationsAndIncentive);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
