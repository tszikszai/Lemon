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
    public class Post : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string BaseUrl = "/api/bands";

        private readonly HttpClient _client;

        public Post(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsBadRequestGivenNoName()
        {
            var band = new BandViewModel
            {
                ActiveFromYear = DateTime.Today.Year
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(BaseUrl, band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Name", responseContent);
            Assert.Contains("The Name field is required.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenNameOver100Chars()
        {
            var band = new BandViewModel
            {
                Name = "A".PadLeft(101),
                ActiveFromYear = DateTime.Today.Year
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(BaseUrl, band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Name", responseContent);
            Assert.Contains("The field Name must be a string with a maximum length of 100.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenActiveFromYearLessThan1900()
        {
            var band = new BandViewModel
            {
                Name = "A",
                ActiveFromYear = 1899
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(BaseUrl, band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("ActiveFromYear", responseContent);
            Assert.Contains($"The field ActiveFromYear must be between 1900 and {DateTime.Today.Year}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenActiveFromYearGreaterThanCurrentYear()
        {
            var band = new BandViewModel
            {
                Name = "A",
                ActiveFromYear = DateTime.Today.Year + 1
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(BaseUrl, band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("ActiveFromYear", responseContent);
            Assert.Contains($"The field ActiveFromYear must be between 1900 and {DateTime.Today.Year}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenActiveToYearLessThan1900()
        {
            var band = new BandViewModel
            {
                Name = "A",
                ActiveToYear = 1899
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(BaseUrl, band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("ActiveToYear", responseContent);
            Assert.Contains($"The field ActiveToYear must be between 1900 and {DateTime.Today.Year}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenActiveToYearGreaterThanCurrentYear()
        {
            var band = new BandViewModel
            {
                Name = "A",
                ActiveToYear = DateTime.Today.Year + 1
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(BaseUrl, band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("ActiveToYear", responseContent);
            Assert.Contains($"The field ActiveToYear must be between 1900 and {DateTime.Today.Year}.", responseContent);
        }

        [Fact]
        public async Task ReturnsBadRequestGivenActiveFromYearGreaterThanActiveToYear()
        {
            var band = new BandViewModel
            {
                Name = "A",
                ActiveFromYear = DateTime.Today.Year,
                ActiveToYear = DateTime.Today.Year - 1
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(BaseUrl, band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Active from Year must be lower than or equal to Active to Year.", responseContent);
        }

        [Fact]
        public async Task ReturnsCreatedGivenValidData()
        {
            var band = new BandViewModel
            {
                Name = "A",
                ActiveFromYear = DateTime.Today.Year - 5,
                ActiveToYear = DateTime.Today.Year
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(BaseUrl, band);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var bandCreated = JsonConvert.DeserializeObject<BandViewModel>(responseContent);
            Assert.Equal("A", bandCreated.Name);
            Assert.Equal(DateTime.Today.Year - 5, bandCreated.ActiveFromYear);
            Assert.Equal(DateTime.Today.Year, bandCreated.ActiveToYear);
            Assert.True(bandCreated.Id > 0);

            Assert.Contains($"{BaseUrl}/{bandCreated.Id}", response.Headers.Location.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
