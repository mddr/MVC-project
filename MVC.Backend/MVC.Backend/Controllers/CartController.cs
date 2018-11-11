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

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("cart")]
        [EmailConfirmed(Roles = "Admin, User")]
        public IActionResult GetCart()
        {
            try
            {
                var cart = _cartService.GetCartItems(CurrentUserId());
                return Ok(CartItemViewModel.ToList(cart));
            }
            catch (Exception ex)
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
                _cartService.AddToCart(viewModel, CurrentUserId());
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
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
                _cartService.UpdateCart(viewModel, CurrentUserId());
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
                _cartService.RemoveCart(CurrentUserId());
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
                _cartService.RemoveFromCart(productId, CurrentUserId());
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

        private int CurrentUserId()
        {
            return int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
