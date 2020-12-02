using HostelProject.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.AdminViewModels.DataBaseViewModels
{
    public class ViolationsAndIncentivesStudentViewModel : IDataBaseViewMode
    {
        public int Id { get; set; }

        [Required]
        public int ViolationsAndIncentivesId { get; set; }

        [Required]
        public int StudentId { get; set; }

        public List<int> ListViolationsAndIncentivesId { get; set; }

        public List<int> ListStudentId { get; set; }
    }
}
