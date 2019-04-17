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
    public class Delete : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string BaseUrl = "/api/musicians";

        private readonly HttpClient _client;

        public Delete(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsNotFoundForInvalidId()
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{BaseUrl}/0");
            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("0", responseContent);
        }

        [Fact]
        public async Task ReturnsNoContentForValidId()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseUrl}");
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            IEnumerable<MusicianViewModel> musicians = JsonConvert.DeserializeObject<IEnumerable<MusicianViewModel>>(responseContent);
            int idToDelete = musicians.First().Id;

            response = await _client.DeleteAsync($"{BaseUrl}/{idToDelete}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            response = await _client.GetAsync($"{BaseUrl}/{idToDelete}");
            responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(idToDelete.ToString(), responseContent);
        }
    }
}
