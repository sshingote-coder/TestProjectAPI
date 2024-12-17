namespace TestProjectAPI.Models
{
    public class Product
    {
        public string? title { get; set; }
        public int price { get; set; }
        public string[] sizes { get; set; } = { };
        public string? description { get; set; }
    }
}
