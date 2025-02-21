using Microsoft.AspNetCore.Mvc;
using R.Models.ViewModels;
using R.Services.IServices;
using R.Models;
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

        [HttpPost("SearchUsers")]
        public ResultModel<List<GetOneUserData>> SearchUsers(SearchUsersInputModel model)
        {
            var result = _service.SearchUsers(model); ;
            return result;
        }

        [HttpPost("GetUserInfo")]
        public ResultModel<GetOneUserData> GetUserInfo(SelectedItemModel model)
        {
            return _service.GetUserInfo(model);
        }
        [HttpPost("DeleteMessage")]
        public ResultModel<bool> DeleteMessage(SelectedItemModel model)
        {
            return new ResultModel<bool>(true, true);
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
                UserId = model.userId
            });
            return Ok(new { message = "opload ok"});
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
