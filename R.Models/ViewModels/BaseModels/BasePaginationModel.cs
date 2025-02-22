using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels.BaseModels
{
    public class BasePaginationModel :BaseInputModel
    {
        public long PageIndex { get; set; }

    }
}
