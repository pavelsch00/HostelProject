using HostelProject.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.AdminViewModels.DataBaseViewModels
{
    public class RoomViewModel : IDataBaseViewMode
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Incorrect room number. Correct room number is [1-2]")]
        public int RoomNumber { get; set; }

        [Required]
        [Range(2, 15, ErrorMessage = "Incorrect floor number. Correct floor number is [2-15]")]
        public int FloorNumber { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Incorrect block number. Correct block number is [1-10]")]
        public int BlockNumber { get; set; }
    }
}
