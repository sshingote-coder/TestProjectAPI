using TestProjectAPI.Models;

namespace TestProjectAPI.Store
{
    public interface IProductStore
    {
        public Task<IList<Product>> ReadAll(CancellationToken cancellationToken);
    }
}
