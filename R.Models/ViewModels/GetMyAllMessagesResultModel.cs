using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.Models.ViewModels.BaseModels;

namespace R.Models.ViewModels
{
    public class GetMyAllMessagesResultModel
    {
        public string SenderUserId{ get; set; }
        public string ReceiverUserId{ get; set; }
        public string SenderName { get; set; }
        public string LastReceivedMessageDate { get; set; }
        public int UnreadMessagesCount { get; set; } 
        public long GenderId { get; set; }
    }

    public class GetAdminAllMessagesResultModel: GetMyAllMessagesResultModel
    {
        public string ReceiverName { get; set; }
        public string Id { get; set; }
        public int TotalMessages { get; set; }
        public int MessageStatusId { get; set; }
        public DateTime SendDate { get; set; }

    }

    public class GetOneUserChatInputModel
    {
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
    }
    public class GetOneUserChatResult
    {
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public string ReceiverName { get; set; }
        public string SenderName { get; set; }
        public string SendDateTime { get; set; }
        public DateTime SendDate { get; set; }
        public int MessageStatusId { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
    }

    public class SendMessageAdminPanel : BaseInputModel
    {
        public string MessageText { get; set; }
        public string? SenderUserId { get; set; }
        public string? ReceiverUserId { get; set; }
        
    }
}
