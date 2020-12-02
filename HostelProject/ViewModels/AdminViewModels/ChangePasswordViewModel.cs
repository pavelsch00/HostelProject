using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.AdminViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Missing New Password")]
        public string NewPassword { get; set; }
    }
}
