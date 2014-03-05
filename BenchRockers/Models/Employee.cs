using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenchRockers.Models
{
    public class Employee
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int EmpId { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public virtual Roles Roles { get; set; }
        public string Account { get; set; }
        public float TotalExp { get; set; }
        public string Location { get; set; }
        public bool IsOnBench { get; set; }
    }
}