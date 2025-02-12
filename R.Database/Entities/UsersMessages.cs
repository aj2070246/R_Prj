using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace R.Database.Entities
{
    public class UsersMessages
    {
        [Key]
        public string Id { get; set; }
        public string SenderUserId { get; set; } 
        public string ReceiverUserId { get; set; } 
        public string MessageText { get; set; }
        public DateTime SendDate { get; set; }
        public int MessageStatusId { get; set; } 
    }
}
