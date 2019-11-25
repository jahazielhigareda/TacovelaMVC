using System;
using System.ComponentModel.DataAnnotations;

namespace Tacovela.MVC.Models.Authentication
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
