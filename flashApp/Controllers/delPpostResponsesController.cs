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
    public class delPpostResponsesController : ApiController
    {
        private smappEntities db = new smappEntities();

       
        // DELETE: api/delPpostResponses/5
        [ResponseType(typeof(postResponse))]
        public async Task<IHttpActionResult> DeletepostResponse(long id)
        {
            postResponse postResponses = (db.postResponses.Where(f => f.postId == id).Select(f=>f)).First();
  
            if (postResponses == null)
            {
                return NotFound();
            }

            db.postResponses.Remove(postResponses);
            await db.SaveChangesAsync();

            return Ok(postResponses);
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