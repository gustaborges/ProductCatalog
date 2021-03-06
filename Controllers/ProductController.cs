using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories.Contracts;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    public class ProductController
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository repository)
        {
            _productRepository = repository;
        }

        [Route("v1/products")]
        [HttpGet]
        [ResponseCache(Duration=1200)] // Por 1200 segundos a resposta fica em 'cache'
        public IEnumerable<ListProductViewModel> GetProducts()
        {
            return _productRepository.Get();
        }

        [Route("v1/products/{id}")]
        [HttpGet]
        public Product GetProduct(int id)
        {
            return _productRepository.Get(id);
        }

        [Route("v1/products")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorProductViewModel model, [FromServices]ICategoryRepository categoryRepository)
        {
            model.Validate();

            if (model.Invalid)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o produto",
                    Data = model
                };
            }            

            if (categoryRepository.NotExists(model.CategoryId))
            {
                model.AddNotification("CategoryId", "Categoria não encontrada");

                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o produto",
                    Data = model
                };
            }

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

            _productRepository.Create(product);

            return new ResultViewModel()
            {
                Success = true,
                Message = "Produto criado com sucesso.",
                Data = product
            };

        }


        [Route("v1/products")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorProductViewModel model)
        {
            model.Validate();

            if (model.Invalid)
                return new ResultViewModel() { Success = false, Message = "Não foi possível alterar o produto", Data = model };

            var product = _productRepository.Get(model.Id);
            
            if(product is null)
                return new ResultViewModel() { Success = false, Message = "Produto não encontrado", Data = model };


            product.Title = model.Title;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.Image = model.Image;
            product.CategoryId = model.CategoryId;
            product.LastUpdateDate = DateTime.Now;


            _productRepository.Update(product);


            return new ResultViewModel()
            {
                Success = true,
                Message = "Produto alterado com sucesso.",
                Data = product
            };
        }


        [Route("v1/products")]
        [HttpDelete]
        public ResultViewModel Delete([FromBody]EditorProductViewModel model)
        {
            var product = _productRepository.Get(model.Id);
            
            if(product is null)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível deletar este item",
                    Data = model
                };
            }

            _productRepository.Delete(product);

            return new ResultViewModel()
            {
                Success = true,
                Message = "Produto deletado com sucesso.",
                Data = product
            };

        }
    }
}