using HostelProject.ViewModels.ManagerViewModels.StudentDataViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.ManagerViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Missing Name")]
        public string FullName { get; set; }

        public int? RoomId { get; set; }

        public int PositionId { get; set; }

        public int SpecialtyId { get; set; }

        public List<RoomViewModel> Room { get; set; }

        public List<PositionViewModel> Position { get; set; }

        public List<SpecialtyViewModel> Specialty { get; set; }

        public string Gender { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/1980", "1/1/2005")]
        public DateTime DateOfBirth { get; set; }

        public int CourseNumber { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/2018", "1/1/2028")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Range(typeof(DateTime), "2/1/2018", "2/1/2028")]
        public DateTime CheckOutDate { get; set; }
    }
}
