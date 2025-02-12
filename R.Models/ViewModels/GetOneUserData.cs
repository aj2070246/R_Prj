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
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string HealthStatus { get; set; }
        public string LiveType { get; set; }
        public string MarriageStatus { get; set; }
        public string Province { get; set; } 
    }
}
