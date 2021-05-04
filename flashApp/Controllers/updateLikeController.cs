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
    public class updateLikeController : ApiController
    {
        private smappEntities db = new smappEntities();

        // GET: api/updateLike
        public IQueryable<postResponse> GetpostResponses()
        {
            return db.postResponses;
        }

        // GET: api/updateLike/5
        [ResponseType(typeof(postResponse))]
        public async Task<IHttpActionResult> GetpostResponse(long id)
        {
            postResponse postResponse = await db.postResponses.FindAsync(id);
            if (postResponse == null)
            {
                return NotFound();
            }

            return Ok(postResponse);
        }

        // PUT: api/updateLike/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutpostResponse(long id, postResponse postResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postResponse.postId)
            {
                return BadRequest();
            }

            db.Entry(postResponse).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!postResponseExists(id))
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

        // POST: api/updateLike
        [ResponseType(typeof(postResponse))]
        public async Task<IHttpActionResult> PostpostResponse(postResponse postResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.postResponses.Add(postResponse);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (postResponseExists(postResponse.postId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = postResponse.postId }, postResponse);
        }

        // DELETE: api/updateLike/5
        [ResponseType(typeof(postResponse))]
        public async Task<IHttpActionResult> DeletepostResponse(long id)
        {
            postResponse postResponse = await db.postResponses.FindAsync(id);
            if (postResponse == null)
            {
                return NotFound();
            }

            db.postResponses.Remove(postResponse);
            await db.SaveChangesAsync();

            return Ok(postResponse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool postResponseExists(long id)
        {
            return db.postResponses.Count(e => e.postId == id) > 0;
        }
    }
}