using Microsoft.AspNetCore.Mvc;
using R.Models.ViewModels;
using R.Services.IServices;
using R.Models;
using R.Models.ViewModels.BaseModels;
using R.Database.Entities;
using SixLabors.ImageSharp;
namespace R.Api.Controllers
{
    //dotnet publish --configuration Release --runtime linux-x64 --self-contained=false -o ./publish
    //sudo systemctl restart nginx


    [ApiController]
    [Route("[controller]")]
    public class ConnectionController : ControllerBase
    {
        private readonly IPublicService _service;
        private readonly ILogger<WeatherForecastController> _logger;


        public ConnectionController(IPublicService service, ILogger<WeatherForecastController> logger)
        {
            _service = service;
            _logger = logger;

        }
        #region all Serach users

        [HttpPost("SearchUsers")]
        public ResultModel<List<GetOneUserData>> SearchUsers(SearchUsersInputModel model)
        {
            var result = _service.SearchUsers(model);
            return result;
        }

        [HttpPost("GetBlockedUsers")]
        public ResultModel<List<GetOneUserData>> GetBlockedUsers(BasePaginationModel model)
        {
            var result = _service.GetBlockedUsers(model);
            return result;
        }
        [HttpPost("GetBlockedMeUsers")]
        public ResultModel<List<GetOneUserData>> GetBlockedMeUsers(BasePaginationModel model)
        {
            var result = _service.GetBlockedMeUsers(model);
            return result;
        }

        [HttpPost("GetFavoriteUsers")]
        public ResultModel<List<GetOneUserData>> GetFavoriteUsers(BasePaginationModel model)
        {
            var result = _service.GetFavoriteUsers(model);
            return result;
        }
        [HttpPost("GetFavoritedMeUsers")]
        public ResultModel<List<GetOneUserData>> GetFavoritedMeUsers(BasePaginationModel model)
        {
            var result = _service.GetFavoritedMeUsers(model);
            return result;
        }
        [HttpPost("LastUsersCheckedMe")]
        public ResultModel<List<GetOneUserData>> LastUsersCheckedMe(BasePaginationModel model)
        {
            var result = _service.LastUsersCheckedMe(model);
            return result;
        }
        #endregion

        [HttpPost("UpdateEmailAddress")]
        public ResultModel<bool> UpdateEmailAddress(EmailUpdateInputModel model)
        {
            return _service.UpdateEmailAddress(model);
        }
        
        [HttpPost("GetUserInfo")]
        public ResultModel<GetOneUserData> GetUserInfo(SelectedItemModel model)
        {
            return _service.GetUserInfo(model);
        }

        [HttpPost("GetMyProfileInfo")]
        public ResultModel<GetMyProfileInfoResultModel> GetMyProfileInfo(SelectedItemModel model)
        {
            return _service.GetMyProfileInfo(model);
        }
        [HttpPost("DeleteMessage")]
        public ResultModel<bool> DeleteMessage(SelectedItemModel model)
        {
            return _service.DeleteMessage(model);
        }


        [HttpPost("GetCountOfUnreadMessages")]
        public ResultModel<UserHeaderData> GetCountOfUnreadMessages(BaseInputModel model)
        {
            return _service.GetCountOfUnreadMessages(model);
        }
        [HttpPost("GetMyAllMessages")]
        public ResultModel<List<GetMyAllMessagesResultModel>> GetMyAllMessages(SelectedItemModel model)
        {
            var result = _service.GetMyAllMessages(model);

            if (result.Model == null)
                return new ResultModel<List<GetMyAllMessagesResultModel>>(result.Model, true, "", 6969);


            if (!result.Model.Any())
                return new ResultModel<List<GetMyAllMessagesResultModel>>(result.Model, true, "", 6969);

            if (result.Model.Count() == 0)
                return new ResultModel<List<GetMyAllMessagesResultModel>>(result.Model, true, "", 6969);

            return result;
        }

        [HttpPost("GetMessagesWithOneUser")]
        public ResultModel<List<GetAllSentMessageResultModel>> GetMessagesWithOneUser(GetAllMessageInputModel model)
        {

            var result = _service.GetMessagesWithOneUser(model);

            if (result.Model == null)
                return new ResultModel<List<GetAllSentMessageResultModel>>(result.Model, true, "", 6969);


            if (!result.Model.Any())
                return new ResultModel<List<GetAllSentMessageResultModel>>(result.Model, true, "", 6969);

            if (result.Model.Count() == 0)
                return new ResultModel<List<GetAllSentMessageResultModel>>(result.Model, true, "", 6969);

            return result;

        }



        [HttpPost("SendReport")]
        public ResultModel<bool> SendMessage(SendReport model)
        {
            return _service.SendReport(model);
        }
        

        [HttpPost("SendMessage")]
        public ResultModel<List<GetAllSentMessageResultModel>> SendMessage(SendMessageInputModel model)
        {
            return _service.SendMessage(model);
        }

        [HttpPost("BlockUserManager")]
        public ResultModel<bool> BlockUserManager(BlockUserManagerInputModel model)
        {
            return _service.BlockUserManager(model);
        }

        [HttpPost("FavoriteUserManager")]
        public ResultModel<bool> FavoriteUserManager(FavoriteUserManagerInputModel model)
        {
            return _service.FavoriteUserManager(model);
        }

        [HttpPost("ChangePassword")]
        public ResultModel<bool> ChangePassword(ChangePasswordInputModel model)
        {
            return _service.ChangePassword(model);
        }
        [HttpPost("UpdateUserInfo")]
        public ResultModel<bool> UpdateUserInfo(UpdateUserInputModel model)
        {
            var result = _service.UpdateUserInfo(model);
            return result;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadProfilePicture(UploadFileInputModel model)
        {

            if (model.File == null)
                return Ok(new ResultModel<bool>(false, false, "فایل ارسال نشده است."));

            string fileName = model.File.FileName; // نام فایل همراه با پسوند
            string extension = Path.GetExtension(fileName); // پسوند فایل (مثل .jpg, .png)
            string contentType = model.File.ContentType; // نوع MIME (مثلاً image/jpeg)
            long fileSize = model.File.Length; // اندازه فایل به بایت

            if (fileSize == 0)
                return Ok(new ResultModel<bool>(false, false, "فایل ارسال نشده است."));


            if (fileSize > 5 * 1024 * 1024) // بیشتر از 5MB
                return Ok(new ResultModel<bool>(false, false, "حجم فایل حداکثر 5 مگابایت است"));

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowedExtensions.Contains(extension.ToLower()))
                return Ok(new ResultModel<bool>(false, false, "فایل نامعتیر.... پسوند های مجاز " + ".jpg | .jpeg | .png | .gif"));


            using var memoryStream = new MemoryStream();
            await model.File.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            var result = _service.UploadProfilePhoto(new ProfilePhotoModel
            {
                ProfilePhoto = fileBytes,
                CurrentUserId = model.CurrentUserId
            });

            return Ok(result);

        }


        [HttpGet("downloadProfilePhoto")]
        public async Task<IActionResult> DownloadProfilePicture(string userId)
        {
            long g = _service.GetGender(userId);
            var result = _service.DownloadProfilePicture(userId);
            if (result == null)
                return Ok(new { photoExists = false, gender = g }); // کلید برای فرانت‌اند

            return File(result, "image/jpeg"); // یا image/png
        }

    }
    public class UploadFileInputModel
    {
        public string? CurrentUserId { get; set; }

        public IFormFile File { get; set; }
    }

}