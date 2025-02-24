using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.Models;
using R.Models.ViewModels;
using R.Models.ViewModels.BaseModels;
using R.Models.ViewModels.DropDownItems;

namespace R.Services.IServices
{
    public interface IPublicService
    {
        public AllDropDownItems GetAllDropDownItems();
        ResultModel<LoginResultModel> login(LoginInputModel model);
        byte[] UploadProfilePhoto(ProfilePhotoModel model);
        ResultModel<bool> RegisterUser(RegisterUserInputModel model);
        bool SaveCaptcha(SaveCaptchaInputModel saveCaptchaInputModel);

        ResultModel<List<GetOneUserData>> SearchUsers(SearchUsersInputModel model);
        ResultModel<List<GetOneUserData>> GetBlockedUsers(BaseInputModel model);
        ResultModel<List<GetOneUserData>> GetBlockedMeUsers(BaseInputModel model);
        ResultModel<List<GetOneUserData>> GetFavoriteUsers(BaseInputModel model);
        ResultModel<List<GetOneUserData>> GetFavoritedMeUsers(BaseInputModel model);
        ResultModel<List<GetOneUserData>> LastUsersCheckedMe(BaseInputModel model);

        ResultModel<GetOneUserData> GetUserInfo(SelectedItemModel model);
        ResultModel<List<GetAllSentMessageResultModel>> GetMessagesWithOneUser(GetAllMessageInputModel model);
        ResultModel<List<GetMyAllMessagesResultModel>> GetMyAllMessages(SelectedItemModel model);
        ResultModel<List<GetAllSentMessageResultModel>> SendMessage(SendMessageInputModel model);
        byte[] DownloadProfilePicture(string userId);
        ResultModel<bool> BlockUserManager(BlockUserManagerInputModel model);
        ResultModel<bool> FavoriteUserManager(FavoriteUserManagerInputModel model);
        ResultModel<GetMyProfileInfoResultModel> GetMyProfileInfo(SelectedItemModel model);
        ResultModel<bool> SendEmailVerifyCode(SendEmailVerifyCodeInputModel model, bool ForResetPassword);
        ResultModel<bool> VerifyEmailCode(CheckEmailVerifyCodeInputModel model, bool ForResetPassword);
        ResultModel<bool> ChangePassword(ChangePasswordInputModel model);
        ResultModel<bool> DeleteMessage(SelectedItemModel model);
    }
}
