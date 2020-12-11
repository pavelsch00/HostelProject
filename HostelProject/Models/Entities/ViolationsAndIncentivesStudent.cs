using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.Models.Entities
{
    [Table("ViolationsAndIncentivesStudents")]
    public class ViolationsAndIncentivesStudent
    {
        public int Id { get; set; }

        public int? ViolationsAndIncentivesId { get; set; }

        public int? StudentId { get; set; }

        public DateTime Date { get; set; }
    }
}
