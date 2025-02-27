using Microsoft.AspNetCore.Mvc;
using R.Models.ViewModels;
using R.Services.IServices;
using R.Models;
using R.Models.ViewModels.DropDownItems;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;
using SixLabors.ImageSharp.PixelFormats;

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

        [HttpPost("SendEmailForNewPassword")]
        public async Task<ResultModel<bool>> SendEmailForNewPassword(SendEmailVerifyCodeInputModel model)
        {
            ResultModel<bool> result = _service.SendEmailVerifyCode(model, true);
            return result;

        }

        [HttpPost("SendEmailVerifyCodeForVerify")]
        public async Task<ResultModel<bool>> SendEmailVerifyCodeForVerify(SendEmailVerifyCodeInputModel model)
        {
            ResultModel<bool> result = _service.SendEmailVerifyCode(model, false);

            return new ResultModel<bool>(true, true);
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


        [HttpPost("login")]
        public ResultModel<LoginResultModel> login(LoginInputModel model)
        {
            var result = _service.login(model);
            return result;
        }

        [HttpGet("GetCaptcha")]
        public IActionResult GetCaptcha()
        {
            try
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
            catch (Exception e)
            {
                return Ok(e.Message.ToString() + " ---------------------" + e.InnerException?.ToString());
            }
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
            using (var image = new Image<Rgba32>(120, 40))
            {
                image.Mutate(x => x.Fill(Color.White));

                // استفاده از فونت Liberation Sans
                var font = SystemFonts.CreateFont("Liberation Serif", 20, FontStyle.Bold);

                // رسم متن کپچا
                image.Mutate(x => x.DrawText(text, font, Color.Black, new PointF(10, 5)));

                using (var ms = new MemoryStream())
                {
                    image.SaveAsPng(ms);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    
    }
}
