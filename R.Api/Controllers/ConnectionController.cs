using Microsoft.AspNetCore.Mvc;
using R.Models.ViewModels;
using R.Services.IServices;
using R.Models;
using R.Models.ViewModels.BaseModels;
namespace R.Api.Controllers
{
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
        public ResultModel<List<GetOneUserData>> GetBlockedUsers(BaseInputModel model)
        {
            var result = _service.GetBlockedUsers(model);
            return result;
        }
        [HttpPost("GetBlockedMeUsers")]
        public ResultModel<List<GetOneUserData>> GetBlockedMeUsers(BaseInputModel model)
        {
            var result = _service.GetBlockedMeUsers(model);
            return result;
        }

        [HttpPost("GetFavoriteUsers")]
        public ResultModel<List<GetOneUserData>> GetFavoriteUsers(BaseInputModel model)
        {
            var result = _service.GetFavoriteUsers(model);
            return result;
        }
        [HttpPost("GetFavoritedMeUsers")]
        public ResultModel<List<GetOneUserData>> GetFavoritedMeUsers(BaseInputModel model)
        {
            var result = _service.GetFavoritedMeUsers(model);
            return result;
        }
        [HttpPost("LastUsersCheckedMe")]
        public ResultModel<List<GetOneUserData>> LastUsersCheckedMe(BaseInputModel model)
        {
            var result = _service.LastUsersCheckedMe(model);
            return result;
        }
        #endregion

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
        public ResultModel<int> GetCountOfUnreadMessages(BaseInputModel model)
        {
            return _service.GetCountOfUnreadMessages(model);
        }

        [HttpPost("GetMyAllMessages")]
        public ResultModel<List<GetMyAllMessagesResultModel>> GetMyAllMessages(SelectedItemModel model)
        {
            return _service.GetMyAllMessages(model);
        }

        [HttpPost("GetMessagesWithOneUser")]
        public ResultModel<List<GetAllSentMessageResultModel>> GetMessagesWithOneUser(GetAllMessageInputModel model)
        {
            return _service.GetMessagesWithOneUser(model);
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


        [HttpPost("upload")]
        public async Task<IActionResult> UploadProfilePicture(UploadFileInputModel model)
        {


            if (model.file == null || model.file.Length == 0)
                return BadRequest("No file uploaded.");

            using var memoryStream = new MemoryStream();
            await model.file.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            _service.UploadProfilePhoto(new ProfilePhotoModel
            {
                ProfilePhoto = fileBytes,
                CurrentUserId = model.userId
            });
            return Ok(new { message = "opload ok" });
        }


        [HttpGet("downloadProfilePhoto")]
        public async Task<IActionResult> DownloadProfilePicture(string userId)
        {
            var result = _service.DownloadProfilePicture(userId);
            return File(result, "image/jpeg"); // یا image/png
        }

    }
    public class UploadFileInputModel
    {
        public IFormFile file { get; set; }
        public string userId { get; set; }

    }

}
