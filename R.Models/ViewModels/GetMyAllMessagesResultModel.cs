using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class GetMyAllMessagesResultModel
    {
        public string SenderUserId{ get; set; }
        public string ReceiverUserId{ get; set; }
        public string SenderName { get; set; }
        public string LastReceivedMessageDate { get; set; }
        public int UnreadMessagesCount { get; set; } 
    }
}
