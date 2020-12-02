using HostelProject.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.AdminViewModels.DataBaseViewModels
{
    public class ViolationsAndIncentiveViewModel : IDataBaseViewMode
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Missing Name")]
        public string Name { get; set; }

        [Required]
        [Range(-200, 200, ErrorMessage = "Incorrect value. Correct value is [-200 - + 200]")]
        public int Score { get; set; }
    }
}
