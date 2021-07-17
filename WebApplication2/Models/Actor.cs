using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models.DAL;

namespace WebApplication2.Models
{
    public class Actor
    {
        string name;
        string known_for_department;
        string birthday;
        string gender;
        string birthPlace;
        string poster_path;
        int id;

        
        public Actor()
        {

        }

        public Actor(string name, string known_for_department, string birthday, string gender, string birthPlace, string poster_path, int id)
        {
            this.name = name;
            this.known_for_department = known_for_department;
            this.birthday = birthday;
            this.gender = gender;
            this.birthPlace = birthPlace;
            this.poster_path = poster_path;
            this.id = id;
        }

        public string Name { get => name; set => name = value; }
        public string Known_for_department { get => known_for_department; set => known_for_department = value; }
        public string Birthday { get => birthday; set => birthday = value; }
        public string Gender { get => gender; set => gender = value; }
        public string BirthPlace { get => birthPlace; set => birthPlace = value; }
        public int Id { get => id; set => id = value; }
        public string Poster_path { get => poster_path; set => poster_path = value; }

        public void Insert()
        {
            DataServices dbs = new DataServices();
            dbs.Insert(this);
        }
        public void Insert(int id)
        {
            DataServices dbs = new DataServices();
            dbs.Insert(this, id);
        }
       

        public List<Actor> Get(int id)
        {
            DataServices ds = new DataServices();
            List<Actor> aList = ds.GetActors(id);
            return aList;
        }
    }
    
}