using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using WebApplication2.Models;

namespace Tar1.Models.DAL
{
    public class DataServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DataServices()
        {
            
        }

        
        // This method creates a connection to the database according to the connectionString name in the web.config 
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

       
        // This method inserts a Series to the Series table 
        public int Insert(Object obj)
        {

            SqlConnection con;
            SqlCommand cmd = new SqlCommand();

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            BuildInsertCommand(con, cmd, obj);      

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (SqlException Ex)
             {
              if (Ex.Number == 2627)
                {
                       //Your Message
                       return 0;
                }
                   return  0;
               }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                   
                    con.Close();
                }
            }

        }

        //insert the episode id and the user id to the favorite table
        public int Insert(Episode episode,int id)
        {
            System.Diagnostics.Debug.WriteLine("here");
            SqlConnection con;
            SqlCommand cmd;
            
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            // helper method to build the insert string
            String cStr = BuildInsertCommand(episode, id);
            cmd = CreateCommand(cStr, con);
            

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (SqlException Ex)
            {
                if (Ex.Number == 2627)
                {
                    //Your Message
                    return 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //insert the actor id and the user id to the favoritve actors table
        public int Insert(Actor actor, int id)
        {
            System.Diagnostics.Debug.WriteLine("here");
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            // helper method to build the insert string
            String cStr = BuildInsertCommand(actor, id);
            cmd = CreateCommand(cStr, con);


            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (SqlException Ex)
            {
                if (Ex.Number == 2627)
                {
                    //Your Message
                    return 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        // Build the Insert command String Favorites
        private String BuildInsertCommand(Episode ep,int id)
        {
            String command="";
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}')", ep.EpisodeId, id);
            String prefix = "INSERT INTO Favorites_2021 " + "([episode_id], [user_id])";
            command = prefix + sb.ToString();

            return command;
        }
        // Build the Insert command String Favorites
        private String BuildInsertCommand(Actor actr, int id)
        {
            String command = "";
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}')", actr.Id,id);
            String prefix = "INSERT INTO FavoritesActors_2021 " + "([actor_id], [user_id])";
            command = prefix + sb.ToString();

            return command;
        }

        // Build the Insert command String

        private void BuildInsertCommand(SqlConnection con, SqlCommand cmd, Object obj)
        {

            StringBuilder sb = new StringBuilder();
            string prefix = null;

            cmd.Connection = con;
            cmd.CommandTimeout = 10;   // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.Text;

            string cmdText = "";
            // use a string builder to create the dynamic string
            if (obj is Episode)
            {
                Episode ep = (Episode)obj;
                
                cmdText += "INSERT INTO Episodes_2021 (episode_id,  name, season, show_id, air_date, overview, poster_path,tvShowname ) " +
                "Values (@episode_id,  @name,@season, @show_id, @air_date, @overview, @poster_path,@tvShowname)";

                cmd.CommandText = cmdText;
                cmd.Parameters.AddWithValue("@name", ep.EpisodeName);
                cmd.Parameters.AddWithValue("@air_date", ep.AirDate);
                cmd.Parameters.AddWithValue("@overview", ep.Overview);
                cmd.Parameters.AddWithValue("@poster_path", ep.Poster);
                cmd.Parameters.AddWithValue("@tvShowname", ep.TvShowname);
                


                cmd.Parameters.Add("@episode_id", SqlDbType.Int);
                cmd.Parameters["@episode_id"].Value = ep.EpisodeId;
                cmd.Parameters.Add("@season", SqlDbType.Int);
                cmd.Parameters["@season"].Value = ep.SeasonNum;
                cmd.Parameters.Add("@show_id", SqlDbType.Int);
                cmd.Parameters["@show_id"].Value = ep.TvShowId;
                

               


            }
            else if (obj is Series)
            {
                Series ser = (Series)obj;
                cmdText += "INSERT INTO Series_2021 (id,  name, air_date, origin_country, original_language, overview, popularity, poster_path ) " +
                "Values (@id,  @name,@air_date, @origin_country, @original_language, @overview, @popularity, @poster_path )";

                cmd.CommandText = cmdText;
                cmd.Parameters.AddWithValue("@overview", ser.Overview);
                cmd.Parameters.AddWithValue("@name", ser.Name);
                cmd.Parameters.AddWithValue("@origin_country", ser.OriginCountry);
                cmd.Parameters.AddWithValue("@original_language", ser.OriginalLanguage);
                cmd.Parameters.AddWithValue("@poster_path", ser.PosterPath);
                cmd.Parameters.AddWithValue("@air_date", ser.AirDate);


                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = ser.Id;

                cmd.Parameters.Add("@popularity", SqlDbType.Float);
                cmd.Parameters["@popularity"].Value = ser.Popularity;

               
            }
            else if (obj is User)
            {

                User usr = (User)obj;
                cmdText += "INSERT INTO Users_2021 (name, last_name, mail, password, phoneNum, gender,  birth_Day, genre, address) " +
                "Values (@name, @last_name, @mail, @password, @phoneNum, @gender, @birth_Day, @genre, @address )";

                cmd.CommandText = cmdText;
                cmd.Parameters.AddWithValue("@name", usr.Name);
                cmd.Parameters.AddWithValue("@last_name", usr.LastName);
                cmd.Parameters.AddWithValue("@mail", usr.Mail);
                cmd.Parameters.AddWithValue("@password", usr.Password);
                cmd.Parameters.AddWithValue("@phoneNum", usr.PhoneNum);
                cmd.Parameters.AddWithValue("@gender", usr.Gender);
                cmd.Parameters.AddWithValue("@genre", usr.Genre);
                cmd.Parameters.AddWithValue("@address", usr.Address);
                cmd.Parameters.Add("@birth_Day", SqlDbType.Int);
                cmd.Parameters["@birth_Day"].Value = usr.BirthY;
            }
            else if (obj is Actor)
            {

                Actor actr = (Actor)obj;
                cmdText += "INSERT INTO Actors_2021 (name, known_for_department, birthday, gender, birth_place,poster_path, id) " +
                "Values (@name, @known_for_department, @birthday, @gender, @birth_place,@poster_path, @id)";

                cmd.CommandText = cmdText;
                cmd.Parameters.AddWithValue("@name", actr.Name);
                cmd.Parameters.AddWithValue("@known_for_department", actr.Known_for_department);
                cmd.Parameters.AddWithValue("@birthday", actr.Birthday);
                cmd.Parameters.AddWithValue("@gender", actr.Gender);
                cmd.Parameters.AddWithValue("@birth_place", actr.BirthPlace);
                cmd.Parameters.AddWithValue("@poster_path", actr.Poster_path);
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = actr.Id;
            }
        }
        
        // Create the SqlCommand
       
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        //get(string email,string pass)
        public User Get(string email,string pass)
        {

            SqlConnection con = null;
            User usr = new User();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Users_2021 WHERE Users_2021.mail='" + email+ "' AND Users_2021.password='" + pass+"'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    usr.Name = (string)dr["name"];
                    usr.LastName = (string)dr["last_name"];
                    usr.Mail =(string)dr["mail"];
                    usr.Password= (string)dr["password"];
                    usr.PhoneNum= (string)dr["phoneNum"];
                    usr.Gender= (string)dr["gender"];
                    usr.BirthY =Convert.ToInt32(dr["birth_Day"]);
                    usr.Address= (string)dr["address"];
                    usr.Genre= (string)dr["genre"];
                    usr.Id = Convert.ToInt32(dr["id"]);
                    if (usr.Mail == "Admin@Admin.net")
                    {
                        usr.IsAdmin = true;
                    }
                }

                return usr;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //GetUsers()
        public List<User> GetUsers()
        {

            SqlConnection con = null;
            List<User> userL = new List<User>();
           

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Users_2021";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    User usr = new User();
                    usr.Name = (string)dr["name"];
                    usr.LastName = (string)dr["last_name"];
                    usr.Mail = (string)dr["mail"];
                    usr.Password = (string)dr["password"];
                    usr.PhoneNum = (string)dr["phoneNum"];
                    usr.Gender = (string)dr["gender"];
                    usr.BirthY = Convert.ToInt32(dr["birth_Day"]);
                    usr.Address = (string)dr["address"];
                    usr.Genre = (string)dr["genre"];
                    usr.Id = Convert.ToInt32(dr["id"]);
                    userL.Add(usr);
                }

                return userL;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
         //episdoes gets
        public List<Episode> Get(int id)
        {
            SqlConnection con = null;
            List<Episode> episodesList = new List<Episode>();
            List<Episode> eListNoDuplicates = new List<Episode>();
            Dictionary<string, string> eps =  new Dictionary<string, string>();
            //start
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Favorites_2021 as fav inner join Episodes_2021 as epi on fav.episode_id = epi.episode_id Where fav.user_id ="+id;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Episode ep = new Episode();

                    ep.EpisodeName = (string)dr["name"];
                    ep.EpisodeId =(int)dr["episode_id"];
                    ep.Overview= (string)dr["overview"]; 
                    ep.Poster= (string)dr["poster_path"]; 
                    ep.SeasonNum = (int)dr["season"]; 
                    ep.TvShowId = (int)dr["show_id"]; 
                    ep.AirDate = (string)dr["air_date"]; 
                    ep.TvShowname= (string)dr["tvShowname"];
                    episodesList.Add(ep);
                }
                
                foreach (var ep in episodesList)
                {
                    if (!eps.ContainsKey(ep.TvShowname))
                    {
                        eps.Add(ep.TvShowname, ep.TvShowname);
                        eListNoDuplicates.Add(ep);
                    }
                }
               return eListNoDuplicates; 
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public List<Episode> Get(string tvShowName, int id)
        {
            SqlConnection con = null;
            List<Episode> episodesList = new List<Episode>();
            //start
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Favorites_2021 as fav inner join Episodes_2021 as epi on fav.episode_id = epi.episode_id Where fav.user_id =" + id+ " AND epi.tvShowname='"+ tvShowName+"'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {  
                    Episode ep = new Episode();

                    ep.EpisodeName = (string)dr["name"];
                    ep.EpisodeId = (int)dr["episode_id"];
                    ep.Overview = (string)dr["overview"];
                    ep.Poster = (string)dr["poster_path"];
                    ep.SeasonNum = (int)dr["season"]; 
                    ep.TvShowId = (int)dr["show_id"]; 
                    ep.AirDate = (string)dr["air_date"];
                    ep.TvShowname = (string)dr["tvShowname"];
                    episodesList.Add(ep);
                }
                return episodesList;
            }
            catch (Exception ex)
            {
                
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<Episode> GetEpisodesList()
        {
            SqlConnection con = null;
            List<Episode> episodesList = new List<Episode>();
            IDictionary<int, int> likesDict = countLikes();
            
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Episodes_2021";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Episode ep = new Episode();

                    ep.EpisodeName = (string)dr["name"];
                    ep.EpisodeId = (int)dr["episode_id"];
                    ep.Overview = (string)dr["overview"];
                    ep.Poster = (string)dr["poster_path"];
                    ep.SeasonNum = (int)dr["season"]; 
                    ep.TvShowId = (int)dr["show_id"]; 
                    ep.AirDate = (string)dr["air_date"];
                    ep.TvShowname = (string)dr["tvShowname"];
                    ep.Likes = likesDict[ep.EpisodeId];
                    episodesList.Add(ep);
                }
                return episodesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //end episode gets

        //count the likes per episode
        public IDictionary<int, int> countLikes()
        {
            SqlConnection con = null;
            IDictionary<int, int> likesDict = new Dictionary<int, int>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT  episode_id,COUNT(*) as 'sum' FROM  Favorites_2021 GROUP BY  episode_id";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                while (dr.Read())
                {   // Read till the end of the data into a row
                    likesDict.Add((int)dr["episode_id"], (int)dr["sum"]);

                }
                return likesDict;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //

        //count series likes
        public IDictionary<int, int> countSeriesLikes()
        {
            SqlConnection con = null;
            IDictionary<int, int> likesDict = new Dictionary<int, int>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT  show_id,COUNT(DISTINCT user_id) as 'sum' FROM Favorites_2021 as fav inner join Episodes_2021 as epi on fav.episode_id=epi.episode_id  GROUP BY  show_id";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                while (dr.Read())
                {   // Read till the end of the data into a row
                    likesDict.Add((int)dr["show_id"], (int)dr["sum"]);

                }
                return likesDict;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //

        //GetSeriesList
        public List<Series> GetSeriesList()
        {
            SqlConnection con = null;
            List<Series> seriesList = new List<Series>();
            IDictionary<int, int> likesDict = countSeriesLikes();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Series_2021";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                while (dr.Read())
                {   // Read till the end of the data into a row
                    Series ser = new Series();

                    ser.Name = (string)dr["name"];
                    ser.Id = (int)dr["id"];
                    ser.AirDate = (string)dr["air_date"];
                    ser.OriginCountry = (string)dr["origin_country"];
                    ser.OriginalLanguage = (string)dr["original_language"];
                    ser.Overview = (string)dr["overview"];
                    ser.Popularity = (float)Convert.ToDouble(dr["popularity"]);
                    ser.PosterPath = (string)dr["poster_path"];
                    ser.Likes = likesDict[ser.Id];
                    seriesList.Add(ser);
                }
                return seriesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //end Series get


        //get Recommended Series by choosing series and compare
        //the series to other user that like it 
        //and then choose the top favorite series between the series adn return it
        public List<string> GetrecommendedSeries(int show_id, int user_id)
        {
            SqlConnection con = null;
            List<int> idList = similarUsers(show_id, user_id);
            List<string> seriesIdList = new List<string>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                
                String selectSTR = "SELECT   tvShowname, COUNT(*) as 'num' FROM Favorites_2021 as fav inner join Episodes_2021 as epi on fav.episode_id = epi.episode_id  and(";
                if (idList.Count == 0)
                {
                    seriesIdList.Add("There are no recommended Tv show by this pick");
                    return seriesIdList;
                }
                foreach (var id in idList)
                {
                    if (idList.Count - 1 == idList.IndexOf(id))
                    {
                        selectSTR += " user_id=" + id;
                    }
                    else
                    {
                        selectSTR += " user_id=" + id+" or ";
                    }
                }
                selectSTR += ") and show_id !="+ show_id + " GROUP BY  tvShowname ORDER BY COUNT(*) Desc; ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                while (dr.Read())
                {   // Read till the end of the data into a row
                    //int serID= (int)dr["show_id"];
                    string serName = (string)dr["tvShowname"];
                    seriesIdList.Add(serName);


                    
                }
                return seriesIdList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

          
        }//end recommand series

        //find similar chosen series
        public List<int> similarUsers(int show_id, int user_id)
        {
            SqlConnection con = null;
            List<int> userIdList= new List<int>();
           
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT DISTINCT  user_id FROM Favorites_2021 as fav inner join Episodes_2021 as epi on fav.episode_id=epi.episode_id and show_id="+ show_id + " and user_id!="+ user_id;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                while (dr.Read())
                {   // Read till the end of the data into a row

                    int id = (int)dr["user_id"];
                    userIdList.Add(id);
                }
                return userIdList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //
        public List<Actor> GetActors(int id)
        {
            SqlConnection con = null;
            List<Actor> actorsList = new List<Actor>();
            //List<Episode> eListNoDuplicates = new List<Episode>();
            //Dictionary<string, string> eps = new Dictionary<string, string>();
            //start
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM FavoritesActors_2021 as fav inner join Actors_2021 as actr on fav.actor_id=actr.id Where fav.user_id =" + id;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Actor actr = new Actor();

                    
                    
                    actr.Name = (string)dr["name"];
                    actr.Known_for_department = (string)dr["known_for_department"];
                    actr.Birthday = (string)dr["birthday"];
                    actr.Gender = (string)dr["gender"];
                    actr.BirthPlace = (string)dr["birth_place"];
                    actr.Poster_path = (string)dr["poster_path"];
                    actr.Id = (int)dr["actor_id"];

                    actorsList.Add(actr);
                }

              
                return actorsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        
    }
}