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
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("user")]
        public IActionResult GetCurrentUserData()
        {
            try
            {
                var userId = CurrentUserId();
                var userData = _userService.GetUserData(userId);
                return Ok(userData);
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
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
