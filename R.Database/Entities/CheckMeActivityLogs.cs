using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Database.Entities
{
    public class CheckMeActivityLogs
    {
        public CheckMeActivityLogs()
        {
            Id = Guid.NewGuid().ToString(); 
            DateTime= DateTime.Now;
        }

        [Key] 
        public string Id { get; set; }

        public DateTime DateTime { get; set; }
        public string UserId_CheckedMe { get; set; }
        public string RUsersId { get; set; }
        public RUsers RUsers { get; set; }
    }
}
