using System;
using MicroBlog.Interfaces;
using MicroBlog.Models;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroBlog.DataProviders
{
    // this class implements the members listed in the interface
    public class DataBaseRepository : IRepository
    {

        private const string DbSource = "microblog.sqlite";
        private const string Connectionstring = "Data Source=" + DbSource + ";Version=3;New=True;";


        // Todo use SQL lite for now
        public DataBaseRepository() {
            using (var conn = new SQLiteConnection(Connectionstring))
            {
               
                if (!File.Exists("microblog.sqlite"))
                {
                    SQLiteConnection.CreateFile("microblog.sqlite");

                } 
                
                //Creating a Table
                string query = "create table IF NOT EXISTS Posts (Id INTEGER PRIMARY KEY, Date VARCHAR NOT NULL DEFAULT CURRENT_DATE, Title nvarchar(255), Content nvarchar(1000) not null) ";
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
            }
        }


        // Creates a connection to database file
        public static void ConnectDatabase()
        {
            return;
           
        }

        /// <summary>
        /// Gets all the posts from the database
        /// </summary>
        /// <returns> A list of type Post</returns>
        public List<Post> GetAll()
        {
            List<Post> allPosts = new List<Post>();


            try {
                using (var conn = new SQLiteConnection(Connectionstring))
                {
                    conn.Open();

                    string query = "select * from Posts order by date desc";


                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Post post = new Post();

                                post.ID = (int) reader["ID"];
                                post.Date = reader["Date"].ToString();
                                post.Title = reader["Title"].ToString();
                                post.Content = reader["Content"].ToString();

                                allPosts.Add(post);

                            }
                        }
                    }

                    
                    conn.Close();


                }
            }
            catch (SQLiteException e)
            {
              
            }
            return allPosts;
        }

        /// <summary>
        /// Get a post from an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A post of id n </returns>
        public Post Get(int id)
        {
            Post post = new Post();
            try
            {

            
            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();

                    //string query = $"select {id} from Posts";
                    string query = $"select * from Posts where Id = {id}";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                post.ID = (int) reader["Id"];
                                post.Date = reader["Date"].ToString();
                                post.Title = reader["Title"].ToString();
                                post.Content = reader["Id"].ToString();

                            }
                        }
                    }

                    conn.Close();

              
            }
            }
            catch (SQLiteException e)
            {

            }
            return post;
        }

        /// <summary>
        /// Create a new blog post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public Post Create(Post post)
        {
            

            if (post == null)
            {
                return null;
            }

            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();
                string query = ($"insert into Posts (Title, Content) values ({post.Title}, {post.Content})");
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }



                 
            
                conn.Close();
            }

            return post.ID > 0 ? post : null;
        }

        //    public async Task<Post> Update(Post post)
        //    {
        //        if (post == null)
        //        {
        //            return null;
        //        }

        //        bool result;
        //        using (var conn = new SQLiteConnection(Connectionstring))
        //        {
        //            conn.Open();

        //            result = await conn.UpdateAsync(post);

        //            conn.Close();
        //        }

        //        return result ? post : null;
        //    }

        //    public bool Delete(int id)
        //    {
        //        bool result = false;

        //        if (id > 0)
        //        {

        //            using (var conn = new SQLiteConnection(Connectionstring))
        //            {
        //                conn.Open();

        //                var item = conn.Get<Post>(id);

        //                if (item != null)
        //                {
        //                    result = conn.Delete(item);
        //                }

        //                conn.Close();
        //            }
        //        }

        //        return result;
        //    }

    }



}