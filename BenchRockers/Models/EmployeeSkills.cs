using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenchRockers.Models
{
    public class EmployeeSkills
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int EmpSkillId { get; set; }
        public int EmpId { get; set; }
        public virtual Employee Employee { get; set; }
        public int SkillId { get; set; }
        public virtual Skills Skills { get; set; }
        public int Rating { get; set; }
    }
}