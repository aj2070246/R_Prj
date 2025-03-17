using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.AdminModels
{
    public  class GetLastUsersResultModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MyDescription { get; set; }
        public string RDescription { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string HealthStatus { get; set; }
        public string LiveType { get; set; }
        public string MarriageStatus { get; set; }
        public string Province { get; set; }
        public string EmailAddress { get; set; }
        public string RelationType { get; set; }
        public string MobileNumber { get; set; }
        public string MobileStatus { get; set; }
        public string EmailStatus { get; set; }
        public string LastActivityDate { get; set; }
        public string BirthDate { get; set; }
        public string MemberDate { get; set; }
        public DateTime LastActivityDateTime { get; set; }
        public DateTime BirthDateTime { get; set; }
        public DateTime MemberDateTime { get; set; }

        public int MobileStatusId { get; set; }
        public int EmailStatusId { get; set; }

    }

    public class AdminLoginResultModel
    {
        public string Token { get; set; }
        public string Id { get; set; }

    }
}
