using MicroBlog.Interfaces;
using MicroBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace MicroBlog.DataProviders
{
    // this class implements the members listed in the interface
    public class DataBaseRepository : IRepository
    {


        private string Connectionstring = "Data Source=" + Startup.dbSource + ";Version=3;New=True;";
        private string TableName = "Posts";

        // Todo use SQL lite for now

        public DataBaseRepository()
        {

            //Create a database if it doesn't exist, create a table with these columns
            //==== So that it automatically happens whenever this class is instantiated.
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
                    string query = $"create table IF NOT EXISTS {TableName} (Id INTEGER PRIMARY KEY, Date VARCHAR NOT NULL DEFAULT CURRENT_DATE, Title nvarchar(255) not null, Content nvarchar(1000) Not NULL) ";
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

            try
            {
                using (var conn = new SQLiteConnection(Connectionstring))
                {
                    conn.Open();

                    string query = $"select * from {TableName} order by date desc";

                    // Read to the rows in the table, assign to class variables. Add to list of objects.
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
                string query = $"insert into {TableName} (Title, Content) values ('{post.Title}', '{post.Content}')";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
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

            return result ? post : null;
        }

        /// <summary>
        /// Remove an entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            bool result = false;

            if (id > 0)
            {

                using (var conn = new SQLiteConnection(Connectionstring))
                {
                    conn.Open();

                    string query = $"DELETE FROM {TableName} WHERE id = {id}";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }


                    result = true;
                    conn.Close();
                }
            }

            return result;
        }

    }



}