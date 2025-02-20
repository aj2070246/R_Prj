using System.ComponentModel.DataAnnotations;

namespace R.Database.Entities
{
    public class CarValue
    {
        [Key]
        public long Id { get; set; }
        public string ItemValue { get; set; }
    }
}