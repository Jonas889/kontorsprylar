using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.Models
{
    public class StoredDbContext : DbContext
    {
        //StoredDbContext tar in all data från tabellerna i databasen
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<ProductsInCategory> ProductsInCategory { get; set; }

        //public DbSet<ProductsViewdByCustomer> ProductsViewdByCustomers { get; set; }
        //public DbSet<WishList> WishLists { get; set; }

    }
}
