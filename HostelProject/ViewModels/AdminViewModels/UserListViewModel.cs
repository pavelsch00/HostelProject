using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.AdminViewModels
{
    public class UserListViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Missing Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Missing UserName")]
        public string UserName { get; set; }
    }
}
