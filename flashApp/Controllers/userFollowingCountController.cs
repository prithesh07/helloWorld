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
    public class userFollowingCountController : ApiController
    {
        private smappEntities db = new smappEntities();


        public async Task<IHttpActionResult> GetuserFollowing(string id)
        {
            var followers = db.userFollowers.Where(f => f.followerUN == id).Select(f => f.userName);
            return Ok(followers);
        }


    }
}