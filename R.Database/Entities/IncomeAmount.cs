using System.ComponentModel.DataAnnotations;

namespace R.Database.Entities
{
    public class IncomeAmount
    {
        [Key]
        public long Id { get; set; }
        public string ItemValue { get; set; }
    }
}