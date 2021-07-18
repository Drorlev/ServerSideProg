using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SeriesController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Series> Get()
        {
            Series ser = new Series();
            List<Series> sList = ser.Get();
            return sList;
        }
        public IEnumerable<int> Getrecommended(int show_id,int user_id)
        {
            Series ser = new Series();
            List<int> sList = ser.Getrecommended( show_id,  user_id);
            return sList;
        }
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post([FromBody] Series ser)
        {
            string name = ser.Name;
            ser.Insert();
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