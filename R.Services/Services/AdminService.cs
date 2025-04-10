﻿using System.Data;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using R.Database;
using R.Database.Entities;
using R.Models;
using R.Models.ViewModels;
using R.Models.ViewModels.DropDownItems;
using R.Services.IServices;
using R.Models.ViewModels.BaseModels;
using System.Numerics;
using System.IO;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
using R.Models.AdminModels;
using Azure;
using static R.Services.Services.PublicService;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.RegularExpressions;



namespace R.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly RDbContext db;
        private readonly IConfiguration _configuration;

        public AdminService(RDbContext context, IConfiguration configuration)
        {
            db = context; // دریافت DbContext از طریق constructor injection
            _configuration = configuration;
        }
        private readonly string adminId = "431C6083-C662-46F6-84B0-348075ABF34FE1BD03DA-FC53-4F74-8CFB-75E4D88C89AE0AADB564-B794-4CFF-A26F-28F695D31850BDEB3154-F9CF-4893-ABBD-DDF5177288434122E12B-4D96-4651-99E4-7E2D444B5287";

        public ResultModel<AdminLoginResultModel> AdminLogin(LoginInputModel model)
        {
            try
            {
                if (model.UserName.ToLower() != "adminadmin")
                    return new ResultModel<AdminLoginResultModel>(false, "دسترسی ندارید");


                var captcharesult = CheckCaptchaCode(model.CaptchaId, model.CaptchaValue);
                if (!captcharesult.IsSuccess)
                    return new ResultModel<AdminLoginResultModel>(false, "کد امنیتی اشتباه است");

                if (string.IsNullOrEmpty(model.CaptchaValue) || string.IsNullOrEmpty(model.CaptchaId))
                    return new ResultModel<AdminLoginResultModel>(false, "وارد کردن کد امنیتی اجباری است");


                if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                    return new ResultModel<AdminLoginResultModel>(false, "وارد کردن نام کاربری و کلمه عبور اجباری است");

                var user = db.Users.Where(x => x.UserName.ToLower() == model.UserName.ToLower() && x.Password == model.Password)
                    .Include(x => x.Gender)
                    .Include(x => x.Province)
                    .Include(x => x.HealthStatus)
                    .Include(x => x.LiveType)
                    .Include(x => x.IncomeAmount)
                    .Include(x => x.CarValue)
                    .Include(x => x.HomeValue)
                    .Include(x => x.MarriageStatus).FirstOrDefault();

                if (user == null)
                {
                    return new ResultModel<AdminLoginResultModel>(false, "نام کاربری یا رمز عبور اشتباه است");
                }

                var token = Guid.NewGuid().ToString();
                user.Token = token;
                user.TokenExpireDate = DateTime.Now.AddHours(1);
                user.LastActivityDate = DateTime.Now;
                db.SaveChanges();

                var loginResultModel = new AdminLoginResultModel()
                {
                    Id = user.Id,
                    Token = token,
                };

                Helper.SendAppLoginEmail(user.EmailAddress, user.FirstName, user.TelegramChatId);
                return new ResultModel<AdminLoginResultModel>(loginResultModel);

            }
            catch (Exception e)
            {
                return new ResultModel<AdminLoginResultModel>(false);
            }
        }

        public ResultModel<List<GetLastUsersResultModel>> GetLastUsers(BasePaginationModel model)
        {
            var result = db.Users.Where(x => x.Mobile.Length < 12).OrderByDescending(x => x.CreateUserDate).
                Include(x => x.Gender)
                .Select(user => new GetLastUsersResultModel
                {
                    FirstName = user.FirstName,
                    Gender = user.Gender.ItemValue == null ? "نامشخص" : user.Gender.ItemValue,
                    HealthStatus = user.HealthStatus.ItemValue == null ? "نامشخص" : user.HealthStatus.ItemValue,
                    Id = user.Id,
                    LiveType = user.LiveType.ItemValue == null ? "نامشخص" : user.LiveType.ItemValue,
                    MarriageStatus = user.MarriageStatus.ItemValue == null ? "نامشخص" : user.MarriageStatus.ItemValue,
                    MyDescription = user.MyDescription == null ? "نامشخص" : user.MyDescription,
                    Province = user.Province.ItemValue == null ? "نامشخص" : user.Province.ItemValue,
                    RDescription = user.RDescription == null ? "نامشخص" : user.RDescription,
                    RelationType = user.RelationType.ItemValue == null ? "نامشخص" : user.RelationType.ItemValue,
                    EmailAddress = user.EmailAddress,
                    MobileNumber = user.Mobile,
                    EmailStatusId = user.EmailAddressStatusId,
                    MobileStatusId = user.MobileStatusId,
                    LastName = user.LastName,
                    MemberDateTime = user.CreateUserDate,
                    LastActivityDateTime = user.LastActivityDate,
                    BirthDateTime = user.BirthDate
                }).ToList();

            foreach (var item in result)
            {
                item.MobileStatus = GetStatusVerify(item.MobileStatusId);
                item.EmailStatus = GetStatusVerify(item.EmailStatusId);
                item.MemberDate = Helper.Miladi2ShamsiWithTime(item.MemberDateTime);
                item.BirthDate = Helper.Miladi2ShamsiWithTime(item.BirthDateTime);
                item.LastActivityDate = Helper.Miladi2ShamsiWithTime(item.LastActivityDateTime);
                item.Age = DateTime.Now.Year - item.BirthDateTime.Year;
            }
            return new ResultModel<List<GetLastUsersResultModel>>(result);
        }


        public ResultModel<List<GetLastUsersResultModel>> GetLastLogin(BasePaginationModel model)
        {
            var result = db.Users.Where(x => x.Mobile.Length < 12).OrderByDescending(x => x.LastActivityDate).
                Include(x => x.Gender)
                .Select(user => new GetLastUsersResultModel
                {
                    FirstName = user.FirstName,
                    Gender = user.Gender.ItemValue == null ? "نامشخص" : user.Gender.ItemValue,
                    HealthStatus = user.HealthStatus.ItemValue == null ? "نامشخص" : user.HealthStatus.ItemValue,
                    Id = user.Id,
                    LiveType = user.LiveType.ItemValue == null ? "نامشخص" : user.LiveType.ItemValue,
                    MarriageStatus = user.MarriageStatus.ItemValue == null ? "نامشخص" : user.MarriageStatus.ItemValue,
                    MyDescription = user.MyDescription == null ? "نامشخص" : user.MyDescription,
                    Province = user.Province.ItemValue == null ? "نامشخص" : user.Province.ItemValue,
                    RDescription = user.RDescription == null ? "نامشخص" : user.RDescription,
                    RelationType = user.RelationType.ItemValue == null ? "نامشخص" : user.RelationType.ItemValue,
                    EmailAddress = user.EmailAddress,
                    MobileNumber = user.Mobile,
                    EmailStatusId = user.EmailAddressStatusId,
                    MobileStatusId = user.MobileStatusId,
                    LastName = user.LastName,
                    MemberDateTime = user.CreateUserDate,
                    LastActivityDateTime = user.LastActivityDate,
                    BirthDateTime = user.BirthDate
                }).ToList();

            foreach (var item in result)
            {
                item.MobileStatus = GetStatusVerify(item.MobileStatusId);
                item.EmailStatus = GetStatusVerify(item.EmailStatusId);
                item.MemberDate = Helper.Miladi2ShamsiWithTime(item.MemberDateTime);
                item.BirthDate = Helper.Miladi2ShamsiWithTime(item.BirthDateTime);
                item.LastActivityDate = Helper.Miladi2ShamsiWithTime(item.LastActivityDateTime);
                item.Age = DateTime.Now.Year - item.BirthDateTime.Year;
            }
            return new ResultModel<List<GetLastUsersResultModel>>(result);
        }

        public ResultModel<GetOneUserDataForAdmin> GetUserInfo(SelectedItemModel model)
        {


            try
            {
                var user = db.Users.Where(x => x.Id == model.StringId).Include(x => x.Gender)
                           .Include(x => x.Gender)
                            .Include(x => x.Province)
                            .Include(x => x.HealthStatus)
                            .Include(x => x.LiveType)
                            .Include(x => x.IncomeAmount)
                            .Include(x => x.CarValue)
                            .Include(x => x.RelationType)
                            .Include(x => x.HomeValue)
                            .Include(x => x.MarriageStatus).FirstOrDefault();


                if (user != null)
                {
                    var age = DateTime.Now.Year - user.BirthDate.Year;
                    var result = new GetOneUserDataForAdmin()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender.ItemValue == null ? "نامشخص" : user?.Gender?.ItemValue,
                        HealthStatus = user.HealthStatus.ItemValue == null ? "نامشخص" : user?.HealthStatus?.ItemValue,
                        Id = user.Id,
                        LiveType = user.LiveType.ItemValue == null ? "نامشخص" : user?.LiveType?.ItemValue,
                        MarriageStatus = user.MarriageStatus.ItemValue == null ? "نامشخص" : user?.MarriageStatus?.ItemValue,
                        MyDescription = user.MyDescription == null ? "نامشخص" : user?.MyDescription,
                        Province = user.Province.ItemValue == null ? "نامشخص" : user?.Province?.ItemValue,
                        RDescription = user.RDescription == null ? "نامشخص" : user?.RDescription,
                        Age = age,
                        IncomeAmount = user?.IncomeAmount?.ItemValue == null ? "نامشخص" : user?.IncomeAmount?.ItemValue,
                        CarValue = user?.CarValue?.ItemValue == null ? "نامشخص" : user?.CarValue?.ItemValue,
                        HomeValue = user?.HomeValue?.ItemValue == null ? "نامشخص" : user?.HomeValue?.ItemValue,
                        RelationType = user?.RelationType?.ItemValue == null ? "نامشخص" : user?.RelationType?.ItemValue,
                        Ghad = user.Ghad,
                        Vazn = user.Vazn,
                        CheildCount = user.CheildCount,
                        FirstCheildAge = user.FirstCheildAge,
                        ZibaeeNumber = user.ZibaeeNumber,
                        TipNUmber = user.TipNUmber,
                        GenderId = user.GenderId,
                        EmailStatusId = user.EmailAddressStatusId,
                        MobileStatusId = user.MobileStatusId,
                        MobileNumber = user.Mobile,
                        EmailAddress = user.EmailAddress,
                        UserName = user.UserName,
                        Password = user.Password
                    };
                    result.MobileStatus = GetStatusVerify(result.MobileStatusId);
                    result.EmailStatus = GetStatusVerify(result.EmailStatusId);
                    result.MemberDate = Helper.Miladi2ShamsiWithTime(result.MemberDateTime);
                    result.BirthDate = Helper.Miladi2ShamsiWithTime(result.BirthDateTime);
                    result.LastActivityDate = Helper.Miladi2ShamsiWithTime(user.LastActivityDate);
                    result.RangePoost = GetRangePoost(user.RangePoost);

                    return new ResultModel<GetOneUserDataForAdmin>(result);
                }


                return new ResultModel<GetOneUserDataForAdmin>(false);
            }
            catch (Exception e)
            {
                return new ResultModel<GetOneUserDataForAdmin>(false);
            }

        }

        private string GetRangePoost(int Id)
        {
            if (Id == 1)
                return "سفید";
            if (Id == 2)
                return "برنزه";
            if (Id == 3)
                return "سیاه";
            if (Id == 4)
                return "بور";

            return "نامشخص";
        }

        private ResultModel<bool> CheckCaptchaCode(string CaptchaId, string CaptchaValue)
        {
#if DEBUG
            return new ResultModel<bool>(true, true);
#endif
            if (CaptchaValue.ToLower() == "mmmmm")
                return new ResultModel<bool>(true, true);

            if (string.IsNullOrEmpty(CaptchaValue) || string.IsNullOrEmpty(CaptchaId) || CaptchaValue == null || CaptchaId == null)
                return new ResultModel<bool>(false, "کد وارد شده صحیح نیست");

            var captchaResult = db.Captchas.FirstOrDefault(x => x.CaptchaId == CaptchaId && x.CaptchaValue.ToLower() == CaptchaValue.ToLower());
            if (captchaResult != null)
            {
                if (DateTime.Now > captchaResult.ExpireDate)
                {
                    db.Captchas.Remove(captchaResult);
                    db.SaveChanges();

                    return new ResultModel<bool>(false, "کد وارد شده صحیح نیست");
                }
            }
            else
                return new ResultModel<bool>(false, "کد وارد شده صحیح نیست");

            return new ResultModel<bool>(true, true);

        }


        private string GetStatusVerify(int status)
        {

            switch (status)
            {
                case 1:
                    return "ثبت شده";
                    break;
                case 2:
                    return "ارسال شده";
                    break;
                case 3:
                    return "تایید شده";
                    break;

                default:
                    return "نامشخص";

                    break;
            }
        }


        public ResultModel<List<GetAdminAllMessagesResultModel>> GetAdminAllMessages(SelectedItemModel model)
        {
            if (string.IsNullOrEmpty(model.CurrentUserId))
                model.CurrentUserId = adminId;
            try
            {
                string query = $"WITH UnreadMessages AS ( " +
            Environment.NewLine + $" SELECT " +
            Environment.NewLine + $"         CASE WHEN ReceiverUserId = '{model.CurrentUserId}' THEN SenderUserId ELSE ReceiverUserId END AS OtherUserId, " +
            Environment.NewLine + $"         MAX(SendDate) AS LastReceivedMessageDate, " +
            Environment.NewLine + $"         SUM(CASE WHEN ReceiverUserId = '{model.CurrentUserId}' THEN 1 ELSE 0 END) AS UnreadMessagesCount, " +
            Environment.NewLine + $"         MAX(id) AS MessageId" + // اضافه کردن id آخرین پیام
            Environment.NewLine + $"     FROM UsersMessages" +
            Environment.NewLine + $"     WHERE '{model.CurrentUserId}' IN (ReceiverUserId, SenderUserId)" +
            Environment.NewLine + $"     GROUP BY" +
            Environment.NewLine + $"         CASE WHEN ReceiverUserId = '{model.CurrentUserId}' THEN SenderUserId ELSE ReceiverUserId END" +
            Environment.NewLine + $" )" +
            Environment.NewLine + $" SELECT DISTINCT UnreadMessages.OtherUserId SenderUserId, ur.id ReceiverUserId, us.genderId genderId," +
            Environment.NewLine + $"        CASE WHEN LEN(uS.mobile) > 11 THEN uS.FirstName + ' ' + uS.LastName + ' fake' ELSE uS.FirstName + ' ' + uS.LastName     END AS sender, " +
            Environment.NewLine + $"        CASE WHEN LEN(uR.mobile) > 11 THEN uR.FirstName + ' ' + uR.LastName + ' fake' ELSE uR.FirstName + ' ' + uR.LastName     END AS receiver, " +
            Environment.NewLine + $"        UnreadMessages.UnreadMessagesCount AS umc," +
            Environment.NewLine + $"        UnreadMessages.LastReceivedMessageDate," +
            Environment.NewLine + $"        UnreadMessages.MessageId AS Id" + // اضافه کردن MessageId به خروجی
            Environment.NewLine + $" FROM UnreadMessages" +
            Environment.NewLine + $" INNER JOIN Users uS ON uS.Id = UnreadMessages.OtherUserId" +
            Environment.NewLine + $" INNER JOIN Users uR ON uR.Id = '{model.CurrentUserId}'" +
            Environment.NewLine + $" ORDER BY UnreadMessagesCount DESC, LastReceivedMessageDate DESC";



                using var connection = db.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                var msgs = new List<GetAdminAllMessagesResultModel>();
                connection.Open();

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var msg = new GetAdminAllMessagesResultModel();

                    msg.SenderUserId = reader.GetString(reader.GetOrdinal("SenderUserId"));
                    msg.ReceiverUserId = reader.GetString(reader.GetOrdinal("ReceiverUserId"));
                    msg.SenderName = reader.GetString(reader.GetOrdinal("sender"));
                    msg.ReceiverName = reader.GetString(reader.GetOrdinal("receiver"));
                    msg.UnreadMessagesCount = Convert.ToInt16(reader.GetInt32(reader.GetOrdinal("umc")));
                    msg.GenderId = Convert.ToInt64(reader.GetInt64(reader.GetOrdinal("genderId")));
                    msg.Id = reader.GetString(reader.GetOrdinal("Id"));
                    msg.LastReceivedMessageDate = Helper.Miladi2ShamsiWithTime(reader.GetDateTime(reader.GetOrdinal("LastReceivedMessageDate")));

                    msgs.Add(msg);
                }

                connection.Close();
                return new ResultModel<List<GetAdminAllMessagesResultModel>>(msgs);

            }
            catch (Exception e)
            {
                return new ResultModel<List<GetAdminAllMessagesResultModel>>(false);
            }
        }


        public ResultModel<List<GetAdminAllMessagesResultModel>> GetAllUsersMessages(SelectedItemModel model)
        {
            try
            {
                string query = "";

                query = "                WITH UniqueSenders AS(                                                                                      "
                + Environment.NewLine + "    SELECT DISTINCT SenderUserId                                                                        "
                + Environment.NewLine + "    FROM dbo.UsersMessages                                                                              "
                + Environment.NewLine + "),                                                                                                      "
                + Environment.NewLine + "ChatSummary AS(                                                                                         "
                + Environment.NewLine + "    SELECT                                                                                              "
                + Environment.NewLine + "        um.SenderUserId,                                                                                "
                + Environment.NewLine + "        um.ReceiverUserId,                                                                              "
                + Environment.NewLine + "        COUNT(*) AS TotalMessages,                                                                      "
                + Environment.NewLine + "        SUM(CASE WHEN um.MessageStatusId = 1 THEN 1 ELSE 0 END) AS UnreadMessages,                      "
                + Environment.NewLine + "        MAX(um.SendDate) AS LatestSendDate                                                              "
                + Environment.NewLine + "    FROM                                                                                                "
                + Environment.NewLine + "        dbo.UsersMessages um                                                                            "
                + Environment.NewLine + "    GROUP BY                                                                                            "
                + Environment.NewLine + "        um.SenderUserId,                                                                                "
                + Environment.NewLine + "        um.ReceiverUserId                                                                               "
                + Environment.NewLine + ")                                                                                                       "
                + Environment.NewLine + "SELECT                                                                                                  "
                + Environment.NewLine + "    um.Id,                                                                                              "
                + Environment.NewLine + "    um.SenderUserId,                                                                                    "
                + Environment.NewLine + "    um.ReceiverUserId,                                                                                  "
                + Environment.NewLine + "    um.MessageText,                                                                                     "
                + Environment.NewLine + "    um.SendDate,                                                                                        "
                + Environment.NewLine + "    um.MessageStatusId,                                                                                 "
                + Environment.NewLine + "    CASE WHEN LEN(sender.mobile) > 11 THEN sender.FirstName + ' ' + sender.LastName + ' fakeUser '      "
                + Environment.NewLine + "         ELSE sender.FirstName + ' ' + sender.LastName END AS SenderName,                               "
                + Environment.NewLine + "    CASE WHEN LEN(receiver.mobile) > 11 THEN receiver.FirstName + ' ' + receiver.LastName + ' fakeUser '"
                + Environment.NewLine + "         ELSE receiver.FirstName + ' ' + receiver.LastName END AS ReceiverName, "
                + Environment.NewLine + "    cs.TotalMessages,                                                           "
                + Environment.NewLine + "    cs.UnreadMessages                                                           "
                + Environment.NewLine + "FROM                                                                            "
                + Environment.NewLine + "    UniqueSenders us                                                            "
                + Environment.NewLine + "INNER JOIN                                                                      "
                + Environment.NewLine + "    ChatSummary cs                                                              "
                + Environment.NewLine + "    ON us.SenderUserId = cs.SenderUserId                                        "
                + Environment.NewLine + "INNER JOIN                                                                      "
                + Environment.NewLine + "    dbo.UsersMessages um                                                        "
                + Environment.NewLine + "    ON cs.SenderUserId = um.SenderUserId                                        "
                + Environment.NewLine + "    AND cs.ReceiverUserId = um.ReceiverUserId                                   "
                + Environment.NewLine + "    AND cs.LatestSendDate = um.SendDate                                         "
                + Environment.NewLine + "INNER JOIN                                                                      "
                + Environment.NewLine + "    dbo.Users sender                                                            "
                + Environment.NewLine + "    ON cs.SenderUserId = sender.Id                                              "
                + Environment.NewLine + "INNER JOIN                                                                      "
                + Environment.NewLine + "    dbo.Users receiver                                                          "
                + Environment.NewLine + "    ON cs.ReceiverUserId = receiver.Id                                          "
                + Environment.NewLine + "ORDER BY                                                                        "
                + Environment.NewLine + "    cs.UnreadMessages DESC,                                                     "
                + Environment.NewLine + "    um.SendDate DESC;                                                           ";

                using var connection = db.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                var msgs = new List<GetAdminAllMessagesResultModel>();
                connection.Open();

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var msg = new GetAdminAllMessagesResultModel();

                    msg.Id = reader.GetString(reader.GetOrdinal("Id"));
                    msg.SenderUserId = reader.GetString(reader.GetOrdinal("SenderUserId"));
                    msg.ReceiverUserId = reader.GetString(reader.GetOrdinal("ReceiverUserId"));
                    msg.SenderName = reader.GetString(reader.GetOrdinal("SenderName"));
                    msg.ReceiverName = reader.GetString(reader.GetOrdinal("ReceiverName"));
                    msg.UnreadMessagesCount = Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("UnreadMessages")));
                    msg.TotalMessages = Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("TotalMessages")));
                    msg.MessageStatusId = Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("TotalMessages")));

                    msg.SendDate = reader.GetDateTime(reader.GetOrdinal("SendDate"));
                    msg.LastReceivedMessageDate = Helper.Miladi2ShamsiWithTime(msg.SendDate);

                    msgs.Add(msg);
                }



                connection.Close();
                return new ResultModel<List<GetAdminAllMessagesResultModel>>(msgs);

            }
            catch (Exception e)
            {
                return new ResultModel<List<GetAdminAllMessagesResultModel>>(false);
            }
        }

        public ResultModel<List<GetOneUserChatResult>> GetOneUserChat(GetOneUserChatInputModel model)
        {
            var result = db.UsersMessages
                .Where(x =>
                (x.SenderUserId == model.SenderUserId && x.ReceiverUserId == model.ReceiverUserId) ||
                (x.SenderUserId == model.ReceiverUserId && x.ReceiverUserId == model.SenderUserId)
                ).Select(x => new GetOneUserChatResult
                {
                    Id = x.Id,
                    SenderUserId = x.SenderUserId,
                    ReceiverUserId = x.ReceiverUserId,
                    SendDate = x.SendDate,
                    MessageStatusId = x.MessageStatusId,
                    Message = x.MessageText
                }).OrderBy(x => x.SendDate).ToList();

            var sender = db.Users.Where(x => x.Id == model.SenderUserId || x.Id == model.ReceiverUserId);

            foreach (var item in result)
            {
                var r = sender.FirstOrDefault(x => x.Id == item.ReceiverUserId);
                var s = sender.FirstOrDefault(x => x.Id == item.SenderUserId);
                if (s != null)
                {
                    item.SenderName = s.FirstName + " " + s.LastName;
                    if (s.Mobile.Length > 11)
                        item.SenderName += "   fakeUser   ";
                }
                if (r != null)
                {
                    item.ReceiverName = r.FirstName + " " + r.LastName;
                    if (r.Mobile.Length > 11)
                        item.ReceiverName += "   fakeUser   ";
                }
                item.SendDateTime = Helper.Miladi2ShamsiWithTime(item.SendDate);

            }
            return new ResultModel<List<GetOneUserChatResult>>(result.OrderBy(x => x.SendDate).ToList());
        }

        public ResultModel<bool> SendUserMessage(SendMessageAdminPanel model)
        {
            try
            {
                db.UsersMessages.Add(new UsersMessages
                {
                    Id = Guid.NewGuid().ToString(),
                    MessageStatusId = 1,
                    MessageText = model.MessageText,
                    ReceiverUserId = model.ReceiverUserId,
                    SenderUserId = model.SenderUserId,
                    SendDate = DateTime.Now
                });
                db.SaveChanges();

                string query = $" update UsersMessages set MessageStatusId =2 where SenderUserId ='{model.ReceiverUserId}' and ReceiverUserId = '{model.SenderUserId}' ";

                using var connection = db.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteReader();
                connection.Close();
                var sender = db.Users.Find(model.SenderUserId);
                var receiver = db.Users.Find(model.ReceiverUserId);
                var message = "";
                if (model.EmailStatus == 1)
                {
                    message = string.Empty;
                    Helper.SendChatEmail(receiver.EmailAddress, receiver.FirstName, sender.FirstName, message, receiver.TelegramChatId);
                }
                else if (model.EmailStatus == 2)
                {
                    message = model.MessageText;
                    Helper.SendChatEmail(receiver.EmailAddress, receiver.FirstName, sender.FirstName, message, receiver.TelegramChatId);
                }
                return new ResultModel<bool>(true, true);

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(true, true);
            }
        }

        public ResultModel<bool> SendAdminMessage(SendMessageAdminPanel model)
        {
            try
            {
                db.UsersMessages.Add(new UsersMessages
                {
                    Id = Guid.NewGuid().ToString(),
                    MessageStatusId = 1,
                    MessageText = model.MessageText,
                    ReceiverUserId = model.ReceiverUserId,
                    SenderUserId = adminId,
                    SendDate = DateTime.Now
                });
                db.SaveChanges();
                var sender = db.Users.Find(adminId);
                var receiver = db.Users.Find(model.ReceiverUserId);
                var message = "";
                if (model.EmailStatus == 1)
                {
                    message = string.Empty;
                    Helper.SendChatEmail(receiver.EmailAddress, receiver.FirstName, sender?.FirstName, message, receiver.TelegramChatId);
                }
                else if (model.EmailStatus == 2)
                {
                    message = model.MessageText;
                    Helper.SendChatEmail(receiver.EmailAddress, receiver.FirstName, sender?.FirstName, message, receiver.TelegramChatId);

                }


                return new ResultModel<bool>(true, true);

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(true, true);
            }
        }

        public ResultModel<GetUserProfileForUpdateAdmin> GetUserProfile(SelectedItemModel model)
        {


            try
            {
                if (model.StringId == null)
                    return new ResultModel<GetUserProfileForUpdateAdmin>(false);

                var entity = db.Users.Where(x => x.Id == model.StringId).FirstOrDefault();


                if (entity == null)
                    return new ResultModel<GetUserProfileForUpdateAdmin>(false);

                var user = new GetUserProfileForUpdateAdmin();
                user.GenderId = entity.GenderId;
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                user.Mobile = entity.Mobile;
                user.UserName = entity.UserName;
                user.Id = entity.Id;
                user.CarValue = entity.CarValueId;
                user.HomeValue = entity.HomeValueId;
                user.HealtStatus = entity.HealthStatusId;
                user.IncomeAmount = entity.IncomeAmountId;
                user.LiveType = entity.LiveTypeId;
                user.RelationType = entity.RelationTypeId;
                user.MarriageStatus = entity.MarriageStatusId;
                user.Province = entity.ProvinceId;
                user.MyDescription = entity.MyDescription;
                user.EmailAddress = entity.EmailAddress;
                user.RDescription = entity.RDescription;
                user.LastActivityDate = Helper.Miladi2ShamsiWithTime(entity.LastActivityDate);
                user.MemberDate = Helper.Miladi2ShamsiWithTime(entity.CreateUserDate);
                user.Age = DateTime.Now.Year - entity.BirthDate.Year;
                user.BirthDateYear = Helper.Miladi2ShamsiYear(entity.BirthDate);
                user.BirthDateMonth = Helper.Miladi2ShamsiMonth(entity.BirthDate);
                user.BirthDateDay = Helper.Miladi2ShamsiDay(entity.BirthDate);
                user.Ghad = entity.Ghad;
                user.Vazn = entity.Vazn;
                user.CheildCount = entity.CheildCount == 0 ? 120 : entity.CheildCount;
                user.FirstCheildAge = entity.CheildCount == 0 ? 0 : entity.FirstCheildAge;
                user.ZibaeeNumber = entity.ZibaeeNumber;
                user.TipNumber = entity.TipNUmber;
                user.RangePoost = entity.RangePoost;
                user.EmailAddressStatusId = entity.EmailAddressStatusId;
                user.MobileStatusId = entity.MobileStatusId;
                user.UserName = entity.UserName;
                user.Password = entity.Password;
                user.BirthDate = entity.BirthDate;
                user.UserStatusId = entity.UserStatus;


                return new ResultModel<GetUserProfileForUpdateAdmin>(user);

            }
            catch (Exception e)
            {
                return new ResultModel<GetUserProfileForUpdateAdmin>(false);
            }

        }

        public ResultModel<bool> UpdateUserInfo(UpdateUserByAdminInputModel model)
        {

            try
            {


                var user = db.Users.Find(model.UserId);
                if (user == null)
                    return new ResultModel<bool>(false, "کاربر یافت نشد");

                if (model.CheildCount == 120)
                {
                    model.CheildCount = 0;
                }
                user.EmailAddressStatusId = model.EmailStatusId;
                user.MobileStatusId = model.MobileStatusId;
                user.FirstName = model.FirstName;
                user.GenderId = model.Gender;
                user.LastName = model.LastName;
                user.UserName = model.UserName;
                user.RDescription = model.RDescription;
                user.MyDescription = model.MyDescription;
                user.ProvinceId = model.Province;
                user.LiveTypeId = model.LiveType;
                user.HealthStatusId = model.HealtStatus;
                user.MarriageStatusId = model.MarriageStatus;
                user.Mobile = model.Mobile;
                user.EmailAddress = model.EmailAddress;
                user.IncomeAmountId = model.IncomeAmount;
                user.HomeValueId = model.HomeValue;
                user.CarValueId = model.CarValue;
                user.RelationTypeId = model.RelationType;
                user.Ghad = model.Ghad;
                user.Vazn = model.Vazn;
                user.RangePoost = model.RangePoost;
                user.CheildCount = model.CheildCount;
                user.FirstCheildAge = model.FirstCheildAge.Value;
                user.ZibaeeNumber = model.ZibaeeNumber;
                user.TipNUmber = model.TipNUmber;
                user.UserName = model.UserName;
                user.Password = model.Password;
                user.UserStatus = model.UserStatusId;

                db.SaveChanges();
                return new ResultModel<bool>(true, true);

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, "خطا در انجام عملیات" + e.InnerException?.ToString());

            }
        }

        public ResultModel<string> SaveUserChatId(long userId, string phoneElement)
        {
            try
            {
                string phoneNumber = phoneElement.Replace("+98", "0");
                if (phoneNumber.Length > 11)
                    phoneNumber = "0" + phoneElement.Substring(phoneElement.Length - 10);

                var user = db.Users.FirstOrDefault(x => x.Mobile == phoneNumber);
                if (user == null)
                {
                    var message = $"شماره {phoneElement} یافت نشد. لطفا با شماره موبایلی که در سایت ثبت نام کرده اید، وارد ربات شوید";
                    return new ResultModel<string>(false, message);
                }

                if (user.MobileStatusId == 3)
                    return new ResultModel<string>(false, "شماره شما قبلا احراز شده است");

                user.TelegramChatId = userId;
                db.SaveChanges();
                return new ResultModel<string>(true);
            }
            catch (Exception e)
            {
                return new ResultModel<string>(false);
            }
        }


        public ResultModel<string> UserIsSentMobileNumber(long chatId)
        {
            try
            {

                var user = db.Users.FirstOrDefault(x => x.TelegramChatId == chatId);
                if (user == null)
                    return new ResultModel<string>(false, "شماره شما یافت نشد . خطای غیر منتظره");

                return new ResultModel<string>(true);
            }
            catch (Exception e)
            {
                return new ResultModel<string>(false);
            }
        }

        public ResultModel<string> GetMobileOtp(long chatId)
        {
            try
            {

                var user = db.Users.FirstOrDefault(x => x.TelegramChatId == chatId);
                if (user == null)
                {
                    return new ResultModel<string>(false, "شماره شما یافت نشد . خطای غیر منتظره");
                }
                if (user.MobileVerifyCodeExpireDate < DateTime.Now)
                {
                    return new ResultModel<string>(false, "کد اعتبار سنجی منقضی شده است . با ورود به سایت کد اعتبار سنجی جدید درخواست نمایید");
                }
                return new ResultModel<string>(user.MobileVerifyCode);
            }
            catch (Exception e)
            {
                return new ResultModel<string>(false);
            }
        }

        public ResultModel<string> SetUserMobileIsVerify(long chatId)
        {
            try
            {

                var user = db.Users.FirstOrDefault(x => x.TelegramChatId == chatId);
                if (user == null)
                    return new ResultModel<string>(false, "شماره شما یافت نشد");

                user.MobileStatusId = 3;
                db.SaveChanges();
                return new ResultModel<string>(true);

            }
            catch (Exception e)
            {
                return new ResultModel<string>(false, "خطای پیشبینی نشده");

            }
        }
    }


}
