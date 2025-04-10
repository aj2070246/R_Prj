﻿using R.Models.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace R.Models.ViewModels

{
    public class GetOneUserData
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MyDescription { get; set; }
        public string RDescription { get; set; }
        public string BirthDate { get; set; }
        public int Age { get; set; }
        public long GenderId { get; set; }
        public string Gender { get; set; }
        public string HealthStatus { get; set; }
        public string LiveType { get; set; }
        public string MarriageStatus { get; set; }
        public string Province { get; set; }
        public string LastActivityDate { get; set; }
        public string ActivityDate { get; set; }
        public string IncomeAmount { get; set; }
        public string CarValue { get; set; }
        public string HomeValue { get; set; }
        public string RelationType { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsFavorite { get; set; }
        public int Ghad { get; set; }
        public int Vazn { get; set; }
        public int CheildCount { get; set; }
        public int FirstCheildAge { get; set; }
        public int ZibaeeNumber { get; set; }
        public int TipNUmber { get; set; }
        public string RangePoost { get; set; }
        public bool IBlocked { get; set; }
        public bool IFavorited { get; set; }
    }
    public class GetOneUserDataForAdmin : GetOneUserData
    {
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string MobileStatus { get; set; }
        public string EmailStatus { get; set; }
        public string MemberDate { get; set; }
        public DateTime LastActivityDateTime { get; set; }
        public DateTime BirthDateTime { get; set; }
        public DateTime MemberDateTime { get; set; }
        public int MobileStatusId { get; set; }
        public int EmailStatusId { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class GetSiteDataResult
    {
        public long UsersCount { get; set; }
        public List<string> lastCreateUsers { get; set; }
        public List<string> lastloginUsers { get; set; }

    }
    public class UserHeaderData
    {
        public int UnreadMessagesCount { get; set; }
        public int DaysToExpire { get; set; }
        public int UnreadMessagesUsersCount { get; set; }
        public string MobileNumber { get; set; }
        public bool MobileIsVerified { get; set; }
        public string VerifyMobileInboxNumber { get; set; }
        public bool EmailIsVerified { get; set; }
        public string? EmailAddress { get; set; }
    }

    public class EmailUpdateInputModel : BaseInputModel
    {
        public string EmailAddress { get; set; }
    }
    public class MobileNumberUpdateInputModel : BaseInputModel
    {
        public string MobileNumber { get; set; }
    }
    public class CheckMobileVerifyCodeInputModel : BaseInputModel
    {
        public string CaptchaValue { get; set; }
        public string CaptchaId { get; set; }
    }

}
