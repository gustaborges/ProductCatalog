using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories.Contracts;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.CategoryViewModels;
using System.Collections.Generic;

namespace ProductCatalog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [Route("v1/categories/{id}")]
        [HttpGet]
        public Category GetCategory(int id)
        {
            return _categoryRepository.Get(id);
        }

        [Route("v1/categories")]
        [HttpGet]
        public IEnumerable<Category> GetCategory()
        {
            return _categoryRepository.Get();
        }


        [Route("v1/categories/{id}/products")]
        [HttpGet]
        public IEnumerable<Product> GetProducts(int id)
        {
            return _categoryRepository.GetProducts(categoryId: id);
        }


        [Route("v1/categories")]
        [HttpPost]
        public ResultViewModel Post([FromBody] EditorCategoryViewModel model)
        {
            model.Validate();

            if (model.Invalid)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível criar categoria",
                    Data = model
                };
            }

            var category = new Category() { Title = model.Title };

            category = _categoryRepository.Create(category);

            return new ResultViewModel()
            {
                Success = true,
                Message = "Categoria criada com sucesso",
                Data = category
            };
        }


        [Route("v1/categories")]
        [HttpPut]
        public ResultViewModel Put([FromBody] EditorCategoryViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel()
                { 
                    Success = false,
                    Message = "Não foi possível alterar categoria",
                    Data = model 
                };
            }

            var category = _categoryRepository.Get(model.Id);
            if (category is null)
            {
                return new ResultViewModel()
                { 
                    Success = false,
                    Message = "Categoria não encontrada",
                    Data = model
                };
            }

            category.Title = model.Title;

            _categoryRepository.Update(category);


            return new ResultViewModel()
            {
                Success = true,
                Message = "Categoria alterada com sucesso",
                Data = category
            };
        }


        [Route("v1/categories")]
        [HttpDelete]
        public ResultViewModel Delete([FromBody] EditorCategoryViewModel model)
        {
            var category = _categoryRepository.Get(model.Id);

            if (category is null)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível deletar este item",
                    Data = model
                };
            }

            _categoryRepository.Delete(category);

            return new ResultViewModel()
            {
                Success = true,
                Message = "Produto deletado com sucesso.",
                Data = category
            };
        }
    }
}