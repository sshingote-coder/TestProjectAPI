using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using Xunit;

namespace TestProjectAPI.UnitTest.Endpoint
{
    public class HealthCheckTest
    {
        private readonly HttpClient _client;

        public HealthCheckTest()
        {
            var builder = new WebHostBuilder().UseStartup<Program>();
            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }

        [Fact]
        public async Task Health_OnInvoke_ReturnsHealthy()
        {
            var response = await _client.GetAsync(Program.HealthEndpoint);
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseString.Should().Be("Healthy");
        }
    }
}
