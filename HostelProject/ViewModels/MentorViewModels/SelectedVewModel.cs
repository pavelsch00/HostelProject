using HostelProject.ViewModels.ManagerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.MentorViewModels
{
    public class SelectedVewModel
    {
        public List<StudentListViewModel> StudentList { get; set; }

        public string FullName { get; set; }

        public string SpecialtyName { get; set; }

        [Range(0, 5, ErrorMessage = "Incorrect course number. Correct course number is [1-5]")]
        public int CourseNumber { get; set; }
    }
}
