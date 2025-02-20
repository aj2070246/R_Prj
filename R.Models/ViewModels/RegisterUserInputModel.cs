using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class RegisterUserInputModel
    {
        public string CaptchaId { get; set; }
        public string CaptchaValue { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string MyDescription { get; set; }
        public string RDescription { get; set; }
        public DateTime BirthDate { get; set; }
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
    }
}
