using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models.DAL;

namespace WebApplication2.Models
{
    public class Series
    {
        int id;
        string name;
        string airDate;
        string originCountry;
        string originalLanguage;
        string overview;
        float popularity;
        string posterPath;
        int likes;

        public Series(int id, string name, string airDate, string originCountry, string originalLanguage, string overview, float popularity, string posterPath)
        {
            this.id = id;
            this.name = name;
            this.airDate = airDate;
            this.originCountry = originCountry;
            this.originalLanguage = originalLanguage;
            this.overview = overview;
            this.popularity = popularity;
            this.posterPath = posterPath;
        }

        public Series()
        {

        }

        public void Insert()
        {
            DataServices dbs = new DataServices();
            dbs.Insert(this);
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string AirDate { get => airDate; set => airDate = value; }
        public string OriginCountry { get => originCountry; set => originCountry = value; }
        public string OriginalLanguage { get => originalLanguage; set => originalLanguage = value; }
        public string Overview { get => overview; set => overview = value; }
        public float Popularity { get => popularity; set => popularity = value; }
        public string PosterPath { get => posterPath; set => posterPath = value; }
        public int Likes { get => likes; set => likes = value; }
    }
}