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
    public class userPostsCountController : ApiController
    {
        private smappEntities db = new smappEntities();        

        [ResponseType(typeof(IList<userFollower>))]
        public async Task<IHttpActionResult> GetUserPost(string id)
        {
            //var posts = db.userPosts.Where(f => f.userName == id).Select(f => new { f.postId, f.contentIMG,f.userName,f.caption ,f.postTime });
            var post = from up in db.userPosts
                       join ur in db.postResponses
                       on up.postId equals ur.postId
                       where up.userName == id
                       orderby up.postTime descending
                       select new
                       {
                           postId = up.postId,
                           userName = up.userName,
                           caption = up.caption,
                           contentIMG = up.contentIMG,
                           postTime = up.postTime,
                           comment = ur.comment
                       }
                       ;

            return Ok(post);
        }
    }
}