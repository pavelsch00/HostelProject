using HostelProject.ViewModels.AdminViewModels.DataBaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels
{
    public class ViolationsAndIncentivesStudentListViewModel
    {
        public ViolationsAndIncentivesStudentListViewModel(int id, string fullName, List<ViolationsAndIncentiveViewModel> violationsAndIncentiveViewModelList, int totalScore)
        {
            Id = id;
            FullName = fullName;
            ViolationsAndIncentiveViewModelList = violationsAndIncentiveViewModelList;
            TotalScore = totalScore;
            ViolationsAndIncentiveViewModelList = new List<ViolationsAndIncentiveViewModel>();
        }

        public ViolationsAndIncentivesStudentListViewModel()
        {

        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public List<ViolationsAndIncentiveViewModel> ViolationsAndIncentiveViewModelList { get; set; }

        public int TotalScore { get; set; }
    }
}
