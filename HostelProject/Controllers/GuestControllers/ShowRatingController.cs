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

        public async Task<IActionResult> IndexAsync(string facultyName, string specialtyName, int courseNumber, string fullName) => View(await GetViolationsAndIncentiveListAsync(facultyName, specialtyName, courseNumber, fullName));

        private async Task<SelectedListViewModel> GetViolationsAndIncentiveListAsync(string facultyName, string speciltyName, int courseNumber, string fullName)
        {
            var studentRating = new SelectedListViewModel();
            studentRating.RatingList = new List<RatingViewModel>();
            List<Student> studentList;
            int count = 1, totalScore = 0;

            if (!string.IsNullOrEmpty(fullName))
            {
                studentList = _studentRepository.GetAll().Where(item => item.FullName.Contains(fullName)).ToList();
            }
            else
            {
                studentList = _studentRepository.GetAll().ToList();
            }

            foreach (var student in studentList)
            {
                foreach (var item in _violationsAndIncentivesStudentRepository.GetAll().ToList().Where(item => item.StudentId == student.Id).Select(a => a.ViolationsAndIncentivesId))
                {
                    totalScore += (await _violationsAndIncentiveRepository.GetById(item)).Score;
                }

                studentRating.RatingList.Add(new RatingViewModel()
                {
                    FullName = student.FullName,
                    CourseNumber = student.CourseNumber,
                    Specialty = (await _specialtyRepository.GetById(student.SpecialtyId)).Name,
                    Faculty = (await _facultyStudentRepository.GetById(_specialtyRepository.GetById(student.SpecialtyId).Result.FacultyId)).Name,
                    Score = totalScore,
                });

                totalScore = 0;
            }

            if (!string.IsNullOrEmpty(facultyName))
            {
                studentRating.RatingList = studentRating.RatingList.Where(item => item.Faculty.Contains(facultyName)).ToList();
            }

            if (!string.IsNullOrEmpty(speciltyName))
            {
                studentRating.RatingList = studentRating.RatingList.Where(item => item.Specialty.Contains(speciltyName)).ToList();
            }

            if (courseNumber != 0)
            {
                studentRating.RatingList = studentRating.RatingList.Where(item => item.CourseNumber == courseNumber).ToList();
            }

            studentRating.RatingList = studentRating.RatingList.OrderByDescending(item => item.Score).ToList();

            foreach (var item in studentRating.RatingList)
            {
                item.Count = count;
                count++;
            }

            return studentRating;
        }
    }
}
