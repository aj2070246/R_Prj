using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class SearchUsersInputModel
    {
        public string UserId { get; set; }
        public long PageIndex { get; set; }
        public long AgeIdFrom { get; set; }
        public long AgeIdTo { get; set; }
        public long HealthStatusId { get; set; }
        public long LiveTypeId { get; set; }
        public long MarriageStatusId { get; set; }
        public long ProvinceId { get; set; }
        public long IncomeId { get; set; }
        public long CarValueId{ get; set; }
        public long HomeValueId { get; set; }
        public long ProfilePhotoId { get; set; }
        public long OnlineStatusId { get; set; }
    }
}
