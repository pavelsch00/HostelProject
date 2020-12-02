using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.Models.Entities
{
    [Table("Rooms")]
    public class Room
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public int FloorNumber { get; set; }

        public int BlockNumber { get; set; }
    }
}
