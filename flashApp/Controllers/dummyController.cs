using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using flashApp.Models;

namespace flashApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class dummyController : ApiController
    {
        private smappEntities db = new smappEntities();

        // GET: api/dummy
        public IQueryable<userPost> GetuserPosts()
        {
            return db.userPosts;
        }

        // GET: api/dummy/5
        [ResponseType(typeof(userPost))]
        public async Task<IHttpActionResult> GetuserPost(long id)
        {
            userPost userPost = await db.userPosts.FindAsync(id);
            if (userPost == null)
            {
                return NotFound();
            }

            return Ok(userPost);
        }

        // PUT: api/dummy/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutuserPost(long id, userPost userPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userPost.postId)
            {
                return BadRequest();
            }

            db.Entry(userPost).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/dummy
        [ResponseType(typeof(userPost))]
        public async Task<IHttpActionResult> PostuserPost(userPost userPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.userPosts.Add(userPost);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (userPostExists(userPost.postId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userPost.postId }, userPost);
        }

        // DELETE: api/dummy/5
        [ResponseType(typeof(userPost))]
        public async Task<IHttpActionResult> DeleteuserPost(long id)
        {
            userPost userPost = await db.userPosts.FindAsync(id);
            if (userPost == null)
            {
                return NotFound();
            }

            db.userPosts.Remove(userPost);
            await db.SaveChangesAsync();

            return Ok(userPost);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userPostExists(long id)
        {
            return db.userPosts.Count(e => e.postId == id) > 0;
        }
    }
}