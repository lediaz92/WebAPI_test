using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_test.Models
{
    public class Profiles
    {
        [Key]
        public Guid ProfileID { get; set; }
        public string Name { get; set; }
        public Boolean IsActive { get; set; }
    }
}
