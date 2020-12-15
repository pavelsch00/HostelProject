using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.ManagerViewModels
{
    public class SelectedStudentVewModel
    {
        public List<StudentListViewModel> StudentList { get; set; }

        public string FullName { get; set; }

        public string SpecialtyName { get; set; }

        public string FacultyName { get; set; }

        [Range(0, 5, ErrorMessage = "Incorrect course number. Correct course number is [1-5]")]
        public int CourseNumber { get; set; }
    }
}
