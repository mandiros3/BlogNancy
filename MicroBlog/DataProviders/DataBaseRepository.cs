using System;
using MicroBlog.Interfaces;
using MicroBlog.Models;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;

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

                                post.ID = reader["Id"].ToString();
                                post.Date = reader["Date"].ToString();
                                post.Title = reader["Title"].ToString();
                                post.Content = reader["Id"].ToString();

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
            List<Post> _post = new List<Post>();
            try
            {

            
            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();

                    string query = $"select{id} from Posts";


                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Post post = new Post();

                                post.ID = reader["Id"].ToString();
                                post.Date = reader["Date"].ToString();
                                post.Title = reader["Title"].ToString();
                                post.Content = reader["Id"].ToString();

                               _post.Add(post);

                            }
                        }
                    }

                    conn.Close();

              
            }
            }
            catch (SQLiteException e)
            {

            }
            return _post;
        }

        public Post Create(Post post)
        {
            if (post == null)
            {
                return null;
            }

            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();

                var postid = conn.Insert(post);
                post.Id = (int)postid;

                conn.Close();
            }

            return post.Id > 0 ? post : null;
        }

        public async Task<Post> Update(Post post)
        {
            if (post == null)
            {
                return null;
            }

            bool result;
            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();

                result = await conn.UpdateAsync(post);

                conn.Close();
            }

            return result ? post : null;
        }

        public bool Delete(int id)
        {
            bool result = false;

            if (id > 0)
            {

                using (var conn = new SQLiteConnection(Connectionstring))
                {
                    conn.Open();

                    var item = conn.Get<Post>(id);

                    if (item != null)
                    {
                        result = conn.Delete(item);
                    }

                    conn.Close();
                }
            }

            return result;
        }
    }



}