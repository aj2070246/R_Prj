namespace R.Database.Entities
{
    public class AppConfigs
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        public string? KeyDescription { get; set; }

    }
}