using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Models;

namespace MVC.Backend.Services
{
    public interface IEmailService
    {
        void SendConfirmationEmail(string address);
        void SendPasswordReset(string address);
        void SendOrderInfo(string address, Order order);
        void SendNewsletter();
    }
}
