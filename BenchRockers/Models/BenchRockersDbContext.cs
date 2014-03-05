using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BenchRockers.Models
{
    public class BenchRockersDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skills> Skill{ get; set; }
        public DbSet<EmployeeSkills> EmployeeSkill { get; set; }
        public DbSet<Recommendations> Recommendation { get; set; }

        public DbSet<Roles> Roles { get; set; }
    }
}