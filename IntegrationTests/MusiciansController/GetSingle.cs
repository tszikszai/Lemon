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

namespace IntegrationTests.MusiciansController
{
    public class GetSingle : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string BaseUrl = "/api/musicians";

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
        public async Task ReturnsMusicianForValidId()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseUrl}/4");
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            MusicianViewModel band = JsonConvert.DeserializeObject<MusicianViewModel>(responseContent);

            Assert.Equal("Thom", band.FirstName);
            Assert.Equal("Yorke", band.LastName);
        }
    }
}
