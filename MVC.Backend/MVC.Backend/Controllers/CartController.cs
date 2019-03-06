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
    /// Api do zarządzania koszykiem
    /// </summary>
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="cartService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        /// <param name="userService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        public CartController(ICartService cartService, IUserService userService)
        {
            _cartService = cartService;
            _userService = userService;
        }

        /// <summary>
        /// Pobiera koszyk zalogowanego użytkownika
        /// </summary>
        /// <returns>Koszyk lub informacje o błędzie</returns>
        [HttpGet]
        [Route("cart")]
        [EmailConfirmed(Roles = "Admin, User")]
        public IActionResult GetCart()
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                var cart = _cartService.GetCartItems(userId);
                var results = cart.Select(i => new CartItemViewModel(i)).ToList();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Dodaje produkt do koszyka zalogowanego użytkownika
        /// </summary>
        /// <param name="viewModel">Produkt do dodania</param>
        /// <returns>Ok lub informacje o błędzie</returns>
        [HttpPost]
        [Route("cart/add")]
        [EmailConfirmed(Roles = "Admin, User")]
        public IActionResult Add([FromBody] CartItemViewModel viewModel)
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                _cartService.AddToCart(viewModel, userId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception )
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Aktualizuje produkt z koszyka (np liczbeność produktu w koszyku)
        /// </summary>
        /// <param name="viewModel">Nowe dane produktu z koszyka</param>
        /// <returns>Ok lub informacje o błędzie</returns>
        [HttpPost]
        [Route("cart/update")]
        [EmailConfirmed(Roles = "Admin, User")]
        public IActionResult Update([FromBody] CartItemViewModel viewModel)
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                _cartService.UpdateCart(viewModel, userId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Usuwa koszyk zalogowanego użytkownika
        /// </summary>
        /// <returns>Ok lub informacje o błędzie</returns>
        [HttpDelete]
        [Route("cart/delete/")]
        [EmailConfirmed(Roles = "Admin, User")]
        public IActionResult Delete()
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);

                _cartService.RemoveCart(userId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Usuwa produkt z koszyka
        /// </summary>
        /// <param name="productId">Id produktu/param>
        /// <returns>Ok lub informacje o błędzie</returns>
        [HttpDelete]
        [Route("cart/delete/{productId}")]
        [EmailConfirmed(Roles = "Admin, User")]
        public IActionResult DeleteFromCart(string productId)
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                _cartService.RemoveFromCart(productId, userId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
