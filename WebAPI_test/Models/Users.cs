using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_test.Models
{
    public class Users
    {
        [Key]
        public Guid UserID { get; set; } = Guid.Empty;
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
        public Guid ProfileID { get; set; } = Guid.Empty;
        public Boolean IsActive { get; set; } = false;
    }
}
