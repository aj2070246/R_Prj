using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class CheckEmailVerifyCodeInputModel
    {
        public string CaptchaId { get; set; }
        public string CaptchaValue { get; set; }
        public string CurrentUserId { get; set; }
        public string? EmailAddress { get; set; }
        public string? EmailVerifyCodeValue { get; set; }
        public string? NewPassword { get; set; }
    }
    public class CheckMobileNumberForResetPasswordInputModel
    {
        public string? CaptchaId { get; set; }
        public string? CaptchaValue { get; set; }
        public string? MobileNumber { get; set; }
    }

    public class CheckMobileNumberForResetPasswordResult
    {
        public string? InboxNumber { get; set; }
        public string? VerifyCode { get; set; }
        public bool? IsValidMobile { get; set; }
    }
    public class CheckMobileVerifyCodeForgetPasswordInputModel
    {
        public string? MobileNumber { get; set; }
    }
    public class CheckMobileVerifyCodeForgetPasswordResultModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

}
