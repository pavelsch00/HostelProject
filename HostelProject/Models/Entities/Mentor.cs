using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.Models.Entities
{
    [Table("Mentors")]
    public class Mentor
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int? FacultyId { get; set; }
    }
}
