using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostelProject.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HostelProject.Models
{
    public class HostelDbContext : IdentityDbContext<User>
    {
        public HostelDbContext(DbContextOptions<HostelDbContext> options)
            : base(options)
        {
          
        }

        public DbSet<Faculty> Facultys { get; set; }

        public DbSet<Mentor> Mentors { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Specialty> Specialtys { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<ReasonForEviction> ReasonForEvictions { get; set; }

        public DbSet<ViolationsAndIncentive> ViolationsAndIncentives { get; set; }

        public DbSet<ViolationsAndIncentivesStudent> ViolationsAndIncentivesStudents { get; set; }
    }
}
