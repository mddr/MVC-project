using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Helpers;
using MVC.Backend.Services;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        [Route("addresses")]
        public IActionResult Addresses()
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


        [HttpGet]
        [Route("address/{id}")]
        public IActionResult Address(int id)
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

        [HttpGet]
        [Route("userAddress/")]
        public IActionResult UserAddress()
        {
            try
            {
                var address = _addressService.GetUserAddress(CurrentUserId());
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

        [HttpPost]
        [Route("address/add")]
        public IActionResult Add([FromBody] AddressViewModel viewModel)
        {
            try
            {
                _addressService.AddAddress(viewModel, CurrentUserId());
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

		[HttpPost]
        [Route("address/update")]
        [Authorize]
        public IActionResult Update([FromBody] AddressViewModel viewModel)
        {
            try
            {
                var userId = CurrentUserId();
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

        private int CurrentUserId()
        {
            return int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}