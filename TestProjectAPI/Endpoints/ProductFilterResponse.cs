using TestProjectAPI.Models;

namespace TestProjectAPI.Endpoints
{
    public class ProductFilterResponse
    {
        public ProductFilterResponse(IEnumerable<Product> products)
        {
            Products = products;
            Filter = new ProductFilter();
        }

        public ProductFilterResponse(IEnumerable<Product> products, (int min, int max) priceRange, string[] sizes, string[] keywords) : this(products)
        {
            Filter.MinPrice = priceRange.min;
            Filter.MaxPrice = priceRange.max;
            Filter.Sizes = sizes;
            Filter.Keywords = keywords;
        }

        public IEnumerable<Product> Products { get; set; }
        public ProductFilter Filter { get; set; }
    }
}
