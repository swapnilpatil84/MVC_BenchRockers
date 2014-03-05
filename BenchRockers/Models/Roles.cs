using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenchRockers.Models
{
    public class Roles
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}