﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MicroBlog.Models;


namespace MicroBlog.Interfaces
{
    public interface IRepository
    {
        List<Post> GetAll();

        Post Get(int id);

        Post Create(Post post);

        //Task<Post> Update(Post post);

      //  bool Delete(int id);
    }
}
