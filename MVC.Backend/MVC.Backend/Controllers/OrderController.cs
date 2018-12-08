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
    [EmailConfirmed(Roles = "Admin, User")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IEmailService emailService, IUserService userService)
        {
            _orderService = orderService;
            _emailService = emailService;
            _userService = userService;
        }

        [HttpGet]
        [Route("orders")]
        public IActionResult Orders()
        {
            try
            {
                var orders = _orderService.GetOrders();
                var results = orders.Select(o => new OrderViewModel(o)).ToList();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("orders/history")]
        public IActionResult OrdersHistory()
        {
            try
            {
                var orders = _orderService.OrderHistory(CurrentUserId());
                return Ok(orders);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("orders/{userId}")]
        public IActionResult Orders(int userId)
        {
            try
            {
                var orders = _orderService.GetOrders(userId);
                var results = orders.Select(o => new OrderViewModel(o)).ToList();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("order/{id}")]
        public IActionResult Order(int id)
        {
            try
            {
                var order = _orderService.GetOrder(id);
                return Ok(new OrderViewModel(order));
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
        [Route("order/add")]
        public IActionResult Add()
        {
            try
            {
                var userId = CurrentUserId();
                var user = _userService.GetUser(userId);
                var order = _orderService.AddOrder(userId);
                _emailService.SendOrderInfo(user.Email, order);
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
        [Route("order/update")]
        public IActionResult Update([FromBody] OrderViewModel viewModel)
        {
            try
            {
                _orderService.UpdateOrder(viewModel, CurrentUserId());
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
        [Route("order/delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
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