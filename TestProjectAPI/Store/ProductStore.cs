using Newtonsoft.Json;
using System.Net;
using System.Xml;
using TestProjectAPI.Models;

namespace TestProjectAPI.Store
{
    public class ProductStore : IProductStore
    {
        //private const string Url = @"https://run.mocky.io/v3/cc147902-4a5a-4b1a-bc00-2220bafb49fd";
        private const string Url = @"https://pastebin.com/raw/JucRNpWs";
        private readonly ILogger<ProductStore> _logger;

        public ProductStore(ILogger<ProductStore> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IList<Product>> ReadAll(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Retrieving () products");
                using var httpClient = new HttpClient();
                var response = await httpClient.GetFromJsonAsync<ProductResponse>(Url, cancellationToken);

                // ReSharper disable once MethodHasAsyncOverload
                Console.WriteLine(JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented));

                return response?.products;
            }
            catch (HttpRequestException httpEx)
            {
                if (httpEx.StatusCode == HttpStatusCode.NoContent)
                {
                    _logger.LogWarning(" did not returned any products.");
                    return new List<Product>();
                }

                _logger.LogError(
                    $"An error occurred while retrieving the  product. StatusCode: {httpEx.StatusCode}, Message: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled error occurred while retrieving the  products. {ex.Message}");
                throw;
            }
        }
    }
}
