using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenchRockers.Models
{
    public class Recommendations
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public int EmpId { get; set; }
        public virtual Employee Employee { get; set; }
        public string SupervisorName{ get; set; }
        public string Recommendation { get; set; }
    }
}