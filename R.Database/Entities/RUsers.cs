using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Database.Entities
{
    public class RUsers
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password  { get; set; }
        public string Mobile  { get; set; }
        public string MyDescription  { get; set; }
        public string RDescription { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public DateTime BirthDate { get; set; }
        public Age Age { get; set; }
        public long AgeId { get; set; }
        public Gender Gender { get; set; }
        public long GenderId { get; set; }
        public HealthStatus HealthStatus { get; set; }
        public long HealthStatusId { get; set; }
        public LiveType LiveType { get; set; }
        public long LiveTypeId { get; set; }
        public MarriageStatus MarriageStatus { get; set; }
        public long MarriageStatusId { get; set; }
        public Province Province { get; set; }
        public long ProvinceId { get; set; }
    }
}
