using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_test.Data;
using WebAPI_test.Models;
using WebAPI_test.Repositories;

namespace WebAPI_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private RepoComments _repoComment;
        private RepoPosts _repoPosts;
        private RepoUsers _repousers;
        private RepoProfiles _repoprofiles;

        private readonly WebAPI_testContext _context;

        public CommentsController(WebAPI_testContext context)
        {
            _context = context;
            _repoComment = new RepoComments(context);
            _repoPosts = new RepoPosts(context);
            _repousers = new RepoUsers(context);
            _repoprofiles = new RepoProfiles(context);
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GetComments()
        {
            // Obtengo todos los comentarios
            List<Comments> comments = await _context.Comments.ToListAsync();

            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));

            Profiles profile = new Profiles();
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
            }

            // Si el usuario es de tipo Editor devuelvo la lista completa
            if (profile != null && profile.Name == "Editor")
            {
                // Chequeo que la lista no esta vacia 
                if (comments == null)
                {
                    return NoContent();
                }

                return comments;
            }

            // Si no es editor sigo con el filtro
            // Creo una lista donde se van a guardar los comentarios despues del filtro
            List<Comments> listaComments = new List<Comments>();

            // Creo una variable Post
            Posts post = new Posts();

            // recorro los comentarios, si coincide con el post que estoy buscando y esta activo lo agrego a la lista
            foreach (Comments comment in comments)
            {
                // Obtengo el post correspondiente al comentario
                post = _repoPosts.GetPostsByID(comment.PostID);

                // Chequeo que el post este activo y aprobado
                if(post.IsActive == true && post.Status == 3)
                {
                    // Chequeo que el comentario este activo
                    if (comment.IsActive == true)
                    {
                        // Agrego a la lista
                        listaComments.Add(comment);
                    }
                }
            }

            // Chequeo que la lista no esta vacia 
            if (listaComments.Count == 0)
            {
                return NoContent();
            }

            return listaComments;
        }

        // GET: api/Comments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> GetComments(Guid id)
        {
            // Obtengo el comentario
            Comments comments = await _context.Comments.FindAsync(id);

            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));
            Profiles profile = new Profiles();
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
            }


            // Si el usuario es de tipo Editor devuelvo la lista completa
            if (profile != null && profile.Name == "Editor")
            {
                // Chequeo que la lista no esta vacia 
                if (comments == null)
                {
                    return NoContent();
                }

                return comments;
            }

            // Si no es editor sigo con el filtro
            // Obtengo el post correspondiente al comentario
            Posts post = _repoPosts.GetPostsByID(id);

            // Chequeo si el post esta activo y aprobado
            if (post.IsActive == true && post.Status == 3)
            {
                //Chequeo que el comentario no sea nulo o que este desactivado
                if (comments == null || comments.IsActive == false)
                {
                    return NotFound();
                }

                return comments;
            }
            else
            {
                return NotFound();
            }
           
        }

        // GET: api/Comments/GetCommentByPostID/{id}
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GetCommentByPostID(Guid id)
        {
            // Obtengo comentarios
            List<Comments> comments = await _context.Comments.ToListAsync();

            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));
            Profiles profile = new Profiles();
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
            }
            // Creo una lista donde voy a agregar las comentarios para el post que coincida con el ID
            List<Comments> listaComments = new List<Comments>();

            // Si el usuario es de tipo Editor devuelvo la lista completa para ese Post
            if (profile != null && profile.Name == "Editor")
            {
                // recorro los comentarios, si coincide con el post que estoy buscando y esta activo lo agrego a la lista
                foreach (Comments comment in comments)
                {
                    // Solo chequeo que el comentario sea el del post que estoy buscando
                    if (comment.PostID == id)
                    {
                        listaComments.Add(comment);
                    }
                }

                // Chequeo que la lista no esta vacia 
                if (listaComments.Count == 0)
                {
                    return NotFound();
                }

                return listaComments;
            }
            
            // Si no es editor sigo con el filtro
            // Obtengo el Post
            Posts post = _repoPosts.GetPostsByID(id);

            // Chequeo que el post este activo y aprobado
            if (post.IsActive == true && post.Status == 3)
            {
                // recorro los comentarios, si coincide con el post que estoy buscando y esta activo lo agrego a la lista
                foreach (Comments comment in comments)
                {
                        if (comment.PostID == id && comment.IsActive == true)
                        {
                            listaComments.Add(comment);
                        }
                }
            }

            // Chequeo que la lista no esta vacia 
            if (listaComments.Count == 0)
            {
                return NotFound();
            }

            return listaComments;
        }


        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<Comments>> PostComments(Comments comments)
        {
            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));

            // Solo los lectores (ningun usuario logueado) pueden crear comentarios
            if (sessionUser == null)
            {
                // Si la fecha de creacion es null le asigno fecha actual
                if (comments.CreatedDate == null)
                {
                    comments.CreatedDate = DateTime.Now;
                }
                comments.IsActive = true;
                _context.Comments.Add(comments);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetComments", new { id = comments.CommentID }, comments);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comments>> DeleteComments(Guid id)
        {
            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));
            Profiles profile = new Profiles();
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
            }

            // Solo el perfil de editor puede eliminar comentarios
            if (profile != null && profile.Name == "Editor")
            {
                var comments = await _context.Comments.FindAsync(id);
                if (comments == null)
                {
                    return NotFound();
                }

                _context.Comments.Remove(comments);
                await _context.SaveChangesAsync();

                return comments;
            }
            else
            {
                return BadRequest();
            }
        }

        private bool CommentsExists(Guid id)
        {
            return _context.Comments.Any(e => e.CommentID == id);
        }
    }
}
