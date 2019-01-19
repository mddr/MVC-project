using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Helpers;
using MVC.Backend.Services;
using MVC.Backend.ViewModels;
using System;
using System.Threading.Tasks;

namespace MVC.Backend.Controllers
{
    /// <summary>
    /// API umożliwiające rejestracje oraz logowanie sie
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="userService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        /// <param name="emailService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        public AccountController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        /// <summary>
        /// Tworzy nowe konto o danych podanych w parametrze
        /// </summary>
        /// <param name="viewModel">Dane nowego konta</param>
        /// <returns>Ok lub informacje o błędzie</returns>
        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] SignupViewModel viewModel)
        {
            try
            {
                await _userService.AddUser(viewModel);
                _emailService.SendConfirmationEmail(viewModel.Email);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Tworzy nowe konto administratora
        /// </summary>
        /// <param name="viewModel">Dane administratora</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Zalogowuje użytkownika jeżeli podano poprawne dane
        /// </summary>
        /// <param name="viewModel">Dane logowania</param>
        /// <returns>JWT lub informacje o błędzie</returns>
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

        /// <summary>
        /// Potwierdza konto użytkownika odpowiadajęce tokenowi podanemu w parametrze
        /// </summary>
        /// <param name="token">Token potwierzający</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Zmienia hasło zalogowanego użytkownika zgodnie z podanym parametrem
        /// </summary>
        /// <param name="viewModel">Strae oraz nowe hasło</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Resetuje hasło i wysyła mail z linkiem do zmiany
        /// </summary>
        /// <param name="viewModel">Email użytkownika resetującego hasło</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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

        /// <summary>
        /// Zmienia hasło użytkownika o mailu podanym w parametrze jeżeli użytkownik podał poprawny token (zawarty w linku do resetu)
        /// </summary>
        /// <param name="viewModel">Mail, hasło, token potwierdzający</param>
        /// <returns>Ok lub informacje o błędzie</returns>
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
