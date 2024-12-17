namespace TestProjectAPI.Endpoints
{
    public class ProductFilter
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string[] Sizes { get; set; } = default!;
        public string[] Keywords { get; set; } = new string[10];

    }
}
