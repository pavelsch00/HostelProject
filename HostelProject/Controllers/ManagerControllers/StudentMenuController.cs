using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostelProject.Interfaces;
using HostelProject.Models.Entities;
using HostelProject.ViewModels.ManagerViewModels;
using HostelProject.ViewModels.ManagerViewModels.StudentDataViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostelProject.Controllers.ManagerControllers
{
    [Authorize(Roles = "Manager")]
    public class StudentMenuController : Controller
    {
        private readonly IRepository<Student> _studentRepository;

        private readonly IRepository<Specialty> _specialtyRepository;

        private readonly IRepository<Faculty> _facultyStudentRepository;

        private readonly IRepository<Room> _roomRepository;

        private readonly IRepository<Position> _positionRepository;

        private readonly IRepository<ReasonForEviction> _reasonForEvictionRepository;

        public StudentMenuController(IRepository<Student> studentRepository, IRepository<Specialty> specialtyRepository,
            IRepository<Faculty> facultyStudentRepository, IRepository<Room> roomRepository, IRepository<Position> positionRepository,
            IRepository<ReasonForEviction> reasonForEvictionRepository)
        {
            _studentRepository = studentRepository;
            _specialtyRepository = specialtyRepository;
            _facultyStudentRepository = facultyStudentRepository;
            _roomRepository = roomRepository;
            _positionRepository = positionRepository;
            _reasonForEvictionRepository = reasonForEvictionRepository;
        }

        public async Task<IActionResult> IndexAsync() => View(await ShowListStudentAsync());

        public IActionResult Create()
        {
            var listRoom = _roomRepository.GetAll().ToList();
            var listSpecialty = _specialtyRepository.GetAll().ToList();
            var listPosition = _positionRepository.GetAll().ToList();

            var viewModel = new StudentViewModel
            {
                Room = (from item in listRoom
                        where _studentRepository.GetAll().ToList()
            .Where(student => student.RoomId == item.Id)
            .Count() < 2
                        select new RoomViewModel(item.Id, item.RoomNumber, item.FloorNumber, item.BlockNumber)).ToList(),
                Position = (from item in listPosition
                            select new PositionViewModel(item.Id, item.Name)).ToList(),
                Specialty = (from item in listSpecialty
                             select new SpecialtyViewModel(item.Id, item.Name, (int)item.FacultyId, _facultyStudentRepository.GetById(item.FacultyId).Result.Name)).ToList(),

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(StudentViewModel viewModel)
        {
            var listRoom = _roomRepository.GetAll().ToList();
            var listSpecialty = _specialtyRepository.GetAll().ToList();
            var listPosition = _positionRepository.GetAll().ToList();

            viewModel.Room = (from item in listRoom
                              where _studentRepository.GetAll().ToList()
                  .Where(student => student.RoomId == item.Id)
                  .Count() < 2
                              select new RoomViewModel(item.Id, item.RoomNumber, item.FloorNumber, item.BlockNumber)).ToList();
            viewModel.Position = (from item in listPosition
                                  select new PositionViewModel(item.Id, item.Name)).ToList();
            viewModel.Specialty = (from item in listSpecialty
                                   select new SpecialtyViewModel(item.Id, item.Name,
                                   (int)item.FacultyId, _facultyStudentRepository.GetById(item.FacultyId).Result.Name)).ToList();

            if (ModelState.IsValid)
            {
                if (await ValidateModel(viewModel) == false)
                {
                    return View(viewModel);
                }

                var student = new Student
                {
                    ReasonForEvictionId = 1,
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
                await _studentRepository.Add(student);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var listRoom = _roomRepository.GetAll().ToList();
            var listSpecialty = _specialtyRepository.GetAll().ToList();
            var listPosition = _positionRepository.GetAll().ToList();
            var student = await _studentRepository.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                RoomId = student.RoomId.GetValueOrDefault(),
                SpecialtyId = student.SpecialtyId.GetValueOrDefault(),
                PositionId = student.PositionId.GetValueOrDefault(),
                CheckInDate = student.CheckInDate,
                CheckOutDate = student.CheckOutDate,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                CourseNumber = student.CourseNumber,
                FullName = student.FullName,

                Room = (from item in listRoom
                            where _studentRepository.GetAll().ToList()
                            .Where(student => student.RoomId == item.Id)
                            .Count() < 2 select new RoomViewModel(item.Id, item.RoomNumber, item.FloorNumber, item.BlockNumber)).ToList(),

                Position = (from item in listPosition
                                  select new PositionViewModel(item.Id, item.Name)).ToList(),

                Specialty = (from item in listSpecialty
                                   select new SpecialtyViewModel(item.Id, item.Name,
                                   (int)item.FacultyId, _facultyStudentRepository.GetById(item.FacultyId).Result.Name)).ToList()

             };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel viewModel)
        {
            var listRoom = _roomRepository.GetAll().ToList();
            var listSpecialty = _specialtyRepository.GetAll().ToList();
            var listPosition = _positionRepository.GetAll().ToList();

            viewModel.Room = (from item in listRoom
                              where _studentRepository.GetAll().ToList()
                              .Where(student => student.RoomId == item.Id)
                              .Count() < 2
                              select new RoomViewModel(item.Id, item.RoomNumber, item.FloorNumber, item.BlockNumber)).ToList();
            viewModel.Position = (from item in listPosition
                                  select new PositionViewModel(item.Id, item.Name)).ToList();
            viewModel.Specialty = (from item in listSpecialty
                                   select new SpecialtyViewModel(item.Id, item.Name,
                                   (int)item.FacultyId, _facultyStudentRepository.GetById(item.FacultyId).Result.Name)).ToList();

            if (ModelState.IsValid)
            {
                if (!await ValidateModel(viewModel))
                {
                    return View(viewModel);
                }

                var student = new Student
                {
                    Id = viewModel.Id,
                    ReasonForEvictionId = 1,
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

        public async Task<IActionResult> Evict(int id)
        {
            var reasonForEvictionPosition = _reasonForEvictionRepository.GetAll().ToList();
            var student = await _studentRepository.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new EvictViewModel
            {
                Id = student.Id,

                EvictList = (from item in reasonForEvictionPosition
                             select new ReasonForEvictionViewModel(item.Id, item.Name)).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Evict(EvictViewModel viewModel)
        {
            var reasonForEvictionPosition = _reasonForEvictionRepository.GetAll().ToList();
            var student = await _studentRepository.GetById(viewModel.Id);

            viewModel.EvictList = (from item in reasonForEvictionPosition
                                   select new ReasonForEvictionViewModel(item.Id, item.Name)).ToList();

            if (ModelState.IsValid)
            {

                student.RoomId = null;
                student.ReasonForEvictionId = viewModel.EvictId;
                await _studentRepository.Edit(_studentRepository.GetById(viewModel.Id).Result.Id, student);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        private async Task<bool> ValidateModel(StudentViewModel viewModel)
        {

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

        private async Task<List<StudentListViewModel>> ShowListStudentAsync()
        {
            var studentsList = new List<StudentListViewModel>();

            foreach (var student in _studentRepository.GetAll().ToList())
            {
                if(student.RoomId != null)
                {
                    studentsList.Add(new StudentListViewModel()
                    {
                        Id = student.Id,
                        FullName = student.FullName,
                        Gender = student.Gender,
                        DateOfBirth = student.DateOfBirth,
                        CourseNumber = student.CourseNumber,
                        Specialty = (await _specialtyRepository.GetById(student.SpecialtyId)).Name,
                        Faculty = (await _facultyStudentRepository.GetById(_specialtyRepository.GetById(student.SpecialtyId).Result.FacultyId)).Name,
                        RoomNumber = (await _roomRepository.GetById(student.RoomId)).RoomNumber,
                        BlockNumber = (await _roomRepository.GetById(student.RoomId)).BlockNumber,
                        FloorNumber = (await _roomRepository.GetById(student.RoomId)).FloorNumber,
                        CheckInDate = student.CheckInDate,
                        CheckOutDate = student.CheckOutDate,
                        Position = (await _positionRepository.GetById(student.PositionId)).Name
                    });
                }
                else
                {
                    studentsList.Add(new StudentListViewModel()
                    {
                        Id = student.Id,
                        FullName = student.FullName,
                        Gender = student.Gender,
                        DateOfBirth = student.DateOfBirth,
                        CourseNumber = student.CourseNumber,
                        Specialty = (await _specialtyRepository.GetById(student.SpecialtyId)).Name,
                        Faculty = (await _facultyStudentRepository.GetById(_specialtyRepository.GetById(student.SpecialtyId).Result.FacultyId)).Name,
                        RoomNumber = null,
                        BlockNumber = null,
                        FloorNumber = null,
                        CheckInDate = student.CheckInDate,
                        CheckOutDate = student.CheckOutDate,
                        Position = (await _positionRepository.GetById(student.PositionId)).Name
                    });
                }
            }

            return studentsList;
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
