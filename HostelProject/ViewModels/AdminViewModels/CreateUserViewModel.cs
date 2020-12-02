using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.AdminViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Missing Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Missing Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Missing Username")]
        public string Username { get; set; }
    }
}
