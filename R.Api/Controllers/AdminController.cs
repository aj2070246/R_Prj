using Microsoft.AspNetCore.Mvc;
using R.Models.ViewModels;
using R.Services.IServices;
using R.Models;
using R.Models.ViewModels.BaseModels;
using R.Database.Entities;
using SixLabors.ImageSharp;
using R.Models.AdminModels;
namespace R.Api.Controllers
{
    //dotnet publish --configuration Release --runtime linux-x64 --self-contained=false -o ./publish
    //sudo systemctl restart nginx


    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IPublicService _service;
        private readonly IAdminService _admin;
        private readonly ILogger<WeatherForecastController> _logger;


        public AdminController(IPublicService service, IAdminService admin, ILogger<WeatherForecastController> logger)
        {
            _service = service;
            _logger = logger;
            _admin = admin;
        }
        [HttpPost("getLastUsers")]
        public ResultModel<List<GetLastUsersResultModel>> GetLastUsers(BasePaginationModel model)
        {
            return _admin.GetLastUsers(model);
        }

        [HttpPost("getLastLogin")]
        public ResultModel<List<GetLastUsersResultModel>> GetLastLogin(BasePaginationModel model)
        {
            return _admin.GetLastLogin(model);
        }

        [HttpPost("adminLogin")]
        public ResultModel<AdminLoginResultModel> AdminLogin(LoginInputModel model)
        {
            return _admin.AdminLogin(model);
        }

        [HttpPost("GetUserInfo")]
        public ResultModel<GetOneUserDataForAdmin> GetUserInfo(SelectedItemModel model)
        {
            return _admin.GetUserInfo(model);
        }

        [HttpPost("GetAdminAllMessages")]
        public ResultModel<List<GetAdminAllMessagesResultModel>> GetAdminAllMessages(SelectedItemModel model)
        {
            var result = _admin.GetAdminAllMessages(model);
            return result;
        }

        [HttpPost("GetAllUsersMessages")]
        public ResultModel<List<GetAdminAllMessagesResultModel>> GetAllUsersMessages(SelectedItemModel model)
        {
            var result = _admin.GetAllUsersMessages(model);
            return result;
        }

        [HttpPost("GetOneUserChat")]
        public ResultModel<List<GetOneUserChatResult>> GetOneUserChat(GetOneUserChatInputModel model)
        {
            var result = _admin.GetOneUserChat(model);
            return result;
        }
        [HttpPost("SendUserMessage")]
        public ResultModel<bool> SendUserMessage(SendMessageAdminPanel model)
        {
            var result = _admin.SendUserMessage(model);
            return result;
        }
        [HttpPost("SendAdminMessage")]
        public ResultModel<bool> SendAdminMessage(SendMessageAdminPanel model)
        {
            var result = _admin.SendAdminMessage(model);
            return result;
        }
    }
}