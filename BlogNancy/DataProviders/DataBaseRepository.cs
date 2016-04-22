using BlogNancy.Interfaces;
using BlogNancy.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace BlogNancy.DataProviders
{

    /// <summary>
    /// Implements the members listed in the IRepository interface using SQLite 
    /// </summary>
    /// <returns> A list of type Post</returns>
    public class DataBaseRepository : IRepository
    {
        private string Connectionstring = "Data Source=" + Startup.dbSource + ";Version=3;New=True;";
        private string TableName = "Posts";

        /// <summary>
        /// Constructor creates a database if it doesn't exist and creates the database's tables
        /// </summary>
        public DataBaseRepository()
        {
            // "using " keywords ensures the object is properly disposed.
            using (var conn = new SQLiteConnection(Connectionstring))
            {
                try
                {
                    if (!File.Exists(Startup.dbSource))
                    {
                        SQLiteConnection.CreateFile(Startup.dbSource);
                    }

                    conn.Open();
                    //Create a Table
                    string query = $"create table IF NOT EXISTS {TableName} (Id INTEGER PRIMARY KEY, Date VARCHAR NOT NULL DEFAULT CURRENT_DATE, Title nvarchar(255) not null, Content nvarchar(1000) Not NULL) ";
                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    command.ExecuteNonQuery();
                }
                //TODO: Handle try catch better cath a more specific error type. 
                catch (SQLiteException ex )
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Gets all the entries from the database
        /// </summary>
        /// <returns> A list of objects of type Post</returns>
        public List<Post> GetAll()
        {
            List<Post> allPosts = new List<Post>();

           
                using (var conn = new SQLiteConnection(Connectionstring))
                {
                try
                {
                    conn.Open();

                    string query = $"SELECT * FROM {TableName} ORDER BY Date DESC";

                    // Read the rows in the table, 
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
                catch (SQLiteException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
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

            using (var conn = new SQLiteConnection(Connectionstring))
            {
                try
                {
                    conn.Open();

                    //string query = $"select {id} from Posts";
                    string query = $"select * from {TableName} where Id = {id}";

                    //Todo There has to be a simpler way. Because I'm just fetching a single row
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                post.ID = Convert.ToInt32(reader["Id"]);
                                post.Date = reader["Date"].ToString();
                                post.Title = reader["Title"].ToString();
                                post.Content = reader["Content"].ToString();
                            }
                        }
                    }
                    conn.Close();

                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return post;
        }

        /// <summary>
        /// Create a new entry in the database.
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
                try
                {
                    string query = $"INSERT INTO {TableName} (Title, Content) VALUES ('{post.Title}', '{post.Content}')";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return post.ID > 0 ? post : null;
        }
        /// <summary>
        /// Edit an existing entry
        /// </summary>
        /// <param name="post"></param>
        /// <returns>a post object</returns>
        public async Task<Post> Update(Post post)
        {
            if (post == null)
            {
                return null;
            }

            bool result = false;

            try
            {

            using (var conn = new SQLiteConnection(Connectionstring))
            {
                conn.Open();
                string query = $"UPDATE {TableName} SET Title = '{post.Title}', Content = '{post.Content}' WHERE Id = {post.ID}";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                    result = true;
                }

                conn.Close();
            }
            }

            //useless catch, I need to handle it properly, either log or display a meaningfull message to the UI
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result ? post : null;
        }
              
        /// <summary>
        /// Remove an entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false</returns>
        /// 
        //TODO: Remember to throw if there's not internet connection, since the db connection is independent from web
        public bool Delete(int id)
        {
            bool result = false;

            if (id > 0)
            {
                try
                {



                    using (var conn = new SQLiteConnection(Connectionstring))
                    {
                        conn.Open();

                        //Todo: If item doesn't exist, return error. Use Post Get(id). 
                        // Won't be a problem in the browser, but as an API, it's better to give too feedback.
                        //CODE EXAMPLE BELOW


                        string query = $"DELETE FROM {TableName} WHERE id = {id}";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        result = true;


                        conn.Close();
                    }
                } catch (SQLiteException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return result;
        }

    }



}