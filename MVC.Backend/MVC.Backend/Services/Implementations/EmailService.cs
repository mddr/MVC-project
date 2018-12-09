using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MVC.Backend.Helpers;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    public class EmailService : IEmailService
    {
        private const string Host = "localhost:3000";
        private readonly IOptions<EmailSettings> _options;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public EmailService(IOptions<EmailSettings> options, ITokenService tokenService, IUserService userService, IProductService productService)
        {
            _options = options;
            _tokenService = tokenService;
            _userService = userService;
            _productService = productService;
        }

        public void SendConfirmationEmail(string address)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.Value.Email));
            message.To.Add(new MailboxAddress(address));
            message.Subject = "Potwierdzenie rejestracji";

            var token = _tokenService.GenerateConfirmationToken(address);
            var url = @"http://" + Host + "/Account/ConfirmEmail/" + token;
            var builder = new BodyBuilder
            {
                TextBody = $@"Kliknij w poniższy link lub skopiuj go do przeglądarki aby dokończyć rejestrację: {url}",
                HtmlBody = $@"<p>Kliknij w poniższy link lub skopiuj go do przeglądarki aby dokończyć rejestrację:<br></p><a href={url}>Link</a>"
            };
            message.Body = builder.ToMessageBody();

            Send(message);
        }

        public void SendPasswordReset(string address)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.Value.Email));
            message.To.Add(new MailboxAddress(address));
            message.Subject = "Reset hasła";

            var token = _tokenService.GenerateResetToken(address);
            var url = @"http://" + Host + "/Account/SetPassword/" + token;
            var builder = new BodyBuilder
            {
                TextBody = $@"Kliknij w poniższy link lub skopiuj go do przeglądarki aby zresetować hasło: {url}",
                HtmlBody = $@"<p>Kliknij w poniższy link lub skopiuj go do przeglądarki aby zresetować hasło:<br></p><a href={url}>Link</a>"
            };
            message.Body = builder.ToMessageBody();

            Send(message);
        }

        public void SendOrderInfo(string address, Order order)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.Value.Email));
            message.To.Add(new MailboxAddress(address));

            var date = order.CreatedAt.ToString("g");
            message.Subject = $"Potwierdzenie złożenia zamówienia z  {date}";

            var viewModel = new OrderViewModel(order);
            var builder = new BodyBuilder
            {
                TextBody = $@"Zamówienie z {date} {viewModel}",
                HtmlBody = $@"<p>Zamówienie z {date}<hr><hr>{viewModel} </p>"
            };
            message.Body = builder.ToMessageBody();

            Send(message);
        }

        public void SendNewsletter()
        {
            var users = _userService.GetUsersForNewsletter();
            var discountedProducts = _productService.GetDiscounted(null);
            var newestProducts = _productService.GetNewest(null);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.Value.Email));
            message.Bcc.AddRange(users.Select(u => new MailboxAddress(u.Email)));

            var date = DateTime.Now.ToString("d");
            message.Subject = $"Newsletter z {date}";

            var discounted = new StringBuilder();
            foreach (var product in discountedProducts)
            {
                var viewModel = new ProductViewModel(product);
                discounted.Append(viewModel);
            }

            var newest = new StringBuilder();
            foreach (var product in newestProducts)
            {
                var viewModel = new ProductViewModel(product);
                newest.Append(viewModel);
            }

            var builder = new BodyBuilder
            {
                TextBody = $@"Newsletter: {date}Przeceny: {discounted} Nowości: {newest}",
                HtmlBody = $@"<p>Newsletter: {date}<hr><hr>Przeceny:<br> {discounted} <hr><hr>Nowości: {newest}</p>"
            };
            message.Body = builder.ToMessageBody();
            
            Send(message);
        }

        private void Send(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(_options.Value.Host, _options.Value.Port, true);
                client.Authenticate(_options.Value.Email, _options.Value.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
