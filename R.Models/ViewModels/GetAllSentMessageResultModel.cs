using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class GetAllSentMessageResultModel
    {
        public string Id { get; set; }
        public string SenderUserId { get; set; }
        public string SenderUserFullName { get; set; }
        public string ReceiverUserId { get; set; }
        public string ReceiverUserFullName { get; set; }
        public string MessageText { get; set; }
        public DateTime SendDate { get; set; }
        public int MessageStatusId { get; set; }
        public string MessageStatus { get; set; }
        public bool IsReceiveMessage { get; set; }
    }
}
