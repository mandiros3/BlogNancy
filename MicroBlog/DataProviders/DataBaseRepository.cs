using System;
using MicroBlog.Interfaces;
using MicroBlog.Models;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroBlog;

namespace MicroBlog.DataProviders
{
    // this class implements the members listed in the interface
    public class DataBaseRepository : IRepository
    {
       
       
        private  string Connectionstring = "Data Source=" + Startup.dbSource + ";Version=3;New=True;";


        // Todo use SQL lite for now
        public DataBaseRepository() {

            using (var conn = new SQLiteConnection(Connectionstring))
            {
                try
                {
                    if (!File.Exists(Startup.dbSource))
                    {
                        SQLiteConnection.CreateFile(Startup.dbSource);

                    }

                    conn.Open();
                    //Creating a Table
                    string query = "create table IF NOT EXISTS Posts (Id INTEGER PRIMARY KEY, Date VARCHAR NOT NULL DEFAULT CURRENT_DATE, Title nvarchar(255) not null, Content nvarchar(1000) Not NULL) ";
                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
            }
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
                                Post post = new Post
                                {
                                    ID = Convert.ToInt32(reader["Id"]),
                                    Date = reader["Date"].ToString(),
                                    Title = reader["Title"].ToString(),
                                    Content = reader["Content"].ToString()
                                };


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
                                post.ID = Convert.ToInt32(reader["Id"]);
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
                string query = ($"insert into Posts (Title, Content) values ('{post.Title}', '{post.Content}')");
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

          public bool Delete(int id)
          {
              bool result = false;

              if (id > 0)
              {

                  using (var conn = new SQLiteConnection(Connectionstring))
                  {
                      conn.Open();

                    string query = ($"DELETE FROM Posts WHERE id = {id})");
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    

                      conn.Close();
                  }
              }

              return result;
          }

    }



}