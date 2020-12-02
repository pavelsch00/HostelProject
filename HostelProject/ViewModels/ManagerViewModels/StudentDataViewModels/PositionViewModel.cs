using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.ManagerViewModels.StudentDataViewModels
{
    public class PositionViewModel
    {
        public PositionViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Missing Name")]
        public string Name { get; set; }

        public override string ToString() => $"{Name}";
    }
}
