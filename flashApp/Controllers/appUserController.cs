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

    public class appUserController : ApiController
    {
        private smappEntities db = new smappEntities();

        // GET: api/appUser
        public IQueryable<appUser> GetappUsers()
        {
            return db.appUsers;
        }

        // GET: api/appUser/5
        [ResponseType(typeof(appUser))]
        public async Task<IHttpActionResult> GetappUser(string id)
        {
            appUser appUser = await db.appUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            return Ok(appUser);
        }

        // PUT: api/appUser/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutappUser(string id, appUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appUser.userName)
            {
                return BadRequest();
            }

            db.Entry(appUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!appUserExists(id))
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

        // POST: api/appUser
        [ResponseType(typeof(appUser))]
        public async Task<IHttpActionResult> PostappUser(appUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.appUsers.Add(appUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (appUserExists(appUser.userName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = appUser.userName }, appUser);
        }

        // DELETE: api/appUser/5
        [ResponseType(typeof(appUser))]
        public async Task<IHttpActionResult> DeleteappUser(string id)
        {
            appUser appUser = await db.appUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            db.appUsers.Remove(appUser);
            await db.SaveChangesAsync();

            return Ok(appUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool appUserExists(string id)
        {
            return db.appUsers.Count(e => e.userName == id) > 0;
        }
    }
}