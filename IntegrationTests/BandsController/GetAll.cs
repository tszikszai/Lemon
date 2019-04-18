using Lemon.Web;
using Lemon.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.BandsController
{
    public class GetAll : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string BaseUrl = "/api/bands";

        private readonly HttpClient _client;

        public GetAll(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsAllBands()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseUrl}");
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            IEnumerable<BandViewModel> bands = JsonConvert.DeserializeObject<IEnumerable<BandViewModel>>(responseContent);

            Assert.Equal(3, bands.Count());
            Assert.Contains(bands, x => x.Name == "Radiohead");
            Assert.Contains(bands, x => x.Name == "Tool");
            Assert.Contains(bands, x => x.Name == "The Doors");
        }
    }
}
