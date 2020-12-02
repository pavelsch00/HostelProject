using HostelProject.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.AdminViewModels.DataBaseViewModels
{
    public class MentorViewModel : IDataBaseViewMode
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int FacultyId { get; set; }

        public List<string> ListUserId { get; set; }

        public List<int> ListFacultyId { get; set; }
    }
}
