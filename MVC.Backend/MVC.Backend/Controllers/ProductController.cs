using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Helpers;
using MVC.Backend.Models;
using MVC.Backend.Services;
using MVC.Backend.ViewModels;
using System;
using System.Linq;

namespace MVC.Backend.Controllers
{
    /// <summary>
    /// Zapewnia API umozliwiające interkacje z danymi produktów
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="productService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        /// <param name="fileService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        /// <param name="userService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        public ProductController(IProductService productService, IFileService fileService, IUserService userService)
        {
            _productService = productService;
            _fileService = fileService;
            _userService = userService;
        }

        /// <summary>
        /// Pobiera wszystkie produkty
        /// </summary>
        /// <returns>Wszystkie produkty lub informacje o błędzie</returns>
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

        /// <summary>
        /// Pobiera produkty widoczne przez klientów
        /// </summary>
        /// <returns>Produkty widoczne przez klientów lub informacje o błędzie</returns>
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

        /// <summary>
        /// Pobiera najpopularniejsze produkty
        /// </summary>
        /// <param name="amount">Ilość produktów do pobrania</param>
        /// <returns>Najpopularniejsze produkty lub informacje o błędziey</returns>
        [HttpGet]
        [Route("products/top/{amount}")]
        public IActionResult GetTopProducts(int amount)
        {
            try
            {
                var products = _productService.GetMostPopular(amount, false);
                var results = products.Select(p => new ProductViewModel(p)).ToList();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Pobiera najnowsze produkty
        /// </summary>
        /// <param name="amount">Liczba produktów do pobrania</param>
        /// <returns>Najnowsze produkty lub informacje o błędzie</returns>
		[HttpGet]
		[Route("products/newest/{amount}")]
		public IActionResult GetNewestProducts(int amount)
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

        /// <summary>
        /// Pobiera produkty z kategorii
        /// </summary>
        /// <param name="categoryId">Id kategorii</param>
        /// <param name="query">Query do paginacji</param>
        /// <returns>Produkty z kategorii określone przez query lub informacje o błędzie</returns>
        [HttpGet]
        [Route("products/{categoryId}")]
        public IActionResult GetProducts(int categoryId, PaginationQuery<Product> query = null)
        {
            try
            {
                var products = _productService.GetProducts(categoryId, false);
                if (query != null)
                    products = query.Paginate(products);
                var results = products.Select(p => new ProductViewModel(p)).ToList();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Pobiera prodkut o id podanym w parametrze
        /// </summary>
        /// <param name="id">Id produktu</param>
        /// <returns>Produkt lub informacje o błędzie</returns>
        [HttpGet]
        [Route("product/{id}")]
        public IActionResult GetProduct(string id)
        {
            try
            {
                var product = _productService.GetProduct(id);
                product.ThumbnailPath = product.FullImagePath; // do strony z detalami dajemy max res
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

        /// <summary>
        /// Pobiera historie zamówień użytkownika
        /// </summary>
        /// <returns>Zwraca zamówienia lub informacje o błędzie</returns>
        [HttpGet]
        [Route("products/history")]
        public IActionResult GetUserHistory()
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
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


        /// <summary>
        /// Dodaje produkt o danych jak w parametrze
        /// </summary>
        /// <param name="viewModel">Dane opisujące produkt</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Aktualizuje prodkut zgodnie z danymi w parametrze
        /// </summary>
        /// <param name="viewModel">Nowe dane produktu</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Usuwa produkt o id podanym w parametrze
        /// </summary>
        /// <param name="id">Id produktu</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Ukrywa produkt o podanym id przed użytkownikami
        /// </summary>
        /// <param name="id">Id produktu do ukrycia</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Umożliwia użytkownikom zobaczenie produktu o id podanym w parametrze
        /// </summary>
        /// <param name="id">Id produktu do ujawnienia</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Pobiera plik o podanym id związany z produktem o podanym id
        /// </summary>
        /// <param name="productId">Id produktu</param>
        /// <param name="fileId">Id pliku do pobrania</param>
        /// <returns>Plik lub informacje o błędzie</returns>
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

        /// <summary>
        /// Dodaje plik do produktu o id w parametrze
        /// </summary>
        /// <param name="viewModel">Dane dotyczące pliku</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Usuwa plik o podanym id zwiaany z produktem o podanym id
        /// </summary>
        /// <param name="productId">Id produktu</param>
        /// <param name="fileId">Id pliku do usunięcia</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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
    }
}
