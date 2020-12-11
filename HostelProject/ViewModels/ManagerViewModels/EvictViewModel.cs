using HostelProject.ViewModels.ManagerViewModels.StudentDataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.ManagerViewModels
{
    public class EvictViewModel
    {
        public int Id { get; set; }

        public int EvictId { get; set; }

        public List<ReasonForEvictionViewModel> EvictList { get; set; }
    }
}
