using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Database.Entities
{
    public class LiveType
    {
        [Key]
        public long Id { get; set; }
        public string ItemValue { get; set; }
    }
}
