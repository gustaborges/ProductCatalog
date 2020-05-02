using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Mapping;
using ProductCatalog.Models;

namespace ProductCatalog.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=pdtctl;");
            optionsBuilder.UseSqlServer(@"Server=localhost,1433; Database=pdtctl; User Id=sa; Password=1Ths6*2hKQMa;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Product>(new ProductMap());
            modelBuilder.ApplyConfiguration<Category>(new CategoryMap());
        }
    }
}