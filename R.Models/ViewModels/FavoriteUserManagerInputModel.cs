using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Models.ViewModels
{
    public class FavoriteUserManagerInputModel
    {
        public string CurrentUserId { get; set; }
        public string DestinationUserId { get; set; }
        public bool SetIsFavorite { get; set; }
    }
}
