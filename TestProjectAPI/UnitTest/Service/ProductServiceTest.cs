using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using TestProjectAPI.Endpoints;
using TestProjectAPI.Models;
using TestProjectAPI.Services;
using TestProjectAPI.Store;
using Xunit;

namespace TestProjectAPI.UnitTest.Service
{
    public class ProductServiceTest
    {
        private const int MaxPrice = 25;
        private const int MinPrice = 10;
        private readonly IList<Product> _Products = new List<Product>
        {
            new()
            {
                title = "A Red Trouser",
                price = 10,
                sizes = new[]
                {
                    "small",
                    "medium",
                    "large"
                },
                description = "This trouser perfectly pairs with a green shirt."
            },
            new()
            {
                title = "A Green Trouser",
                price = 11,
                sizes = new[]
                {
                    "small"
                },
                description = "This trouser perfectly pairs with a blue shirt."
            },
            new()
            {
                title = "A Blue Trouser",
                price = 12,
                sizes = new[]
                {
                    "medium"
                },
                description = "This trouser perfectly pairs with a red shirt."
            },
            new()
            {
                title = "A Red Trouser",
                price = 13,
                sizes = new[]
                {
                    "large"
                },
                description = "This trouser perfectly pairs with a green shirt."
            },
            new()
            {
                title = "A Green Trouser",
                price = 14,
                sizes = new[]
                {
                    "small",
                    "medium"
                },
                description = "This trouser perfectly pairs with a blue shirt."
            },
            new()
            {
                title = "A Blue Trouser",
                price = 15,
                sizes = new[]
                {
                    "small",
                    "large"
                },
                description = "This trouser perfectly pairs with a red shirt."
            },
            new()
            {
                title = "A Red Trouser",
                price = 16,
                sizes = new[]
                {
                    "medium",
                    "large"
                },
                description = "This trouser perfectly pairs with a green shirt."
            },
            new()
            {
                title = "A Green Trouser",
                price = 17,
                description = "This trouser perfectly pairs with a blue shirt."
            },
            new()
            {
                title = "A Blue Trouser",
                price = 18,
                sizes = new[]
                {
                    "small",
                    "medium",
                    "large"
                },
                description = "This trouser perfectly pairs with a red belt."
            },
            new()
            {
                title = "A Red Trouser",
                price = 19,
                sizes = new[]
                {
                    "small"
                },
                description = "This trouser perfectly pairs with a green belt."
            },
            new()
            {
                title = "A Green Trouser",
                price = 20,
                sizes = new[]
                {
                    "medium"
                },
                description = "This trouser perfectly pairs with a blue belt."
            },
            new()
            {
                title = "A Blue Trouser",
                price = 21,
                sizes = new[]
                {
                    "large"
                },
                description = "This trouser perfectly pairs with a red belt."
            },
            new()
            {
                title = "A Red Trouser",
                price = 22,
                sizes = new[]
                {
                    "small",
                    "medium"
                },
                description = "This trouser perfectly pairs with a green belt."
            },
            new()
            {
                title = "A Green Trouser",
                price = 23,
                sizes = new[]
                {
                    "small",
                    "large"
                },
                description = "This trouser perfectly pairs with a blue belt."
            },
            new()
            {
                title = "A Blue Trouser",
                price = 24,
                sizes = new[]
                {
                    "medium",
                    "large"
                },
                description = "This trouser perfectly pairs with a red belt."
            },
            new()
            {
                title = "A Red Trouser",
                price = 25,
                description = "This trouser perfectly pairs with a green belt."
            },
            new()
            {
                title = "A Green Shirt",
                price = 10,
                sizes = new[]
                {
                    "small",
                    "medium",
                    "large"
                },
                description = "This shirt perfectly pairs with a blue hat."
            },
            new()
            {
                title = "A Blue Shirt",
                price = 11,
                sizes = new[]
                {
                    "small"
                },
                description = "This shirt perfectly pairs with a red hat."
            },
            new()
            {
                title = "A Red Shirt",
                price = 12,
                sizes = new[]
                {
                    "medium"
                },
                description = "This shirt perfectly pairs with a green hat."
            },
            new()
            {
                title = "A Green Shirt",
                price = 13,
                sizes = new[]
                {
                    "large"
                },
                description = "This shirt perfectly pairs with a blue hat."
            },
            new()
            {
                title = "A Blue Shirt",
                price = 14,
                sizes = new[]
                {
                    "small",
                    "medium"
                },
                description = "This shirt perfectly pairs with a red hat."
            },
            new()
            {
                title = "A Red Shirt",
                price = 15,
                sizes = new[]
                {
                    "small",
                    "large"
                },
                description = "This shirt perfectly pairs with a green hat."
            },
            new()
            {
                title = "A Green Shirt",
                price = 16,
                sizes = new[]
                {
                    "medium",
                    "large"
                },
                description = "This shirt perfectly pairs with a blue hat."
            },
            new()
            {
                title = "A Blue Shirt",
                price = 17,
                description = "This shirt perfectly pairs with a red hat."
            },
            new()
            {
                title = "A Red Shirt",
                price = 18,
                sizes = new[]
                {
                    "small",
                    "medium",
                    "large"
                },
                description = "This shirt perfectly pairs with a green bag."
            },
            new()
            {
                title = "A Green Shirt",
                price = 19,
                sizes = new[]
                {
                    "small"
                },
                description = "This shirt perfectly pairs with a blue bag."
            },
            new()
            {
                title = "A Blue Shirt",
                price = 20,
                sizes = new[]
                {
                    "medium"
                },
                description = "This shirt perfectly pairs with a red bag."
            },
            new()
            {
                title = "A Red Shirt",
                price = 21,
                sizes = new[]
                {
                    "large"
                },
                description = "This shirt perfectly pairs with a green bag."
            },
            new()
            {
                title = "A Green Shirt",
                price = 22,
                sizes = new[]
                {
                    "small",
                    "medium"
                },
                description = "This shirt perfectly pairs with a blue bag."
            },
            new()
            {
                title = "A Blue Shirt",
                price = 23,
                sizes = new[]
                {
                    "small",
                    "large"
                },
                description = "This shirt perfectly pairs with a red bag."
            },
            new()
            {
                title = "A Red Shirt",
                price = 24,
                sizes = new[]
                {
                    "medium",
                    "large"
                },
                description = "This shirt perfectly pairs with a green bag."
            },
            new()
            {
                title = "A Green Shirt",
                price = 25,
                description = "This shirt perfectly pairs with a blue bag."
            },
            new()
            {
                title = "A Blue Hat",
                price = 10,
                sizes = new[]
                {
                    "small",
                    "medium",
                    "large"
                },
                description = "This hat perfectly pairs with a red shoe."
            },
            new()
            {
                title = "A Red Hat",
                price = 11,
                sizes = new[]
                {
                    "small"
                },
                description = "This hat perfectly pairs with a green shoe."
            },
            new()
            {
                title = "A Green Hat",
                price = 12,
                sizes = new[]
                {
                    "medium"
                },
                description = "This hat perfectly pairs with a blue shoe."
            },
            new()
            {
                title = "A Blue Hat",
                price = 13,
                sizes = new[]
                {
                    "large"
                },
                description = "This hat perfectly pairs with a red shoe."
            },
            new()
            {
                title = "A Red Hat",
                price = 14,
                sizes = new[]
                {
                    "small",
                    "medium"
                },
                description = "This hat perfectly pairs with a green shoe."
            },
            new()
            {
                title = "A Green Hat",
                price = 15,
                sizes = new[]
                {
                    "small",
                    "large"
                },
                description = "This hat perfectly pairs with a blue shoe."
            },
            new()
            {
                title = "A Blue Hat",
                price = 16,
                sizes = new[]
                {
                    "medium",
                    "large"
                },
                description = "This hat perfectly pairs with a red shoe."
            },
            new()
            {
                title = "A Red Hat",
                price = 17,
                description = "This hat perfectly pairs with a green shoe."
            },
            new()
            {
                title = "A Green Hat",
                price = 18,
                sizes = new[]
                {
                    "small",
                    "medium",
                    "large"
                },
                description = "This hat perfectly pairs with a blue tie."
            },
            new()
            {
                title = "A Blue Hat",
                price = 19,
                sizes = new[]
                {
                    "small"
                },
                description = "This hat perfectly pairs with a red tie."
            },
            new()
            {
                title = "A Red Hat",
                price = 20,
                sizes = new[]
                {
                    "medium"
                },
                description = "This hat perfectly pairs with a green tie."
            },
            new()
            {
                title = "A Green Hat",
                price = 21,
                sizes = new[]
                {
                    "large"
                },
                description = "This hat perfectly pairs with a blue tie."
            },
            new()
            {
                title = "A Blue Hat",
                price = 22,
                sizes = new[]
                {
                    "small",
                    "medium"
                },
                description = "This hat perfectly pairs with a red tie."
            },
            new()
            {
                title = "A Red Hat",
                price = 23,
                sizes = new[]
                {
                    "small",
                    "large"
                },
                description = "This hat perfectly pairs with a green tie."
            },
            new()
            {
                title = "A Green Hat",
                price = 24,
                sizes = new[]
                {
                    "medium",
                    "large"
                },
                description = "This hat perfectly pairs with a blue tie."
            },
            new()
            {
                title = "A Blue Hat",
                price = 25,
                description = "This hat perfectly pairs with a red tie."
            }
        };
        private readonly string[] _sizes = { "small", "medium", "large" };
        // The 5 top most used words in the description are :This, perfectly, pairs, with, a.
        // The 10 most used words in the description are: red, green, shoe, trouser, shirt, tie, hat, shirt, belt, bag
        private readonly string[] _keywords =
            {
                "bag",
                "belt",
                "blue",
                "green",
                "hat",
                "red",
                "shirt",
                "shoe",
                "tie",
                "trouser"
            };

        [Fact]
        public async Task ShouldReturnAllProductsWhenNoRequestFilter()
        {
            // Arrange
            var productStore = Substitute.For<IProductStore>();
            var memoryCache = Substitute.For<IMemoryCache>();
            var logger = Substitute.For<ILogger<IProductService>>();
            var ct = new CancellationToken();
            productStore.ReadAll(ct).Returns(_Products);
            var productService = new ProductService(productStore, memoryCache, logger);

            // Act
            var response = await productService.FilterAsync(new ProductFilterRequest(), ct);

            // Assert
            await productStore.Received().ReadAll(ct);
            response.Products.Should().BeEquivalentTo(_Products);
        }

        [NUnit.Framework.Theory]
        [InlineData(null, null, null)]
        [InlineData(10, "small", "red,green")]
        [InlineData(null, "medium", "blue")]
        [InlineData(20, null, "blue")]
        [InlineData(0, null, "blue")]
        [InlineData(10, "small", null)]
        [InlineData(10, "small", ",")]
        public async Task ShouldReturnAlProductsForRequestFilter(int? price, string size, string highlights)
        {
            // Arrange
            var productStore = Substitute.For<IProductStore>();
            var memoryCache = Substitute.For<IMemoryCache>();
            var logger = Substitute.For<ILogger<IProductService>>();
            var ct = new CancellationToken();
            productStore.ReadAll(ct).Returns(_Products);
            var productService = new ProductService(productStore, memoryCache, logger);

            // Act
            var response =
                await productService.FilterAsync(
                    new ProductFilterRequest() { size = size, hightlight = highlights, maxprice = price }, ct);

            // Assert
            await productStore.Received().ReadAll(ct);
            response.Products.All(p =>
            {
                if (price > 0) p.price.Should().BeLessOrEqualTo((int)price);
                if (!string.IsNullOrWhiteSpace(size)) p.sizes.Should().Contain(size);
                if (!string.IsNullOrWhiteSpace(highlights))
                {
                    foreach (var keyword in highlights.Split(ProductService.CommonSeparators, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (p.description.Contains(keyword))
                        {
                            p.description.Should().Contain($"<em>{keyword}</em>");
                        }
                    }
                }
                return true;
            });
        }

        [NUnit.Framework.Theory]
        [InlineData(null, null, null)]
        [InlineData(10, "small", "red,green")]
        [InlineData(null, "medium", "blue")]
        [InlineData(20, null, "blue")]
        [InlineData(10, "small", null)]
        [InlineData(10, "small", "")]
        public async Task ShouldReturnFilterForAllRequest(int? price, string size, string highlights)
        {
            // Arrange
            var productStore = Substitute.For<IProductStore>();
            var memoryCache = Substitute.For<IMemoryCache>();
            var logger = Substitute.For<ILogger<IProductService>>();
            var ct = new CancellationToken();
            productStore.ReadAll(ct).Returns(_Products);
            var productService = new ProductService(productStore, memoryCache, logger);

            // Act
            var response =
                await productService.FilterAsync(
                    new ProductFilterRequest() { size = size, hightlight = highlights, maxprice = price }, ct);

            // Assert
            await productStore.Received().ReadAll(ct);
            response.Filter.MinPrice.Should().Be(MinPrice);
            response.Filter.MaxPrice.Should().Be(MaxPrice);
            response.Filter.Sizes.Should().BeEquivalentTo(_sizes);
            response.Filter.Keywords.OrderBy(s => s).Should().BeEquivalentTo(_keywords);
        }
    }
}
