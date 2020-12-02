using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.Models.Entities
{
    [Table("Specialtys")]
    public class Specialty
    {
        public int Id { get; set; }

        public int? FacultyId { get; set; }

        public string Name { get; set; }
    }
}
