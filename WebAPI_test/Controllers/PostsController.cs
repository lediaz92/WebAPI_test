using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using WebAPI_test.Data;
using WebAPI_test.Models;
using WebAPI_test.Repositories;

namespace WebAPI_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private RepoPosts _repoposts;
        private RepoUsers _repousers;
        private RepoProfiles _repoprofiles;

        private readonly WebAPI_testContext _context;

        public PostsController(WebAPI_testContext context)
        {
            _context = context;
            _repoposts = new RepoPosts(_context);
            _repousers = new RepoUsers(_context);
            _repoprofiles = new RepoProfiles(_context);
        }


        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posts>>> GetPosts()
        {
            List<Posts> listaPosts = await _context.Posts.ToListAsync();
            List<Posts> listaAux = new List<Posts>();

            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));

            // Si no hay usuario logueado
            if (sessionUser == null)
            {
                foreach (Posts p in listaPosts)
                {
                    if (p.Status == 3 && p.IsActive == true)
                    {
                        //Agrego solo los que estan aprobados
                        listaAux.Add(p);
                    }
                }
            }
            else
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                Profiles profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);

                if (profile.Name == "Writer")
                {
                    foreach (Posts p in listaPosts)
                    {
                        if (p.Status == 2 && p.IsActive == true)
                        {
                            //Agrego solo los que estan rechazados
                            listaAux.Add(p);
                        }
                    }
                }
                else if (profile.Name == "Editor")
                {
                    // El editor tiene acceso a la lista completa
                    listaAux = listaPosts;
                }
                else
                {
                    return NotFound();
                }
            }
            return listaAux;
        }


        // GET: api/Posts/5E63656E-18EA-EA11-BBC0-74D435EE802A
        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> GetPosts(Guid id)
        {

            var posts = await _context.Posts.FindAsync(id);
            Posts postAux = null;

            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));

            // Si no hay usuario logueado
            if (sessionUser == null)
            {
                if (posts.Status == 3 && posts.IsActive == true)
                {
                    //Agrego solo el post si esta aprobado
                    postAux = posts;
                }
            }
            else
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                Profiles profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);

                if (profile.Name == "Writer")
                {
                    if (posts.Status == 2 && posts.IsActive == true)
                    {
                    //Agrego solo el post si esta rechazado
                    postAux = posts;
                    }
                }
                else if (profile.Name == "Editor")
                {
                    // El editor tiene acceso a la lista completa
                    postAux = posts;
                }
                else
                {
                    return NotFound();
                }
            }

            if (postAux == null)
            {
                return NotFound();
            }

            return postAux;
        }

        // GET: api/Posts/GetPostsByTitle/Titulo3
        [Route("[action]/{title}")]
        [HttpGet]
        public async Task<ActionResult<Posts>> GetPostsByTitle(string title)
        {

            Posts posts =  _repoposts.GetPostsByTitle(title);
            Posts postAux = null;

            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));

            // Si no hay usuario logueado
            if (sessionUser == null)
            {
                if (posts.Status == 3 && posts.IsActive == true)
                {
                    //Agrego solo el post si esta aprobado
                    postAux = posts;
                }
            }
            else
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                Profiles profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);

                if (profile.Name == "Writer")
                {
                    if (posts.Status == 2 && posts.IsActive == true)
                    {
                        //Agrego solo el post si esta rechazado
                        postAux = posts;
                    }
                }
                else if (profile.Name == "Editor")
                {
                    // El editor tiene acceso a la lista completa
                    postAux = posts;
                }
                else
                {
                    return NotFound();
                }
            }

            if (postAux == null)
            {
                return NotFound();
            }

            return postAux;
        }

        // PUT: api/Posts/DeactivatePosts/5E63656E-18EA-EA11-BBC0-74D435EE802A
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DeactivatePosts(Guid id)
        {
            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                Profiles profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
                if (profile.Name == "Editor")
                {
                    // Obtengo el Post a desactivar
                    Posts posts = _repoposts.GetPostsByID(id);

                    // Chequeo que no este actualmente inactivo
                    if (posts.IsActive == false)
                    {
                        return BadRequest();
                    }

                    // Desactivo y agrego datos de actualizacion
                    posts.IsActive = false;
                    posts.UpdatedByID = sessionUser.UserID;
                    posts.UpdatedDate = DateTime.Now;

                    _context.Entry(posts).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PostsExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return NoContent();
        }

        // PUT: api/Posts/ActivatePosts/5E63656E-18EA-EA11-BBC0-74D435EE802A
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ActivatePosts(Guid id)
        {
            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                Profiles profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
                if (profile.Name == "Editor")
                {
                    // Obtengo el Post a activar
                    Posts posts = _repoposts.GetPostsByID(id);

                    // Chequeo que no este actualmente activo
                    if (posts.IsActive == true)
                    {
                        return BadRequest();
                    }
                    // Activo y agrego datos de actualizacion
                    posts.IsActive = true;
                    posts.UpdatedByID = sessionUser.UserID;
                    posts.UpdatedDate = DateTime.Now;

                    _context.Entry(posts).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PostsExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return NoContent();
        }

        // PUT: api/Posts/ApprovePosts/5E63656E-18EA-EA11-BBC0-74D435EE802A
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ApprovePosts(Guid id)
        {
            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                Profiles profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
                if (profile.Name == "Editor")
                {
                    // Obtengo el Post a aprobar
                    Posts posts = _repoposts.GetPostsByID(id);

                    // Chequeo que no este actualmente aprobado o desactivado
                    if (posts.Status == 3 || posts.IsActive == false)
                    {
                        return BadRequest();
                    }
                    // apruebo, pongo fecha de publicacion (en caso de tener se sobreescribe) y agrego datos de actualizacion
                    posts.Status = 3; //aprobado
                    posts.PublishedDate = DateTime.Now;
                    posts.UpdatedByID = sessionUser.UserID;
                    posts.UpdatedDate = DateTime.Now;

                    _context.Entry(posts).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PostsExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return NoContent();
        }


        // PUT: api/Posts/RejectPosts/5E63656E-18EA-EA11-BBC0-74D435EE802A
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> RejectPosts(Guid id)
        {
            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                Profiles profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
                if (profile.Name == "Editor")
                {
                    // Obtengo el Post a rechazar
                    Posts posts = _repoposts.GetPostsByID(id);

                    // Chequeo que no este actualmente rechazado o desactivado
                    if (posts.Status == 2 || posts.IsActive == false)
                    {
                        return BadRequest();
                    }
                    // rechazo y agrego datos de actualizacion
                    posts.Status = 2; //rechazado
                    posts.UpdatedByID = sessionUser.UserID;
                    posts.UpdatedDate = DateTime.Now;

                    _context.Entry(posts).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PostsExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return NoContent();
        }


        // PUT: api/Posts/EditPosts/5E63656E-18EA-EA11-BBC0-74D435EE802A
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> EditPosts(Guid id, Posts postEdit)
        {
            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                Profiles profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
                if (profile.Name == "Writer")
                {
                    // Obtengo el Post a editar
                    Posts posts = _repoposts.GetPostsByID(id);
                    Boolean changes = false;


                    // Chequeo que este en estado de rechazado y activo
                    if (posts.Status != 2 || posts.IsActive == false)
                    {
                        return BadRequest();
                    }

                    // Me fijo si lo que cabio fue el titulo y que no este null
                    if (postEdit.Title != posts.Title && postEdit.Title != null)
                    {
                        posts.Title = postEdit.Title;
                        changes = true;
                    }

                    //Me fijo si lo que cambio fue el texto y que no este null
                    if (postEdit.Text != posts.Text && postEdit.Text != null)
                    {
                        posts.Text = postEdit.Text;
                        changes = true;
                    }

                    // Si no cambio ni el Titulo ni Texto no avanzo
                    if (changes == false)
                    {
                        return BadRequest();
                    }

                    //Vuelvo al estado de pendiente aprobacion agrego datos de actualizacion
                    posts.Status = 1;
                    posts.UpdatedByID = sessionUser.UserID;
                    posts.UpdatedDate = DateTime.Now;

                    _context.Entry(posts).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PostsExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return NoContent();
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<ActionResult<Posts>> PostPosts(Posts posts)
        {
            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                Profiles profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
                if (profile.Name == "Writer")
                {
                    // Si la fecha de creacion es null le asigno fecha actual
                    if (posts.CreatedDate == null)
                    {
                        posts.CreatedDate = DateTime.Now;
                    }
                    // Siempre la fecha de actualizacion es la actual
                    if (posts.UpdatedDate == null)
                    {
                        posts.UpdatedDate = DateTime.Now;
                    }
                    // Le asigno el ID del usuario logueado como el que creo el Post
                    if (posts.CreatedByID == default)
                    {
                        posts.CreatedByID = sessionUser.UserID;
                    }
                    // Le asigno el ID del usuario logueado como el ultimo que actualizo el Post
                    if (posts.UpdatedByID == default)
                    {
                        posts.UpdatedByID = sessionUser.UserID;
                    }
                    posts.IsActive = true;
                    posts.Status = 1;
                    _context.Posts.Add(posts);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetPosts", new { id = posts.PostID }, posts);
                }
            }
            return BadRequest();
        }

        // DELETE: api/Posts/5E63656E-18EA-EA11-BBC0-74D435EE802A
        [HttpDelete("{id}")]
        public async Task<ActionResult<Posts>> DeletePosts(Guid id)
        {
            // obtengo (si hay) usuario logueado
            Users sessionUser = _repousers.GetUserByLogin(HttpContext.Session.GetString("SessionUser"), HttpContext.Session.GetString("SessionPass"));
            if (sessionUser != null)
            {
                // Si hay usuario logueado me fijo el perfil que tiene
                Profiles profile = _repoprofiles.GetProfileByID(sessionUser.ProfileID);
                if(profile.Name == "Editor")
                {
                    var posts = await _context.Posts.FindAsync(id);
                    if (posts == null)
                    {
                        return NotFound();
                    }

                    _context.Posts.Remove(posts);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }
            return BadRequest();
        }

        private bool PostsExists(Guid id)
        {
            return _context.Posts.Any(e => e.PostID == id);
        }
    }
}
