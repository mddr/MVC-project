using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Services;
using MVC.Backend.ViewModels;
using System;
using System.Linq;

namespace MVC.Backend.Controllers
{
    /// <summary>
    /// API do zarządanie adresem użytkownika
    /// </summary>
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="addressService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        /// <param name="userService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        public AddressController(IAddressService addressService, IUserService userService)
        {
            _addressService = addressService;
            _userService = userService;
        }

        /// <summary>
        /// Pobiera wszystkie adresy
        /// </summary>
        /// <returns>Wszystkie adresy lub informacje o błędzie</returns>
        [HttpGet]
        [Route("addresses")]
        public IActionResult GetAddresses()
        {
            try
            {
                var addresses = _addressService.GetAddresses();
                var results = addresses.Select(a => new AddressViewModel(a)).ToList();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Pobiera adres o id podanym w paremetrze
        /// </summary>
        /// <param name="id">Id adresu</param>
        /// <returns>Adres lub informacje o błędzie</returns>
        [HttpGet]
        [Route("address/{id}")]
        public IActionResult GetAddress(int id)
        {
            try
            {
                var address = _addressService.GetAddress(id);
                return Ok(new AddressViewModel(address));
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
        /// Pobiera adres zalogowanego użytkownika
        /// </summary>
        /// <returns>Adres lub informacje o błędzie</returns>
        [HttpGet]
        [Route("userAddress/")]
        public IActionResult GetUserAddress()
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                var address = _addressService.GetUserAddress(userId);
                return Ok(new AddressViewModel(address));
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
        /// Dodaje adres zalogowanego użytkownika
        /// </summary>
        /// <param name="viewModel">Nowy adres</param>
        /// <returns>Ok lub informacje o błędzie</returns>
        [HttpPost]
        [Route("address/add")]
        public IActionResult Add([FromBody] AddressViewModel viewModel)
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                _addressService.AddAddress(viewModel, userId);
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
        /// Zastępuje adres użytkownika o id podanym w parametrze
        /// </summary>
        /// <param name="viewModel">Nowy adres</param>
        /// <param name="userId">Id użytkownika</param>
        /// <returns>Ok lub informacje o błędzie</returns>
		[HttpPost]
		[Route("address/add/{userId}")]
		public IActionResult SetAddress([FromBody] AddressViewModel viewModel, int userId)
		{
			try
			{
				_addressService.AddAddress(viewModel, userId);
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
        /// Aktualizuje dane adresu zalogowanego użytkownika
        /// </summary>
        /// <param name="viewModel">Nowe dane adresu</param>
        /// <returns>Ok lub informacje o błędzie</returns>
		[HttpPost]
        [Route("address/update")]
        [Authorize]
        public IActionResult Update([FromBody] AddressViewModel viewModel)
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                _addressService.UpdateAddress(userId, viewModel);
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
        /// Usuwa adres o podanym id
        /// </summary>
        /// <param name="id">Id adresu</param>
        /// <returns>Ok lub informacje o błędzie</returns>
        [HttpDelete]
        [Route("address/delete/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                _addressService.DeleteAddress(id);
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