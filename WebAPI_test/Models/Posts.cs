using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_test.Models
{
    public class Posts
    {
        [Key]
        public Guid PostID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Status { get; set; }
        public DateTime? PublishedDate { get; set; }
        public Boolean IsActive { get; set; }
        public Guid CreatedByID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid UpdatedByID { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
