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
    public class userFollowersController : ApiController
    {
        private smappEntities db = new smappEntities();

        

        // PUT: api/userFollowers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutuserFollower(string id, userFollower userFollower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userFollower.userName)
            {
                return BadRequest();
            }

            db.Entry(userFollower).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userFollowerExists(id))
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

        // POST: api/userFollowers
        [ResponseType(typeof(userFollower))]
        public async Task<IHttpActionResult> PostuserFollower(userFollower userFollower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.userFollowers.Add(userFollower);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (userFollowerExists(userFollower.userName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userFollower.userName }, userFollower);
        }

        // DELETE: api/userFollowers/5
        [ResponseType(typeof(userFollower))]
        public async Task<IHttpActionResult> DeleteuserFollower(string id)
        {
            String[] arr = id.Split(';');
            string id1 = arr[0];
            string id2 = arr[1];
            userFollower userFollower = db.userFollowers.Where(f => f.userName == id1 & f.followerUN == id2).FirstOrDefault();           
           
            if (userFollower == null)
            {
                return NotFound();
            }
            
            db.userFollowers.Remove(userFollower);
            await db.SaveChangesAsync();

            return Ok(userFollower);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userFollowerExists(string id)
        {
            return db.userFollowers.Count(e => e.userName == id) > 0;
        }
    }
}