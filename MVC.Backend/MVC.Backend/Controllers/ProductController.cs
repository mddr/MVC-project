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
    [EmailConfirmed(Roles = "Admin")]
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
                return Ok(ProductViewModel.ToList(products));
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
                return Ok(ProductViewModel.ToList(products));
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

        [HttpPost]
        [Route("product/add")]
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("product/delete/{id}")]
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

    }
}
