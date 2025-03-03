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
        public string LastName { get; set; }
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
        public int  Ghad { get; set; }
        public int Vazn { get; set; }
        public int CheildCount { get; set; }
        public int FirstCheildAge { get; set; }
        public int ZibaeeNumber { get; set; }
        public int TipNUmber { get; set; }
        public string RangePoost { get; set; }
    }
}
