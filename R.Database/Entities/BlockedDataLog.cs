using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Database.Entities
{
    public class BlockedDataLog
    {
       

        [Key]
        public string Id { get; set; } 
        public DateTime DateTime { get; set; }
        public string SourceUserId{ get; set; }
        public string BlockedUserId { get; set; }
        public BlockedDataLog()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
