using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R.Database;
using R.Database.Entities;
using R.Models;
using R.Models.ViewModels;
using R.Services.IServices;

namespace R.Services.Services
{
    public class PublicService : IPublicService
    {
        private readonly RDbContext db;

        public PublicService(RDbContext context)
        {
            db = context; // دریافت DbContext از طریق constructor injection
        }

        public AllDropDownItems GetAllDropDownItems()
        {
            var result = new AllDropDownItems();

            result.Provinces = db.Province.Select(x => new GetAllProvinceModel()
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

        public ResultModel<LoginResultModel> login(LoginInputModel model)
        {
            try
            {
                if (model.CaptchaValue != "1" || model.CaptchaId != "1")
                {

                    var captchaResult = db.Captchas.FirstOrDefault(x => x.CaptchaId == model.CaptchaId && x.CaptchaValue == model.CaptchaValue);
                    if (captchaResult != null)
                    {
                        var date = DateTime.Now.AddMinutes(3);
                        if (date > captchaResult.ExpireDate)
                        {
                            db.Captchas.Remove(captchaResult);
                            db.SaveChanges();

                            return new ResultModel<LoginResultModel>(false, "کد وارد شده صحیح نیست");
                        }
                    }
                }
                var user = db.Users.Where(x => x.UserName == model.UserName && x.Password == model.Password)
                    .Include(x => x.Gender)
                    .Include(x => x.Province)
                    .Include(x => x.HealthStatus)
                    .Include(x => x.LiveType)
                    .Include(x => x.MarriageStatus).FirstOrDefault();
                if (user == null)
                {
                    return new ResultModel<LoginResultModel>(false, "نام کاربری یا رمز عبور اشتباه است");

                }
                var age = DateTime.Now.Year - user.BirthDate.Year;
                var loginResultModel = new LoginResultModel()
                {
                    Age = age,
                    BirthDate = user.BirthDate,
                    Gender = user.Gender.ItemValue,
                    Province = user.Province.ItemValue,
                    HealthStatus = user.HealthStatus.ItemValue,
                    LiveType = user.LiveType.ItemValue,
                    MarriageStatus = user.MarriageStatus.ItemValue,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = Guid.NewGuid().ToString(),
                    Mobile = user.Mobile,
                    MyDescription = user.MyDescription,
                    RDescription = user.RDescription,
                    UserName = user.UserName,
                };
                return new ResultModel<LoginResultModel>(loginResultModel);

            }
            catch (Exception e)
            {
                return new ResultModel<LoginResultModel>(false);
            }
        }

        public bool RegisterUser(RegisterUserInputModel model)
        {
            try
            {
                var age = (DateTime.Now.Year - model.BirthDate.Year);
                db.Users.Add(new RUsers
                {
                    AgeId = age,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    UserName = model.UserName,
                    RDescription = model.RDescription,
                    MyDescription = model.MyDescription,
                    ProvinceId = model.Province,
                    GenderId = model.Gender,
                    LiveTypeId = model.LiveType,
                    HealthStatusId = model.HealthStatus,
                    MarriageStatusId = model.MarriageStatus,

                });
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
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
                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }
    }
}
