using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.MentorViewModels.ViolationsAndIncentivesDataViewModels
{
    public class ViolationsAndIncentivesViewModel
    {
        public ViolationsAndIncentivesViewModel(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public int Id { get; set; }

        public string FullName { get; set; }
    }
}
