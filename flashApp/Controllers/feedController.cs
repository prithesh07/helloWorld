using flashApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace flashApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class feedController : ApiController
    {
        private smappEntities db = new smappEntities();

        // GET: api/appUser/5
        [ResponseType(typeof(appUser))]
        public async Task<IHttpActionResult> GetappUser(string id)
        {
            var followers =  db.userFollowers.Where(f => f.followerUN == id).Select(f => f.userName).ToList();
            List<custom> postIdList = new List<custom>();
            IList mixedList = new ArrayList();
            foreach (var element in followers)
            {
                var posts = from up in db.userPosts
                            join ur in db.postResponses
                            on up.postId equals ur.postId
                            orderby up.postTime descending
                            where up.userName == element
                            select new
                            {
                                postId = up.postId,
                                userName = up.userName,
                                caption = up.caption,
                                contentIMG = up.contentIMG,
                                postTime = up.postTime,
                                comment = ur.comment
                            };

                if (posts.Count() > 0)
                {
                    mixedList.Add(posts);
                }

            }
            
            return Ok(mixedList);


        }
        public class custom
        {
            long a;
            string b;
            string c;
            string d;
            DateTime? e;
            custom(long f,string g,string h,string i,DateTime? j)
            {
                a = f;
                b = g;
                c = h;
                d = i;
                j = e;
            }
        }

    }
}
