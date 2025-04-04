﻿using R.Models.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class GetMyProfileInfoResultModel
    {
        public string Id { get; set; }
        public long GenderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MyDescription { get; set; }
        public string EmailAddress { get; set; }
        public string RDescription { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public long BirthDateYear { get; set; }
        public long BirthDateMonth { get; set; }
        public long BirthDateDay { get; set; }
        public int Age { get; set; }
        public long? HealtStatus { get; set; }
        public long? LiveType { get; set; }
        public long MarriageStatus { get; set; }
        public long Province { get; set; }
        public string LastActivityDate { get; set; }
        public string MemberDate { get; set; }
        public long? IncomeAmount { get; set; }
        public long? CarValue { get; set; }
        public long? HomeValue { get; set; }
        public long? RelationType { get; set; }

        public int Ghad { get; set; }
        public int Vazn { get; set; }
        public int RangePoost { get; set; }
        public int CheildCount { get; set; }
        public int FirstCheildAge { get; set; }
        public int ZibaeeNumber { get; set; }
        public int TipNumber { get; set; }
        public int EmailAddressStatusId { get; set; }

    }

    public class GetUserProfileForUpdateAdmin : GetMyProfileInfoResultModel
    {
        public int MobileStatusId { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public int UserStatusId { get; set; }
    }



    public class UpdateUserByAdminInputModel : BaseInputModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string MyDescription { get; set; }
        public string RDescription { get; set; } 
        public string BirthDateString { get; set; }
        public long Gender { get; set; }
        public long HealtStatus { get; set; }
        public long LiveType { get; set; }
        public long MarriageStatus { get; set; }
        public long Province { get; set; }
        public long IncomeAmount { get; set; }
        public long HomeValue { get; set; }
        public long CarValue { get; set; }

        public long RelationType { get; set; }

        public string? EmailAddress { get; set; }
        public int Ghad { get; set; }
        public int Vazn { get; set; }
        public int RangePoost { get; set; }
        public int CheildCount { get; set; }
        public int? FirstCheildAge { get; set; }
        public int ZibaeeNumber { get; set; }
        public int TipNUmber { get; set; }
        public int UserStatusId { get; set; }
        public int MobileStatusId { get; set; }
        public int EmailStatusId { get; set; }


    }
}
