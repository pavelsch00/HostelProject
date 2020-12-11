using HostelProject.ViewModels.MentorViewModels.ViolationsAndIncentivesDataViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.MentorViewModels
{
    public class AddViolationViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ViolationsAndIncentivesId { get; set; }

        public List<ViolationsAndIncentivesViewModel> ListViolationsAndIncentives { get; set; }

        public string FullName { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/2019", "1/1/2025")]
        public DateTime Date { get; set; }
    }
}
