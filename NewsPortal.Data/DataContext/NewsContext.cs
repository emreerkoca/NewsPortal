using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.DataContext
{
    public class NewsContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<Image> Image { get; set; }
    }
}
