using Microsoft.AspNetCore.Authorization;
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
    /// Zapewnia API umozliwiające interkacje z danymi użytkowników
    /// </summary>
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="userService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        /// <param name="emailService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        /// <summary>
        /// Zwraca dane zalogowanego użytkownika
        /// </summary>
        /// <returns>Dane użytkownika lub informacje o błędzie</returns>
        [HttpGet]
        [Route("user")]
        public IActionResult GetCurrentUserData()
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                var userData = _userService.GetUserData(userId);
                return Ok(userData);
            }
            catch (ArgumentException )
            {
                return BadRequest();
            }
            catch (Exception )
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Umozliwia pobranie danych uztykownika o przekazanym id
        /// </summary>
        /// <param name="id">Id żądanego użytkownika</param>
        /// <returns>Dane użytkownika lub informacje o błędzie</returns>
		[HttpGet]
		[Route("user/{id}")]
		public IActionResult GetUser(int id)
		{
			try
			{
				var user = _userService.GetUser(id);
				return Ok(user);
			}
			catch (ArgumentException )
			{
				return BadRequest();
			}
			catch (Exception )
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

        /// <summary>
        /// Zwraca dane wszystkich użytkowników
        /// </summary>
        /// <returns>Dane użytkowników lub informacje o błędzie</returns>
		[HttpGet]
		[Route("users")]
		public IActionResult GetUsers()
		{
			try
			{
				var users = _userService.GetUsers();
				return Ok(users.Select(u => new UserViewModel(u)));
			}
			catch (ArgumentException )
			{
				return BadRequest();
			}
			catch (Exception )
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

        /// <summary>
        /// Aktualizuje dane użytkownika zgodnie z parametrem
        /// </summary>
        /// <param name="viewModel">Nowe wartości danych użytkownika</param>
        /// <returns>Ok lub informacje o błędzie</returns>
		[HttpPost]
		[Route("user/update")]
		public IActionResult UpdateUser([FromBody]UserViewModel viewModel)
		{
			try
			{
				_userService.UpdateUser(viewModel);
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
        /// Usuwa uzytkownika o danym id
        /// </summary>
        /// <param name="id">Id uzytkownika do usunięcia</param>
        /// <returns>Ok lub informacje o błędzie</returns>
		[HttpDelete]
		[Route("user/delete/{id}")]
		public IActionResult DeleteUser(int id)
		{
			try
			{
				_userService.DeleteUser(id);
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
        /// Wysyla newsletter dla użytkowników zgadzających się na jego otrzymywanie
        /// </summary>
        /// <returns>Ok lub informacje o błędzie</returns>
        [HttpPost]
        [Route("users/newsletter")]
        [EmailConfirmed(Roles = "Admin")]
        public IActionResult SendNewsLetter()
        {
            try
            {
                _emailService.SendNewsletter();
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
