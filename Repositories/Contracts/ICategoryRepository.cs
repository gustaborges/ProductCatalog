using System.Collections.Generic;
using ProductCatalog.Models;

namespace ProductCatalog.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        bool Exists(int id);
        bool NotExists(int id);
        Category Get(int id);
        IEnumerable<Category> Get();
        IEnumerable<Product> GetProducts(int categoryId);
        Category Create(Category category);
        void Update(Category category);
        bool Delete(Category category);        
    }
}