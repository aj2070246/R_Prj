using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.Models.ViewModels.BaseModels;

namespace R.Models.ViewModels
{
    public class SearchUsersInputModel: BasePaginationModel
    {
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
