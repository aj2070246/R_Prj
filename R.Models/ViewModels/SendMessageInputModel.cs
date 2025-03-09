using R.Models.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class SendMessageInputModel
    {
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public string MessageText { get; set; }
    }
    public class SendReport:BaseInputModel
    {
        public string ReportedUserId { get; set; }

    }
}
