using HostelProject.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.AdminViewModels.DataBaseViewModels
{
    public class SpecialtyViewModel : IDataBaseViewMode
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Missing Name")]
        public string Name { get; set; }

        [Required]
        public int FacultyId { get; set; }

        public List<int> ListFacultyId { get; set; }
    }
}
