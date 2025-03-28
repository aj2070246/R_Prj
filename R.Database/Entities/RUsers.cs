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
        public string Password { get; set; }
        public int UserStatus { get; set; } // -1 is deleted , 1 = isactive
        public DateTime LastActivityDate { get; set; }

        public string MyDescription { get; set; }
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
        public IncomeAmount? IncomeAmount { get; set; }
        public long? IncomeAmountId { get; set; }
        public CarValue? CarValue { get; set; }
        public long? CarValueId { get; set; }
        public HomeValue? HomeValue { get; set; }
        public long? HomeValueId { get; set; }
        public RelationType? RelationType { get; set; }
        public long? RelationTypeId { get; set; }

        public int Ghad { get; set; }
        public int Vazn { get; set; }
        public int RangePoost { get; set; }
        public int CheildCount { get; set; }
        public int FirstCheildAge { get; set; }
        public int ZibaeeNumber { get; set; }
        public int TipNUmber { get; set; }

        public ICollection<BlockedDataLog> BlockedDataLog { get; set; }   //اینها مرا بلاک کرده ند 
        public ICollection<FavoriteDataLog> FavoriteDataLog { get; set; } // من اینها را بلاک کرده م
        public ICollection<CheckMeActivityLogs> CheckMeLogDataLog { get; set; }   //اینها مرا بلاک کرده ند 

        public string Mobile { get; set; }
        public string? MobileVerifyCode { get; set; }
        public DateTime? MobileVerifyCodeExpireDate { get; set; }
        public int MobileStatusId { get; set; } // 1 = reg and not verift , 2 = send code , 3 = verified , 

        public long? TelegramChatId { get; set; }
        public string? TelegramUserName { get; set; }
        public int? PhoneVerifyWay { get; set; }//telegram - whatsapp


        public string? EmailAddress { get; set; } // 1 = reg and not verift , 2 = send code , 3 = verified , 
        public int EmailAddressStatusId { get; set; } // 1 = reg and not verift , 2 = send code , 3 = verified , 
        public string? EmailVerifyCode { get; set; }
        public DateTime? EmailVerifyCodeExpireDate { get; set; }

        public byte[]? ProfilePicture { get; set; } // ذخیره عکس به صورت باینری



    }
}
