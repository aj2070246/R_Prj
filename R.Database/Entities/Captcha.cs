using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Database.Entities
{
    public class Captcha
    {
        [Key]
        public string CaptchaId { get; set; }
        public string CaptchaValue { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
