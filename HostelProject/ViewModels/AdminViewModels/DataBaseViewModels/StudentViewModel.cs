using HostelProject.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.AdminViewModels.DataBaseViewModels
{
    public class StudentViewModel : IDataBaseViewMode
    {
        public int Id { get; set; }

        [Required]
        public int ReasonForEvictionId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public int PositionId { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        [Required(ErrorMessage = "Missing Name")]
        public string FullName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/1980", "1/1/2005")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int CourseNumber { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/2018", "1/1/2028")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Range(typeof(DateTime), "2/1/2018", "2/1/2028")]
        public DateTime CheckOutDate { get; set; }

        public List<int> ListReasonForEvictionId { get; set; }

        public List<int> ListRoomId { get; set; }

        public List<int> ListPositionId { get; set; }

        public List<int> ListSpecialtyId { get; set; }
    }
}
