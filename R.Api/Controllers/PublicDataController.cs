using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using R.Database;
using R.Models.ViewModels;
using R.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using R.Models;
using R.Models.ViewModels.DropDownItems;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;
using System.Net;
namespace R.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicDataController : ControllerBase
    {
        private readonly IPublicService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public PublicDataController(IPublicService service, ILogger<WeatherForecastController> logger)
        {
            _service = service;
            _logger = logger;

        }

        [HttpGet("GetAllDropDownsItems")]
        public ResultModel<AllDropDownItems> GetAllDropDownsItems()
        {
            var result = _service.GetAllDropDownItems();
            return new ResultModel<AllDropDownItems>(result);
        }
     
        [HttpPost("SendEmailVerifyCodeForResetPassword")]
        public async Task<ResultModel<bool>> SendEmailVerifyCodeForResetPassword(SendEmailVerifyCodeInputModel model)
        {
            ResultModel<bool> result = _service.SendEmailVerifyCode(model,true);
             
            return new ResultModel<bool>(true,true);

        }

        [HttpPost("SendEmailVerifyCodeForVerify")]
        public async Task<ResultModel<bool>> SendEmailVerifyCodeForVerify(SendEmailVerifyCodeInputModel model)
        {
            ResultModel<bool> result = _service.SendEmailVerifyCode(model, false);

            return new ResultModel<bool>(true, true);
        }

        [HttpPost("VerifyEmailCodeForResetPassword")]
        public ResultModel<bool> VerifyEmailCodeForResetPassword(CheckEmailVerifyCodeInputModel model)
        {
            ResultModel<bool> result = _service.VerifyEmailCode(model, true);
            return result;
        }
        [HttpPost("VerifyEmailCodeForAcceptEmail")]
        public async Task<ResultModel<bool>> VerifyEmailCodeForAcceptEmail(CheckEmailVerifyCodeInputModel model)
        {
            ResultModel<bool> result = _service.VerifyEmailCode(model, false);
            return result;
        }

        [HttpPost("RegisterUser")]
        public ResultModel<bool> RegisterUser(RegisterUserInputModel model)
        {
            var result = _service.RegisterUser(model);
            return result;
        }
        [HttpPost("UpdateUserInfo")]
        public ResultModel<bool> UpdateUserInfo(UpdateUserInputModel model)
        {
            var result = _service.UpdateUserInfo(model);
            return result;
        }


        [HttpPost("login")]
        public ResultModel<LoginResultModel> login(LoginInputModel model)
        {
            var result = _service.login(model);
            return result;
        }
        [HttpGet("GetCaptcha")]
        public IActionResult GetCaptcha()
        {
            string captchaText = GenerateRandomText();
            string captchaId = Guid.NewGuid().ToString();

            // ذخیره کپچا در کش یا دیتابیس
            SaveCaptchaToDatabase(captchaId, captchaText);

            // ایجاد تصویر کپچا
            string base64Image = GenerateCaptchaImage(captchaText);

            return Ok(new
            {
                image = "data:image/png;base64," + base64Image,
                guid = captchaId
            });
        }

        private string GenerateRandomText()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void SaveCaptchaToDatabase(string captchaId, string text)
        {
            _service.SaveCaptcha(new SaveCaptchaInputModel { CaptchaId = captchaId, CaptchaValue = text });

        }

        private string GenerateCaptchaImage(string text)
        {
            using (Bitmap bitmap = new Bitmap(120, 40))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                Font font = new Font("Arial", 20, FontStyle.Bold);
                g.DrawString(text, font, Brushes.Black, 10, 5);

                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}
