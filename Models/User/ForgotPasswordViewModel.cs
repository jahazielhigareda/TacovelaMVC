using System;
using System.ComponentModel.DataAnnotations;

namespace Tacovela.MVC.Models.User
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
