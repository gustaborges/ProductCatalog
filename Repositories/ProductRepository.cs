using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories.Contracts;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListProductViewModel> Get()
        {
            return _context.Products
            .Include(x => x.Category)
            .Select(x => new ListProductViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Category = x.Category.Title,
                CategoryId = x.CategoryId
            })
            .AsNoTracking()
            .ToList();
        }

        public Product Get(int id)
        {
            return _context.Products.Find(id);
        }

        public Product Create(Product product) 
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }


        public void Update(Product product) 
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        
        public void Delete(Product product) 
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}