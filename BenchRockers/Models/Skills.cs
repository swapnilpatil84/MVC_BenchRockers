using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenchRockers.Models
{
    public class Skills
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int SkillId { get; set; }
        public string Name { get; set; }
    }
}