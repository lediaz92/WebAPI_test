using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_test.Data;
using WebAPI_test.Models;

namespace WebAPI_test.Repositories
{
    public class RepoUsers
    {
        private static WebAPI_testContext _contextUsers;

        public RepoUsers(WebAPI_testContext context)
        {
            _contextUsers = context;
        }

        public async Task<ActionResult<Users>> Login(string us, string pass)
        {

            // Query for the Blog named ADO.NET Blog
            var user = GetUserByLogin(us, pass);

            return user;

        }

        public Users GetUserByLogin(string us, string pass)
        {
            Users user = _contextUsers.Users
                            .Where(u => u.User == us && u.Password == pass)
                            .FirstOrDefault();
            return user;
        }
    }
}
