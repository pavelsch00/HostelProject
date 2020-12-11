using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.ManagerViewModels.StudentDataViewModels
{
    public class ReasonForEvictionViewModel
    {
        public ReasonForEvictionViewModel(int id, string ame)
        {
            Id = id;
            Name = ame;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
