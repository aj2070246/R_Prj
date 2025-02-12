using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class AllDropDownItems
    {
        public AllDropDownItems()
        {
            Ages = new List<GetAllAgeModel>();
            Genders = new List<GetAllGenderModel>();
            HealtStatus = new List<GetAllHealthStatusModel>();
            LiveTypes = new List<GetAllLiveTypeModel>();
            MarriageStatus = new List<GetAllMarriageStatusModel>();
            Provinces = new List<GetAllProvinceModel>();
        }
        public List<GetAllAgeModel> Ages { get; set; }
        public List<GetAllGenderModel> Genders { get; set; }
        public List<GetAllHealthStatusModel> HealtStatus { get; set; }
        public List<GetAllLiveTypeModel> LiveTypes { get; set; }
        public List<GetAllMarriageStatusModel> MarriageStatus { get; set; }
        public List<GetAllProvinceModel> Provinces { get; set; }
    }
}
