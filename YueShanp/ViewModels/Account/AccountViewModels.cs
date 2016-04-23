﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YueShanp.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email帳號")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "驗證碼")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "記住此瀏覽器?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email帳號")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email帳號")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "記住登入資訊")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email帳號")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 需要至少 {2} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "請再輸入一次密碼")]
        [Compare("Password", ErrorMessage = "密碼不相符。")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email帳號")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 需要至少 {2} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "請再輸入一次密碼")]
        [Compare("Password", ErrorMessage = "密碼不相符。")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email帳號")]
        public string Email { get; set; }
    }
}
