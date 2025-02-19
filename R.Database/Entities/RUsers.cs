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
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password  { get; set; }

        public int UserStatus { get; set; } // -1 is deleted , 1 = isactive


        public string Mobile  { get; set; }
        public string? MobileVerifyCode  { get; set; }
        public DateTime? MobileVerifyCodeExpireDate { get; set; }
        public int MobileStatusId  { get; set; } // 1 = reg and not verift , 2 = send code , 3 = verified , 
        
        
        public string? EmailAddress{ get; set; } // 1 = reg and not verift , 2 = send code , 3 = verified , 
        public int EmailAddressStatusId  { get; set; } // 1 = reg and not verift , 2 = send code , 3 = verified , 
        public string? EmailVerifyCode { get; set; }
        public DateTime? EmailVerifyCodeExpireDate { get; set; }

        public string MyDescription  { get; set; }
        public string RDescription { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public DateTime BirthDate { get; set; } 
        public DateTime CreateUserDate { get; set; }
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
