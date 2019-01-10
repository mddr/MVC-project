using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Helpers;
using MVC.Backend.Services;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        public CartController(ICartService cartService, IUserService userService)
        {
            _cartService = cartService;
            _userService = userService;
        }

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
