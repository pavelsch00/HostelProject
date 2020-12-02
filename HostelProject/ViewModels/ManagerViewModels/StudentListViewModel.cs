using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.ManagerViewModels
{
    public class StudentListViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int? RoomNumber { get; set; }

        public int? FloorNumber { get; set; }

        public int? BlockNumber { get; set; }

        public string Position { get; set; }

        public string Specialty { get; set; }

        public string Faculty { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CourseNumber { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }
    }
}
