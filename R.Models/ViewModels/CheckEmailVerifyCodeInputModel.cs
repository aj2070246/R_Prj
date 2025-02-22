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
        public string EmailAddress { get; set; }
        public string EmailCode { get; set; }
        public string NewPassword { get; set; }
    }
}
