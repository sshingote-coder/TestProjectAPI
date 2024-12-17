using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework.Internal;
using System.Net;
using Xunit;

namespace TestProjectAPI.UnitTest.Endpoint
{
    public class SwaggerTests
    {
        private readonly HttpClient _client;

        public SwaggerTests()
        {
            var builder = new WebHostBuilder().UseStartup<Program>();
            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }

        [Fact]
        public async Task Swagger_OnInvoke_ReturnsHealthy()
        {
            var response = await _client.GetAsync(Program.SwaggerEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

}
