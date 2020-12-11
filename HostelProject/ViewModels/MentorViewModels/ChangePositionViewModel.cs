using HostelProject.ViewModels.ManagerViewModels.StudentDataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.MentorViewModels
{
    public class ChangePositionViewModel
    {
        public int Id { get; set; }

        public int PositionId { get; set; }

        public List<PositionViewModel> PositionList { get; set; }
    }
}
