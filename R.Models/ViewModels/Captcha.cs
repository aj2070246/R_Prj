using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace R.Models.ViewModels

{
    public class SaveCaptchaInputModel
    { 
        public string CaptchaId { get; set; }
        public string CaptchaValue { get; set; }
    }
}
