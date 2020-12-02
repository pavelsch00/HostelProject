using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.ManagerViewModels.StudentDataViewModels
{
    public class SpecialtyViewModel
    {
        public SpecialtyViewModel(int id, string name, int facultyId, string facultyName)
        {
            Id = id;
            Name = name;
            FacultyId = facultyId;
            FacultyName = facultyName;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Missing Name")]
        public string Name { get; set; }

        public int FacultyId { get; set; }

        public string FacultyName { get; set; }

        public override string ToString() => $"{Name} ({FacultyName})";
    }
}
