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

namespace HostelProject.Controllers.AdminControllers.TableControllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller, ITableController<StudentViewModel>
    {
        private readonly IRepository<Student> _studentRepository;

        private readonly IRepository<ReasonForEviction> _reasonForEviction;

        private readonly IRepository<Room> _roomRepository;

        private readonly IRepository<Specialty> _specialtyRepository;

        private readonly IRepository<Position> _positionRepository;

        public StudentController(IRepository<Student> studentRepository, IRepository<ReasonForEviction> reasonForEviction,
            IRepository<Room> roomRepository, IRepository<Specialty> specialtyRepository, IRepository<Position> positionRepository)
        {
            _studentRepository = studentRepository;
            _reasonForEviction = reasonForEviction;
            _roomRepository = roomRepository;
            _specialtyRepository = specialtyRepository;
            _positionRepository = positionRepository;
        }

        public IActionResult Index()
        {
            var viewModels = new List<StudentViewModel>();

            foreach (var item in _studentRepository.GetAll())
            {
                viewModels.Add(new StudentViewModel
                {
                    Id = item.Id,
                    ReasonForEvictionId = item.ReasonForEvictionId.GetValueOrDefault(),
                    RoomId = item.RoomId.GetValueOrDefault(),
                    SpecialtyId = item.SpecialtyId.GetValueOrDefault(),
                    PositionId = item.PositionId.GetValueOrDefault(),
                    CheckInDate = item.CheckInDate,
                    CheckOutDate = item.CheckOutDate,
                    DateOfBirth = item.DateOfBirth,
                    Gender = item.Gender,
                    CourseNumber = item.CourseNumber,
                    FullName = item.FullName
                });
            }

            return View(viewModels);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepository.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            var listReasonForEvictionId = _reasonForEviction.GetAll().Select(item => item.Id).ToList();
            var listRoomId = _roomRepository.GetAll().Select(item => item.Id).ToList();
            var listSpecialtyId = _specialtyRepository.GetAll().Select(item => item.Id).ToList();
            var listPositionId = _positionRepository.GetAll().Select(item => item.Id).ToList();

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                ReasonForEvictionId = student.ReasonForEvictionId.GetValueOrDefault(),
                RoomId = student.RoomId.GetValueOrDefault(),
                SpecialtyId = student.SpecialtyId.GetValueOrDefault(),
                PositionId = student.PositionId.GetValueOrDefault(),
                CheckInDate = student.CheckInDate,
                CheckOutDate = student.CheckOutDate,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                CourseNumber = student.CourseNumber,
                FullName = student.FullName,
                ListReasonForEvictionId = listReasonForEvictionId,
                ListRoomId = listRoomId,
                ListSpecialtyId = listSpecialtyId,
                ListPositionId = listPositionId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel viewModel)
        {
            var listReasonForEvictionId = _reasonForEviction.GetAll().Select(item => item.Id).ToList();
            var listRoomId = _roomRepository.GetAll().Select(item => item.Id).ToList();
            var listSpecialtyId = _specialtyRepository.GetAll().Select(item => item.Id).ToList();
            var listPositionId = _positionRepository.GetAll().Select(item => item.Id).ToList();

            viewModel.ListReasonForEvictionId = listReasonForEvictionId;
            viewModel.ListRoomId = listRoomId;
            viewModel.ListSpecialtyId = listSpecialtyId;
            viewModel.ListPositionId = listPositionId;

            if (ModelState.IsValid)
            {
                if (!await ValidateModel(viewModel))
                {
                    return View(viewModel);
                }

                var student = new Student
                {
                    Id = viewModel.Id,
                    ReasonForEvictionId = viewModel.ReasonForEvictionId,
                    RoomId = viewModel.RoomId,
                    SpecialtyId = viewModel.SpecialtyId,
                    PositionId = viewModel.PositionId,
                    CheckInDate = viewModel.CheckInDate,
                    CheckOutDate = viewModel.CheckOutDate,
                    DateOfBirth = viewModel.DateOfBirth,
                    Gender = viewModel.Gender,
                    CourseNumber = viewModel.CourseNumber,
                    FullName = viewModel.FullName,
                };

                await _studentRepository.Edit(student.Id, student);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepository.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                ReasonForEvictionId = student.ReasonForEvictionId.GetValueOrDefault(),
                RoomId = student.RoomId.GetValueOrDefault(),
                SpecialtyId = student.SpecialtyId.GetValueOrDefault(),
                PositionId = student.PositionId.GetValueOrDefault(),
                CheckInDate = student.CheckInDate,
                CheckOutDate = student.CheckOutDate,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                CourseNumber = student.CourseNumber,
                FullName = student.FullName
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentViewModel viewModel)
        {
            var student = new Student
            {
                Id = viewModel.Id,
                ReasonForEvictionId = viewModel.ReasonForEvictionId,
                RoomId = viewModel.RoomId,
                SpecialtyId = viewModel.SpecialtyId,
                PositionId = viewModel.PositionId,
                CheckInDate = viewModel.CheckInDate,
                CheckOutDate = viewModel.CheckOutDate,
                DateOfBirth = viewModel.DateOfBirth,
                Gender = viewModel.Gender,
                CourseNumber = viewModel.CourseNumber,
                FullName = viewModel.FullName
            };

            await _studentRepository.Delete(student);

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var listReasonForEvictionId = _reasonForEviction.GetAll().Select(item => item.Id).ToList();
            var listRoomId = _roomRepository.GetAll().Select(item => item.Id).ToList();
            var listSpecialtyId = _specialtyRepository.GetAll().Select(item => item.Id).ToList();
            var listPositionId = _positionRepository.GetAll().Select(item => item.Id).ToList();

            var viewModel = new StudentViewModel
            {
                ListReasonForEvictionId = listReasonForEvictionId,
                ListRoomId = listRoomId,
                ListSpecialtyId = listSpecialtyId,
                ListPositionId = listPositionId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel viewModel)
        {
            var listReasonForEvictionId = _reasonForEviction.GetAll().Select(item => item.Id).ToList();
            var listRoomId = _roomRepository.GetAll().Select(item => item.Id).ToList();
            var listSpecialtyId = _specialtyRepository.GetAll().Select(item => item.Id).ToList();
            var listPositionId = _positionRepository.GetAll().Select(item => item.Id).ToList();

            viewModel.ListReasonForEvictionId = listReasonForEvictionId;
            viewModel.ListRoomId = listRoomId;
            viewModel.ListSpecialtyId = listSpecialtyId;
            viewModel.ListPositionId = listPositionId;

            if (ModelState.IsValid)
            {
                if (await ValidateModel(viewModel) == false)
                {
                    return View(viewModel);
                }

                var student = new Student
                {
                    Id = viewModel.Id,
                    ReasonForEvictionId = viewModel.ReasonForEvictionId,
                    RoomId = viewModel.RoomId,
                    SpecialtyId = viewModel.SpecialtyId,
                    PositionId = viewModel.PositionId,
                    CheckInDate = viewModel.CheckInDate,
                    CheckOutDate = viewModel.CheckOutDate,
                    DateOfBirth = viewModel.DateOfBirth,
                    Gender = viewModel.Gender,
                    CourseNumber = viewModel.CourseNumber,
                    FullName = viewModel.FullName
                };

                await _studentRepository.Add(student);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        private async Task<bool> ValidateModel(StudentViewModel viewModel)
        {
            if (viewModel.FullName == null)
            {
                ModelState.AddModelError("", "Enter FullName");
                return false;
            }

            if (await _roomRepository.GetById(viewModel.RoomId) == null)
            {
                ModelState.AddModelError("", "RoomId does not exist");
                return false;
            }

            if (await _reasonForEviction.GetById(viewModel.ReasonForEvictionId) == null)
            {
                ModelState.AddModelError("", "ReasonForEvictionId does not exist");
                return false;
            }

            if (await _specialtyRepository.GetById(viewModel.SpecialtyId) == null)
            {
                ModelState.AddModelError("", "SpecialtyId does not exist");
                return false;
            }

            if (await _positionRepository.GetById(viewModel.PositionId) == null)
            {
                ModelState.AddModelError("", "PositionId does not exist");
                return false;
            }

            if (viewModel.CheckInDate > viewModel.CheckOutDate)
            {
                ModelState.AddModelError("", "Incorrect CheckInDate or CheckOutDate");
                return false;
            }

            if (!await CheckRoomGenderAsync(viewModel))
            {
                ModelState.AddModelError("", "Only students of the same gender can live in a room");
                return false;
            }

            if (!CheckcountStudentInRoom(viewModel))
            {
                ModelState.AddModelError("", "Only two students can live in one room");
                return false;
            }

            return true;
        }

        private async Task<bool> CheckRoomGenderAsync(StudentViewModel viewModel)
        {
            var roomList = _roomRepository.GetAll().ToList();
            var studentsList = _studentRepository.GetAll().ToList();
            var viewModelRoom = await _roomRepository.GetById(viewModel.RoomId);

            roomList.Remove(await _roomRepository.GetById(viewModel.RoomId));

            foreach (var item in roomList)
            {
                if (item.FloorNumber == viewModelRoom.FloorNumber && item.BlockNumber == viewModelRoom.BlockNumber)
                {
                    foreach (var student in studentsList)
                    {
                        if (student.RoomId == item.Id && student.Gender.Replace(" ", "") != viewModel.Gender.Replace(" ", ""))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }


        private bool CheckcountStudentInRoom(StudentViewModel viewModel) => 
            _studentRepository.GetAll().ToList()
            .Where(item => item.RoomId == viewModel.RoomId)
            .Count() < 2;
    } 
}
