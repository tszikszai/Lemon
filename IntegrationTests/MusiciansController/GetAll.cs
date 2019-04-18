using Lemon.Web;
using Lemon.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.MusiciansController
{
    public class GetAll : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string BaseUrl = "/api/musicians";

        private readonly HttpClient _client;

        public GetAll(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsAllMusicians()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseUrl}");
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            IEnumerable<MusicianViewModel> musicians = JsonConvert.DeserializeObject<IEnumerable<MusicianViewModel>>(responseContent);

            Assert.Equal(3, musicians.Count());
            Assert.Contains(musicians, x => x.FirstName == "Thom" && x.LastName == "Yorke");
            Assert.Contains(musicians, x => x.FirstName == "Maynard James" && x.LastName == "Keenan");
            Assert.Contains(musicians, x => x.FirstName == "Jim" && x.LastName == "Morrison");
        }
    }
}
