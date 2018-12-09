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
        private readonly IFileService _fileService;

        public ProductController(IProductService productService, IFileService fileService)
        {
            _productService = productService;
            _fileService = fileService;
        }

        [HttpGet]
        [Route("products")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _productService.GetProducts(null);
                var results = products.Select(p => new ProductViewModel(p)).ToList();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("products/visible")]
        public IActionResult GetVisibleProducts()
        {
            try
            {
                var products = _productService.GetProducts(false);
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
                var products = _productService.GetMostPopular(amount, false);
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
				var products = _productService.GetNewest(amount, false);
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
                var products = _productService.GetProducts(categoryId, false);
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
                return BadRequest(ex.Message);
            }
            catch (Exception)
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("product/hide/{id}")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult HideProduct(string id)
        {
            try
            {
                _productService.SetProductVisibility(id, false);
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

        [HttpPost]
        [Route("product/show/{id}")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult ShowProduct(string id)
        {
            try
            {
                _productService.SetProductVisibility(id, true);
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
        [Route("product/{productId}/file/{fileId}")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult GetFile(string productId, int fileId)
        {
            try
            {
                var file = _productService.GetFile(productId, fileId);
                var fileContent = _fileService.GetFileContent(file.FilePath);
                return File(fileContent.Bytes, fileContent.FileType, file.FileName);
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

        [HttpPost]
        [Route("product/{id}/file/add")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult AddFile([FromBody] FileRequestViewModel viewModel)
        {
            try
            {
                _productService.AddFile(viewModel);
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

        [HttpDelete]
        [Route("product/{productId}/file/delete/{fileId}")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult DeleteFile(string productId, int fileId)
        {
            try
            {
                _productService.DeleteFile(productId, fileId);
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

        private int CurrentUserId()
        {
            return int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
