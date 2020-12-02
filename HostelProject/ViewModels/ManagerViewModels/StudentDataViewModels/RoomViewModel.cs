using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.ViewModels.ManagerViewModels.StudentDataViewModels
{
    public class RoomViewModel
    {
        public RoomViewModel(int id, int roomNumber, int floorNumber, int blockNumber)
        {
            Id = id;
            RoomNumber = roomNumber;
            FloorNumber = floorNumber;
            BlockNumber = blockNumber;
        }

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

        public override string ToString() => $"{FloorNumber} - {BlockNumber} - {RoomNumber}";
    }
}
