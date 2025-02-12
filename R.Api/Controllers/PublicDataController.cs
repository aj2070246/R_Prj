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

        [HttpGet("RegisterUser")]
        public ResultModel<bool> RegisterUser(RegisterUserInputModel model)
        {
            bool result = _service.RegisterUser(model);
            return new ResultModel<bool>(result, result);
        }


        [HttpGet("login")]
        public ResultModel<LoginResultModel> login(LoginInputModel model)
        {
            var  result = _service.login(model);
            return result ;
        }

        [HttpGet("GetCaptcha")]
        public IActionResult GetCaptcha()
        {
            // ایجاد عدد تصادفی سه‌رقمی
            Random random = new Random();
            int captchaCode = random.Next(100, 999);

            bool result = _service.SaveCaptcha(new SaveCaptchaInputModel()
            {
                CaptchaId = Guid.NewGuid().ToString(),
                CaptchaValue = captchaCode.ToString()
            });

            // تنظیم ابعاد تصویر
            int width = 100, height = 50;
            using Bitmap bitmap = new Bitmap(width, height);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            // تنظیم فونت و رنگ
            using Font font = new Font("Arial", 20, FontStyle.Bold);
            using SolidBrush brush = new SolidBrush(Color.Black);
            graphics.DrawString(captchaCode.ToString(), font, brush, new PointF(20, 10));

            // تبدیل تصویر به بایت و ارسال به کاربر
            using MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            return File(ms.ToArray(), "image/png");
        }
    }
}
