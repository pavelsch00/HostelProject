using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.Models.Entities
{
    [Table("Positions")]
    public class Position
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
