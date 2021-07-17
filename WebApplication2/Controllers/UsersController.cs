using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<User> Get()
        {
            User usr = new User();
            List<User> uList = usr.Get();
            return uList;
        }


        public HttpResponseMessage Get(string email, string password)
        {
            User usr = new User();
            User user = usr.Get(email, password);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Email address or Password is incorrect");

        }


        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post([FromBody] User usr)
        {
            //string name = usr.Name;
            usr.Insert();
            return 1;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}