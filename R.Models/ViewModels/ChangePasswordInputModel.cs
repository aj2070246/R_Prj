using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.Models.ViewModels.BaseModels;

namespace R.Models.ViewModels
{
    public class ChangePasswordInputModel : BaseInputModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
