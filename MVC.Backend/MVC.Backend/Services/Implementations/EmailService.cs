using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MVC.Backend.Helpers;

namespace MVC.Backend.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> _options;
        private readonly ITokenService _tokenService;
        private const string Host = "localhost:3000";

        public EmailService(IOptions<EmailSettings> options, ITokenService tokenService)
        {
            _options = options;
            _tokenService = tokenService;
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
                TextBody = @"<p>Kliknij w poniższy link aby dokończyć rejestrację:<br></p>
<a></a>",
                HtmlBody = $@"<p>Kliknij w poniższy link aby dokończyć rejestrację:<br></p>
<a href={url}>Link</a>"
            };
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(_options.Value.Host, _options.Value.Port, true);
                client.Authenticate(_options.Value.Email, _options.Value.Password);
                client.Send(message);
                client.Disconnect(true);
            }
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
                TextBody = @"<p>Kliknij w poniższy link aby zresetować hasło:<br></p>
<a></a>",
                HtmlBody = $@"<p>Kliknij w poniższy link aby zresetować hasło:<br></p>
<a href={url}>Link</a>"
            };
            message.Body = builder.ToMessageBody();

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
