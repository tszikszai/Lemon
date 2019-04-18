using Lemon.Web;
using Lemon.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.BandsController
{
    public class GetSingle : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string BaseUrl = "/api/bands";

        private readonly HttpClient _client;

        public GetSingle(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsNotFoundForInvalidId()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseUrl}/0");
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("0", responseContent);
        }

        [Fact]
        public async Task ReturnsBandForValidId()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseUrl}/1");
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            BandViewModel band = JsonConvert.DeserializeObject<BandViewModel>(responseContent);

            Assert.Equal("Radiohead", band.Name);
        }
    }
}
