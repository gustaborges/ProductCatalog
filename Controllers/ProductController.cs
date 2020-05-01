using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    public class ProductController
    {
        private readonly StoreDataContext _context;

        public ProductController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("/products")]
        [HttpGet]
        public IEnumerable<ListProductViewModel> GetProducts()
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

        [Route("/products/{id}")]
        [HttpGet]
        public Product GetProduct(int id)
        {
            return _context.Products.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [Route("/products")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorProductViewModel model)
        {
            var product = new Product()
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
                Image = model.Image,
                CategoryId = model.CategoryId,

                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return new ResultViewModel()
            {
                Success = true,
                Message = "Produto criado com sucesso.",
                Data = product
            };

        }

        
        [Route("/products")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorProductViewModel model)
        {  
            var product = _context.Products.Find(model.Id);
            product.Title = model.Title;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.Image = model.Image;
            product.CategoryId = model.CategoryId;

            product.LastUpdateDate = DateTime.Now;

            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();

            return new ResultViewModel()
            {
                Success = true,
                Message = "Produto alterado com sucesso.",
                Data = product
            };
        }

        
        [Route("/products")]
        [HttpDelete]
        public ResultViewModel Delete([FromBody]Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();

            return new ResultViewModel()
            {
                Success = true,
                Message = "Produto deletado com sucesso.",
                Data = product
            };
        }
    }
}