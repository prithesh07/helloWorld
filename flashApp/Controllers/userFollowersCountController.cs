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

    public class userFollowersCountController : ApiController
    {
        private smappEntities db = new smappEntities();

        [ResponseType(typeof(IList<userFollower>))]
        public async Task<IHttpActionResult> GetuserFollower(string id)
        {
            var followers = db.userFollowers.Where(f => f.userName == id).Select(f => f.followerUN);
            return Ok(followers);
        }
    }
}