using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPI_test.Data;
using WebAPI_test.Models;
using WebAPI_test.Repositories;

namespace WebAPI_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private RepoUsers _repoUsers;

        private readonly WebAPI_testContext _context;

        private Users loggedUser = new Users();

        public UsersController(WebAPI_testContext context)
        {
            _context = context;
            _repoUsers = new RepoUsers(_context);
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(Guid id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }


        // GET: api/Users/Login/esctritor/123
        [Route("[action]/{user}/{pass}")]
        [HttpGet]
        public async Task<ActionResult<Users>> Login(string user, string pass)
        {

            var users = await _repoUsers.Login(user, pass);

            if (users.Value == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("SessionUser", user);
            HttpContext.Session.SetString("SessionPass", pass);
            return users;
        }


        // GET: api/Users/Logout
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<Users>> Logout()
        {

            HttpContext.Session.SetString("SessionUser", "");
            HttpContext.Session.SetString("SessionPass", "");
            return Ok();
        }



        private bool UsersExists(Guid id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
