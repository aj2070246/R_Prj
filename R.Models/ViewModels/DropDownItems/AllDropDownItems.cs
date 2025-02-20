using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels.DropDownItems
{
    public class AllDropDownItems
    {
        public AllDropDownItems()
        {
            RelationType = new List<GetAllRelationTypeMode>();
            Ages = new List<GetAllAgeModel>();
            Genders = new List<GetAllGenderModel>();
            HealtStatus = new List<GetAllHealthStatusModel>();
            LiveTypes = new List<GetAllLiveTypeModel>();
            MarriageStatus = new List<GetAllMarriageStatusModel>();
            Provinces = new List<GetAllProvinceModel>();
            HomeValue = new List<GetAllHomeValueModel>();
            IncomeAmount = new List<GetAllIncomeAmountModel>();
            ProfilePhoto = new List<GetAllProfilePhotoModel>()
            {
                new GetAllProfilePhotoModel
                {
                    Id =1 ,
                    ItemValue = "داشته باشد"
                },
                new GetAllProfilePhotoModel
                {
                    Id =2 ,
                    ItemValue = "نداشته باشد"
                },
            };
            OnlineStatus = new List<GetAllOnlineStatusModel>(){
                new GetAllOnlineStatusModel
                {
                    Id =1 ,
                    ItemValue = "فقط کاربران آنلاین"
                },
                new GetAllOnlineStatusModel
                {
                    Id =2 ,
                    ItemValue = "کاربران آنلاین تا یک ساعت پیش"
                },
            };
        }
        public List<GetAllAgeModel> Ages { get; set; }
        public List<GetAllGenderModel> Genders { get; set; }
        public List<GetAllHealthStatusModel> HealtStatus { get; set; }
        public List<GetAllLiveTypeModel> LiveTypes { get; set; }
        public List<GetAllMarriageStatusModel> MarriageStatus { get; set; }
        public List<GetAllProvinceModel> Provinces { get; set; }
        public List<GetAllHomeValueModel> HomeValue { get; set; }
        public List<GetAllCarValueModel> CarValue { get; set; }
        public List<GetAllIncomeAmountModel> IncomeAmount { get; set; }
        public List<GetAllOnlineStatusModel> OnlineStatus { get; set; }
        public List<GetAllProfilePhotoModel> ProfilePhoto { get; set; }
        public List<GetAllRelationTypeMode> RelationType { get; set; }
    }
}
