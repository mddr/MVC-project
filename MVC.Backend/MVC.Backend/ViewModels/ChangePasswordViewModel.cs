using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
		public string Email { get; set; }
		public string NewPassword { get; set; }
        public string Token { get; set; }
    }

	public class RequestResetPasswordViewModel
	{
		public string Email { get; set; }
	}
}
