using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using R.Database;
using R.Models.ViewModels;
using R.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            return _service.SearchUsers(model);
        }

        [HttpPost("GetUserInfo")]
        public ResultModel<GetOneUserData> GetUserInfo(SelectedItemModel model)
        {
            return _service.GetUserInfo(model);
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

    }
}
