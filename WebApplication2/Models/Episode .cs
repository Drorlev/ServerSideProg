using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models.DAL;

namespace Tar1.Models
{
    public class Episode
    {
        int tvShowId;
        int seasonNum;
        int episodeId;
        string episodeName;
        string poster;
        string overview;
        string airDate;
        string tvShowname;
        int likes;

        public Episode(int tvShowId, int seasonNum, int episodeId, string episodeName, string poster, string overview, string airDate, string tvShowname)
        {
            this.tvShowId = tvShowId;
            this.seasonNum = seasonNum;
            this.episodeId = episodeId;
            this.episodeName = episodeName;
            this.poster = poster;
            this.overview = overview;
            this.airDate = airDate;
            this.TvShowname = tvShowname;
        }
        public Episode()
        {

        }

        public void Insert()
        {
            DataServices dbs = new DataServices();
            dbs.Insert(this);
        }
        public void Insert( int id)
        {
            DataServices dbs = new DataServices();
            dbs.Insert(this, id);
        }
       
        public int TvShowId { get => tvShowId; set => tvShowId = value; }
        public int SeasonNum { get => seasonNum; set => seasonNum = value; }
        public int EpisodeId { get => episodeId; set => episodeId = value; }
        public string EpisodeName { get => episodeName; set => episodeName = value; }
        public string Poster { get => poster; set => poster = value; }
        public string Overview { get => overview; set => overview = value; }
        public string AirDate { get => airDate; set => airDate = value; }
        public string TvShowname { get => tvShowname; set => tvShowname = value; }
        public int Likes { get => likes; set => likes = value; }

        public List<Episode> Get()
        {
            DataServices ds = new DataServices();
            List<Episode> eList = ds.GetEpisodesList();
            return eList;
        }
        public List<Episode> Get(int id)
        {
            DataServices ds = new DataServices();
            List<Episode> eList = ds.Get(id);
            return eList;
        }
        public List<Episode> Get(string tvShowName,int id)
        {
            DataServices ds = new DataServices();
            List<Episode> eList = ds.Get(tvShowName,id);
            return eList;
        }
       
    }
}