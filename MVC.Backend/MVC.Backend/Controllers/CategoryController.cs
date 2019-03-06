using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Helpers;
using MVC.Backend.Services;
using MVC.Backend.ViewModels;
using System;
using System.Linq;

namespace MVC.Backend.Controllers
{
    /// <summary>
    /// Odpowiada za API do zarządzania kategoriami produktów
    /// </summary>
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="categoryService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Pobiera wszystkie kategorie
        /// </summary>
        /// <returns>Kategorie produktów lub informacje o błędzie</returns>
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Pobiera kategorie widoczne przez klientów
        /// </summary>
        /// <returns>Kategorie produktów lub informacje o błędzie</returns>
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

        /// <summary>
        /// Pobiera informacje o kategorii o id podanym w parametrze
        /// </summary>
        /// <param name="id">Id kategorii</param>
        /// <returns>Informacje o kategorii lub informacje o błędzie</returns>
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

        /// <summary>
        /// Dodaje kategorie o danych podanych w parametrze
        /// </summary>
        /// <param name="viewModel">Dane nowej kategorii</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Aktualizuje dane kategorii o id podanym w parametrze
        /// </summary>
        /// <param name="viewModel">Nowe dane kategorii</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Usuwa kategorie o id podanym w parametrze
        /// </summary>
        /// <param name="id">Id kategorii do usunięcia</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Ukrywa kategorie i wszystkie produkty z niej
        /// </summary>
        /// <param name="id">Id kategorii do ukrycia</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Ujawnia kategorie i wszystkie produkty z niej
        /// </summary>
        /// <param name="id">Id kategorii do ujawnienia</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Zwraca pdf podsumowujący kategorie o id podanym w parametrze
        /// </summary>
        /// <param name="id">Id kategeroii</param>
        /// <returns>Plik podsumowania lub informacje o błędzie</returns>
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
