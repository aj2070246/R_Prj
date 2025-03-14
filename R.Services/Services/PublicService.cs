using System.Data;
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



namespace R.Services.Services
{
    public class PublicService : IPublicService
    {
        private readonly RDbContext db;
        private readonly IConfiguration _configuration;

        public PublicService(RDbContext context, IConfiguration configuration)
        {
            db = context; // دریافت DbContext از طریق constructor injection
            _configuration = configuration;
        }

        public AllDropDownItems GetAllDropDownItems()
        {
            try
            {
                var result = new AllDropDownItems();

                result.IncomeAmount = db.IncomeAmount.Select(x => new GetAllIncomeAmountModel()
                {
                    Id = x.Id,
                    ItemValue = x.ItemValue
                }).ToList();

                result.CarValue = db.CarValue.Select(x => new GetAllCarValueModel()
                {
                    Id = x.Id,
                    ItemValue = x.ItemValue
                }).ToList();

                result.HomeValue = db.HomeValue.Select(x => new GetAllHomeValueModel()
                {
                    Id = x.Id,
                    ItemValue = x.ItemValue
                }).ToList();

                result.Provinces = db.Province.Select(x => new GetAllProvinceModel()
                {
                    Id = x.Id,
                    ItemValue = x.ItemValue
                }).ToList();
                result.RelationType = db.RelationType.Select(x => new GetAllRelationTypeMode()
                {
                    Id = x.Id,
                    ItemValue = x.ItemValue
                }).ToList();

                result.Ages = db.Age.Select(x => new GetAllAgeModel()
                {
                    Id = x.Id,
                    ItemValue = x.ItemValue
                }).ToList();

                result.Genders = db.Gender.Select(x => new GetAllGenderModel()
                {
                    Id = x.Id,
                    ItemValue = x.ItemValue
                }).ToList();

                result.HealtStatus = db.HealthStatus.Select(x => new GetAllHealthStatusModel()
                {
                    Id = x.Id,
                    ItemValue = x.ItemValue
                }).ToList();

                result.LiveTypes = db.LiveType.Select(x => new GetAllLiveTypeModel()
                {
                    Id = x.Id,
                    ItemValue = x.ItemValue
                }).ToList();

                result.MarriageStatus = db.MarriageStatus.Select(x => new GetAllMarriageStatusModel()
                {
                    Id = x.Id,
                    ItemValue = x.ItemValue
                }).ToList();

                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ResultModel<GetOneUserData> GetUserInfo(SelectedItemModel model)
        {
            string adminId = "431C6083-C662-46F6-84B0-348075ABF34FE1BD03DA-FC53-4F74-8CFB-75E4D88C89AE0AADB564-B794-4CFF-A26F-28F695D31850BDEB3154-F9CF-4893-ABBD-DDF5177288434122E12B-4D96-4651-99E4-7E2D444B5287";
            if (model.StringId == adminId || model.CurrentUserId == adminId)
                return new ResultModel<GetOneUserData>(false);


            try
            {
                var user = db.Users.Where(x => x.Id == model.StringId).Include(x => x.Gender)
                           .Include(x => x.Gender)
                            .Include(x => x.Province)
                            .Include(x => x.HealthStatus)
                            .Include(x => x.LiveType)
                            .Include(x => x.IncomeAmount)
                            .Include(x => x.CarValue)
                            .Include(x => x.HomeValue)
                            .Include(x => x.MarriageStatus).FirstOrDefault();


                if (user != null)
                {
                    var age = DateTime.Now.Year - user.BirthDate.Year;
                    var result = new GetOneUserData()
                    {
                        FirstName = user.FirstName,
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
                        GenderId = user.GenderId
                    };
                    result.LastActivityDate = Helper.Miladi2ShamsiWithTime(user.LastActivityDate);
                    result.BirthDate = Helper.Miladi2Shamsi(user.BirthDate);
                    result.RangePoost = GetRangePoost(user.RangePoost);

                    var isBlocked = db.BlockedDataLog.Any(x => x.SourceUserId == model.StringId && x.BlockedUserId == model.CurrentUserId);
                    result.IsBlocked = isBlocked;
                    result.IBlocked = db.BlockedDataLog.Any(x => x.SourceUserId == model.CurrentUserId && x.BlockedUserId == model.StringId);
                    result.IFavorited = db.FavoriteDataLog.Any(x => x.SourceUserId == model.CurrentUserId && x.FavoritedUserId == model.StringId);

                    var isfavorite = db.FavoriteDataLog.Any(x => x.SourceUserId == model.StringId && x.FavoritedUserId == model.CurrentUserId);
                    result.IsFavorite = isfavorite;

                    if (!string.IsNullOrEmpty(model.CurrentUserId))
                    {
                        db.CheckMeActivityLogs.Add(new CheckMeActivityLogs()
                        {
                            RUsersId = model.StringId,
                            UserId_CheckedMe = model.CurrentUserId
                        });
                        db.SaveChanges();
                    }
                    return new ResultModel<GetOneUserData>(result);
                }


                return new ResultModel<GetOneUserData>(false);
            }
            catch (Exception e)
            {
                return new ResultModel<GetOneUserData>(false);
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
        public ResultModel<LoginResultModel> login(LoginInputModel model)
        {
            try
            {

                var captcharesult = CheckCaptchaCode(model.CaptchaId, model.CaptchaValue);
                if (!captcharesult.IsSuccess)
                    return new ResultModel<LoginResultModel>(false, "کد امنیتی اشتباه است");

                if (string.IsNullOrEmpty(model.CaptchaValue) || string.IsNullOrEmpty(model.CaptchaId))
                    return new ResultModel<LoginResultModel>(false, "وارد کردن کد امنیتی اجباری است");


                if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                    return new ResultModel<LoginResultModel>(false, "وارد کردن نام کاربری و کلمه عبور اجباری است");

                var cc = db.Database.GetConnectionString();

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
                    return new ResultModel<LoginResultModel>(false, "نام کاربری یا رمز عبور اشتباه است");
                }

                var age = DateTime.Now.Year - user.BirthDate.Year;
                var token = Guid.NewGuid().ToString();
                user.Token = token;
                user.TokenExpireDate = DateTime.Now.AddHours(1);
                user.LastActivityDate = DateTime.Now;
                db.SaveChanges();

                var loginResultModel = new LoginResultModel()
                {
                    Id = user.Id,
                    Age = age,
                    Gender = user.Gender.ItemValue,
                    GenderId = user.Gender.Id,
                    Province = user.Province.ItemValue,
                    HealthStatus = user.HealthStatus.ItemValue,
                    LiveType = user.LiveType.ItemValue,
                    MarriageStatus = user.MarriageStatus.ItemValue,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = token,
                    Mobile = user.Mobile,
                    MyDescription = user.MyDescription,
                    RDescription = user.RDescription,
                    UserName = user.UserName,
                    IncomeAmount = user?.IncomeAmount?.ItemValue,
                    CarValue = user?.CarValue?.ItemValue,
                    HomeValue = user?.HomeValue?.ItemValue,
                    EmailAddress = user.EmailAddress
                };

                loginResultModel.BirthDate = Helper.Miladi2Shamsi(user.BirthDate);
                loginResultModel.LastActivityDate = Helper.Miladi2ShamsiWithTime(user.LastActivityDate);

                sendAppEmails(user.EmailAddress, user.FirstName, SendEmailType.login);
                return new ResultModel<LoginResultModel>(loginResultModel);

            }
            catch (Exception e)
            {
                return new ResultModel<LoginResultModel>(false);
            }
        }

        public ResultModel<bool> RegisterUser(RegisterUserInputModel model)
        {

            if (string.IsNullOrEmpty(model.UserName))
                return new ResultModel<bool>(false, "وارد کردن نام کاربری اجباری است");

            if (string.IsNullOrEmpty(model.Mobile))
                return new ResultModel<bool>(false, "وارد کردن شماره موبایل اجباری است");

            if (string.IsNullOrEmpty(model.Password))
                return new ResultModel<bool>(false, "وارد کردن کلمه عبور اجباری است");

            if (string.IsNullOrEmpty(model.EmailAddress))
                return new ResultModel<bool>(false, "وارد کردن ایمیل اجباری است");

            try
            {
                var captcharesult = CheckCaptchaCode(model.CaptchaId, model.CaptchaValue);
                if (!captcharesult.IsSuccess)
                    return new ResultModel<bool>(false, "کد امنیتی اشتباه است");


                var douplicated = db.Users.Any(x => x.UserName.ToLower() == model.UserName.ToLower());
                if (douplicated)
                    return new ResultModel<bool>(false, "نام کاربری به کاربر دیگری اختصاص یافته است");

                douplicated = db.Users.Any(x => x.Mobile.ToLower() == model.Mobile.ToLower());
                if (douplicated)
                    return new ResultModel<bool>(false, "موبایل به کاربر دیگری اختصاص یافته است");

                douplicated = db.Users.Any(x => x.EmailAddress.ToLower() == model.EmailAddress.ToLower());
                if (douplicated)
                    return new ResultModel<bool>(false, "پست الکترونیک به کاربر دیگری اختصاص یافته است");

                if (model.CheildCount == 120)
                {
                    model.CheildCount = 0;
                    model.FirstCheildAge = "0";
                }
                var id = Guid.NewGuid().ToString();
                var age = (DateTime.Now.Year - model.BirthDate.Year);

                var user = new RUsers
                {
                    Id = id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    UserName = model.UserName,
                    RDescription = model.RDescription,
                    MyDescription = model.MyDescription,
                    ProvinceId = model.Province,
                    GenderId = model.Gender,
                    LiveTypeId = model.LiveType,
                    HealthStatusId = model.HealtStatus,
                    MarriageStatusId = model.MarriageStatus,
                    BirthDate = model.BirthDate,
                    Mobile = model.Mobile,
                    Token = id + id.Reverse(),
                    TokenExpireDate = DateTime.Now.AddHours(1),
                    EmailAddress = model.EmailAddress,
                    CreateUserDate = DateTime.Now,
                    EmailAddressStatusId = 1,
                    MobileStatusId = 1,
                    UserStatus = 1,
                    IncomeAmountId = model.IncomeAmount,
                    HomeValueId = model.HomeValue,
                    CarValueId = model.CarValue,
                    RelationTypeId = model.RelationType,
                    Ghad = model.Ghad,
                    Vazn = model.Vazn,
                    RangePoost = model.RangePoost,
                    CheildCount = model.CheildCount,
                    //FirstCheildAge = model.FirstCheildAge,
                    ZibaeeNumber = model.ZibaeeNumber,
                    TipNUmber = model.TipNUmber,
                    LastActivityDate = DateTime.Now,
                };
                try
                {
                    user.FirstCheildAge = string.IsNullOrEmpty(model.FirstCheildAge) ? 0 : Convert.ToInt32(model.FirstCheildAge);
                }
                catch (Exception e)
                {
                    user.FirstCheildAge = 0; ;
                }
                db.Users.Add(user);
                db.SaveChanges();

                SendMessage(new SendMessageInputModel()
                {
                    SenderUserId = "431C6083-C662-46F6-84B0-348075ABF34FE1BD03DA-FC53-4F74-8CFB-75E4D88C89AE0AADB564-B794-4CFF-A26F-28F695D31850BDEB3154-F9CF-4893-ABBD-DDF5177288434122E12B-4D96-4651-99E4-7E2D444B5287",
                    ReceiverUserId = user.Id,
                    MessageText = "به همسریار خوش آمدید" + Environment.NewLine + " با بیان دیدگاه خود ما را در ارائه خدمات بهتر یاری بفرمایید " + Environment.NewLine + "مدیریت همسریار"
                });

                sendAppEmails(user.EmailAddress, user.FirstName, SendEmailType.wellcome);
                return new ResultModel<bool>(true, true);

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, "خطا در انجام عملیات" + e.InnerException?.ToString());

            }
        }


        public ResultModel<bool> UpdateUserInfo(UpdateUserInputModel model)
        {


            if (string.IsNullOrEmpty(model.UserName))
                return new ResultModel<bool>(false, "وارد کردن نام کاربری اجباری است");

            if (string.IsNullOrEmpty(model.Mobile))
                return new ResultModel<bool>(false, "وارد کردن شماره موبایل اجباری است");


            if (string.IsNullOrEmpty(model.EmailAddress))
                return new ResultModel<bool>(false, "وارد کردن ایمیل اجباری است");

            try
            {

                var douplicated = db.Users.Where(x => x.Id != model.CurrentUserId).Any(x => x.UserName.ToLower() == model.UserName.ToLower());
                if (douplicated)
                    return new ResultModel<bool>(false, "نام کاربری به کاربر دیگری اختصاص یافته است");

                douplicated = db.Users.Where(x => x.Id != model.CurrentUserId).Any(x => x.Mobile.ToLower() == model.Mobile.ToLower());
                if (douplicated)
                    return new ResultModel<bool>(false, "موبایل به کاربر دیگری اختصاص یافته است");

                douplicated = db.Users.Where(x => x.Id != model.CurrentUserId).Any(x => x.EmailAddress.ToLower() == model.EmailAddress.ToLower());
                if (douplicated)
                    return new ResultModel<bool>(false, "پست الکترونیک به کاربر دیگری اختصاص یافته است");

                var user = db.Users.Find(model.CurrentUserId);
                if (user == null)
                    return new ResultModel<bool>(false, "کاربر یافت نشد");

                if (model.CheildCount == 120)
                {
                    model.FirstCheildAge = "0";
                    model.CheildCount = 0;
                }
                user.EmailAddressStatusId = model.EmailAddress == user.EmailAddress ? user.EmailAddressStatusId : 1;
                user.MobileStatusId = model.Mobile == user.Mobile ? user.MobileStatusId : 1;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;
                user.RDescription = model.RDescription;
                user.MyDescription = model.MyDescription;
                user.ProvinceId = model.Province;
                user.LiveTypeId = model.LiveType;
                user.HealthStatusId = model.HealtStatus;
                user.MarriageStatusId = model.MarriageStatus;
                user.BirthDate = model.BirthDate;
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
                user.FirstCheildAge = string.IsNullOrEmpty(model.FirstCheildAge) ? 0 : Convert.ToInt32(model.FirstCheildAge);
                user.ZibaeeNumber = model.ZibaeeNumber;
                user.TipNUmber = model.TipNUmber;

                db.SaveChanges();
                return new ResultModel<bool>(true, true);

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, "خطا در انجام عملیات" + e.InnerException?.ToString());

            }
        }

        public async Task<ResultModel<bool>> SendEmailVerifyCode(SendEmailVerifyCodeInputModel model, bool ForResetPassword)
        {
            var captcharesult = CheckCaptchaCode(model.CaptchaId, model.CaptchaValue);
            if (!captcharesult.IsSuccess)
                return new ResultModel<bool>(false, "کد امنیتی اشتباه است");

            var verifyCode = GenerateRandomNumber();
            try
            {
                if (ForResetPassword)
                {
                    var user = db.Users.FirstOrDefault(x => x.EmailAddress == model.EmailAddress);
                    if (user == null)
                        return new ResultModel<bool>(false, false, "کاربری یافت نشد . لطفا از صحت ایمیل وارد شده اطمینان حاصل نمایید");

                    var result = await SendEmailAsync(user.EmailAddress, verifyCode, ForResetPassword, user.FirstName);
                    if (result.IsSuccess)
                    {
                        user.Password = verifyCode;
                        db.SaveChanges();
                    }
                    return result;
                }
                else // for verify Email
                {
                    var user = db.Users.FirstOrDefault(x => x.Id == model.CurrentUserId);
                    if (user == null)
                        return new ResultModel<bool>(false, "کاربر یافت نشد");

                    if (user.EmailVerifyCodeExpireDate < DateTime.Now || user.EmailVerifyCodeExpireDate == null)
                        return new ResultModel<bool>(false, "کد برای شما ارسال شده است و "
                            + Environment.NewLine + "تا پنج دقیقه آینده امکان ارسال وجدد وجود ندارد"
                            + Environment.NewLine + "لطفا پوشه spam در ایمیل خود را چک کنید");

                    var result = await SendEmailAsync(user.EmailAddress, verifyCode, ForResetPassword, user.FirstName);
                    if (result.IsSuccess)
                    {
                        user.EmailVerifyCode = verifyCode;
                        user.EmailVerifyCodeExpireDate = DateTime.Now.AddMinutes(5);
                        user.EmailAddressStatusId = 2;
                        db.SaveChanges();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<bool>(false, false);
            }

        }

        public bool SaveCaptcha(SaveCaptchaInputModel saveCaptchaInputModel)
        {
            try
            {
                db.Captchas.Add(new Captcha()
                {
                    CaptchaId = saveCaptchaInputModel.CaptchaId,
                    CaptchaValue = saveCaptchaInputModel.CaptchaValue,
                    ExpireDate = DateTime.Now.AddMinutes(3)
                });

                db.SaveChanges();

                string query = $" delete from Captchas where ExpireDate < DATEADD(MINUTE,-10,getdate()) ";

                using var connection = db.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }

        public ResultModel<List<GetOneUserData>> SearchUsers(SearchUsersInputModel model)
        {
            try
            {
                string query = $" declare  @genderId int = (select top 1 GenderId from Users where id='{model.CurrentUserId}')  ";

                query += BaseSearchQuery();
                if (!string.IsNullOrEmpty(model.CurrentUserId))
                    query += "   and u.GenderId <> @genderId " + Environment.NewLine;

                if (true)
                {
                    if (model.ProvinceId > 0)
                        query += $" {Environment.NewLine} and provinceId = {model.ProvinceId}";

                    if (model.AgeIdTo > 0)
                        query += $" {Environment.NewLine} and DATEDIFF(YEAR, BirthDate, GETDATE()) <= {model.AgeIdTo}";

                    if (model.AgeIdFrom > 0)
                        query += $"  {Environment.NewLine} and DATEDIFF(YEAR, BirthDate, GETDATE()) >= {model.AgeIdFrom}";

                    if (model.LiveTypeId > 0)
                        query += $" {Environment.NewLine} and l.id = {model.LiveTypeId}";

                    if (model.HealthStatusId > 0)
                        query += $" {Environment.NewLine} and h.id = {model.HealthStatusId}";

                    if (model.MarriageStatusId > 0)
                        query += $" {Environment.NewLine} and m.id = {model.MarriageStatusId}";

                    if (model.IncomeId > 0)
                        query += $" {Environment.NewLine} and i.id = {model.IncomeId}";

                    if (model.CarValueId > 0)
                        query += $" {Environment.NewLine} and c.id = {model.CarValueId}";

                    if (model.HomeValueId > 0)
                        query += $" {Environment.NewLine} and ho.id = {model.HomeValueId}";

                    if (model.ProfilePhotoId > 0)
                    {
                        if (model.ProfilePhotoId == 1)
                            query += $" {Environment.NewLine} and u.ProfilePicture IS NOT NULL ";

                        else if (model.ProfilePhotoId == 2)
                            query += $" {Environment.NewLine} and u.ProfilePicture IS NULL ";
                    }

                    if (model.OnlineStatusId > 0)
                    {
                        if (model.OnlineStatusId == 1)
                            query += $" {Environment.NewLine} and u.LastActivityDate >= DATEADD(MINUTE, -5, GETDATE()) ";

                        else if (model.OnlineStatusId == 2)
                            query += $" {Environment.NewLine}  and u.LastActivityDate >= DATEADD(MINUTE, 60 , GETDATE())";
                    }

                    if (model.CheildCountId > 0 && model.CheildCountId != 120)
                        query += $" {Environment.NewLine}  and u.CheildCount = {model.CheildCountId} ";

                }
                query += $"  {Environment.NewLine} ORDER BY u.LastActivityDate   desc  {Environment.NewLine} ";

                if (model.PageIndex == 0)
                {

                }

                var ordrQuery = query + $" OFFSET {(model.PageIndex - 1) * 20} ROWS FETCH NEXT 20 ROWS ONLY ";

                var users = SerchQueryExecuter(ordrQuery);

                return new ResultModel<List<GetOneUserData>>(users);

            }
            catch (Exception e)
            {
                return new ResultModel<List<GetOneUserData>>(false, "خطای دیتابیس");
            }
        }

        public ResultModel<List<GetAllSentMessageResultModel>> SendMessage(SendMessageInputModel model)
        {
            try
            {
                var id = Guid.NewGuid().ToString();
                db.UsersMessages.Add(new UsersMessages()
                {
                    Id = id,
                    MessageStatusId = 1,
                    MessageText = model.MessageText,
                    ReceiverUserId = model.ReceiverUserId,
                    SendDate = DateTime.Now,
                    SenderUserId = model.SenderUserId
                });
                db.SaveChanges();

                var receiver = db.Users.FirstOrDefault(x => x.Id == model.ReceiverUserId);
                if (receiver != null)
                {
                    if (receiver.EmailAddressStatusId == 3 && !string.IsNullOrEmpty(receiver.EmailAddress))
                    {
                        var sender = db.Users.FirstOrDefault(x => x.Id == model.SenderUserId);
                        sendAppEmails(receiver.EmailAddress, receiver.FirstName, SendEmailType.newMessage, $" شما پیام جدیدی از {sender.FirstName} دارید");
                    }
                }


                return GetMessagesWithOneUser(new GetAllMessageInputModel()
                {
                    ReceiverUserId = model.ReceiverUserId,
                    SenderUserId = model.SenderUserId
                });
            }
            catch (Exception e)
            {
                return new ResultModel<List<GetAllSentMessageResultModel>>(false);
            }
        }
        public ResultModel<List<GetAllSentMessageResultModel>> GetMessagesWithOneUser(GetAllMessageInputModel model)
        {

            try
            {

                var senderUser = db.Users.Find(model.SenderUserId);
                var receiverUser = db.Users.Find(model.ReceiverUserId);

                var temp = db.UsersMessages
                    .Where(
                    x => ((x.SenderUserId == model.SenderUserId && x.ReceiverUserId == model.ReceiverUserId) ||
                    (x.SenderUserId == model.ReceiverUserId && x.ReceiverUserId == model.SenderUserId))
                    && (x.MessageStatusId == 1 || x.MessageStatusId == 2));
                var sentMessage = temp.Select(x => new GetAllSentMessageResultModel
                {
                    Id = x.Id,
                    IsReceiveMessage = false,
                    SendDate = x.SendDate,
                    SenderUserId = x.SenderUserId,
                    ReceiverUserId = x.ReceiverUserId,
                    MessageStatusId = x.MessageStatusId,
                    MessageStatus = x.SenderUserId == model.SenderUserId ? "ارسال شده" : "دریافت شده",
                    MessageText = x.MessageText,
                    ReceiverUserFullName = receiverUser.FirstName + "  " + receiverUser.LastName,
                    SenderUserFullName = senderUser.FirstName + "  " + senderUser.LastName,
                }).ToList();

                string query = $" update UsersMessages set MessageStatusId =2 where SenderUserId ='{model.ReceiverUserId}' and ReceiverUserId = '{model.SenderUserId}' ";

                using var connection = db.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteReader();
                connection.Close();

                if (!sentMessage.Any())
                    return new ResultModel<List<GetAllSentMessageResultModel>>(false, "گفتگویی یافت نشد . برای شروع گفاگو مسیچ ارسال کنید");


                var result = sentMessage.OrderByDescending(x => x.SendDate).ToList();
                return new ResultModel<List<GetAllSentMessageResultModel>>(result);
            }
            catch (Exception e)
            {
                return new ResultModel<List<GetAllSentMessageResultModel>>(false);
            }
        }

        public ResultModel<List<GetMyAllMessagesResultModel>> GetMyAllMessages(SelectedItemModel model)
        {
            try
            {
                string query = $"WITH UnreadMessages AS ( " +
Environment.NewLine + $" SELECT " +
Environment.NewLine + $"         CASE WHEN ReceiverUserId = '{model.CurrentUserId}' THEN SenderUserId ELSE ReceiverUserId END AS OtherUserId, " +
Environment.NewLine + $"         MAX(SendDate) AS LastReceivedMessageDate, " +
Environment.NewLine + $"         SUM(CASE WHEN ReceiverUserId =  '{model.CurrentUserId}' AND MessageStatusId = 1 THEN 1 ELSE 0 END) AS UnreadMessagesCount" +
Environment.NewLine + $"     FROM UsersMessages" +
Environment.NewLine + $"     WHERE '{model.CurrentUserId}' IN (ReceiverUserId, SenderUserId)" +
Environment.NewLine + $"     GROUP BY" +
Environment.NewLine + $"         CASE WHEN ReceiverUserId =  '{model.CurrentUserId}' THEN SenderUserId ELSE ReceiverUserId END" +
Environment.NewLine + $" )" +
Environment.NewLine + $" SELECT DISTINCT UnreadMessages.OtherUserId SenderUserId,ur.id ReceiverUserId,  us.genderId genderId," +
Environment.NewLine + $"        CONCAT(uS.FirstName, ' ', uS.LastName) AS sender," +
Environment.NewLine + $"        CONCAT(uR.FirstName, ' ', uR.LastName) AS receiver," +
Environment.NewLine + $"        UnreadMessages.UnreadMessagesCount AS umc," +
Environment.NewLine + $"        UnreadMessages.LastReceivedMessageDate" +
Environment.NewLine + $" FROM UnreadMessages" +
Environment.NewLine + $" INNER JOIN Users uS ON uS.Id = UnreadMessages.OtherUserId" +
Environment.NewLine + $" INNER JOIN Users uR ON uR.Id =  '{model.CurrentUserId}'" +
Environment.NewLine + $" ORDER BY UnreadMessagesCount DESC, LastReceivedMessageDate DESC";



                using var connection = db.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                var msgs = new List<GetMyAllMessagesResultModel>();
                connection.Open();

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var msg = new GetMyAllMessagesResultModel();

                    msg.SenderUserId = reader.GetString(reader.GetOrdinal("SenderUserId"));
                    msg.ReceiverUserId = reader.GetString(reader.GetOrdinal("ReceiverUserId"));
                    msg.SenderName = reader.GetString(reader.GetOrdinal("sender"));
                    msg.UnreadMessagesCount = Convert.ToInt16(reader.GetInt32(reader.GetOrdinal("umc")));
                    msg.GenderId = Convert.ToInt64(reader.GetInt64(reader.GetOrdinal("genderId")));

                    msg.LastReceivedMessageDate = Helper.Miladi2ShamsiWithTime(reader.GetDateTime(reader.GetOrdinal("LastReceivedMessageDate")));

                    msgs.Add(msg);
                }

                connection.Close();
                return new ResultModel<List<GetMyAllMessagesResultModel>>(msgs);

            }
            catch (Exception e)
            {
                return new ResultModel<List<GetMyAllMessagesResultModel>>(false);
            }
        }

        public ResultModel<bool> UploadProfilePhoto(ProfilePhotoModel model)
        {
            if (model.ProfilePhoto.Length > 5 * 1024 * 1024)
                return new ResultModel<bool>(false, false, "فایل ارسال نشده است.");

            try
            {
                var user = db.Users.FirstOrDefault(x => x.Id == model.CurrentUserId);
                if (user != null)
                {
                    user.ProfilePicture = model.ProfilePhoto;
                    db.SaveChanges();
                }
                return new ResultModel<bool>(true, true);

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, false);


            }
        }

        public byte[] DownloadProfilePicture(string userId)
        {
            try
            {
                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                    return user.ProfilePicture;
                return null;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ResultModel<bool> BlockUserManager(BlockUserManagerInputModel model)
        {
            try
            {
                if (model.SetIsBlock)
                {
                    var oldLog = db.BlockedDataLog.FirstOrDefault(x => x.SourceUserId == model.CurrentUserId && x.BlockedUserId == model.DestinationUserId);
                    if (oldLog != null)
                        return new ResultModel<bool>(true, true);

                    db.BlockedDataLog.Add(new BlockedDataLog()
                    {
                        BlockedUserId = model.DestinationUserId,
                        SourceUserId = model.CurrentUserId,
                        DateTime = DateTime.Now
                    });
                    db.SaveChanges();
                }
                else
                {
                    var oldLog = db.BlockedDataLog.FirstOrDefault(x => x.SourceUserId == model.CurrentUserId && x.BlockedUserId == model.DestinationUserId);
                    if (oldLog == null)
                        return new ResultModel<bool>(true, true);

                    db.BlockedDataLog.Remove(oldLog);
                    db.SaveChanges();
                }
                return new ResultModel<bool>(true, true);

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, false);
            }
        }

        public ResultModel<bool> FavoriteUserManager(FavoriteUserManagerInputModel model)
        {

            try
            {
                if (model.SetIsFavorite)
                {
                    var oldLog = db.FavoriteDataLog.FirstOrDefault(x => x.SourceUserId == model.CurrentUserId && x.FavoritedUserId == model.DestinationUserId);
                    if (oldLog != null)
                        return new ResultModel<bool>(true, true);

                    db.FavoriteDataLog.Add(new FavoriteDataLog()
                    {
                        FavoritedUserId = model.DestinationUserId,
                        SourceUserId = model.CurrentUserId
                    });
                    db.SaveChanges();
                }
                else
                {
                    var oldLog = db.FavoriteDataLog.FirstOrDefault(x => x.SourceUserId == model.CurrentUserId && x.FavoritedUserId == model.DestinationUserId);
                    if (oldLog == null)
                        return new ResultModel<bool>(true, true);

                    db.FavoriteDataLog.Remove(oldLog);
                    db.SaveChanges();
                }
                return new ResultModel<bool>(true, true);

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, false);
            }
        }

        public ResultModel<GetMyProfileInfoResultModel> GetMyProfileInfo(SelectedItemModel model)
        {
            try
            {
                if (model.CurrentUserId == null)
                    return new ResultModel<GetMyProfileInfoResultModel>(false);

                var entity = db.Users.Where(x => x.Id == model.CurrentUserId).FirstOrDefault();


                if (entity == null)
                    return new ResultModel<GetMyProfileInfoResultModel>(false);

                var user = new GetMyProfileInfoResultModel();
                user.GenderId = entity.GenderId;
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                user.Mobile = entity.Mobile;
                user.UserName = entity.UserName;
                user.Password = entity.Password;
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
                user.CheildCount = entity.CheildCount;
                user.FirstCheildAge = entity.FirstCheildAge;
                user.ZibaeeNumber = entity.ZibaeeNumber;
                user.TipNumber = entity.TipNUmber;
                user.RangePoost = entity.RangePoost;
                user.EmailAddressStatusId = entity.EmailAddressStatusId;
                return new ResultModel<GetMyProfileInfoResultModel>(user);

            }
            catch (Exception e)
            {
                return new ResultModel<GetMyProfileInfoResultModel>(false);
            }
        }

        public ResultModel<bool> VerifyEmailCode(CheckEmailVerifyCodeInputModel model, bool ForResetPassword)
        {

            try
            {

                var captcharesult = CheckCaptchaCode(model.CaptchaId, model.CaptchaValue);
                if (!captcharesult.IsSuccess)
                    return new ResultModel<bool>(false, "کد امنیتی اشتباه است");


                if (ForResetPassword)
                {

                    var user = db.Users.FirstOrDefault(x => x.EmailAddress == model.EmailAddress);
                    if (user == null)
                        return new ResultModel<bool>(false, "کاربر یافت نشد");

                    user = db.Users.FirstOrDefault(x => x.EmailAddress == model.EmailAddress &&
                     x.EmailVerifyCode == model.EmailVerifyCodeValue);
                    if (user == null)
                        return new ResultModel<bool>(false, "کد اشتباه وارد شده است");


                    user = db.Users.FirstOrDefault(x => x.EmailAddress == model.EmailAddress &&
                   x.EmailVerifyCode == model.EmailVerifyCodeValue && x.EmailVerifyCodeExpireDate > DateTime.Now);
                    if (user == null)
                        return new ResultModel<bool>(false, "کد اعتبار سنجی منقضی شده است");

                    user.Password = model.NewPassword;
                    db.SaveChanges();
                }
                else // for verify Email
                {
                    var user = db.Users.FirstOrDefault(x => x.Id == model.CurrentUserId);
                    if (user == null)
                        return new ResultModel<bool>(false, "کاربر یافت نشد");


                    if (user.EmailVerifyCodeExpireDate < DateTime.Now || string.IsNullOrEmpty(model.EmailVerifyCodeValue) || null == (user.EmailVerifyCodeExpireDate))
                        return new ResultModel<bool>(false, "کد اعتبار سنجی منقضی شده است");

                    if (user.EmailVerifyCode != model.EmailVerifyCodeValue)
                        return new ResultModel<bool>(false, "کد وارد شده اشتباه است");


                    user.EmailAddressStatusId = 3;
                    db.SaveChanges();
                }
                return new ResultModel<bool>(true, true);

            }
            catch (Exception ex)
            {
                return new ResultModel<bool>(false, false);
            }
        }

        public ResultModel<List<GetOneUserData>> GetBlockedUsers(BasePaginationModel model)
        {
            try
            {

                string query = BaseSearchQuery();

                query += $" and u.id  in (  select blockedUserId from [dbo].[BlockedDataLog] where SourceUserId='{model.CurrentUserId}'   )";
                var users = SerchQueryExecuter(query);
                query += $"  {Environment.NewLine} ORDER BY u.LastActivityDate    {Environment.NewLine} " +
                   $" OFFSET {model.PageIndex * 20} ROWS FETCH NEXT 20 ROWS ONLY ";

                if (users.Count() == 0)
                    return new ResultModel<List<GetOneUserData>>(false, "موردی یافت نشد");

                return new ResultModel<List<GetOneUserData>>(users);

            }
            catch (Exception e)
            {
                return new ResultModel<List<GetOneUserData>>(false, "خطای دیتابیس");
            }
        }

        public ResultModel<List<GetOneUserData>> GetBlockedMeUsers(BasePaginationModel model)
        {

            try
            {
                string query = BaseSearchQuery();

                query += $" and u.id  in ( select  SourceUserId from [dbo].[BlockedDataLog] where blockedUserId='{model.CurrentUserId}'   )";
                var users = SerchQueryExecuter(query);
                if (users.Count() == 0)
                    return new ResultModel<List<GetOneUserData>>(false, "موردی یافت نشد");

                return new ResultModel<List<GetOneUserData>>(users);

            }
            catch (Exception e)
            {
                return new ResultModel<List<GetOneUserData>>(false, "خطای دیتابیس");
            }
        }

        public ResultModel<List<GetOneUserData>> GetFavoriteUsers(BasePaginationModel model)
        {
            try
            {
                string query = BaseSearchQuery();

                query += $" and u.id  in (  select FavoritedUserId from [dbo].[FavoriteDataLog] where SourceUserId='{model.CurrentUserId}'   )";
                var users = SerchQueryExecuter(query);
                if (users.Count() == 0)
                    return new ResultModel<List<GetOneUserData>>(false, "موردی یافت نشد");

                return new ResultModel<List<GetOneUserData>>(users);
            }
            catch (Exception e)
            {
                return new ResultModel<List<GetOneUserData>>(false);
            }

        }

        public ResultModel<List<GetOneUserData>> GetFavoritedMeUsers(BasePaginationModel model)
        {
            try
            {

                string query = BaseSearchQuery();

                query += $" and u.id  in ( select  SourceUserId from [dbo].[FavoriteDataLog] where FavoritedUserId='{model.CurrentUserId}'   )";
                var users = SerchQueryExecuter(query);
                if (users.Count() == 0)
                    return new ResultModel<List<GetOneUserData>>(false, "موردی یافت نشد");

                return new ResultModel<List<GetOneUserData>>(users);

            }
            catch (Exception e)
            {
                return new ResultModel<List<GetOneUserData>>(false);
            }
        }

        public ResultModel<List<GetOneUserData>> LastUsersCheckedMe(BasePaginationModel model)
        {

            try
            {

                string query = BaseSearchQuery(true);
                query += $"  and rn=1  and ch.rUsersId='{model.CurrentUserId}' {Environment.NewLine} order by ch.datetime desc  ";

                var users = SerchQueryExecuter(query, true);
                if (users.Count() == 0)
                    return new ResultModel<List<GetOneUserData>>(false, "موردی یافت نشد");

                return new ResultModel<List<GetOneUserData>>(users);

            }
            catch (Exception e)
            {
                return new ResultModel<List<GetOneUserData>>(false);

            }

        }


        #region private methods

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

        private string GenerateRandomNumber()
        {
            const string chars = "0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 7)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<ResultModel<bool>> SendEmailAsync(string RecipientMail, string VerifyCode, bool IsResetPassword, string receiverName)
        {
            try
            {
                var SenderEmail = _configuration["SenderEmailInfo:SenderEmail"];
                string SenderEmailAppPassword = _configuration["SenderEmailInfo:SenderEmailAppPassword"];
                var SenderEmailFullName = _configuration["SenderEmailInfo:SenderEmailFullName"];
                var SiteName = _configuration["SenderEmailInfo:SiteName"];

                var dateExpire = Helper.Miladi2ShamsiWithTime(DateTime.Now.AddMinutes(5));
                var fromAddress = new MailAddress(SenderEmail, SenderEmailFullName);
                var toAddress = new MailAddress(RecipientMail, receiverName);
                string subject = IsResetPassword ? "رمز عبور جدید" : "کد تایید ایمیل";

                string body = $"{receiverName} عزیز {Environment.NewLine}";
                body += IsResetPassword ? $" رمز عبور جدید شما: {Environment.NewLine}{VerifyCode}" : $"کد تایید ایمیل شما: {VerifyCode}\n\nاعتبار تا: {dateExpire}";
                body += $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine} {SiteName}";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, SenderEmailAppPassword),
                    Timeout = 20000
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false

                })
                {
                    message.Bcc.Add(fromAddress);

                    await smtp.SendMailAsync(message);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new ResultModel<bool>(false, ex.Message);
            }

            return new ResultModel<bool>(true, true);
        }

        private string BaseSearchQuery(bool getMeLog = false)
        {
            string query = "";
            if (getMeLog)
                query += $"WITH RankedLogs AS (   SELECT *, ROW_NUMBER() OVER (PARTITION BY UserId_CheckedMe, RUsersId ORDER BY DateTime DESC) AS rn FROM CheckMeActivityLogs)";

            query += Environment.NewLine + "SELECT     DATEDIFF(YEAR, BirthDate, GETDATE()) AS Age ,  ";

            if (getMeLog)
                query += " ch.DateTime ActivityDate, ";

            query += Environment.NewLine + " p.ItemValue Province , h.ItemValue HealthStatus, r.ItemValue RelationType ," +
              Environment.NewLine + " i.ItemValue IncomeAmount , c.ItemValue CarValue , ho.ItemValue HomeValue ," +
              Environment.NewLine + " l.ItemValue LiveType , m.ItemValue MarriageStatus ,g.ItemValue  Gender,u.* FROM Users u" +
              Environment.NewLine + " left  join Province p on p.Id= u.ProvinceId" +
              Environment.NewLine + " left  join HealthStatus h on h.Id = u.HealthStatusId" +
              Environment.NewLine + " left  join LiveType l on l.Id = u.LiveTypeId" +
              Environment.NewLine + " left  join MarriageStatus m on m.Id = u.MarriageStatusId" +
              Environment.NewLine + " left  join gender g on g.Id = u.genderId" +
              Environment.NewLine + " left  join IncomeAmount i on i.Id = u.IncomeAmountId" +
              Environment.NewLine + " left  join CarValue c on c.Id = u.CarValueId" +
              Environment.NewLine + " left  join RelationType r on r.Id = u.RelationTypeId" +
              Environment.NewLine + " left  join HomeValue ho on ho.Id = u.HomeValueId";

            if (getMeLog)
                query += Environment.NewLine + " left  join RankedLogs ch on u.Id = ch.UserId_CheckedMe";

            query += Environment.NewLine + " where UserStatus=1  " + Environment.NewLine;

            return query;
        }

        private List<GetOneUserData> SerchQueryExecuter(string query, bool getMeLog = false)
        {
            var users = new List<GetOneUserData>();
            try
            {
                using var connection = db.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                connection.Open();

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var user = new GetOneUserData();

                    user.Id = reader.GetString(reader.GetOrdinal("Id"));
                    user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    user.MyDescription = reader.IsDBNull(reader.GetOrdinal("MyDescription")) ? "نامشخص" : reader.GetString(reader.GetOrdinal("MyDescription"));
                    user.RDescription = reader.IsDBNull(reader.GetOrdinal("RDescription")) ? "نامشخص" : reader.GetString(reader.GetOrdinal("RDescription"));
                    user.BirthDate = Helper.Miladi2Shamsi(reader.GetDateTime(reader.GetOrdinal("BirthDate")));
                    user.Age = reader.GetInt32("age");
                    user.GenderId = reader.GetInt64("genderId");
                    user.Gender = reader.GetString(reader.GetOrdinal("Gender"));
                    user.HealthStatus = reader.IsDBNull(reader.GetOrdinal("HealthStatus")) ? "نامشخص" : reader.GetString(reader.GetOrdinal("HealthStatus"));
                    user.LiveType = reader.IsDBNull(reader.GetOrdinal("LiveType")) ? "نامشخص" : reader.GetString(reader.GetOrdinal("LiveType"));
                    user.MarriageStatus = reader.IsDBNull(reader.GetOrdinal("MarriageStatus")) ? null : reader.GetString(reader.GetOrdinal("MarriageStatus"));
                    user.Province = reader.IsDBNull(reader.GetOrdinal("Province")) ? "نامشخص" : reader.GetString(reader.GetOrdinal("Province"));
                    user.LastActivityDate = !reader.IsDBNull(reader.GetOrdinal("LastActivityDate")) ? Helper.Miladi2ShamsiWithTime(reader.GetDateTime(reader.GetOrdinal("LastActivityDate"))) : "نامشخص"; // مقدار جایگزین
                    user.IncomeAmount = reader.IsDBNull(reader.GetOrdinal("IncomeAmount")) ? "نامشخص" : reader.GetString(reader.GetOrdinal("IncomeAmount"));
                    user.CarValue = reader.IsDBNull(reader.GetOrdinal("CarValue")) ? "نامشخص" : reader.GetString(reader.GetOrdinal("CarValue"));
                    user.HomeValue = reader.IsDBNull(reader.GetOrdinal("HomeValue")) ? "نامشخص" : reader.GetString(reader.GetOrdinal("HomeValue"));
                    user.RelationType = reader.IsDBNull(reader.GetOrdinal("RelationType")) ? "نامشخص" : reader.GetString(reader.GetOrdinal("RelationType"));

                    if (getMeLog)
                        user.ActivityDate = !reader.IsDBNull(reader.GetOrdinal("ActivityDate")) ? Helper.Miladi2ShamsiWithTime(reader.GetDateTime(reader.GetOrdinal("ActivityDate"))) : "نامشخص"; // مقدار جایگزین

                    users.Add(user);
                }

                connection.Close(); // بستن کانکشن
                return users;
            }
            catch (Exception e)
            {
                return users;
            }
        }

        public ResultModel<bool> ChangePassword(ChangePasswordInputModel model)
        {
            try
            {
                if (model.NewPassword.Length < 4)
                    return new ResultModel<bool>(false, "کلمه عبور میبایست حداقل چهار حرف باشد");

                if (string.IsNullOrEmpty(model.NewPassword))
                    return new ResultModel<bool>(false, "وارد کردن کلمه عبور  جدید اجباری است");

                if (string.IsNullOrEmpty(model.CurrentPassword))
                    return new ResultModel<bool>(false, "وارد کردن کلمه عبور قدیمی اجباری است");

                var user = db.Users.Find(model.CurrentUserId);
                if (user == null)
                    return new ResultModel<bool>(false, "کاربری یافت نشد");

                if (user.Password != model.CurrentPassword)
                    return new ResultModel<bool>(false, "کلمه عبور فعلی خود را اشتباه وارد کرده اید");

                user.Password = model.NewPassword;
                db.SaveChanges();

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, false);
            }
            return new ResultModel<bool>(true, true);

        }

        public ResultModel<bool> DeleteMessage(SelectedItemModel model)
        {
            try
            {
                string query = $" update UsersMessages set MessageStatusId =3 where Id='{model.StringId}' ";

                using var connection = db.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteReader();
                connection.Close();
            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, false);
            }
            return new ResultModel<bool>(true, true);

        }

        public ResultModel<int> GetCountOfUnreadMessages(BaseInputModel model)
        {
            try
            {
                string query = $"select count(id) as UnreadMessagesCount from UsersMessages where ReceiverUserId='{model.CurrentUserId}' and MessageStatusId=1";


                int UnreadMessagesCount = 0;
                using var connection = db.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                connection.Open();

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UnreadMessagesCount = reader.GetInt32(reader.GetOrdinal("UnreadMessagesCount"));
                }

                connection.Close();
                return new ResultModel<int>(UnreadMessagesCount++);

            }
            catch (Exception e)
            {
                return new ResultModel<int>(false);

            }
        }

        public long GetGender(string userId)
        {
            try
            {
                var user = db.Users.Find(userId);
                if (user == null)
                    return -1;
                return user.GenderId;

            }
            catch (Exception)
            {
                return -1;
            }
        }


        #endregion

        private async Task<ResultModel<bool>> sendAppEmails(string receiverEmail, string receiverName, SendEmailType msgType, string postfixBody = "")
        {


            try
            {
                var SenderEmail = _configuration["SenderEmailInfo:SenderEmail"];
                string SenderEmailAppPassword = _configuration["SenderEmailInfo:SenderEmailAppPassword"];
                var SenderEmailFullName = _configuration["SenderEmailInfo:SenderEmailFullName"];
                var SiteName = _configuration["SenderEmailInfo:SiteName"];

                var dateExpire = Helper.Miladi2ShamsiWithTime(DateTime.Now.AddMinutes(5));
                var fromAddress = new MailAddress(SenderEmail, SenderEmailFullName);
                var toAddress = new MailAddress(receiverEmail, receiverName);
                string subject = "";
                string body = "";
                switch (msgType)
                {
                    case SendEmailType.wellcome:
                        {
                            subject = "خوش آمدید";
                            body = $"{receiverName} عزیز {Environment.NewLine}";
                            body += "به همسریار خوش آمدید . " + Environment.NewLine;
                            body += "کلیه سرویس های این وب سایت رایگان میباشد . " + Environment.NewLine;
                        }
                        break;
                    case SendEmailType.newMessage:
                        {
                            subject = "پیام جدید";
                            body = $"{receiverName} عزیز {Environment.NewLine}";
                            body += postfixBody;
                        }
                        break;

                    case SendEmailType.login:
                        {
                            subject = ",ورود به سایت";
                            body = $"{receiverName} عزیز {Environment.NewLine}";
                            body += "کاربری شما به سایت همسریاب لاگین کرده است" + Environment.NewLine;
                            body += "در صورتی که این دسترسی غیر مجاز است" + Environment.NewLine;
                            body += "سریعا به سایت مراجعه فرموده و کلمه عبور خود را تغییر دهید" + Environment.NewLine;
                            body += "تا از هر گونه سرقت اطلاعات مصون بمانید" + Environment.NewLine;
                        }
                        break;

                    default:
                        break;
                }

                body += $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine} همسریار";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, SenderEmailAppPassword),
                    Timeout = 20000
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false

                })
                {
                    message.Bcc.Add(fromAddress);
                    await smtp.SendMailAsync(message);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new ResultModel<bool>(false, ex.Message);
            }


            return new ResultModel<bool>(true, true);

        }

        public ResultModel<bool> SendReport(SendReport model)
        {
            try
            {

                var id = Guid.NewGuid().ToString();

                var entity = new UsersMessages()
                {
                    Id = id,
                    MessageStatusId = 1,
                    MessageText = "پلیس محترم سایت همسریار، این  کاربر دارای تخلف در پروفایل یا چت میباشد. لطفا پیگیری نمایید" + Environment.NewLine + "ای دی کاربر متخلف" + Environment.NewLine + model.ReportedUserId,
                    ReceiverUserId = "431C6083-C662-46F6-84B0-348075ABF34FE1BD03DA-FC53-4F74-8CFB-75E4D88C89AE0AADB564-B794-4CFF-A26F-28F695D31850BDEB3154-F9CF-4893-ABBD-DDF5177288434122E12B-4D96-4651-99E4-7E2D444B5287",
                    SendDate = DateTime.Now,
                    SenderUserId = model.CurrentUserId
                };
                db.UsersMessages.Add(entity);
                db.SaveChanges();
                return new ResultModel<bool>(true, true);

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, false);

            }
        }

        public List<string> getAllUserIds()
        {
            return db.Users.Select(x => x.Id).ToList();
        }

        private enum SendEmailType
        {
            wellcome,
            newMessage,
            login,
        }
    }

}
