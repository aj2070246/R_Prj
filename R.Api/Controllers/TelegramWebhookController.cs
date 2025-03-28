using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using R.Api.Controllers;
using R.Services.IServices;

[ApiController]
[Route("webhook")]
public class TelegramWebhookController : ControllerBase
{

    private readonly string _telegramToken;
    private readonly IPublicService _publicService;
    private readonly IAdminService _adminService;
    public TelegramWebhookController(IPublicService service, IAdminService adminService)
    {
        _publicService = service;
        _adminService = adminService;
        _telegramToken = _publicService.GetConfig("telegromBotToken").Model.Substring(1);

    }
   
    private static Dictionary<long, string> userPhoneNumbers = new(); // ذخیره شماره کاربران بر اساس chat_id

    [HttpPost]
    public async Task<IActionResult> ReceiveUpdate([FromBody] object update)
    {

        try
        {
            var json = JsonSerializer.Serialize(update);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("message", out var message))
            {
                return BadRequest();
            }

            if (!message.TryGetProperty("chat", out var chat) || !chat.TryGetProperty("id", out var chatIdElement))
            {
                return BadRequest();
            }

            long userId = chatIdElement.GetInt64();

            // بررسی ارسال شماره تلفن
            if (message.TryGetProperty("contact", out var contact) && contact.TryGetProperty("phone_number", out var phoneElement))
            {
                string phoneNumber = phoneElement.GetString();
                userPhoneNumbers[userId] = phoneNumber; // ذخیره شماره برای کاربر
                var saveIdRsult = _adminService.SaveUserChatId(userId, phoneNumber);

                if (!saveIdRsult.IsSuccess)
                    await RequestPhoneNumber(userId, saveIdRsult.Message, true);
                else
                    await SendMessage(userId, $"✅ شماره شما ثبت شد: {phoneNumber}\nلطفاً کد اعتبار سنجی خود را وارد کنید.");

                return Ok();
            }

            // بررسی ارسال متن (کد OTP یا دستورات دیگر)
            if (message.TryGetProperty("text", out var textElement))
            {
                string userInput = textElement.GetString();

                if (userPhoneNumbers.TryGetValue(userId, out string userPhone)) // آیا کاربر شماره داده است؟
                {
                    var otp = _adminService.GetMobileOtp(userPhone);
                    if (otp.IsSuccess)
                    {

                        if (userInput == otp.Model)
                        {
                            var setResult = _adminService.SetUserMobileIsVerify(userPhone);
                            if (setResult.IsSuccess)
                            {
                                await SendMessage(userId, "✅ کد صحیح است! ثبت‌نام شما موفقیت‌آمیز بود.");
                                userPhoneNumbers.Remove(userId); // حذف شماره از حافظه
                            }
                            else
                                await SendMessage(userId,setResult.Message);

                        }
                        else
                        {
                            await SendMessage(userId, "❌ کد وارد شده اشتباه است");
                        }
                    }
                    else
                    {
                        await SendMessage(userId, otp.Message);
                    }
                }
                else
                {
                    await RequestPhoneNumber(userId); // درخواست ارسال شماره تلفن
                }
            }

            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ خطا: {ex.Message}");
            return StatusCode(500, "خطای داخلی سرور");
        }
    }

    // ارسال پیام ساده
    private async Task SendMessage(long userId, string message)
    {
        var apiUrl = $"https://api.telegram.org/bot{_telegramToken}/sendMessage";
        var payload = new
        {
            chat_id = userId,
            text = message
        };

        var jsonPayload = JsonSerializer.Serialize(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        await client.PostAsync(apiUrl, content);
    }

    // درخواست شماره تلفن با دکمه
    private async Task RequestPhoneNumber(long userId, string message = "", bool hasError = false)
    {
        var apiUrl = $"https://api.telegram.org/bot{_telegramToken}/sendMessage";

        var payload = new
        {
            chat_id = userId,
            text = "📌 لطفاً شماره تلفن خود را ارسال کنید. برای ارسال، روی دکمه زیر کلیک کنید.",
            reply_markup = new
            {
                keyboard = new[]
                {
                    new[]
                    {
                        new
                        {
                            text = "📞 ارسال شماره تلفن"  ,
                            request_contact = true
                        }
                    }
                },
                resize_keyboard = true,
                one_time_keyboard = true
            }
        };

        if (hasError)
            payload = new
            {
                chat_id = userId,
                text = message,
                reply_markup = new
                {
                    keyboard = new[]
                    {
                    new[]
                    {
                        new
                        {
                            text = "📞 دکمه شماره تلفن",
                            request_contact = true
                        }
                    }
                },
                    resize_keyboard = true,
                    one_time_keyboard = true
                }
            };
        var jsonPayload = JsonSerializer.Serialize(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        await client.PostAsync(apiUrl, content);
    }
}
