using System.ComponentModel.DataAnnotations;

namespace R.Database.Entities
{
    public class HomeValue
    {

        [Key]
        public long Id { get; set; }
        public string ItemValue { get; set; }
    }
}