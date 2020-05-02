using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories.Contracts;

namespace ProductCatalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDataContext _context;
        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public Category Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }


        public bool Delete(Category category)
        {
            
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return true;
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public Category Get(int id)
        {
            return _context.Categories.Find(id);        
        }

        public IEnumerable<Category> Get()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        public IEnumerable<Product> GetProducts(int categoryId)
        {
            return _context.Products.AsNoTracking().Where(x => x.CategoryId == categoryId).ToList();
        }

        public bool Exists(int id)
        {
            return _context.Categories.Any(x => x.Id == id);
        }
        public bool NotExists(int id)
        {
            return !Exists(id);
        }
    }
}