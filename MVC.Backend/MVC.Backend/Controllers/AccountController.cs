﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Data;
using MVC.Backend.Helpers;
using MVC.Backend.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public AccountController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] SignupViewModel viewModel)
        {
            try
            {
                await _userService.AddUser(viewModel);
                _emailService.SendConfirmationEmail(viewModel.Email);
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
        public async Task<IActionResult> SignupAdmin([FromBody] SignupViewModel viewModel)
        {
            try
            {
                await _userService.AddUser(viewModel, Enums.Roles.Admin);
                _emailService.SendConfirmationEmail(viewModel.Email);
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
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
        {
            try
            {
                var result = await _userService.Login(viewModel);
                return result;
            }
            catch (ArgumentException )
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
		[Route("account/ConfirmEmail/{token}")]
		public async Task<IActionResult> ConfirmEmail(string token)
        {
            try
            {
                await _userService.ConfirmEmail(token);
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
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel viewModel)
        {
            try
            {
                var userId = _userService.GetCurrentUserId(HttpContext);
                await _userService.ChangePassword(userId, viewModel.OldPassword, viewModel.NewPassword);
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
        public IActionResult ResetPassword([FromBody] RequestResetPasswordViewModel viewModel)
        {
            try
            {
                var user = _userService.GetUser(viewModel.Email);
                _emailService.SendPasswordReset(user.Email);
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
        public async Task<IActionResult> SetPassword([FromBody] ResetPasswordViewModel viewModel)
        {
            try
            {
                var user = _userService.GetUser(viewModel.Email);
                await _userService.SetPassword(user.Id, viewModel.NewPassword, viewModel.Token);
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
