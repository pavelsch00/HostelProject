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
    public class RoomController : Controller, ITableController<RoomViewModel>
    {
        public readonly IRepository<Room> _roomRepository;

        public RoomController(IRepository<Room> positionRepository)
        {
            _roomRepository = positionRepository;
        }

        public IActionResult Index()
        {
            var viewModels = new List<RoomViewModel>();

            foreach (var item in _roomRepository.GetAll().ToList())
            {
                viewModels.Add(new RoomViewModel { Id = item.Id, FloorNumber = item.FloorNumber,
                    BlockNumber = item.BlockNumber, RoomNumber = item.RoomNumber });
            }

            return View(viewModels);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomRepository.GetById(id);

            if (room == null)
            {
                return NotFound();
            }

            var viewModel = new RoomViewModel { Id = room.Id, FloorNumber = room.FloorNumber,
                BlockNumber = room.BlockNumber, RoomNumber = room.RoomNumber };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoomViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var room = new Room { Id = viewModel.Id, FloorNumber = viewModel.FloorNumber,
                    BlockNumber = viewModel.BlockNumber, RoomNumber = viewModel.RoomNumber };

                await _roomRepository.Edit(room);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomRepository.GetById(id);

            if (room == null)
            {
                return NotFound();
            }

            var viewModel = new RoomViewModel { Id = room.Id, FloorNumber = room.FloorNumber,
                BlockNumber = room.BlockNumber, RoomNumber = room.RoomNumber };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RoomViewModel viewModel)
        {
            var room = new Room
            {
                Id = viewModel.Id,
                FloorNumber = viewModel.FloorNumber,
                BlockNumber = viewModel.BlockNumber,
                RoomNumber = viewModel.RoomNumber
            };

            await _roomRepository.Delete(room);

            return RedirectToAction("Index");
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RoomViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var room = new Room { FloorNumber = viewModel.FloorNumber,
                    BlockNumber = viewModel.BlockNumber, RoomNumber = viewModel.RoomNumber };

                await _roomRepository.Add(room);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
