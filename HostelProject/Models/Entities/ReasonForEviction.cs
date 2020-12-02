using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.Models.Entities
{
    [Table("ReasonForEvictions")]
    public class ReasonForEviction
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
