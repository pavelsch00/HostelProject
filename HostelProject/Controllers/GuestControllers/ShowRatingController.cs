using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostelProject.Interfaces;
using HostelProject.Models.Entities;
using HostelProject.ViewModels.GuestViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HostelProject.Controllers.GuestControllers
{
    public class ShowRatingController : Controller
    {
        private readonly IRepository<Student> _studentRepository;

        private readonly IRepository<ViolationsAndIncentive> _violationsAndIncentiveRepository;

        private readonly IRepository<ViolationsAndIncentivesStudent> _violationsAndIncentivesStudentRepository;

        private readonly IRepository<Specialty> _specialtyRepository;

        private readonly IRepository<Faculty> _facultyStudentRepository;

        public ShowRatingController(IRepository<Student> studentRepository, IRepository<ViolationsAndIncentive> violationsAndIncentiveRepository,
            IRepository<ViolationsAndIncentivesStudent> violationsAndIncentivesStudentRepository, IRepository<Specialty> specialtyRepository,
            IRepository<Faculty> facultyStudentRepository)
        {
            _studentRepository = studentRepository;

            _violationsAndIncentiveRepository = violationsAndIncentiveRepository;

            _violationsAndIncentivesStudentRepository = violationsAndIncentivesStudentRepository;

            _specialtyRepository = specialtyRepository;

            _facultyStudentRepository = facultyStudentRepository;
        }

        public async Task<IActionResult> IndexAsync() => View(await GetViolationsAndIncentiveListAsync());

        private async Task<List<RatingViewModel>> GetViolationsAndIncentiveListAsync()
        {
            var studentRating = new List<RatingViewModel>();

            int count = 1, totalScore = 0;


            foreach (var student in _studentRepository.GetAll().ToList())
            {
                foreach (var item in _violationsAndIncentivesStudentRepository.GetAll().ToList().Where(item => item.StudentId == student.Id).Select(a => a.ViolationsAndIncentivesId))
                {
                    totalScore += (await _violationsAndIncentiveRepository.GetById(item)).Score;
                }

                studentRating.Add(new RatingViewModel()
                {
                    FullName = student.FullName,
                    CourseNumber = student.CourseNumber,
                    Specialty = (await _specialtyRepository.GetById(student.SpecialtyId)).Name,
                    Faculty = (await _facultyStudentRepository.GetById(_specialtyRepository.GetById(student.SpecialtyId).Result.FacultyId)).Name,
                    Score = totalScore,
                });

                totalScore = 0;
            }

            studentRating = studentRating.OrderByDescending(item => item.Score).ToList();

            foreach (var item in studentRating)
            {
                item.Count = count;
                count++;
            }

            return studentRating;
        }
    }
}
