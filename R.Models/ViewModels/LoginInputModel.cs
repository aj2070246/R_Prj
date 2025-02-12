using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class LoginInputModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CaptchaId { get; set; }
        public string CaptchaValue { get; set; }

    }
}
