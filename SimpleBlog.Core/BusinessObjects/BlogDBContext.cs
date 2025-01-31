﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Core.BusinessObjects
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext() : base("name = BlogDBConnectionString")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
