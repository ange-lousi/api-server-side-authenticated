using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using alou149.Models;

namespace alou149.Data
{
    public class WebAPIDBContext : DbContext
    {
        public WebAPIDBContext(DbContextOptions<WebAPIDBContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Users> User { get; set; }
        public DbSet<Orders> Order { get; set; }

    }
}
