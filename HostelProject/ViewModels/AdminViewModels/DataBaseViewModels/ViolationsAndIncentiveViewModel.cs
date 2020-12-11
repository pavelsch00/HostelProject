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
        public ViolationsAndIncentiveViewModel(int id, string name, int score, DateTime date)
        {
            Id = id;
            Name = name;
            Score = score;
            Date = date;
        }

        public ViolationsAndIncentiveViewModel()
        {

        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Missing Name")]
        public string Name { get; set; }

        [Required]
        [Range(-200, 200, ErrorMessage = "Incorrect value. Correct value is [-200 - + 200]")]
        public int Score { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/2017", "1/1/2021")]
        public DateTime Date { get; set; }
    }
}
