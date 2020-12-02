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
    public class ReasonForEvictionController : Controller, ITableController<ReasonForEvictionViewModel>
    {
        private readonly IRepository<ReasonForEviction> _reasonForEvictionRepository;

        public ReasonForEvictionController(IRepository<ReasonForEviction> reasonForEvictionRepository)
        {
            _reasonForEvictionRepository = reasonForEvictionRepository;
        }

        public IActionResult Index()
        {
            var viewModels = new List<ReasonForEvictionViewModel>();

            foreach (var item in _reasonForEvictionRepository.GetAll().ToList())
            {
                viewModels.Add(new ReasonForEvictionViewModel { Id = item.Id, Name = item.Name });
            }

            return View(viewModels);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reasonForEviction = await _reasonForEvictionRepository.GetById(id);

            if (reasonForEviction == null)
            {
                return NotFound();
            }

            var viewModel = new ReasonForEvictionViewModel { Id = reasonForEviction.Id, Name = reasonForEviction.Name };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReasonForEvictionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var reasonForEviction = new ReasonForEviction { Id = viewModel.Id, Name = viewModel.Name };

                await _reasonForEvictionRepository.Edit(reasonForEviction);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reasonForEviction = await _reasonForEvictionRepository.GetById(id);

            if (reasonForEviction == null)
            {
                return NotFound();
            }

            var viewModel = new ReasonForEvictionViewModel { Id = reasonForEviction.Id, Name = reasonForEviction.Name };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ReasonForEvictionViewModel viewModel)
        {
            var reasonForEviction = new ReasonForEviction { Id = viewModel.Id, Name = viewModel.Name };

            await _reasonForEvictionRepository.Delete(reasonForEviction);

            return RedirectToAction("Index");
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ReasonForEvictionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var reasonForEviction = new ReasonForEviction { Name = viewModel.Name };

                await _reasonForEvictionRepository.Add(reasonForEviction);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
