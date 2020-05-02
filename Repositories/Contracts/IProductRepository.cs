using System.Collections.Generic;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Repositories.Contracts
{
    public interface IProductRepository
    {
        IEnumerable<ListProductViewModel> Get();
        Product Get(int id);
        Product Create(Product product);
        void Update(Product product);
        void Delete(Product product);
        
    }
}