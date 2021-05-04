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
    public class postResponsesController : ApiController
    {
        private smappEntities db = new smappEntities();

        // GET: api/postResponses
        public IQueryable<postResponse> GetpostResponses()
        {
            return db.postResponses;
        }

        // GET: api/postResponses/5
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

        // PUT: api/postResponses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutpostResponse(long id)
        {
            postResponse postres = (db.postResponses.Where(f => f.postId == id).Select(f => f)).FirstOrDefault();
            long a = long.Parse(postres.comment);
            a += 1;
            postres.comment = "" + a;           
           

            db.Entry(postres).State = EntityState.Modified;

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

        // POST: api/postResponses
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

        // DELETE: api/postResponses/5
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