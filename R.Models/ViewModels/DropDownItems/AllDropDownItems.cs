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
            Ghad = new List<GetAllGhad>();
            for (int i = 130; i < 200; i++)
            {
                Ghad.Add(new GetAllGhad() { Id = i, ItemValue = i.ToString() });
            }
            Vazn = new List<GetAllVazn>();
            for (int i = 40; i < 150; i++)
            {
                Vazn.Add(new GetAllVazn() { Id = i, ItemValue = i.ToString() });
            }
            CheildCount = new List<GetAllCheildCount>();
            for (int i = 0; i < 10; i++)
            {
                CheildCount.Add(new GetAllCheildCount() { Id = i, ItemValue = i.ToString() });
            }
            FirstCheildAge = new List<GetAllFirstCheildAge>();
            for (int i = 1; i < 40; i++)
            {
                FirstCheildAge.Add(new GetAllFirstCheildAge() { Id = i, ItemValue = i.ToString() });
            }
            RangePoost = new List<GetAllRangePoost>
            {
                new GetAllRangePoost()
                {
                    Id=1,
                    ItemValue = "سفید"
                } ,
                 new GetAllRangePoost()
                {
                    Id=1,
                    ItemValue = "برنز"
                },
                  new GetAllRangePoost()
                {
                    Id=1,
                    ItemValue = "سیاه"
                },
                   new GetAllRangePoost()
                {
                    Id=1,
                    ItemValue = "بور"
                }
            };
            ZibaeeNumber = new List<GetAllGhad>();
            for (int i = 1; i < 5; i++)
            {
                Ghad.Add(new GetAllGhad() { Id = i, ItemValue = i.ToString() });
            }
            TipNUmber = new List<GetAllGhad>();
            for (int i = 1; i < 5; i++)
            {
                Ghad.Add(new GetAllGhad() { Id = i, ItemValue = i.ToString() });
            }

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
        public List<GetAllGhad> Ghad { get; set; }
        public List<GetAllVazn> Vazn { get; set; }
        public List<GetAllRangePoost> RangePoost { get; set; }
        public List<GetAllCheildCount> CheildCount { get; set; }
        public List<GetAllFirstCheildAge> FirstCheildAge { get; set; }
        public List<GetAllGhad> ZibaeeNumber { get; set; }
        public List<GetAllGhad> TipNUmber { get; set; }

    }
}
