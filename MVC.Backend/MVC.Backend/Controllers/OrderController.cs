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
    /// Odpowiada za API do zamówień
    /// </summary>
    [EmailConfirmed(Roles = "Admin, User")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="orderService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        /// <param name="emailService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        /// <param name="userService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        public OrderController(IOrderService orderService, IEmailService emailService, IUserService userService)
        {
            _orderService = orderService;
            _emailService = emailService;
            _userService = userService;
        }

        /// <summary>
        /// Pobiera wszystkie zamówienia
        /// </summary>
        /// <returns>Wszystkie zamówienia lub informacje o błędzie</returns>
        [HttpGet]
        [Route("orders")]
        public IActionResult GetOrders()
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

        /// <summary>
        /// Pobiera historie zamówień zalogowanego użytkownika
        /// </summary>
        /// <returns>Zwraca zamowienia lub informacje o błędzie</returns>
        [HttpGet]
        [Route("orders/history")]
        public IActionResult GetOrdersHistory()
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                var orders = _orderService.OrderHistory(userId);
                return Ok(orders);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Pobiera zamówienia użytkownika o id podanym w parametrze
        /// </summary>
        /// <param name="userId">Id użytkownika</param>
        /// <returns>Zamówienia użytkownika lub informacje o błędzie</returns>
        [HttpGet]
        [Route("orders/{userId}")]
        public IActionResult GetOrders(int userId)
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

        /// <summary>
        /// Pobiera informacje o zamówienia o id podanym w parametrze
        /// </summary>
        /// <param name="id">Id zamówienia</param>
        /// <returns>Dane dotyczące zamówienia lub informacje o błędzie</returns>
        [HttpGet]
        [Route("order/{id}")]
        public IActionResult GetOrder(int id)
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

        /// <summary>
        /// Tworzy zamówienie skłądujące się z produktów w koszyku zalogowanego użytkownika
        /// </summary>
        /// <returns>Ok lub informacje o błędzie</returns>
        [HttpPost]
        [Route("order/add")]
        public IActionResult Add()
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                var user = _userService.GetUser(userId);
                var order = _orderService.AddOrder(userId);
                _emailService.SendOrderInfo(user.Email, order);
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
        /// Aktualizuje zamówienie zgodnie z danymi przekazanymi w parametrze
        /// </summary>
        /// <param name="viewModel">Nowe dane dotyczace zamówienia</param>
        /// <returns>Ok lub informacje o błędzie</returns>
        [HttpPost]
        [Route("order/update")]
        public IActionResult Update([FromBody] OrderViewModel viewModel)
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                _orderService.UpdateOrder(viewModel, userId);
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
        /// Usuwa zamówienie o id podanym w parametrze
        /// </summary>
        /// <param name="id">Id zamówienia do usuniecia</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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