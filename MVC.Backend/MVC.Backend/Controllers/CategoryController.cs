using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Helpers;
using MVC.Backend.Services;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Controllers
{    
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        
        [HttpGet]
        [Route("categories")]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = _categoryService.GetCategories();
                var results = categories.Select(c => new CategoryViewModel(c)).ToList();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("categories/visible")]
        public IActionResult GetVisibleCategories()
        {
            try
            {
                var categories = _categoryService.GetVisibleCategories();
                var results = categories.Select(c => new CategoryViewModel(c)).ToList();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("category/{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var category = _categoryService.GetCategory(id);
                return Ok(new CategoryViewModel(category));
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("category/add")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult Add([FromBody] CategoryViewModel viewModel)
        {
            try
            {
                _categoryService.AddCategory(viewModel);
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("category/update")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult Update([FromBody] CategoryViewModel viewModel)
        {
            try
            {
                _categoryService.UpdateCategory(viewModel);
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("category/delete/{id}")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("category/hide/{id}")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult HideCategory(int id)
        {
            try
            {
                _categoryService.SetCategoryVisibility(id, false);
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("category/show/{id}")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult ShowCategory(int id)
        {
            try
            {
                _categoryService.SetCategoryVisibility(id, true);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("category/{id}/summary")]
        public IActionResult GetCategorySummary(int id)
        {
            try
            {
                var category = _categoryService.GetCategory(id);
                var bytes = _categoryService.GeneratePdfSummary(id);

                var fileName = category.Name + "-cennik_" + Guid.NewGuid() + ".pdf";
                return File(bytes, "application/pdf", fileName);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
