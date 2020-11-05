using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Data.Entity;

namespace WebApplication1.Repository
{
    public class PostRepository
    {
        public List<Post> TraerTodos()
        {
            using (var db = new BlogContext())
            {
                return db.blogPosts.OrderByDescending(x=>x.FechaDeCreacion).ToList();

            }

        }

        public void Alta(Post model)
        {
            using (var db = new BlogContext())
            {
                db.blogPosts.Add(model);
                db.SaveChanges();
            }
        }


    }
}