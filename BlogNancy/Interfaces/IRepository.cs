using System.Collections.Generic;
using System.Threading.Tasks;
using MicroBlog.Models;


namespace MicroBlog.Interfaces
{
    public interface IRepository
    {
        List<Post> GetAll();
        /// <summary>
        /// Get a post from an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A post of id n </returns>
        Post Get(int id);

        Post Create(Post post);

        Task<Post> Update(Post post);

       bool Delete(int id);
    }
}
