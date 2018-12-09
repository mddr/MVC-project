using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Helpers;
using MVC.Backend.Services;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("products")]
        public IActionResult Products()
        {
            try
            {
                var products = _productService.GetProducts();
                var results = products.Select(p => new ProductViewModel(p)).ToList();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("products/top/{amount}")]
        public IActionResult TopProducts(int amount)
        {
            try
            {
                var products = _productService.GetMostPopular(amount);
                var results = products.Select(p => new ProductViewModel(p)).ToList();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

		[HttpGet]
		[Route("products/newest/{amount}")]
		public IActionResult NewestProducts(int amount)
		{
			try
			{
				var products = _productService.GetNewest(amount);
			    var results = products.Select(p => new ProductViewModel(p)).ToList();
			    return Ok(results);
            }
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
        [Route("products/{categoryId}")]
        public IActionResult Products(int categoryId)
        {
            try
            {
                var products = _productService.GetProducts(categoryId);
                var results = products.Select(p => new ProductViewModel(p)).ToList();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("product/{id}")]
        public IActionResult Product(string id)
        {
            try
            {
                var product = _productService.GetProduct(id);
                return Ok(new ProductViewModel(product));
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

        [HttpGet]
        [Route("products/history")]
        public IActionResult GetUserHistory()
        {
            try
            {
                var userId = CurrentUserId();
                var history = _productService.GetUserHistory(userId);
                var results = history.Select(p => new ProductViewModel(p)).ToList();
                return Ok(results);
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        

        [HttpPost]
        [Route("product/add")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult Add([FromBody] ProductViewModel viewModel)
        {
            try
            {
                _productService.AddProduct(viewModel);
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
        [Route("product/update")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult Update([FromBody] ProductViewModel viewModel)
        {
            try
            {
                _productService.UpdateProduct(viewModel);
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

        [HttpDelete]
        [Route("product/delete/{id}")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            try
            {
                _productService.DeleteProduct(id);
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

        private int CurrentUserId()
        {
            return int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
