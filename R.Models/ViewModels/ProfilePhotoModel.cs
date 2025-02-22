using R.Models.ViewModels.BaseModels;

namespace R.Models.ViewModels
{
    public class ProfilePhotoModel:BaseInputModel
    {
        public byte[] ProfilePhoto { get; set; }
    }
}