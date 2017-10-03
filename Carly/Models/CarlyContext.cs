using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Carly.Models
{
    public class CarlyContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Degem> Degems { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Admin> Users { get; set; }
        public DbSet<Comment> Comment { get; set; }


    }
}