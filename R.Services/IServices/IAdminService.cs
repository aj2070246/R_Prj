using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.Models;
using R.Models.AdminModels;
using R.Models.ViewModels;
using R.Models.ViewModels.BaseModels;
using R.Models.ViewModels.DropDownItems;

namespace R.Services.IServices
{
    public interface IAdminService
    {
        ResultModel<AdminLoginResultModel> AdminLogin(LoginInputModel model);
        ResultModel<List<GetLastUsersResultModel>> GetLastLogin(BasePaginationModel model);
        ResultModel<List<GetLastUsersResultModel>> GetLastUsers(BasePaginationModel model);
        ResultModel<List<GetAdminAllMessagesResultModel>> GetAdminAllMessages(SelectedItemModel model);
        ResultModel<GetOneUserDataForAdmin> GetUserInfo(SelectedItemModel model);
        ResultModel<List<GetAdminAllMessagesResultModel>> GetAllUsersMessages(SelectedItemModel model);
        ResultModel<List<GetOneUserChatResult>> GetOneUserChat(GetOneUserChatInputModel model);
        ResultModel<bool> SendUserMessage(SendMessageAdminPanel model);
        ResultModel<bool> SendAdminMessage(SendMessageAdminPanel model);
        ResultModel<GetUserProfileForUpdateAdmin> GetUserProfile(SelectedItemModel model);
        ResultModel<bool> UpdateUserInfo(UpdateUserByAdminInputModel model);
    }
}
