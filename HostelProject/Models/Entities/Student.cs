using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.Models.Entities
{
    [Table("Students")]
    public class Student
    {
        public int Id { get; set; }

        public int? ReasonForEvictionId { get; set; }

        public int? RoomId { get; set; }

        public int? PositionId { get; set; }

        public int? SpecialtyId { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CourseNumber { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }
    }
}
