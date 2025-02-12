using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.Models;
using R.Models.ViewModels;

namespace R.Services.IServices
{
    public interface IPublicService
    {
        public AllDropDownItems GetAllDropDownItems();
        ResultModel<LoginResultModel> login(LoginInputModel model);
        bool RegisterUser(RegisterUserInputModel model);
        bool SaveCaptcha(SaveCaptchaInputModel saveCaptchaInputModel);
    }
}
