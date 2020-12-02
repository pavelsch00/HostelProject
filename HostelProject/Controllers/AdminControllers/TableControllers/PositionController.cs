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
    public class PositionController : Controller, ITableController<PositionViewModel>
    {
        private readonly IRepository<Position> _positionRepository;

        public PositionController(IRepository<Position> positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public IActionResult Index()
        {
            var viewModels = new List<PositionViewModel>();

            foreach (var item in _positionRepository.GetAll().ToList())
            {
                viewModels.Add(new PositionViewModel { Id = item.Id, Name = item.Name });
            }

            return View(viewModels);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var position = await _positionRepository.GetById(id);

            if (position == null)
            {
                return NotFound();
            }

            var viewModel = new PositionViewModel { Id = position.Id, Name = position.Name };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PositionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var position = new Position { Id = viewModel.Id, Name = viewModel.Name };

                await _positionRepository.Edit(position);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var position = await _positionRepository.GetById(id);

            if (position == null)
            {
                return NotFound();
            }

            var viewModel = new PositionViewModel { Id = position.Id, Name = position.Name };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PositionViewModel viewModel)
        {
            var position = new Position { Id = viewModel.Id, Name = viewModel.Name };

            await _positionRepository.Delete(position);

            return RedirectToAction("Index");
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(PositionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var position = new Position { Name = viewModel.Name };

                await _positionRepository.Add(position);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
