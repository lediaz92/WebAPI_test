using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_test.Models
{
    public class Comments
    {
        [Key]
        public Guid CommentID { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public Guid PostID { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
