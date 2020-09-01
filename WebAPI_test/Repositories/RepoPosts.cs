using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_test.Data;
using WebAPI_test.Models;

namespace WebAPI_test.Repositories
{
    public class RepoPosts
    {
        private static WebAPI_testContext _contextPost;

        public RepoPosts(WebAPI_testContext context)
        {
            _contextPost = context;
        }


        public Posts GetPostsByTitle(string title)
        {
            // Query para obtener un post por su titulo
            var post = _contextPost.Posts
                            .Where(p => p.Title == title)
                            .FirstOrDefault();
            return post;
        }

        public async Task<ActionResult<Posts>> GetPostsByTitleAsync(string title)
        {
            return GetPostsByTitle(title);
            
        }

        public Posts GetPostsByID (Guid id)
        {
            // Query para obtener un post por su titulo
            var post = _contextPost.Posts
                            .Where(p => p.PostID == id)
                            .FirstOrDefault();
            return post;
        }


        public async Task<ActionResult<Posts>> GetPostsByIDAsync(Guid id)
        {
            return GetPostsByID(id);

        }
    }
}
