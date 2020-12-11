using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HostelProject.Interfaces;
using HostelProject.Models.Entities;
using HostelProject.ViewModels.ManagerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostelProject.Controllers.HostelMentorControllers
{
    [Authorize(Roles = "Mentor")]
    public class HostelMentorController : Controller
    {

        private readonly IRepository<Student> _studentRepository;

        private readonly IRepository<Specialty> _specialtyRepository;

        private readonly IRepository<Faculty> _facultyStudentRepository;

        private readonly IRepository<Room> _roomRepository;

        private readonly IRepository<Position> _positionRepository;

        private readonly IRepository<Mentor> _mentorRepository;

        public HostelMentorController(IRepository<Student> studentRepository, IRepository<Specialty> specialtyRepository,
            IRepository<Faculty> facultyStudentRepository, IRepository<Room> roomRepository, IRepository<Position> positionRepository, IRepository<Mentor> mentorRepository)
        {
            _studentRepository = studentRepository;
            _specialtyRepository = specialtyRepository;
            _facultyStudentRepository = facultyStudentRepository;
            _roomRepository = roomRepository;
            _positionRepository = positionRepository;
            _mentorRepository = mentorRepository;
        }

        public async Task<IActionResult> IndexAsync() => View(await ShowListStudentAsync());

        private async Task<List<StudentListViewModel>> ShowListStudentAsync()
        {
            var studentsList = new List<StudentListViewModel>();

            var mentorFacultyId = _specialtyRepository.GetAll().Where(item => item.FacultyId == _mentorRepository
                .GetAll().Where(ment => ment.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value).Select(u => u.FacultyId).FirstOrDefault())
                .Select(item => item.FacultyId).FirstOrDefault();
            var specialtyIdList = _specialtyRepository.GetAll().Where(item => item.FacultyId == mentorFacultyId).Select(item => item.Id).ToList();
            var studentList = new List<Student>();

            foreach (var specialtyId in specialtyIdList)
            {
                studentList.AddRange(_studentRepository.GetAll().Where(item => item.SpecialtyId == specialtyId).ToList());
            }
            
            foreach (var student in studentList.Where(item => item.RoomId != null))
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

            return studentsList;
        }
    }
}
