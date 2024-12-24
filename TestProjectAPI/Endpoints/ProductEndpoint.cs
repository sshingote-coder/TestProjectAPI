
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestProjectAPI.Services;

namespace TestProjectAPI.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductEndpoint : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<IProductService> _logger;

        public ProductEndpoint(IProductService productService, ILogger<IProductService> logger)
        {
            Console.WriteLine(
                $"Initializing Product endpoint {(productService == null ? "without" : "with")} ProductService");
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("/filter")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<ProductFilterResponse>>> HandleAsync(
            [FromQuery] ProductFilterRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _productService.FilterAsync(request, cancellationToken);
                _logger?.LogInformation(
               $"Received Response product filter request : {JsonConvert.SerializeObject(response, Formatting.Indented)}");
                return Ok(response);
            }
            catch (HttpRequestException httpEx)
            {
                if (httpEx.StatusCode == HttpStatusCode.NoContent) return NoContent();

                return Problem(httpEx.Message, null, (int?)httpEx.StatusCode, " API error");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError, "API error");
            }
        }
    }
}
