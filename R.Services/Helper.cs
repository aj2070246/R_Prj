using R.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using R.Database;
using static R.Services.Services.PublicService;
using System.Runtime.InteropServices.JavaScript;
using R.Models.ViewModels;
using Telegram.Bot;
using Microsoft.Extensions.DependencyInjection;

namespace R.Services
{
    public static class Helper
    {
        private static RDbContext db;


        private static IServiceScopeFactory _serviceScopeFactory;

        public static void Configure(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        private static Dictionary<string, string> configDate = null;
        private static string telegramBotToken = string.Empty;
        public static Dictionary<string, string> GetConfigs(string KeyName)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<RDbContext>(); // دریافت DbContext جدید

                if (configDate == null)
                {
                    var configList = dbContext.AppConfigs.Where(x => x.KeyName.ToLower().Contains(KeyName.ToLower())).ToDictionary(x => x.KeyName, x => x.KeyValue);
                    configDate = configList;
                }
                return configDate;
            }
        }
        public static ResultModel<string> GetConfig(string KeyName)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<RDbContext>(); // دریافت DbContext جدید

                    var config = dbContext.AppConfigs.FirstOrDefault(x => x.KeyName.ToLower() == KeyName.ToLower());
                    if (config != null)
                    {
                        telegramBotToken = config.KeyValue.Substring(1);
                        return new ResultModel<string>(telegramBotToken);
                    }
                    return new ResultModel<string>(false);
                }
            }
            catch (Exception e)
            {
                return new ResultModel<string>(false);
            }
        }

        public static async Task<ResultModel<bool>> SendChatEmail(string receiverEmail, string receiverName, string senderName, string messageText, long? chatId)
        {
            try
            {
                string subject = "پیام خصوصی جدید";
                string body = receiverName + " عزیز ";
                body += $"شما یک پیام خصوصی جدید از {senderName} دارید";

                if (!string.IsNullOrEmpty(messageText))
                {
                    body += Environment.NewLine + Environment.NewLine + Environment.NewLine + "متن پیام" + Environment.NewLine + Environment.NewLine + messageText;
                    body += Environment.NewLine + Environment.NewLine + "به منظور مشاهده متن پیام های قبلی و همچنین ارسال پاسخ به سایت مراجعه فرمایید";
                }
                else
                    body += Environment.NewLine + Environment.NewLine + "به منظور مشاهده متن پیام  و همچنین ارسال پاسخ به سایت مراجعه فرمایید";


                SendEmail(receiverEmail, receiverName, body, subject, chatId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new ResultModel<bool>(false, ex.Message);
            }


            return new ResultModel<bool>(true, true);

        }

        public static async Task<ResultModel<bool>> SendAppLoginEmail(string receiverEmail, string receiverName, long? chatId)
        {
            try
            {
                string subject = "";
                string body = "";
                subject = "ورود به سایت";
                body = $"{receiverName} عزیز {Environment.NewLine}";
                body += "کاربری شما به سایت همسریابی همسریار وارد شده است" + Environment.NewLine;
                body += "در صورتی که این دسترسی غیر مجاز است" + Environment.NewLine;
                body += "سریعا به سایت مراجعه فرموده و کلمه عبور خود را تغییر دهید" + Environment.NewLine;
                body += "تا از هر گونه سرقت اطلاعات مصون بمانید" + Environment.NewLine;

                SendEmail(receiverEmail, receiverName, body, subject, chatId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new ResultModel<bool>(false, ex.Message);
            }


            return new ResultModel<bool>(true, true);

        }

        public static async Task<ResultModel<bool>> SendAppRegisterEmail(string receiverEmail, string receiverName, long? chatId)
        {
            string senderName = "همسریار";
            try
            {
                string subject = "";
                string body = "";
                subject = "خوش آمدید";
                body = $"{receiverName} عزیز {Environment.NewLine}";
                body += "به همسریار خوش آمدید . " + Environment.NewLine;
                body += "کلیه سرویس های این وب سایت رایگان میباشد . " + Environment.NewLine;

                SendEmail(receiverEmail, receiverName, body, subject, chatId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new ResultModel<bool>(false, ex.Message);
            }


            return new ResultModel<bool>(true, true);

        }

        public static async Task<ResultModel<bool>> SendEmailVerifyCode(string? emailAddress, string verifyCode, long? chatId)
        {

            string subject = "تایید ایمیل";
            try
            {
                string body = "";
                subject = "خوش آمدید";
                body += "به همسریار خوش آمدید . " + Environment.NewLine;
                body += "کلیه سرویس های این وب سایت رایگان میباشد . " + Environment.NewLine;
                body += "کد اعتبار سنجی" + Environment.NewLine + Environment.NewLine + verifyCode;
                SendEmail(emailAddress, emailAddress, body, subject, chatId);
            }
            catch (Exception ex)
            {
                return new ResultModel<bool>(false, ex.Message);
            }
            return new ResultModel<bool>();
        }

        private static async Task<ResultModel<bool>> SendEmail(string receiverEmail, string receiverName, string body, string subject, long? chatId)
        {

            string exception = "";

            try
            {

                var emailKeys = GetConfigs("email");
                var senderEmail = emailKeys["SenderEmail"];
                string senderEmailAppPassword = emailKeys["SenderEmailAppPassword"];
                var senderEmailFullName = emailKeys["SenderEmailFullName"];

                var fromAddress = new MailAddress(senderEmail, senderEmailFullName);
                var toAddress = new MailAddress(receiverEmail, receiverName);

                string smsText = body + $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine} همسریار{Environment.NewLine}{Environment.NewLine} https://hamsaryar.com";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, senderEmailAppPassword),
                    Timeout = 20000
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = smsText,
                    IsBodyHtml = false

                })
                {
                    message.Bcc.Add(fromAddress);
                    smtp.SendMailAsync(message);
                    Console.WriteLine("Email sent successfully.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                exception = ex.Message;
            }


            try
            {
                if (chatId > 0)
                {
                    string smsText = body + $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine} همسریار {Environment.NewLine}{Environment.NewLine} https://hamsaryar.com";

                    var sendResult = SendTelegramMessage(chatId, smsText);
                }

            }
            catch (Exception e)
            {
                exception = exception + e.Message;

            }
            if (!string.IsNullOrEmpty(exception))
                return new ResultModel<bool>(false, exception);

            return new ResultModel<bool>(true, true);

        }


        private static async Task<ResultModel<bool>> SendTelegramMessage(long? chatId, string message)
        {
            if (string.IsNullOrEmpty(telegramBotToken))
                telegramBotToken = GetConfig("telegromBotToken").Model;
            TelegramBotClient Bot = new TelegramBotClient(telegramBotToken);

            try
            {
                Bot.SendTextMessageAsync(
                   chatId: chatId,
                   text: message
               );
                return new ResultModel<bool>(true, true);
            }
            catch (Exception ex)
            {
                return new ResultModel<bool>(false, false);
            }

        }
        public static string Miladi2Shamsi(DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
                return "تاریخ نامعتبر";
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);

            string persianDate = $"{year:0000}/{month:00}/{day:00}";
            return persianDate;
        }
        public static string Miladi2ShamsiWithTime(DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
                return "تاریخ نامعتبر";
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);

            int hour = persianCalendar.GetHour(dateTime);
            int minute = persianCalendar.GetMinute(dateTime);

            string persianDate = $"{year:0000}/{month:00}/{day:00} {hour:00}:{minute:00}";
            return persianDate;
        }
        public static int Miladi2ShamsiYear(DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
                return 0;
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(dateTime);
            return year;
        }
        public static int Miladi2ShamsiMonth(DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
                return 0;
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetMonth(dateTime);
            return year;
        }
        public static int Miladi2ShamsiDay(DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
                return 0;
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetDayOfMonth(dateTime);
            return year;
        }

    }
}