using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Helpers;
using MVC.Backend.Services;

namespace MVC.Backend.Controllers
{
    [EmailConfirmed(Roles = "Admin, User")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("orders")]
        public IActionResult Orders()
        {
            try
            {
                var orders = _orderService.GetOrders();
                return Ok(OrderViewModel.ToList(orders));
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
                return Ok(OrderViewModel.ToList(orders));
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
        public IActionResult Add([FromBody] OrderViewModel viewModel)
        {
            try
            {
                _orderService.AddOrder(viewModel);
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
        [Route("order/update")]
        public IActionResult Update([FromBody] OrderViewModel viewModel)
        {
            try
            {
                _orderService.UpdateOrder(viewModel);
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
    }
}