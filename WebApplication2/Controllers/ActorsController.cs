using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ActorsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Actor> Get(int id)
        {
            Actor actr = new Actor();
            List<Actor> aList = actr.Get(id);
            return aList;
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public int Post([FromBody] Actor actr)
        {
            string name = actr.Name;
            actr.Insert();
            return 1;
        }
        public int Post([FromBody] Actor actr, int id)
        {
            string name = actr.Name;
            actr.Insert(id);
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