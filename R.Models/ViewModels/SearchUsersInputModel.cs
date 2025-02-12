using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class SearchUsersInputModel
    {
        public long PageIndex { get; set; }
        public long PageItemsCount { get; set; }
        public long AgeIdFrom { get; set; }
        public long AgeIdTo { get; set; }
        public long GenderId { get; set; }
        public long HealthStatusId { get; set; }
        public long LiveTypeId { get; set; }
        public long MarriageStatusId { get; set; }
        public long ProvinceId { get; set; }
        public bool IsOnline { get; set; }
    }
}
