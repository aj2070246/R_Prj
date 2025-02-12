using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class GetAllMessageInputModel
    {
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
    }
}
