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
    public class delPostsController : ApiController
    {
        private smappEntities db = new smappEntities();

       
        // DELETE: api/delPosts/5
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