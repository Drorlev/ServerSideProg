using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tar1.Models;

namespace Tar1.Controllers
{
    public class EpisodesController : ApiController
    {
        public IEnumerable<Episode> Get()
        {
            Episode ep = new Episode();
            List<Episode> eList = ep.Get();
            return eList;
        }
        public IEnumerable<Episode> Get(int id)
        {
            Episode ep = new Episode();
            List<Episode> eList = ep.Get(id);
            return eList;
        }

        // GET api/<controller>/tvShowName
        public IEnumerable<Episode> Get(string tvShowName,int id)
        {
            Episode ep = new Episode();
            List<Episode> eList = ep.Get(tvShowName,id);
            return eList;
        }

        // POST api/<controller>
        public int Post([FromBody] Episode ep)
        {
            //string name = ep.EpisodeName;
            ep.Insert();
            return 1;
        }

        public int Post([FromBody] Episode ep, int id)
        {
            //string name = ep.EpisodeName;
            ep.Insert(id);
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