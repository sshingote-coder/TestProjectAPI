using TestProjectAPI.Endpoints;

namespace TestProjectAPI.Services
{
    public interface IProductService
    {
        public Task<ProductFilterResponse> FilterAsync(ProductFilterRequest request,
            CancellationToken cancellationToken);
    }
}
