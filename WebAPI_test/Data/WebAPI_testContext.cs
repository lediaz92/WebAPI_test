using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI_test.Models;

namespace WebAPI_test.Data
{
    public class WebAPI_testContext : DbContext
    {
        public WebAPI_testContext (DbContextOptions<WebAPI_testContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPI_test.Models.Posts> Posts { get; set; }

        public DbSet<WebAPI_test.Models.Users> Users { get; set; }

        public DbSet<WebAPI_test.Models.Profiles> Profiles { get; set; }

        public DbSet<WebAPI_test.Models.Comments> Comments { get; set; }
    }
}
