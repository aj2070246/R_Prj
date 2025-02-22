using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Database.Entities
{
    public class FavoriteDataLog
    {
        public FavoriteDataLog()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public DateTime DateTime { get; set; }
        public string SourceUserId { get; set; }
        public string FavoritedUserId { get; set; }
    }
}
