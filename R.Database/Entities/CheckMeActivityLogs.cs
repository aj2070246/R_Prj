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
        }

        [Key] 
        public string Id { get; set; }

        public DateTime DateTime { get; set; }
        public string UserId_CheckedMe { get; set; }
    }
}
